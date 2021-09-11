import { TestBed } from '@angular/core/testing';

import { HumanresourcesService } from './humanresources.service';

describe('HumanresourcesService', () => {
  let service: HumanresourcesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HumanresourcesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
