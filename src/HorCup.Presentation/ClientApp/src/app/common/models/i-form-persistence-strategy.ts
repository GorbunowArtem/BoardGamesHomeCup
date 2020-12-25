import { Observable } from 'rxjs';

export interface IFormPersistenceStrategy<T> {
  save(model: T): Observable<void>;
  readonly successMessage: string;
  readonly model: T;
}
