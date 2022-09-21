import { Component, OnInit } from '@angular/core';
import { ThemeService } from 'app/core/services/theme.service';

@Component({
  selector: 'app-theme-switcher',
  templateUrl: './theme-switcher.component.html',
  styleUrls: ['./theme-switcher.component.scss'],
})
export class ThemeSwitcherComponent implements OnInit {
  constructor(private themeService: ThemeService) {}

  ngOnInit(): void {}

  public switchTheme(): void {
    this.themeService.switchTheme();
  }
}
