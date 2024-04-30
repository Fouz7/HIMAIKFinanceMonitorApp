import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from './Feature/landing-page/landing-page.component';
import { AdminDashboardComponent } from './Feature/admin-dashboard/admin-dashboard.component';
import { LoginComponent } from './Feature/login/login.component';
import { AuthGuard } from './Core/Utils/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: LandingPageComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'admin-dashboard',
    component: AdminDashboardComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
