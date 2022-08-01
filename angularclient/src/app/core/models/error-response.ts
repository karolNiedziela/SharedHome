import { HttpStatusCode } from '@angular/common/http';

export interface ErrorResponse {
  status: HttpStatusCode;
  title: string;
  type: string;
  traceId: string;
  detail: string;
  errors: string[];
}
