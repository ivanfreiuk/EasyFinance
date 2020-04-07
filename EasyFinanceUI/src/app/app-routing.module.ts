import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReceiptMainComponent } from './components/receipt/receipt-main/receipt-main.component';
import { ChartMainComponent } from './components/chart/chart-main/chart-main.component';

const routes: Routes = [
  {
    path: 'receipts',
    component: ReceiptMainComponent
  },
  {
    path: 'statistic',
    component: ChartMainComponent
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
