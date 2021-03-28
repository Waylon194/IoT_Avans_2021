import { TestBed } from '@angular/core/testing';

import { FormSubmissionsService } from './form-submissions.service';

describe('FormSubmissionsService', () => {
  let service: FormSubmissionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FormSubmissionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
