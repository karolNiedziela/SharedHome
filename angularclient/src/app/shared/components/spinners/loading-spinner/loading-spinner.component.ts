import {
  AfterViewChecked,
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  Input,
  ViewChild,
} from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { ProgressSpinnerMode } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-loading-spinner',
  templateUrl: './loading-spinner.component.html',
  styleUrls: ['./loading-spinner.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoadingSpinnerComponent implements AfterViewChecked {
  @Input() diameter: number = 60;

  @ViewChild('loadingText') loadingText!: ElementRef<HTMLDivElement>;

  color: ThemePalette = 'primary';
  mode: ProgressSpinnerMode = 'indeterminate';

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
