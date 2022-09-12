export interface ConfirmationModalConfig {
  modalTitle?: string;
  confirmationText?: string;
  confirmationProperties?: string[];
  yesButtonText?: string;
  noButtonText?: string;
  onOpen?(): boolean;
  onSave?(): any;
  onClose?(): boolean;
  onDismiss?(): boolean;
}
