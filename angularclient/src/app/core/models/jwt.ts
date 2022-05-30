export interface Jwt {
  accessToken: string;
  refreshToken: string;
  expiry: number;
  userId: string;
  email: string;
  roles: string[];
  claims: { type: string; values: string[] };
}
