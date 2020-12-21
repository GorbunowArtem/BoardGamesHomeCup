import { HarnessLoader } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatCardModule } from '@angular/material/card';
import { MatCardHarness } from '@angular/material/card/testing';
import { MatIconModule } from '@angular/material/icon';
import { By } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BarRatingModule } from 'ngx-bar-rating';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { testGame1 } from '../test-data/test-game';
import { GameCardComponent } from './game-card.component';

describe('GameCardComponent', () => {
  let component: GameCardComponent;
  let fixture: ComponentFixture<GameCardComponent>;
  let loader: HarnessLoader;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GameCardComponent],
      imports: [
        BrowserAnimationsModule,
        MatCardModule,
        MatIconModule,
        BarRatingModule,
        AppRoutingModule
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GameCardComponent);
    component = fixture.componentInstance;
    component.game = testGame1;
    loader = TestbedHarnessEnvironment.loader(fixture);
    fixture.detectChanges();
  });

  it('should subtitle to equal "Сыграно партий: 1"', async () => {
    const card = await loader.getHarness(MatCardHarness);

    const subtitle = await card.getSubtitleText();

    expect(subtitle).toEqual('Сыграно партий: 1');
  });

  it('should card title to equal game title', async () => {
    const card = await loader.getHarness(MatCardHarness);

    const subtitle = await card.getTitleText();

    expect(subtitle).toEqual(testGame1.title.substring(0, 20));
  });

  it('should label contain number of players', () => {
    const playersNumber = fixture.debugElement.query(By.css('.players-number'));

    console.log(playersNumber.nativeElement);

    expect(playersNumber.nativeElement.textContent).toEqual(
      `people_alt ${testGame1.minPlayers} - ${testGame1.maxPlayers} `
    );
  });
});
