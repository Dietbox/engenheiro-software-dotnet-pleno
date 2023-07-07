// Números
export const DecimalToMoney = function (number = 0) { return number.toLocaleString("pt-BR", { maximumFractionDigits: 2, minimumFractionDigits: 2 }); }

// Email:
export const IsEmail = function (value) { return /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(String(value).toLowerCase()); }

// Guid:
export const NewGuid = function () { return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) { var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8); return v.toString(16); }); }


export function IsJSON(data) {
    try {
      JSON.parse(data);
      return true;
    } catch (error) {
      return false;
    }
  }
  