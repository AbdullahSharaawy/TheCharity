import { Routes } from '@angular/router';
import { MainPage } from './Pages/main-page/main-page';
import { OrgUserChoice } from './Pages/org-user-choice/org-user-choice';

export const routes: Routes = [
  { path: '', component: MainPage },
  { path: 'org-user-choice', component: OrgUserChoice },
  // add routes here
];