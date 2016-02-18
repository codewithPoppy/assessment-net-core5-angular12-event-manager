import { Component, OnInit } from '@angular/core';
import {
  faCalendar,
  faEnvelope,
  faPlus,
} from '@fortawesome/free-solid-svg-icons';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { GuestService } from '../shared/service/guest.service';
import { ConfirmModalComponent as ConfirmModal } from 'src/app/shared/component/confirm-modal/confirm-modal.component';
import { ToastService } from '../shared/component/toast-service/toast.service';

@Component({
  selector: 'app-guest',
  templateUrl: './guest.component.html',
  styleUrls: ['./guest.component.sass'],
})
export class GuestComponent implements OnInit {
  faCalendar = faCalendar;
  faEnvelope = faEnvelope;
  faPlus = faPlus;

  constructor(
    public service: GuestService,
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
        this.service.deleteGuest(id).subscribe(
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
