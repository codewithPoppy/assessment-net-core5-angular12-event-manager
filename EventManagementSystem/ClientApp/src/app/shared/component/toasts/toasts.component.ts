import { Component, OnInit, TemplateRef } from '@angular/core';
import { ToastService } from '../toast-service/toast.service';

@Component({
  selector: 'app-toasts',
  templateUrl: './toasts.component.html',
  styleUrls: ['./toasts.component.sass'],
  host: { '[class.ngb-toasts]': 'true' },
})
export class ToastsComponent implements OnInit {
  constructor(public toastService: ToastService) {}

  isTemplate(toast: any) {
    return toast.textOrTpl instanceof TemplateRef;
  }

  ngOnInit(): void {}
}
