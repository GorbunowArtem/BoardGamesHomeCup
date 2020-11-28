import { HarnessLoader } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { Component, Input } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatCardModule } from '@angular/material/card';
import { MatCardHarness } from '@angular/material/card/testing';
import { Player } from '../models/player';
import { PlayerCardComponent } from './player-card.component';

@Component({ selector: 'hc-avatar', template: '' })
class ValidationErrorsComponentStub {
  @Input() name!: string;
}

describe('PlayerCardComponent', () => {
  const testPlayer: Player = {
    birthDate: new Date('1995-12-17T03:24:00'),
    firstName: 'Test',
    lastName: 'Player',
    nickname: 'Test P'
  };
  let component: PlayerCardComponent;
  let loader: HarnessLoader;
  let fixture: ComponentFixture<PlayerCardComponent>;

  beforeEach(async () => {
    TestBed.configureTestingModule({
      declarations: [PlayerCardComponent],
      imports: [MatCardModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerCardComponent);
    loader = TestbedHarnessEnvironment.loader(fixture);
    component = fixture.componentInstance;
    component.player = testPlayer;
    fixture.detectChanges();
  });

  it('should title to equal fullName ', async () => {
    const card = await loader.getHarness(MatCardHarness);

    const title = await card.getTitleText();

    expect(title).toEqual(`${testPlayer.firstName} ${testPlayer.lastName}`);
  });

  it('should subtitle to be player nickname', async () => {
    const card = await loader.getHarness(MatCardHarness);

    const subTitle = await card.getSubtitleText();

    expect(subTitle).toBe(testPlayer.nickname);
  });
});
