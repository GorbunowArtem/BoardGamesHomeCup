import { inject } from '@angular/core/testing';
import { PlayersService } from './players.service';

describe('Service: Players', () => {
  let playersService: PlayersService;
  let httpClientMock: any;

  beforeEach(() => {
    httpClientMock = jasmine.createSpyObj('HttpClient', ['get', 'post', 'head']);
    playersService = new PlayersService(httpClientMock);
  });

  it('should get players constraints', () => {
    playersService.getConstraints().subscribe();

    expect(httpClientMock.get.calls.count).toBe(1);
  });
});
