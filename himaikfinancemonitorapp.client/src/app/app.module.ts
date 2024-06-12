import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



import { ButtonModule } from 'primeng/button';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LandingPageComponent } from './Feature/landing-page/landing-page.component';
import { HeaderCompComponent } from './Core/Components/header-comp/header-comp.component';
import { CardComponent } from './Core/Components/card/card.component';
import { TabViewComponent } from './Core/Components/tab-view/tab-view.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { AuthInterceptor } from './Core/Utils/auth.interceptor';
import { MinisterCardComponent } from './Core/Components/minister-card/minister-card.component';
import { AdminDashboardComponent } from './Feature/admin-dashboard/admin-dashboard.component';
import { AdminSidebarComponent } from './Core/Components/admin-sidebar/admin-sidebar.component';
import { LoginComponent } from './Feature/login/login.component';
import { DashboardComponent } from './Core/Components/dashboard/dashboard.component';
import { IncomeTableComponent } from './Core/Components/income-table/income-table.component';
import { TransactionTableComponent } from './Core/Components/transaction-table/transaction-table.component';
import { HeaderPositionDirective} from './Core/Directives/app-header-position.directive';
import { DashboardHeaderComponent } from './Core/Components/dashboard-header/dashboard-header.component';

@NgModule({
    declarations: [
        AppComponent,
        LandingPageComponent,
        HeaderCompComponent,
        CardComponent,
        TabViewComponent,
        MinisterCardComponent,
        AdminDashboardComponent,
        AdminSidebarComponent,
        LoginComponent,
        DashboardComponent,
        IncomeTableComponent,
        TransactionTableComponent,
        HeaderPositionDirective,
        DashboardHeaderComponent
    ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    ButtonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    provideAnimationsAsync(),
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
