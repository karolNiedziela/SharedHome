import { InvitationStatus } from '../enums/invitation-status';

export interface Invitation {
  houseGroupId: number;
  requestedByPersonId: string;
  requestedToPersonId: string;
  invitationStatus: InvitationStatus;
  sentByFirstName: string;
  sentByLastName: string;
}
