export interface AuthenticationResponse {
  accessToken: string;
  refreshToken: string;
  expiry: number;
  userId: string;
  firstName: string;
  lastName: string;
  email: string;
  roles: string[];
  claims: { type: string; values: string[] };
}
