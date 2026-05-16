import { Routes } from '@angular/router';
import { Home } from './components/home/home';
import { Admin } from './components/admin/admin';
import { MsalGuard } from '@azure/msal-angular';

export const routes: Routes = [
    { path: 'home', component: Home, canActivate: [MsalGuard] },
    { path: 'admin', component: Admin, canActivate: [MsalGuard] },
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { 
    path: '**', 
    redirectTo: '' 
  }
];
