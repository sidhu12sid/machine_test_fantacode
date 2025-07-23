import { Component, signal } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { Sidebar } from './sidebar/sidebar';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,Sidebar,NgIf],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  constructor (private router:Router) {};

ngOnInit(){
  const isLoggedIn = !!localStorage.getItem('Username');
  if(isLoggedIn){
      this.router.navigate(['/dashboard'], { replaceUrl: true });
  }else{
    this.router.navigate([''], { replaceUrl: true });
  }
}

  protected readonly title = signal('login-app-client');

  showSideBar() : boolean {
    const result =  !['/'].includes(this.router.url);
    return result;
  }
}
