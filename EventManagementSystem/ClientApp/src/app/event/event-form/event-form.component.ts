import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { NgForm } from '@angular/forms';
import { faCalendar } from '@fortawesome/free-solid-svg-icons';
import { EventService } from 'src/app/shared/service/event.service';
import { Event } from 'src/app/shared/model/event.model';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ToastService } from 'src/app/shared/component/toast-service/toast.service';

@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
  styleUrls: ['./event-form.component.sass'],
})
export class EventFormComponent implements OnInit {
  faCalendar = faCalendar;
  dateModel: NgbDateStruct;

  constructor(
    public service: EventService,
    private route: ActivatedRoute,
    private toast: ToastService,
    private router: Router,
    private _location: Location
  ) {
    // set start date as today
    const today: Date = new Date();
    this.dateModel = {
      year: today.getFullYear(),
      month: today.getMonth() + 1,
      day: today.getDate(),
    };

    // get guests for auto complete
    this.service.refreshGuests();

    // Get id if present in the URL
    this.route.params.subscribe((params: Params) => {
      if (params['id']) {
        this.service.getEvent(params['id']).subscribe(
          (res) => {
            this.service.formData = res as Event;
            // set current date of date picker
            this.setDate(this.service.formData.date);
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

  setDate(dateStr: string) {
    const date: Date = new Date(Date.parse(dateStr));
    this.dateModel = {
      year: date.getFullYear(),
      month: date.getMonth() + 1,
      day: date.getDate(),
    };
  }

  onSubmit(form: NgForm) {
    // date sync
    let date: Date = new Date(this.dateModel.year, this.dateModel.month);
    date.setUTCDate(this.dateModel.day);
    this.service.formData.date = date.toISOString();
    // guests sync
    this.service.formData.guests = this.service.formData.guests.map(
      (guest) => ({ ...guest, id: isNaN(guest.id) ? 0 : guest.id })
    );
    if (this.service.formData.id == 0) this.insertRecord(form);
    else this.updateRecord(form);
    this.router.navigateByUrl(`events`);
  }

  insertRecord(form: NgForm) {
    this.service.createEvent().subscribe(
      (res) => {
        this.resetForm(form);
        this.service.refreshList();
        this.router.navigateByUrl(`events`);
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
    this.service.formData = new Event();
  }

  updateRecord(form: NgForm) {
    this.service.updateEvent().subscribe(
      (res) => {
        this.resetForm(form);
        this.service.refreshList();
        this.router.navigateByUrl(`events`);
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
