import { Component, OnInit } from '@angular/core';
import { ReceiptService } from 'src/app/services/receipt.service';
import { CategoryService } from 'src/app/services/category.service';
import { CurrencyService } from 'src/app/services/currency.service';
import { PaymentMethodService } from 'src/app/services/payment-method.service';
import { Currency } from 'src/app/models/currency';
import { Category } from 'src/app/models/category';
import { PaymentMethod } from 'src/app/models/payment-method';
import { Receipt } from 'src/app/models/receipt';

@Component({
  selector: 'app-receipt-dialog',
  templateUrl: './receipt-dialog.component.html',
  styleUrls: ['./receipt-dialog.component.css']
})
export class ReceiptDialogComponent implements OnInit {

  private categorySource: Array<Category>;
  private currencySource: Array<Currency>;
  private paymentMethodSource: Array<PaymentMethod>;
  private currentReceipt: Receipt;

  constructor(private receiptSvc: ReceiptService,
    private categorySvc: CategoryService,
    private currencySvc: CurrencyService,
    private paymentMethodSvc: PaymentMethodService) {

    this.categorySvc.getAll().subscribe(data => {
      this.categorySource = data;
    });
    this.currencySvc.getAll().subscribe(data => {
      this.currencySource = data;
    });
    this.paymentMethodSvc.getAll().subscribe(data => {
      this.paymentMethodSource = data;
    });
  }

  ngOnInit() {
  }

}
