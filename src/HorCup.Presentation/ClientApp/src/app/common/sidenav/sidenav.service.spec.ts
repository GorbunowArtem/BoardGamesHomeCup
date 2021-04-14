import { MatSidenav } from '@angular/material/sidenav';
import { SidenavService } from './sidenav.service';

describe('Service: Sidenav', () => {
  let sidenavService: SidenavService;
  let matSideNavMock: MatSidenav;

  beforeEach(() => {
    matSideNavMock = jasmine.createSpyObj('MatSidenav', ['open', 'close']);

    sidenavService = new SidenavService();

    sidenavService.setSidenav(matSideNavMock);
  });

  it('should open sidenav', () => {
    sidenavService.open();

    expect(matSideNavMock.open).toHaveBeenCalled();
  });

  it('should close sidenav', () => {
    sidenavService.close();

    expect(matSideNavMock.close).toHaveBeenCalled();
  });
});
