import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';
import { CommonService } from './common.service';
import { ConstraintsViewModel } from './models/constraints';
import { constraints1 } from './test-data/test-constraints';

describe('Service: Common', () => {
  let commonService: CommonService;
  let httpClientMock: any;

  beforeEach(() => {
    httpClientMock = jasmine.createSpyObj(HttpClient, {
      get: of<ConstraintsViewModel>(constraints1)
    });

    commonService = new CommonService(httpClientMock);
  });

  it('should get constraints from server and save them in local variable on "init" call', () => {
    commonService.init();

    expect(commonService.constraints).toEqual(constraints1);
  });
});
