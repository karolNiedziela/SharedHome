import { HouseGroupMember } from './housegroup-member';
export interface HouseGroup {
  id: string;
  name: string;
  members: HouseGroupMember[];
}
