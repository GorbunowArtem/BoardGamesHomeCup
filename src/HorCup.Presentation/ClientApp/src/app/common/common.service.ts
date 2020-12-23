import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConstraintsViewModel } from './models/constraints';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  private _constraints!: ConstraintsViewModel;

  public get constraints() {
    return this._constraints;
  }

  public constructor(private _httpClient: HttpClient) {}

  public init() {
    this._httpClient
      .get<ConstraintsViewModel>('/constraints')
      .subscribe((constraints) => (this._constraints = constraints));
  }
}
