import { HarnessLoader } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonHarness } from '@angular/material/button/testing';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatToolbarHarness } from '@angular/material/toolbar/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Subject } from 'rxjs';
import { PlaysService } from '../plays.service';
import { PlaysNavBarComponent } from './plays-nav-bar.component';

describe('PlaysNavBarComponent', () => {
  let fixture: ComponentFixture<PlaysNavBarComponent>;
  let loader: HarnessLoader;
  let playsServiceMock;

  beforeEach(async () => {
    playsServiceMock = {
      searchParamsChangedSubject: new Subject(),
      search: jasmine.createSpy().and.returnValue({
        subscribe: () => {
          return {
            items: [],
            total: 1
          };
        }
      }),
      add: jasmine.createSpy()
    };

    await TestBed.configureTestingModule({
      imports: [
        MatToolbarModule,
        MatButtonModule,
        MatIconModule,
        MatInputModule,
        MatBottomSheetModule,
        BrowserAnimationsModule
      ],
      declarations: [PlaysNavBarComponent],
      providers: [{ provide: PlaysService, useValue: playsServiceMock }]
    }).compileComponents();

    fixture = TestBed.createComponent(PlaysNavBarComponent);
    fixture.detectChanges();
    loader = TestbedHarnessEnvironment.loader(fixture);
  });

  describe('in view mode', () => {
    let toolbar: MatToolbarHarness;

    beforeEach(async () => {
      toolbar = await loader.getHarness(MatToolbarHarness);
    });

    it('should have menu button ', async () => {
      const buttons = await toolbar.getAllHarnesses(MatButtonHarness.with({ text: 'menu' }));

      expect(buttons.length).toBe(1);
    });

    it('should have search button ', async () => {
      const buttons = await toolbar.getAllHarnesses(MatButtonHarness.with({ text: 'search' }));

      expect(buttons.length).toBe(1);
    });

    it('should have filter button ', async () => {
      const buttons = await toolbar.getAllHarnesses(MatButtonHarness.with({ text: 'filter_list' }));

      expect(buttons.length).toBe(1);
    });
  });
});
