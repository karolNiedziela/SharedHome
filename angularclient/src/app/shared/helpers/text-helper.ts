import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TextHelper {
  public clipText(text: string): string {
    if (text.length < 8) {
      return text;
    }
    return text.substring(0, 6) + '...';
  }
}
