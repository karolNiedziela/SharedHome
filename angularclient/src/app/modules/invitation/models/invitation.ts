import { InvitationStatus } from '../enums/invitation-status';

export interface Invitation {
  houseGroupId: number;
  requestedByPersonId: string;
  requestedToPersonId: string;
  InvitationStatus: InvitationStatus;
  sendByFirstName: string;
  sendByLastName: string;
}
