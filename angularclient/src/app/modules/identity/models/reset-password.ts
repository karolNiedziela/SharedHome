export interface ResetPassword {
  email: string;
  code: string;
  newPassword: string;
  confirmNewPassword: string;
}
