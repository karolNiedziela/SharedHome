export interface ModalConfig {
  modalTitle: string;
  saveButtonLabel?: string;
  isSaveButtonVisible?: boolean;
  closeButtonLabel?: string;
  isCloseButtonVisible?: boolean;
  onSave?(): Promise<any> | any;
  onClose?(): Promise<any> | any;
  onDismiss?(): void;
  applyToEachOperation?(): Promise<any> | any;
}
