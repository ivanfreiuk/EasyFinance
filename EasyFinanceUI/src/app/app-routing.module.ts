import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReceiptMainComponent } from './components/receipt/receipt-main/receipt-main.component';

const routes: Routes = [
  {
    path: 'receipts',
    component: ReceiptMainComponent
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
