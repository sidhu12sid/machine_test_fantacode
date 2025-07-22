import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponse, LoginRequest, LoginResponse } from '../interfaces/login-response';
import { firstValueFrom, Observable } from 'rxjs';
import { environments } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class Auth {
  constructor(private http :HttpClient){}

  baseUrl : string = environments.baseUrl;

  // login(data: LoginRequest) : Observable<LoginResponse>{
  //      return this.http.post<LoginResponse>(this.baseUrl + '/Auth/user-login', data);
  // }

  async login(data :LoginRequest){
      const response = await firstValueFrom(this.http.post<ApiResponse<LoginResponse>>(this.baseUrl + '/Auth/user-login', data));
      return response;
  }
}
