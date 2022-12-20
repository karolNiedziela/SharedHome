export class DateHelper {

  constructor() { }

  public static getYearListSince2022() : number[] {
    let years = [];
    let currentYear: number = new Date().getFullYear();

    for (let i = currentYear; i >= 2022; i--) {
        years.push(i);
    }

    return years.reverse();
  }
}
