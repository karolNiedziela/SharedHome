export interface ModalConfig {
  modalTitle: string;
  saveButtonLabel?: string;
  isSaveButtonVisible?: boolean;
  closeButtonLabel?: string;
  isCloseButtonVisible?: boolean;
  onSave?(): any;
  onClose?(): any;
  onDismiss?(): void;
  applyToEachOperation?(): any;
}
