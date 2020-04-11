import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Receipt } from '../../models/receipt';
import { FileHelper } from 'src/app/helpers/file-helper';
import { ReceiptView } from 'src/app/models/receipt-view';
import { map } from 'rxjs/operators';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class ReceiptService {

  private apiUrl: string = 'https://localhost:44398/api';

  constructor(private http: HttpClient, private fileHelper: FileHelper, private datePipe: DatePipe) { }

  getAll(userId: number = null): Observable<ReceiptView[]> {
    
    const params = userId ? new HttpParams().set('userId', JSON.stringify(userId)) : null;

    return this.http.get<Receipt[]>(`${this.apiUrl}/receipts`,{params:params})
      .pipe<ReceiptView[]>(
        map((data: Receipt[]) => {
          let receipts = new Array<ReceiptView>();
          data.forEach((receipt: Receipt) => {
            let receiptView = new ReceiptView();
            receiptView.id = receipt.id;
            receiptView.imageURL = receipt.receiptPhotoId ?
              this.fileHelper.getImageSafeURL(receipt.receiptPhoto.fileBytes, receipt.receiptPhoto.fileName) : null;
            receiptView.merchant = receipt.merchant;
            receiptView.categoryName = receipt.categoryId ? receipt.category.name : '(без категорії)';
            receiptView.paymentMethodName = receipt.paymentMethodId ? receipt.paymentMethod.name : '(без способу оплати)';
            receiptView.totalAmount = receipt.totalAmount;
            receiptView.currencyGenericCode = receipt.currencyId ? receipt.currency.genericCode : null;
            receiptView.purchaseDate = receipt.purchaseDate;
            receiptView.description = receipt.description;

            receipts.push(receiptView);
          });
          return receipts;
        })
      )
  }

  getExpensesByCategories(userId: number) {
    return this.http.get(`${this.apiUrl}/receipts/expensesbycategories/${userId}`).pipe(map((data: any[]) => {
      let items = new Array();
      data.forEach(d => {
        let item = {
          value: d.total,
          name: d.categoryName ? d.categoryName : '(без категорії)'
        };
        items.push(item);
      });
      return items;
    }))
  }

  getExpensesForPeriod(userId: number) {
    const params = userId ? new HttpParams().set('userId', JSON.stringify(userId)) : null;
    return this.http.get(`${this.apiUrl}/receipts/allperiodexpenses`, {params:params})
    .pipe(map((data: any[]) => {
      let items = new Array();
      data.forEach(d => {
        let item = {
          value: d.total,
          name: this.datePipe.transform(d.purchaseDate, 'dd-MM-yyyy')
        };
        items.push(item);
      });
      return items;
    }))
  }

  getById(id: number): Observable<Receipt> {
    return this.http.get<Receipt>(`${this.apiUrl}/receipts/${id}`);
  }

  post(receipt: Receipt) {
    return this.http.post(`${this.apiUrl}/receipts/create`, receipt);
  }

  autoScan(photoId: number) {
    return this.http.post(`${this.apiUrl}/receipts/autoscan`, photoId);
  }

  update(receipt: Receipt) {
    return this.http.put(`${this.apiUrl}/receipts/${receipt.id}`, receipt);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/receipts/${id}`);
  }
}
