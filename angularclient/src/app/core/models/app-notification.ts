export interface AppNotification {
  title: string;
  message?: string;
  isRead: boolean;
  type?: string;
  target: string;
  operation: string;
}
