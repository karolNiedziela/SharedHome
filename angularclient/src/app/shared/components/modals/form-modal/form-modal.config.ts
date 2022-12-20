export interface FormModalConfig {
  modalTitle: string;
  saveButtonLabel?: string;
  isSaveButtonVisible?: boolean;
  closeButtonLabel?: string;
  isCloseButtonVisible?: boolean;
  onSave: () => any;
  onClose?: () => void;
  onDismiss?: () => void;
  onReset?: () => void;
}
