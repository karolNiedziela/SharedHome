import { NetContentType } from '../enums/net-content-type';

export interface NetContent {
  netContent: string;
  netContentType?: NetContentType;
}
