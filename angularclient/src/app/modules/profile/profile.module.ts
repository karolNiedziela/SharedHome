import { ChangePasswordModalComponent } from './modals/change-password-modal/change-password-modal.component';
import { TranslateModule } from '@ngx-translate/core';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileRoutingModule } from './profile-routing.module';
import { ProfileSettingsComponent } from './profile-settings/profile-settings.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { MaterialModule } from 'src/app/material.module';
import { UploadProfileImageModalComponent } from './modals/upload-profile-image-modal/upload-profile-image-modal.component';

@NgModule({
  declarations: [
    ProfileSettingsComponent,
    ChangePasswordModalComponent,
    UploadProfileImageModalComponent,
  ],
  imports: [
    CommonModule,
    ProfileRoutingModule,
    SharedModule,
    TranslateModule,
    MaterialModule,
  ],
})
export class ProfileModule {}
