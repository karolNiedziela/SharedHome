export interface ConfirmationModalConfig {
  modalTitle?: string;
  confirmationText?: string;
  yesButtonText?: string;
  noButtonText?: string;
  onOpen?(): Promise<boolean> | boolean;
  onSave?(): Promise<any> | any;
  onClose?(): Promise<boolean> | boolean;
  onDismiss?(): boolean;
}
