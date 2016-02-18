import { Guest } from './guest.model';

export class Event {
  id: number = 0;
  name: string = '';
  date: string = '';
  guests: Guest[] = [];
}
