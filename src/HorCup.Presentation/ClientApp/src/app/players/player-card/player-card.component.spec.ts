import { HarnessLoader } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { Component, Input } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonHarness } from '@angular/material/button/testing';
import { MatCardModule } from '@angular/material/card';
import { MatCardHarness } from '@angular/material/card/testing';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatMenuHarness, MatMenuItemHarness } from '@angular/material/menu/testing';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HcAvatarComponent } from 'src/app/common/hc-avatar/hc-avatar.component';
import { ConfirmationDialogMockComponent } from 'src/app/common/test-data/confirmation-dialog-mock';
import { HeaderCardMockComponent } from 'src/app/common/test-data/header-card-mock';
import { NavBarMockComponent } from 'src/app/nav-bar/test-data/nav-bar-header-mock';
import { Player } from '../models/player';
import { PlayersService } from '../players.service';
import { PlayerCardComponent } from './player-card.component';

@Component({ selector: 'hc-avatar', template: '' })
class ValidationErrorsComponentStubComponent {
  @Input() public name!: string;
}

xdescribe('PlayerCardComponent', () => {
  const testPlayer: Player = {
    birthDate: new Date('1995-12-17T03:24:00'),
    firstName: 'Test',
    lastName: 'Player',
    nickname: 'Test P'
  };
  let component: PlayerCardComponent;
  let loader: HarnessLoader;
  let fixture: ComponentFixture<PlayerCardComponent>;
  let playersServiceMock: any;

  beforeEach(async () => {
    playersServiceMock = {
      delete: jasmine.createSpy()
    };

    await TestBed.configureTestingModule({
      declarations: [
        PlayerCardComponent,
        HcAvatarComponent,
        HeaderCardMockComponent,
        NavBarMockComponent
      ],
      imports: [
        BrowserAnimationsModule,
        MatCardModule,
        MatDialogModule,
        MatMenuModule,
        MatButtonModule,
        MatIconModule,
        MatToolbarModule
      ],
      providers: [
        { provide: PlayersService, useValue: playersServiceMock },
        { provide: MatDialog, useValue: ConfirmationDialogMockComponent }
      ]
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

  it('should delete user', async () => {
    const menu = await loader.getHarness(MatMenuHarness);

    await menu.open();

    const deleteButton = await loader.getHarness(MatButtonHarness);

    await deleteButton.click();

    expect(playersServiceMock.delete).toHaveBeenCalled();
  });
});
