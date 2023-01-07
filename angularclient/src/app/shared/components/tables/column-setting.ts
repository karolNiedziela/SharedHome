import { CellPipeFormat } from './cell-pipe-format';

export interface TableColumn {
  name: string;
  dataKey: string;
  position?: 'right' | 'left';
  isSortable?: boolean;
  format?: CellPipeFormat;
  enumType?: any;
}
