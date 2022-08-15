export interface Modalable {
  openModal(): void;
  onSave(): void;
  onClose(): void;
  onDismiss(): void;
}
