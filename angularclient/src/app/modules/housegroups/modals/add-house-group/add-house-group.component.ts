import { AddHouseGroup } from './../../models/add-house-group';
import { HouseGroupService } from './../../services/housegroup.service';
import { Modalable } from './../../../../core/models/modalable';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormModalComponent } from 'src/app/shared/components/modals/form-modal/form-modal.component';
import { FormModalConfig } from 'src/app/shared/components/modals/form-modal/form-modal.config';

@Component({
  selector: 'app-add-house-group',
  templateUrl: './add-house-group.component.html',
  styleUrls: ['./add-house-group.component.scss'],
})
export class AddHouseGroupComponent implements Modalable, OnInit {
  addHouseGroupForm!: FormGroup;

  @ViewChild('modal') private modal!: FormModalComponent;
  public modalConfig: FormModalConfig = {
    modalTitle: 'house_groups.create_house_group',
    onSave: () => this.onSave(),
  };

  constructor(private houseGroupService: HouseGroupService) {}

  ngOnInit(): void {
    this.addHouseGroupForm = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.maxLength(30),
      ]),
    });
  }

  openModal(): void {
    this.modal.open();
  }
  onSave(): void {
    const name = this.addHouseGroupForm.get('name')?.value;

    const addHouseGroup: AddHouseGroup = {
      name: name,
    };

    this.modal.save(this.houseGroupService.add(addHouseGroup));
  }
}
