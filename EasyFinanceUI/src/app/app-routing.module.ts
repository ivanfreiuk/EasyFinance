import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReceiptMainComponent } from './components/receipt/receipt-main/receipt-main.component';
import { ChartMainComponent } from './components/chart/chart-main/chart-main.component';
import { CategoryChartComponent } from './components/chart/category-chart/category-chart.component';
import { PeriodChartComponent } from './components/chart/period-chart/period-chart.component';

const routes: Routes = [
  {
    path: 'receipts',
    component: ReceiptMainComponent
  },
  {
    path: 'statistic',
    component: ChartMainComponent,
    children: [
      { path: '', redirectTo: 'categories', pathMatch: 'full' },
      { path: 'categories', component: CategoryChartComponent },
      { path: 'periods', component: PeriodChartComponent },
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
