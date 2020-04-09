import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReceiptService } from './core/receipt.service';
import { CategoryService } from './core/category.service';
import { CurrencyService } from './core/currency.service';
import { PaymentMethod } from '../models/payment-method';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
  ]
})
export class ServicesModule { }
