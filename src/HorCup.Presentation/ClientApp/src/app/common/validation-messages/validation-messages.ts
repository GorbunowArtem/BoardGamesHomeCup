export const RequiredField = {
  type: 'required',
  message: 'Обязательное поле'
};

export function minLength(minLength: number): HcValidationMessage {
  return {
    type: 'minlength',
    message: `Минимальное количество символов ${minLength}`
  };
}

export function maxLength(maxLength: number): HcValidationMessage {
  return {
    type: 'maxlength',
    message: `Максимальное количество символов ${maxLength}`
  };
}

export function max(max: number): HcValidationMessage {
  return {
    type: 'max',
    message: `Максимально допустимое значение ${max}`
  };
}

export function min(min: number): HcValidationMessage {
  return {
    type: 'min',
    message: `Минимально допустимое значение ${min}`
  };
}

export interface HcValidationMessage {
  type: string;
  message: string;
}
