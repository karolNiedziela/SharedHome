import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';

import { SettingRoutingModule } from './setting-routing.module';
import { SettingComponent } from './setting/setting.component';

@NgModule({
  declarations: [
    SettingComponent
  ],
  imports: [SharedModule, SettingRoutingModule],
})
export class SettingModule {}
