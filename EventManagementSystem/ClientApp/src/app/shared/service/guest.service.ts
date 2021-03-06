import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Allergy, Guest } from '../model/guest.model';
import { ToastService } from '../component/toast-service/toast.service';

@Injectable({
  providedIn: 'root',
})
export class GuestService {
  formData: Guest = new Guest();
  list: Guest[] = [];
  allergies: Allergy[] = [];

  constructor(private http: HttpClient, private toast: ToastService) {}

  refreshAllergies() {
    this.http
      .get('allergies')
      .toPromise()
      .then((res) => {
        this.allergies = res as Allergy[];
      })
      .catch((err: Error) =>
        this.toast.show(err.message, {
          classname: 'bg-danger text-light',
          delay: 5000,
        })
      );
  }

  refreshList() {
    this.http
      .get('guests')
      .toPromise()
      .then((res) => {
        this.list = res as Guest[];
      })
      .catch((err: Error) =>
        this.toast.show(err.message, {
          classname: 'bg-danger text-light',
          delay: 5000,
        })
      );
  }

  createGuest() {
    return this.http.post('guests', this.formData);
  }

  getGuest(id: number) {
    return this.http.get(`guests/${id}`);
  }

  updateGuest() {
    return this.http.put(`guests/${this.formData.id}`, this.formData);
  }

  deleteGuest(id: number) {
    return this.http.delete(`guests/${id}`);
  }
}
