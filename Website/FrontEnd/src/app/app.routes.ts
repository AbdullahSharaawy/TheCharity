import { Routes } from '@angular/router';
import { MainPage } from './Pages/main-page/main-page';
import { SignIn } from './Pages/sign-in/sign-in';

export const routes: Routes = [
  { path: '', component: MainPage },
  { path: 'sign-in', component: SignIn },
  // add routes here
];