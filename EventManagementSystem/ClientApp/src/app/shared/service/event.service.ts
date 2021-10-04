import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastService } from '../component/toast-service/toast.service';
import { Event } from '../model/event.model';
import { Guest } from '../model/guest.model';

export interface GuestTag {
  id: number;
  fullName: string;
}

@Injectable({
  providedIn: 'root',
})
export class EventService {
  formData: Event = new Event();
  list: Event[] = [];
  guests: Guest[] = [];

  constructor(private http: HttpClient, private toast: ToastService) {}

  refreshGuests() {
    this.http
      .get('guests')
      .toPromise()
      .then((res) => {
        this.guests = res as Guest[];
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
      .get('events')
      .toPromise()
      .then((res) => (this.list = res as Event[]))
      .catch((err: Error) =>
        this.toast.show(err.message, {
          classname: 'bg-danger text-light',
          delay: 5000,
        })
      );
  }

  createEvent() {
    return this.http.post('events', this.formData);
  }

  getEvent(id: number) {
    return this.http.get(`events/${id}`);
  }

  updateEvent() {
    return this.http.put(`events/${this.formData.id}`, this.formData);
  }

  deleteEvent(id: number) {
    return this.http.delete(`events/${id}`);
  }
}
