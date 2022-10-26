import { InvitationStatus } from '../enums/invitation-status';

export interface Invitation {
  houseGroupId: string;
  houseGroupName: string;
  requestedByPersonId: string;
  requestedToPersonId: string;
  invitationStatus: InvitationStatus;
  sentByFirstName: string;
  sentByLastName: string;
  sentByFullName: string;
}
