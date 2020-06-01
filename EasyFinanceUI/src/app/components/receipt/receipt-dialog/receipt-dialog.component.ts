import { Component, OnInit, Inject } from '@angular/core';
import { ReceiptService } from 'src/app/services/core/receipt.service';
import { CategoryService } from 'src/app/services/core/category.service';
import { CurrencyService } from 'src/app/services/core/currency.service';
import { PaymentMethodService } from 'src/app/services/core/payment-method.service';
import { Currency } from 'src/app/models/currency';
import { Category } from 'src/app/models/category';
import { PaymentMethod } from 'src/app/models/payment-method';
import { Receipt } from 'src/app/models/receipt';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ReceiptPhotoService } from 'src/app/services/core/receipt-photo.service';
import { ReceiptDialogData } from 'src/app/helper-models/receipt-dialog-data';
import { FormMode } from 'src/app/constants/form-mode';
import { FileHelper } from 'src/app/helpers/file-helper';
import { MatSnackBar } from '@angular/material';
import { AuthenticationService } from 'src/app/services/user/authentication.service';

@Component({
  selector: 'app-receipt-dialog',
  templateUrl: './receipt-dialog.component.html',
  styleUrls: ['./receipt-dialog.component.css']
})
export class ReceiptDialogComponent implements OnInit {

  private categorySource: Array<Category>;
  private currencySource: Array<Currency>;
  private paymentMethodSource: Array<PaymentMethod>;
  public currentReceipt: Receipt;
  private imageFile: File;
  private imageURL: any = 'assets/images/photo_upload.svg';

  receiptForm: FormGroup;
  formMode: FormMode;
  dialogTitle: string;

  constructor(private receiptSvc: ReceiptService,
    private authSvc: AuthenticationService,
    private categorySvc: CategoryService,
    private currencySvc: CurrencyService,
    private paymentMethodSvc: PaymentMethodService,
    private photoSvc: ReceiptPhotoService,
    private fileHelper: FileHelper,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<ReceiptDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private dialogData: ReceiptDialogData) {

    this.formMode = dialogData.formMode;
    this.categorySvc.getAll().subscribe(data => {
      this.categorySource = data;
    });
    this.currencySvc.getAll().subscribe(data => {
      this.currencySource = data;
    });
    this.paymentMethodSvc.getAll().subscribe(data => {
      this.paymentMethodSource = data;
    });

    if (this.formMode === FormMode.New) {
      this.dialogTitle = "Новий чек"
      this.currentReceipt = new Receipt();
      this.receiptForm = this.createFormGroup(this.currentReceipt);
    } else {
      this.dialogTitle = `Редагувати чек# ${dialogData.receiptId}`;
      this.receiptSvc.getById(dialogData.receiptId).subscribe((receipt: Receipt) => {
        this.currentReceipt = receipt;
        this.receiptForm = this.createFormGroup(this.currentReceipt);
        this.imageURL = this.fileHelper.getImageSafeURL(this.currentReceipt.receiptPhoto.fileBytes, this.currentReceipt.receiptPhoto.fileName);
      });
    }
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
    if (this.receiptForm.invalid && (!this.imageFile || !this.imageURL)) {
      this.showNotification('Помилка! Некоректно введені дані.', 'Закрити')
      return;
    }

    this.populateReceiptData();

    switch (this.formMode) {
      case FormMode.New: {
        this.photoSvc.post(this.imageFile).subscribe((id: number) => {
          this.currentReceipt.receiptPhotoId = id;
          this.receiptSvc.post(this.currentReceipt).subscribe((id: number) => {
            this.showNotification(`Успішно додано чек# ${id}`, 'Закрити')
          });
          this.dialogRef.close();
        });
        break;
      }
      case FormMode.Edit: {
        this.receiptSvc.update(this.currentReceipt).subscribe(() => {
          this.showNotification(`Чек# ${this.currentReceipt.id} успішно збережено.`, 'Закрити')
        });
        break;
      }
    }
  }

  onStartAutoScan() {
    // TODO implement logic ( черга )
    this.showNotification('Чек відправлено на сканування.', 'Закрити');
    this.dialogRef.close()
  }

  onGetAutoScanned() {
    this.photoSvc.post(this.imageFile).subscribe((id: number) => {
      this.receiptSvc.autoScan(id).subscribe((data: Receipt) => {
        this.currentReceipt = data;
        this.receiptForm = this.createFormGroup(this.currentReceipt);
        this.formMode = FormMode.Edit;
        this.showNotification(`Чек# ${data.id} успішно відскановано.`, 'Закрити')
      },
        error => {
          this.showNotification('Помилка! Невдалося відсканувати чек.', 'Закрити');
        });
    });
  }

  isNewMode(): boolean {
    return this.formMode === FormMode.New;
  }

  populateReceiptData() {
    const receiptValue = this.receiptForm.value;
    this.currentReceipt.userId = this.authSvc.currentUserValue.id;
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

  showNotification(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 5000,
    });
  }
}
