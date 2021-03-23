import { HarnessLoader, parallel } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatBottomSheet, MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonHarness } from '@angular/material/button/testing';
import { MatIconModule } from '@angular/material/icon';
import { MatIconHarness } from '@angular/material/icon/testing';
import { MatInputModule } from '@angular/material/input';
import { MatInputHarness } from '@angular/material/input/testing';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatToolbarHarness } from '@angular/material/toolbar/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Subject } from 'rxjs';
import { SearchPlaysOptions } from '../models/search-plays-options';
import { PlaysFilterComponent } from '../plays-filter/plays-filter.component';
import { PlaysService } from '../plays.service';
import { PlaysNavBarComponent } from './plays-nav-bar.component';

describe('PlaysNavBarComponent', () => {
  let fixture: ComponentFixture<PlaysNavBarComponent>;
  let loader: HarnessLoader;
  let playsServiceMock;
  let bottomSheetMock: any;
  let toolbar: MatToolbarHarness;

  beforeEach(async () => {
    bottomSheetMock = jasmine.createSpyObj(['open']);

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
      providers: [
        { provide: PlaysService, useValue: playsServiceMock },
        { provide: MatBottomSheet, useValue: bottomSheetMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(PlaysNavBarComponent);
    fixture.detectChanges();
    loader = TestbedHarnessEnvironment.loader(fixture);
    toolbar = await loader.getHarness(MatToolbarHarness);
  });

  describe('in view mode', () => {
    it('should have menu button ', async () => {
      const buttons = await toolbar.getAllHarnesses(MatButtonHarness.with({ text: 'menu' }));

      expect(buttons.length).toBe(1);
    });

    it('should have search button ', async () => {
      const buttons = await toolbar.getAllHarnesses(MatButtonHarness.with({ text: 'search' }));

      expect(buttons.length).toBe(1);
    });

    it('should show search field', async () => {
      const searchBtn = await toolbar.getHarness(MatButtonHarness.with({ text: 'search' }));

      await searchBtn.click();

      const searchInputs = await toolbar.getAllHarnesses(MatInputHarness);

      expect(searchInputs.length).toBe(1);
    });

    it('should have filter button ', async () => {
      const buttons = await toolbar.getAllHarnesses(MatButtonHarness.with({ text: 'filter_list' }));

      expect(buttons.length).toBe(1);
    });

    it('should open filter', async () => {
      const filterBtn = await toolbar.getHarness(MatButtonHarness.with({ text: 'filter_list' }));

      await filterBtn.click();

      expect(bottomSheetMock.open).toHaveBeenCalledWith(PlaysFilterComponent, {
        data: new SearchPlaysOptions()
      });
    });
  });

  describe('in searchView', () => {
    beforeEach(async () => {
      const searchButton = await toolbar.getHarness(MatButtonHarness.with({ text: 'search' }));

      await searchButton.click();
    });

    it('should have back button', async () => {
      const backButtons = await toolbar.getAllHarnesses(
        MatButtonHarness.with({ text: 'arrow_back' })
      );

      expect(backButtons.length).toBe(1);
    });

    it('should have search icon', async () => {
      const icons = await loader.getAllHarnesses(MatIconHarness);

      const name = await icons[1].getName();

      expect(name).toEqual('search');
    });
  });
});
