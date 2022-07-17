import { FormGroup, FormControl, Validators } from '@angular/forms';
import {
  Component,
  OnChanges,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';

@Component({
  selector: 'app-add-shopping-list',
  templateUrl: './add-shopping-list.component.html',
  styleUrls: ['./add-shopping-list.component.scss'],
})
export class AddShoppingListComponent implements OnInit {
  addShoppingListForm!: FormGroup;

  @ViewChild('modal') private modal!: ModalComponent;

  public modalConfig: ModalConfig = {
    modalTitle: 'Add shopping list',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  constructor() {
    this.addShoppingListForm = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.maxLength(20),
      ]),
      test: new FormControl('', [
        Validators.required,
        Validators.maxLength(20),
      ]),
    });
  }

  ngOnInit(): void {}

  openModal(): void {
    this.modal.open();
  }

  onSave(): void {
    this.addShoppingListForm.reset();
  }

  onClose(): void {
    this.addShoppingListForm.reset();
  }

  onDismiss(): void {
    this.addShoppingListForm.reset();
  }
}
