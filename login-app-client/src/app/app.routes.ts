import { Routes } from '@angular/router';
import { LoginComponent } from './login-component/login-component';
import { Dashboard } from './dashboard/dashboard';

export const routes: Routes = [
{
    path: '',
    component: LoginComponent,
  },
  {
    path: 'dashboard',
    component: Dashboard,
  }
];
