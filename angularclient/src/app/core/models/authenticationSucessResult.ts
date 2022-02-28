export interface AuthenticationSucessResult {
  accessToken: string;
  expiry: number;
  userId: string;
  role: string;
  email: string;
}
