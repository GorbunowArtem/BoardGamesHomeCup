import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PlayPanelComponent } from './play-panel.component';
import { HarnessLoader } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatExpansionPanelHarness } from '@angular/material/expansion/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { testPlay1 } from '../test-data/test-play';
import { MatTableModule } from '@angular/material/table';
import { MatTableHarness } from '@angular/material/table/testing';
import { By } from '@angular/platform-browser';

describe('PlayPanelComponent', () => {
  let component: PlayPanelComponent;
  let fixture: ComponentFixture<PlayPanelComponent>;
  let loader: HarnessLoader;
  let panel: MatExpansionPanelHarness;

  beforeEach(async () => {
    TestBed.configureTestingModule({
      declarations: [PlayPanelComponent],
      imports: [BrowserAnimationsModule, MatExpansionModule, MatTableModule]
    }).compileComponents();
  });

  beforeEach(async () => {
    fixture = TestBed.createComponent(PlayPanelComponent);
    component = fixture.componentInstance;
    component.play = testPlay1;
    loader = TestbedHarnessEnvironment.loader(fixture);
    fixture.detectChanges();
    panel = await loader.getHarness(MatExpansionPanelHarness);
  });

  it('should title have game title', async () => {
    const title = await panel.getTitle();

    expect(title).toEqual(testPlay1.gameTitle);
  });

  it('should description has winner player name', async () => {
    const description = await panel.getDescription();

    expect(description).toEqual('Player 1');
  });

  describe('Play info', () => {
    beforeEach(async () => {
      await panel.expand();
    });

    it('should display played date', async () => {
      const playInfo = fixture.debugElement.query(By.css('.play-info'));

      expect(playInfo.nativeElement.textContent).toEqual(
        `Дата: 3/2/20, 12:00 AMЗаметки: ${testPlay1.notes}`
      );
    });

    it('should display table header', async () => {
      const table = await loader.getHarness(MatTableHarness);

      const headers = await table.getHeaderRows();

      const rowText = await headers[0].getCellTextByColumnName();

      expect(rowText['player-name']).toEqual('Имя игрока');
      expect(rowText.score).toEqual('Очки');
    });

    it('should display player data', async () => {
      const table = await loader.getHarness(MatTableHarness);

      const row = await table.getRows();

      const rowText = await row[0].getCellTextByColumnName();

      expect(rowText['player-name']).toEqual('Player 1');
      expect(rowText.score).toEqual('22');
    });
  });
});
