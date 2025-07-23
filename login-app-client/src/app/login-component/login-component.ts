import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Auth } from '../services/auth';
import { LoginRequest } from '../interfaces/login-response';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-component',
  imports: [ReactiveFormsModule],
  templateUrl: './login-component.html',
  styleUrl: './login-component.scss'
})
export class LoginComponent {

  constructor(private authService : Auth, private router:Router){}

  loginForm = new FormGroup({
    userName : new FormControl(''),
    passWord : new FormControl(''),
  });

async loginUser(){
  const payLoad : LoginRequest = {
    username : this.loginForm.value.userName  ?? '',
    password :this.loginForm.value.passWord  ?? ''
  }

  try{
    const response = await this.authService.login(payLoad);
    if(response.status &&  !response.error){
      console.log(response.message, response.data);
      localStorage.setItem('Username', response.data.userName);
      this.router.navigate(['/dashboard'], { replaceUrl: true }); // preventing going back to login page after login success
    }else{
      console.log(response.message, response.data);
    }
  }
  catch(error){
    console.log('Login failed', error)
  }

}

}
