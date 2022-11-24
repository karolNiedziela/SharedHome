import { TranslateModule } from '@ngx-translate/core';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileRoutingModule } from './profile-routing.module';
import { SharedModule } from 'app/shared/shared.module';
import { ProfileSettingsComponent } from './profile-settings/profile-settings.component';
import { UploadProfileImageModalComponent } from './modals/upload-profile-image-modal/upload-profile-image-modal.component';
import { ChangePasswordModalComponent } from './change-password-modal/change-password-modal.component';

@NgModule({
  declarations: [ProfileSettingsComponent, UploadProfileImageModalComponent, ChangePasswordModalComponent],
  imports: [CommonModule, ProfileRoutingModule, SharedModule, TranslateModule],
})
export class ProfileModule {}
