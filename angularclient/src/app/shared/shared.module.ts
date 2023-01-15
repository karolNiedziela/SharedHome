import { UploadImageComponent } from './components/inputs/upload-image/upload-image.component';
import { MoneyFormComponent } from './forms/money-form/money-form.component';
import { DataPropertyGetterPipe } from './pipes/data-property-getter.pipe';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FooterComponent } from './components/footer/footer.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { LoadingSpinnerComponent } from './components/spinners/loading-spinner/loading-spinner.component';
import { ErrorComponent } from './components/errors/error/error.component';
import { FormControlPipe } from './pipes/form-control.pipe';
import { TranslateModule } from '@ngx-translate/core';
import { MaterialModule } from './../material.module';
import { CommonModule, DatePipe } from '@angular/common';
import { NgModule } from '@angular/core';
import { TextInputComponent } from './components/inputs/text-input/text-input.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { PasswordInputComponent } from './components/inputs/password-input/password-input.component';
import { ButtonComponent } from './components/buttons/button/button.component';
import { PasswordFormComponent } from './forms/password-form/password-form.component';
import { FirstLettersToTitleCasePipe } from './pipes/first-letters-to-title-case.pipe';
import { EnumAsStringPipe } from './pipes/enum-as-string.pipe';
import { PopupMenuComponent } from './components/menus/popup-menu/popup-menu.component';
import { TableComponent } from './components/tables/table/table.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { SingleSelectComponent } from './components/selects/single-select/single-select.component';
import { SingleEnumSelectComponent } from './components/selects/single-enum-select/single-enum-select.component';
import { FormModalComponent } from './components/modals/form-modal/form-modal.component';
import { CircleButtonComponent } from './components/buttons/circle-button/circle-button.component';
import { NumberInputComponent } from './components/inputs/number-input/number-input.component';
import { FormGroupPipe } from './pipes/form-group.pipe';
import { ConfirmationModalComponent } from './components/modals/confirmation-modal/confirmation-modal.component';
import { FormatCellPipe } from './pipes/format-cell.pipe';
import { CurrencySelectComponent } from './components/selects/currency-select/currency-select.component';
import { LanguageSelectComponent } from './components/selects/language-select/language-select.component';
import { ProfileDropdownComponent } from './components/profile-dropdown/profile-dropdown.component';
import { DragAndDropDirective } from './directives/drag-and-drop.directive';
import { DateInputComponent } from './components/inputs/date-input/date-input.component';
import { NotificationBellComponent } from '../modules/notifications/notification-bell/notification-bell.component';
import { YearMonthInputComponent } from './components/inputs/year-month-input/year-month-input.component';
import { YearSelectComponent } from './components/selects/year-select/year-select.component';

@NgModule({
  declarations: [
    TextInputComponent,
    PasswordInputComponent,
    FormControlPipe,
    ButtonComponent,
    PasswordFormComponent,
    ErrorComponent,
    LoadingSpinnerComponent,
    SidebarComponent,
    NavbarComponent,
    FooterComponent,
    EnumAsStringPipe,
    FirstLettersToTitleCasePipe,
    PopupMenuComponent,
    TableComponent,
    DataPropertyGetterPipe,
    PaginationComponent,
    SingleSelectComponent,
    SingleEnumSelectComponent,
    FormModalComponent,
    CircleButtonComponent,
    NumberInputComponent,
    FormGroupPipe,
    ConfirmationModalComponent,
    MoneyFormComponent,
    FormatCellPipe,
    CurrencySelectComponent,
    LanguageSelectComponent,
    ProfileDropdownComponent,
    UploadImageComponent,
    DragAndDropDirective,
    DateInputComponent,
    NotificationBellComponent,
    YearMonthInputComponent,
    YearSelectComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    TranslateModule,
    FontAwesomeModule,
  ],
  exports: [
    CommonModule,
    RouterModule,
    FormsModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    TextInputComponent,
    PasswordInputComponent,
    FormControlPipe,
    ButtonComponent,
    ErrorComponent,
    PasswordFormComponent,
    LoadingSpinnerComponent,
    SidebarComponent,
    NavbarComponent,
    FooterComponent,
    EnumAsStringPipe,
    FirstLettersToTitleCasePipe,
    PopupMenuComponent,
    DataPropertyGetterPipe,
    TableComponent,
    PaginationComponent,
    SingleSelectComponent,
    SingleEnumSelectComponent,
    FormModalComponent,
    CircleButtonComponent,
    FormGroupPipe,
    NumberInputComponent,
    ConfirmationModalComponent,
    EnumAsStringPipe,
    MoneyFormComponent,
    CurrencySelectComponent,
    LanguageSelectComponent,
    ProfileDropdownComponent,
    UploadImageComponent,
    DragAndDropDirective,
    DateInputComponent,
    YearMonthInputComponent,
    YearSelectComponent,
  ],
  providers: [
    DatePipe,
    FormControlPipe,
    EnumAsStringPipe,
    FirstLettersToTitleCasePipe,
    DataPropertyGetterPipe,
    FormGroupPipe,
    FormatCellPipe,
  ],
})
export class SharedModule {}
