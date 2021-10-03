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
  dateModel: NgbDateStruct = { year: 1985, month: 7, day: 15 };

  constructor(
    public service: EventService,
    private route: ActivatedRoute,
    private toast: ToastService,
    private router: Router,
    private _location: Location
  ) {
    // Get id if present in the URL
    this.route.params.subscribe((params: Params) => {
      if (params['id']) {
        this.service.getEvent(params['id']).subscribe(
          (res) => {
            this.service.formData = res as Event;
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
    const date: Date = new Date(dateStr);
    this.dateModel = {
      year: date.getUTCFullYear(),
      month: date.getUTCMonth(),
      day: date.getUTCDate(),
    };
  }

  onSubmit(form: NgForm) {
    this.service.formData.date = new Date(
      this.dateModel.year,
      this.dateModel.month,
      this.dateModel.day
    ).toISOString();
    if (this.service.formData.id == 0) this.insertRecord(form);
    else this.updateRecord(form);
    this.router.navigateByUrl(`events`);
  }

  insertRecord(form: NgForm) {
    this.service.createEvent().subscribe(
      (res) => {
        this.resetForm(form);
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
