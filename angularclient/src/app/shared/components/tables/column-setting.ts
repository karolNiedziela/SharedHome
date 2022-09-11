import { CellPipeFormat } from './cell-pipe-format';

export interface ColumnSetting {
  propertyName: string;
  header?: string;
  format?: CellPipeFormat;
  enumType?: object;
}
