import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { faCalendar } from '@fortawesome/free-solid-svg-icons';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { ToastService } from 'src/app/shared/component/toast-service/toast.service';
import { Allergy, Guest } from 'src/app/shared/model/guest.model';
import { GuestService } from 'src/app/shared/service/guest.service';

@Component({
  selector: 'app-guest-form',
  templateUrl: './guest-form.component.html',
  styleUrls: ['./guest-form.component.sass'],
})
export class GuestFormComponent implements OnInit {
  faCalendar = faCalendar;
  dateOfBirthModel: NgbDateStruct = { year: 1985, month: 7, day: 15 };

  constructor(
    public service: GuestService,
    private toast: ToastService,
    private route: ActivatedRoute,
    private _location: Location,
    private router: Router
  ) {
    // get allergies for auto complete
    this.service.refreshAllergies();
    // Get id if present in the URL
    this.route.params.subscribe((params: Params) => {
      if (params['id']) {
        this.service.getGuest(params['id']).subscribe(
          (res: any) => {
            this.service.formData = res as Guest;
            // set current date of date picker
            this.setDateOfBirth(this.service.formData.dateOfBirth);
          },
          (err: Error) =>
            this.toast.show(err.message, {
              classname: 'bg-danger text-light',
              delay: 5000,
            })
        );
      }
    });
  }

  ngOnInit(): void {}

  setDateOfBirth(dateOfBirth: string) {
    const date: Date = new Date(Date.parse(dateOfBirth));
    this.dateOfBirthModel = {
      year: date.getFullYear(),
      month: date.getMonth() + 1,
      day: date.getDate(),
    };
  }

  onSubmit(form: NgForm) {
    // date sync
    let date: Date = new Date(
      this.dateOfBirthModel.year,
      this.dateOfBirthModel.month
    );
    date.setUTCDate(this.dateOfBirthModel.day);
    this.service.formData.dateOfBirth = date.toISOString();
    // guests sync
    this.service.formData.allergies = this.service.formData.allergies.map(
      (allergy) => ({
        id: isNaN(allergy.id) ? 0 : allergy.id,
        name: allergy.name,
      })
    );
    if (this.service.formData.id == 0) this.insertRecord(form);
    else this.updateRecord(form);
    this.router.navigateByUrl(`guests`);
  }

  insertRecord(form: NgForm) {
    this.service.createGuest().subscribe(
      (res) => {
        this.resetForm(form);
        this.service.refreshList();
        this.router.navigateByUrl(`guests`);
      },
      (err: Error) =>
        this.toast.show(err.message, {
          classname: 'bg-danger text-light',
          delay: 5000,
        })
    );
  }

  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new Guest();
  }

  updateRecord(form: NgForm) {
    this.service.updateGuest().subscribe(
      (res) => {
        this.resetForm(form);
        this.service.refreshList();
        this.router.navigateByUrl(`guests`);
      },
      (err: Error) =>
        this.toast.show(err.message, {
          classname: 'bg-danger text-light',
          delay: 5000,
        })
    );
  }

  goBack() {
    this._location.back();
  }
}
