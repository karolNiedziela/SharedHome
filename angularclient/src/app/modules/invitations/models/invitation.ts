import { InvitationStatus } from '../enums/invitation-status';

export interface Invitation {
  houseGroupId: number;
  houseGroupName: string;
  requestedByPersonId: string;
  requestedToPersonId: string;
  invitationStatus: InvitationStatus;
  sentByFirstName: string;
  sentByLastName: string;
  sentByFullName: string;
}
