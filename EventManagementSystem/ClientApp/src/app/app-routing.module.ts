import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { EventComponent } from './event/event.component';
import { EventFormComponent } from './event/event-form/event-form.component';
import { GuestComponent } from './guest/guest.component';
import { GuestFormComponent } from './guest/guest-form/guest-form.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'guests', component: GuestComponent, pathMatch: 'full' },
  { path: 'guests/new', component: GuestFormComponent, pathMatch: 'full' },
  { path: 'guests/:id/edit', component: GuestFormComponent, pathMatch: 'full' },
  { path: 'events', component: EventComponent, pathMatch: 'full' },
  { path: 'events/new', component: EventFormComponent, pathMatch: 'full' },
  { path: 'events/:id/edit', component: EventFormComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
