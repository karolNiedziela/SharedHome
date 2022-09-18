import { HouseGroupMember } from './housegroup-member';
export interface HouseGroup {
  id: number;
  name: string;
  members: HouseGroupMember[];
}
