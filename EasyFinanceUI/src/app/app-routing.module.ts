import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReceiptMainComponent } from './components/receipt/receipt-main/receipt-main.component';
import { ChartMainComponent } from './components/chart/chart-main/chart-main.component';
import { CategoryChartComponent } from './components/chart/category-chart/category-chart.component';
import { PeriodChartComponent } from './components/chart/period-chart/period-chart.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegisterComponent } from './components/user/register/register.component';
import { DinamicChartComponent } from './components/chart/dinamic-chart/dinamic-chart.component';
import { CanActivate } from '@angular/router/src/utils/preactivation';
import { AuthGuard } from './guards/auth.guard';


const routes: Routes = [
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'receipts',
    component: ReceiptMainComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'statistic',
    component: ChartMainComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: 'categories', pathMatch: 'full' },
      { path: 'categories', component: CategoryChartComponent },
      { path: 'periods', component: PeriodChartComponent },
      { path: 'dinamics', component: DinamicChartComponent }
    ]
  },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'receipts'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
