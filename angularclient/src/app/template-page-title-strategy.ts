import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterStateSnapshot, TitleStrategy } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

@Injectable()
export class TemplatePageTitleStrategy extends TitleStrategy {
  constructor(
    private readonly title: Title,
    private readonly translateService: TranslateService
  ) {
    super();
  }

  override updateTitle(routerState: RouterStateSnapshot) {
    const title = this.buildTitle(routerState);

    if (title) {
      this.translateService.get(title).subscribe((translatedTitle: string) => {
        this.title.setTitle(`Shared Home - ${translatedTitle}`);
      });
    } else {
      this.title.setTitle('Shared Home');
    }
  }
}
