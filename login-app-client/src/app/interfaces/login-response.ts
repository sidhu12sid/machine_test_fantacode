export interface ApiResponse<T> {
  error: boolean;
  status: boolean;
  message: string;
  data: T;
}


export interface LoginResponse {
  accessToken:string;
  username:string;
}

export interface LoginRequest{
  username : string;
  password:string;
}
