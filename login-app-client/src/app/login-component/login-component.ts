import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Auth } from '../services/auth';
import { LoginRequest } from '../interfaces/login-response';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login-component',
  imports: [ReactiveFormsModule],
  templateUrl: './login-component.html',
  styleUrl: './login-component.scss'
})
export class LoginComponent {

  constructor(private authService : Auth, private router:Router, private toaster: ToastrService){}

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
      this.toaster.success(response.message, 'Success!');
      localStorage.setItem('Username', response.data.userName);
      this.router.navigate(['/dashboard'], { replaceUrl: true }); // preventing going back to login page after login success
    }else{

      this.toaster.error(response.message, 'Failed');
    }
  }
  catch(error : any){
     this.toaster.error(error.error.message, 'Failed');
  }

}

}
