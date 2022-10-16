import {
  AfterViewChecked,
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  ViewChild,
} from '@angular/core';

@Component({
  selector: 'app-loading-spinner',
  templateUrl: './loading-spinner.component.html',
  styleUrls: ['./loading-spinner.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoadingSpinnerComponent implements AfterViewChecked {
  @ViewChild('loadingText') loadingText!: ElementRef<HTMLDivElement>;

  constructor() {}

  ngAfterViewChecked(): void {
    const text = this.loadingText.nativeElement.textContent;
    if (text?.length == 10) {
      // Loading...
      document.documentElement.style.setProperty('--my-end-width', '100px');
    } else {
      document.documentElement.style.setProperty('--my-end-width', '120px');
    }
  }
}
