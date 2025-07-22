export interface LoginResponse {
  accessToken:string;
  username:string;
}

export interface LoginRequest{
  username : string;
  password:string;
}
