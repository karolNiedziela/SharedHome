import { AddHouseGroup } from './../../models/add-house-group';
import { HouseGroupService } from './../../services/housegroup.service';
import { Modalable } from './../../../../core/models/modalable';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';

@Component({
  selector: 'app-add-house-group',
  templateUrl: './add-house-group.component.html',
  styleUrls: ['./add-house-group.component.scss'],
})
export class AddHouseGroupComponent implements Modalable, OnInit {
  addHouseGroupForm!: FormGroup;
  errorMessages: string[] = [];

  @ViewChild('modal') private modal!: ModalComponent;
  public modalConfig: ModalConfig = {
    modalTitle: 'Add house group',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  constructor(private houseGroupService: HouseGroupService) {}

  ngOnInit(): void {
    this.addHouseGroupForm = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.maxLength(40),
      ]),
    });
  }

  openModal(): void {
    this.modal.open();
  }
  onSave(): void {
    if (this.addHouseGroupForm.invalid) {
      this.addHouseGroupForm.markAllAsTouched();
      return;
    }

    const name = this.addHouseGroupForm.get('name')?.value;

    const addHouseGroup: AddHouseGroup = {
      name: name,
    };

    this.houseGroupService.add(addHouseGroup).subscribe({
      next: () => {
        this.addHouseGroupForm.reset();

        this.modal.close();
      },
      error: (errors: string[]) => {
        this.errorMessages = errors;
      },
    });
  }
  onClose(): void {
    this.addHouseGroupForm.reset();
  }
  onDismiss(): void {
    this.addHouseGroupForm.reset();
  }
}
