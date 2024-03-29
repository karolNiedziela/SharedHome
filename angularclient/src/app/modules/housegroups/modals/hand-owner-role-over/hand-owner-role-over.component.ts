import { HouseGroupMember } from './../../models/housegroup-member';
import { HouseGroupService } from './../../services/housegroup.service';
import { HandOwnerRoleOver } from './../../models/hand-owner-role-over';
import { FormGroup, FormControl } from '@angular/forms';
import { Modalable } from './../../../../core/models/modalable';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormModalComponent } from 'src/app/shared/components/modals/form-modal/form-modal.component';
import { FormModalConfig } from 'src/app/shared/components/modals/form-modal/form-modal.config';

@Component({
  selector: 'app-hand-owner-role-over',
  templateUrl: './hand-owner-role-over.component.html',
  styleUrls: ['./hand-owner-role-over.component.scss'],
})
export class HandOwnerRoleOverComponent implements Modalable, OnInit {
  @Input() houseGroupId!: string;
  @Input() member!: HouseGroupMember;

  @ViewChild('modal')
  private modal!: FormModalComponent;
  public modalConfig: FormModalConfig = {
    modalTitle: 'house_groups.change_house_group_owner',
    onSave: () => this.onSave(),
  };

  errorMessages: string[] = [];

  handOwnerRoleOverForm!: FormGroup;

  constructor(private houseGroupService: HouseGroupService) {}

  ngOnInit(): void {
    this.handOwnerRoleOverForm = new FormGroup({
      newOwnerPerson: new FormControl({ value: '', disabled: true }),
    });
  }

  openModal(): void {
    this.handOwnerRoleOverForm.patchValue({
      newOwnerPerson: this.member.fullName,
    });
    this.modal.open();
  }
  onSave(): void {
    const handOwnerRoleOver: HandOwnerRoleOver = {
      houseGroupId: this.houseGroupId,
      newOwnerPersonId: this.member.personId,
    };

    this.modal.save(
      this.houseGroupService.handOwnerRoleOver(handOwnerRoleOver)
    );
  }
}
