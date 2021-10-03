import { Component, OnInit } from '@angular/core';
import { faCalendar, faPlus } from '@fortawesome/free-solid-svg-icons';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastService } from '../shared/component/toast-service/toast.service';
import { EventService } from '../shared/service/event.service';
import { ConfirmModalComponent as ConfirmModal } from 'src/app/shared/component/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.sass'],
})
export class EventComponent implements OnInit {
  faCalendar = faCalendar;
  faPlus = faPlus;

  constructor(
    public service: EventService,
    private modalService: NgbModal,
    private toast: ToastService
  ) {}

  ngOnInit(): void {
    this.service.refreshList();
  }

  onDelete(id: number) {
    this.modalService
      .open(ConfirmModal)
      .result.then(() => {
        this.service.deleteEvent(id).subscribe(
          (res) => {
            this.service.refreshList();
          },
          (err: Error) =>
            this.toast.show(err.message, {
              classname: 'bg-danger text-light',
              delay: 5000,
            })
        );
      })
      .catch(() => {});
  }
}
