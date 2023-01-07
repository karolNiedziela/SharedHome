export interface ConfirmationModalConfig {
  modalTitle?: string;
  confirmationText?: string;
  confirmationProperties?: string[];
  yesButtonText?: string;
  noButtonText?: string;
  onConfirm: () => void;
  onClose?: () => void;
}
