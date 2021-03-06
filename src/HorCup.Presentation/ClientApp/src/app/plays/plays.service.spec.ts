import { PlaysService } from './plays.service';
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';
import { Play } from './models/play';
import { testPlay1 } from './test-data/test-play';
import { PagedSearchResponse } from '../common/paged-search-response';
import { SearchPlaysOptions } from './models/search-plays-options';

describe('Service: Play', () => {
  let service: PlaysService;
  let httpClientMock: any;
  const testResponse = {
    items: { $values: [testPlay1] },
    total: 1
  };

  beforeEach(() => {
    httpClientMock = jasmine.createSpyObj(HttpClient, {
      get: of<PagedSearchResponse<Play>>(testResponse),
      post: of({})
    });

    service = new PlaysService(httpClientMock);
  });

  it('should return plays', (done) => {
    const options = new SearchPlaysOptions();

    service.search(options).subscribe((res) => {
      expect(res).toEqual(testResponse);
      done();
    });

    expect(httpClientMock.get).toHaveBeenCalledWith('https://localhost:5005/plays', {
      params: options as any
    });
  });

  it('should add play', () => {
    service.add(testPlay1 as any);

    expect(httpClientMock.post).toHaveBeenCalledWith('https://localhost:5005/plays', testPlay1);
  });
});
