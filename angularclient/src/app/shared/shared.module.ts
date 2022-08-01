import { TranslateModule } from '@ngx-translate/core';
import { PasswordsFormComponent } from './components/forms/passwords-form/passwords-form.component';
import { PasswordInputComponent } from './components/inputs/password-input/password-input.component';
import { NgModule } from '@angular/core';
import { FooterComponent } from './components/footer/footer.component';
import { EmailInputComponent } from './components/inputs/email-input/email-input.component';
import { TextInputComponent } from './components/inputs/text-input/text-input.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ModalComponent } from './components/modals/modal/modal.component';
import { ConfirmationModalComponent } from './components/modals/confirmation-modal/confirmation-modal.component';
import { ButtonComponent } from './components/buttons/button/button.component';
import { PopupMenuComponent } from './components/menus/popup-menu/popup-menu.component';
import { NumberInputComponent } from './components/inputs/number-input/number-input.component';
import { SingleSelectComponent } from './components/selects/single-select/single-select.component';
import { EnumAsStringPipe } from './pipes/enum-as-string.pipe';
import { CurrencySelectComponent } from './components/selects/currency-select/currency-select.component';

@NgModule({
  declarations: [
    ButtonComponent,
    TextInputComponent,
    EmailInputComponent,
    PasswordInputComponent,
    PasswordsFormComponent,
    SidebarComponent,
    FooterComponent,
    ModalComponent,
    ConfirmationModalComponent,
    PopupMenuComponent,
    NumberInputComponent,
    SingleSelectComponent,
    EnumAsStringPipe,
    CurrencySelectComponent,
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
  ],
  exports: [
    CommonModule,
    FontAwesomeModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonComponent,
    TextInputComponent,
    NumberInputComponent,
    EmailInputComponent,
    PasswordInputComponent,
    PasswordsFormComponent,
    SidebarComponent,
    FooterComponent,
    ModalComponent,
    ConfirmationModalComponent,
    PopupMenuComponent,
    SingleSelectComponent,
    EnumAsStringPipe,
    CurrencySelectComponent,
  ],
})
export class SharedModule {}
