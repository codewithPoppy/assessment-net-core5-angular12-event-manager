export class Guest {
  id: number = 0;
  firstName: string = '';
  lastName: string = '';
  fullName?: string = '';
  email: string = '';
  dateOfBirth: string = '';
  allergies: Allergy[] = [];
}

export class Allergy {
  id: number = 0;
  name: string = '';
}
