import { HarnessLoader } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonHarness } from '@angular/material/button/testing';
import { MatIconModule } from '@angular/material/icon';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AddItemComponent } from './add-item.component';

describe('AddItemBtnComponent', () => {
  let fixture: ComponentFixture<AddItemComponent>;
  let loader: HarnessLoader;
  let component: AddItemComponent;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MatButtonModule, BrowserAnimationsModule, MatIconModule],
      declarations: [AddItemComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(AddItemComponent);
    fixture.detectChanges();
    loader = TestbedHarnessEnvironment.loader(fixture);
    component = fixture.componentInstance;
  });

  it('should emit showAddDialog', async () => {
    spyOn(component.showAddDialog, 'emit');

    const addBtn = await loader.getHarness(MatButtonHarness.with({ text: 'add' }));

    await addBtn.click();

    expect(component.showAddDialog.emit).toHaveBeenCalled();
  });
});
