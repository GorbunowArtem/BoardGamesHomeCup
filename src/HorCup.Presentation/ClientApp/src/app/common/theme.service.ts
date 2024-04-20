import { OverlayContainer } from '@angular/cdk/overlay';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  private _darkTheme = new Subject<boolean>();
  public isDarkTheme = this._darkTheme.asObservable();

  public constructor(private _overlayContainer: OverlayContainer) {}

  public setDarkTheme(isDarkTheme: boolean): void {
    if (isDarkTheme) {
      this._overlayContainer.getContainerElement().classList.add('dark-theme');
    } else {
      this._overlayContainer.getContainerElement().classList.remove('dark-theme');
    }

    this._darkTheme.next(isDarkTheme);
  }
}
