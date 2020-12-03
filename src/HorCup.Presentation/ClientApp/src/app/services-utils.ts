import { HttpParams } from '@angular/common/http';

export function toHttpParams(options: any) {
  let params = new HttpParams();

  for (const key in options) {
    if (Object.prototype.hasOwnProperty.call(options, key)) {
      const element = options[key];
      if (element !== undefined) {
        params = params.set(key, element.toString());
      }
    }
  }

  return params;
}
