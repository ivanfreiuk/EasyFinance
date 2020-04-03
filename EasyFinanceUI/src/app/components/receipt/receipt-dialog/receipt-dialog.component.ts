import { Component, OnInit } from '@angular/core';
import { ReceiptService } from 'src/app/services/receipt.service';
import { CategoryService } from 'src/app/services/category.service';
import { CurrencyService } from 'src/app/services/currency.service';
import { PaymentMethodService } from 'src/app/services/payment-method.service';
import { Currency } from 'src/app/models/currency';
import { Category } from 'src/app/models/category';
import { PaymentMethod } from 'src/app/models/payment-method';
import { Receipt } from 'src/app/models/receipt';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ReceiptPhotoService } from 'src/app/services/receipt-photo.service';
import { ReceiptPhoto } from 'src/app/models/receipt-photo';

@Component({
  selector: 'app-receipt-dialog',
  templateUrl: './receipt-dialog.component.html',
  styleUrls: ['./receipt-dialog.component.css']
})
export class ReceiptDialogComponent implements OnInit {

  private categorySource: Array<Category>;
  private currencySource: Array<Currency>;
  private paymentMethodSource: Array<PaymentMethod>;

  private currentReceipt: Receipt = new Receipt();
  private imageFile: File;
  private imageURL: any = 'assets/images/photo_upload.svg';

  receiptForm: FormGroup;

  constructor(private receiptSvc: ReceiptService,
    private categorySvc: CategoryService,
    private currencySvc: CurrencyService,
    private paymentMethodSvc: PaymentMethodService,
    private photoSvc: ReceiptPhotoService,
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ReceiptDialogComponent>) {
    this.receiptForm = this.createFormGroup(this.currentReceipt);

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

  onFileUpload() {
    const fileUpload = document.getElementById('fileUpload') as HTMLInputElement;
    fileUpload.onchange = () => {
      this.imageFile = fileUpload.files[0];
      this.setImageURL(this.imageFile);
    };
    fileUpload.click();
  }

  onSave() {
    if (this.receiptForm.valid && !!this.imageFile) {
      // NEW
      this.populateReceiptData();
      this.photoSvc.post(this.imageFile).subscribe((id:number)=>{
        this.currentReceipt.receiptPhotoId = id;
        this.receiptSvc.post(this.currentReceipt).subscribe((id:number)=>{ console.log('CERATED'+id)})
      });     
    } else {
      console.log("FORM INVALID")
    }

  }
  onRunAutoScanning() {
    this.dialogRef.close()
  }

  populateReceiptData() {
    const receiptValue = this.receiptForm.value;
    this.currentReceipt.merchant = receiptValue.merchant;
    this.currentReceipt.purchaseDate = receiptValue.purchaseDate;
    this.currentReceipt.totalAmount = receiptValue.totalAmount;
    this.currentReceipt.currencyId = receiptValue.currency;
    this.currentReceipt.paymentMethodId = receiptValue.paymentMethod;
    this.currentReceipt.categoryId = receiptValue.category;
    this.currentReceipt.description = receiptValue.description;
  }

  createFormGroup(receipt: Receipt) {
    return this.formBuilder.group({
      merchant: [receipt.merchant],
      purchaseDate: [receipt.purchaseDate],
      totalAmount: [receipt.totalAmount, [Validators.min(0), Validators.max(1000000), Validators.pattern("^[0-9]+(.[0-9]{0,2})?$")]],
      currency: [receipt.currencyId],
      paymentMethod: [receipt.paymentMethodId],
      category: [receipt.categoryId],
      description: [receipt.description],
      receiptPhoto: [receipt.receiptPhotoId]
    });
  }

  setImageURL(file: File | Blob) {
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = (_event) => {
      this.imageURL = reader.result;
    };
  }
}
