/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PlaysService } from './plays.service';

describe('Service: Play', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PlaysService]
    });
  });

  it('should ...', inject([PlaysService], (service: PlaysService) => {
    expect(service).toBeTruthy();
  }));
});
