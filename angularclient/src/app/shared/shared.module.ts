import { FormModalComponent } from './components/modals/form-modal/form-modal.component';
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
import { CommonModule, DatePipe } from '@angular/common';
import { ConfirmationModalComponent } from './components/modals/confirmation-modal/confirmation-modal.component';
import { ButtonComponent } from './components/buttons/button/button.component';
import { PopupMenuComponent } from './components/menus/popup-menu/popup-menu.component';
import { NumberInputComponent } from './components/inputs/number-input/number-input.component';
import { EnumAsStringPipe } from './pipes/enum-as-string.pipe';
import { CurrencySelectComponent } from './components/selects/currency-select/currency-select.component';
import { CircleButtonComponent } from './components/buttons/circle-button/circle-button.component';
import { ErrorComponent } from './components/errors/error/error.component';
import { LoadingSpinnerComponent } from './components/spinners/loading-spinner/loading-spinner.component';
import { ThemeSwitcherComponent } from './components/other/theme-switcher/theme-switcher.component';
import { TableComponent } from './components/tables/table/table.component';
import { StyleCellDirective } from './directives/style-cell.directive';
import { FormatCellPipe } from './pipes/format-cell.pipe';
import { DateInputComponent } from './components/inputs/date-input/date-input.component';
import { MoneyFormComponent } from './components/forms/money-form/money-form.component';
import { FormGroupPipe } from './pipes/form-group.pipe';
import { NetContentFormComponent } from './components/forms/net-content-form/net-content-form.component';
import { EnumSelectComponent } from './components/selects/enum-select/enum-select.component';
import { SingleSelectComponent } from './components/selects/single-select/single-select.component';
import { LanguageSelectComponent } from './components/selects/language-select/language-select.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FirstLettersToTitleCasePipe } from './pipes/first-letters-to-title-case.pipe';
import { NotificationBellComponent } from './components/notifications/notification-bell/notification-bell.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { UploadImageComponent } from './components/inputs/upload-image/upload-image.component';
import { ProgressComponent } from './components/progress/progress.component';
import { DragAndDropDirective } from './directives/drag-and-drop.directive';
import { ProfileDropdownComponent } from './components/profile-dropdown/profile-dropdown.component';

@NgModule({
  declarations: [
    ButtonComponent,
    TextInputComponent,
    EmailInputComponent,
    PasswordInputComponent,
    PasswordsFormComponent,
    SidebarComponent,
    FooterComponent,
    FormModalComponent,
    ConfirmationModalComponent,
    PopupMenuComponent,
    NumberInputComponent,
    EnumSelectComponent,
    EnumAsStringPipe,
    CurrencySelectComponent,
    CircleButtonComponent,
    ErrorComponent,
    LoadingSpinnerComponent,
    ThemeSwitcherComponent,
    TableComponent,
    StyleCellDirective,
    FormatCellPipe,
    DateInputComponent,
    MoneyFormComponent,
    FormGroupPipe,
    NetContentFormComponent,
    SingleSelectComponent,
    LanguageSelectComponent,
    NavbarComponent,
    NotificationBellComponent,
    FirstLettersToTitleCasePipe,
    PaginationComponent,
    UploadImageComponent,
    ProgressComponent,
    DragAndDropDirective,
    ProfileDropdownComponent,
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
    FormModalComponent,
    ConfirmationModalComponent,
    PopupMenuComponent,
    EnumSelectComponent,
    EnumAsStringPipe,
    CurrencySelectComponent,
    CircleButtonComponent,
    ErrorComponent,
    LoadingSpinnerComponent,
    ThemeSwitcherComponent,
    TableComponent,
    StyleCellDirective,
    DateInputComponent,
    MoneyFormComponent,
    FormGroupPipe,
    NetContentFormComponent,
    SingleSelectComponent,
    LanguageSelectComponent,
    NavbarComponent,
    FirstLettersToTitleCasePipe,
    PaginationComponent,
    UploadImageComponent,
    DragAndDropDirective,
    ProfileDropdownComponent,
  ],
  providers: [DatePipe, EnumAsStringPipe, FirstLettersToTitleCasePipe],
})
export class SharedModule {}
