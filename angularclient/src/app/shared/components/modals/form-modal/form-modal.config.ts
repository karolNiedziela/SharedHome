import { Observable } from 'rxjs';

export interface FormModalConfig {
  modalTitle: string;
  saveButtonLabel?: string;
  isSaveButtonVisible?: boolean;
  closeButtonLabel?: string;
  isCloseButtonVisible?: boolean;
  saveOperation?: Observable<any>;
  onSave: () => any;
  onClose?: () => void;
  onDismiss?: () => void;
  onReset?: () => void;
}
