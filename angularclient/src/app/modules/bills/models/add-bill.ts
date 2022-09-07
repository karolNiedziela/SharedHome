export interface AddBill {
  billType: number;
  serviceProviderName: string;
  dateOfPayment: Date;
  cost?: number;
  currency?: string;
}
