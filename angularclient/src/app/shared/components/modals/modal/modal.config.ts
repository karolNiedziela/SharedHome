export interface ModalConfig {
  modalTitle: string;

  saveButtonLabel?: string;
  closeButtonLabel?: string;
  onOpen?(): Promise<boolean> | boolean;
  onSave?(): Promise<any> | any;
  onClose?(): Promise<boolean> | boolean;
  onDismiss?(): boolean;
}
