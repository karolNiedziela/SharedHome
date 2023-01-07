export interface PopupMenuConfig {
  onEdit?: any;
  isEditVisible?: boolean;
  onDelete?: any;
  isDeleteVisible?: boolean;
  isHidden?: boolean;
  additionalPopupMenuItems?: AdditionalPopupMenuItem[];
}

export interface AdditionalPopupMenuItem {
  text: string;
  onClick: any;
}
