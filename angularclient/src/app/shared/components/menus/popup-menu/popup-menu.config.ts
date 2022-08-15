export interface PopupMenuConfig {
  isHidden?: boolean;
  onEdit?: any;
  isEditVisible?: boolean;
  onDelete?: any;
  isDeleteVisible?: boolean;
  additionalPopupMenuItems?: AdditionalPopupMenuItem[];
}

export interface AdditionalPopupMenuItem {
  text: string;
  onClick: any;
}
