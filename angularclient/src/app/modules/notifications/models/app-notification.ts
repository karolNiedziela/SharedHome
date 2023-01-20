import { TargetType } from './../constants/target-type';
export interface AppNotification {
  title: string;
  message?: string;
  isRead: boolean;
  type?: string;
  target: TargetType;
  operation: string;
  createdByFullName?: string;
  createdAt: Date;
}
