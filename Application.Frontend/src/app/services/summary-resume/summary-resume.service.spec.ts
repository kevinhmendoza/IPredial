import { TestBed } from '@angular/core/testing';

import { SummaryResumeService } from './summary-resume.service';

describe('SummaryResumeService', () => {
  let service: SummaryResumeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SummaryResumeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
