import { HttpParams } from '@angular/common/http';

export function toHttpParams(options: any) {
  let params = new HttpParams();

  for (const key in options) {
    if (Object.prototype.hasOwnProperty.call(options, key)) {
      const element = options[key];
      if (element !== undefined && element !== null) {
        if (Array.isArray(element)) {
          if (element.length > 0) {
            element.forEach((el) => {
              params = params.set(key, el);
            });
          }
        } else {
          params = params.set(key, element.toString());
        }
      }
    }
  }

  return params;
}
