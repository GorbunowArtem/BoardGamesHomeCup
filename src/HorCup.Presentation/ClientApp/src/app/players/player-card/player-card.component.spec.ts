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
import { RouterTestingModule } from '@angular/router/testing';
import { HcAvatarComponent } from 'src/app/common/hc-avatar/hc-avatar.component';
import { ConfirmationDialogMockComponent } from 'src/app/common/test-data/confirmation-dialog-mock';
import { HeaderCardMockComponent } from 'src/app/common/test-data/header-card-mock';
import { NavBarMockComponent } from 'src/app/nav-bar/test-data/nav-bar-header-mock';
import { PlayersService } from '../players.service';
import { testPlayer1 } from '../test-data/test-player';
import { PlayerCardComponent } from './player-card.component';

@Component({ selector: 'hc-avatar', template: '' })
class ValidationErrorsComponentStubComponent {
  @Input() public name!: string;
}

describe('PlayerCardComponent', () => {
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
        MatToolbarModule,
        RouterTestingModule
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
    component.player = testPlayer1;
    fixture.detectChanges();
  });

  xit('should delete user', async () => {
    const menu = await loader.getHarness(MatMenuHarness);

    await menu.open();

    const deleteButton = await loader.getHarness(MatButtonHarness);

    await deleteButton.click();

    expect(playersServiceMock.delete).toHaveBeenCalled();
  });
});
