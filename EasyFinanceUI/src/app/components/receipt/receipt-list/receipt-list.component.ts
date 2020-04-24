import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { ReceiptService } from 'src/app/services/core/receipt.service';
import { FileHelper } from 'src/app/helpers/file-helper';
import { ReceiptView } from 'src/app/models/receipt-view';
import { formatDate } from "@angular/common";
import { ReceiptPhotoService } from 'src/app/services/core/receipt-photo.service';
import { MatDialog } from '@angular/material/dialog';
import { ReceiptDialogComponent } from '../receipt-dialog/receipt-dialog.component';
import { FormMode } from 'src/app/constants/form-mode';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { Observable } from 'rxjs';
import { AuthenticationService } from 'src/app/services/user/authentication.service';
import { ReceiptFilterCriteria } from 'src/app/models/receipt-filter-criteria';

@Component({
  selector: 'app-receipt-list',
  templateUrl: './receipt-list.component.html',
  styleUrls: ['./receipt-list.component.css']
})
export class ReceiptListComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  observableData: Observable<ReceiptView[]>;
  public dataSource: MatTableDataSource<ReceiptView> = new MatTableDataSource<ReceiptView>();
  private filterCriteria: ReceiptFilterCriteria;

  constructor(private dialog: MatDialog,
    private changeDetectorRef: ChangeDetectorRef,
    private receiptSvc: ReceiptService,
    private authSvc: AuthenticationService) {
      this.filterCriteria = new ReceiptFilterCriteria();
      this.filterCriteria.userId = this.authSvc.currentUserValue.id;
  }

  get dataSourceValue() {
    return this.dataSource.data;
  }

  set dataSourceValue(value: Array<ReceiptView>) {
    this.dataSource.data = value;
  }

  get filterCriteriaValue() {
    return this.filterCriteria;
  }

  set filterCriteriaValue(value: ReceiptFilterCriteria) {
    this.filterCriteria = value;
    this.refreshDataSource();
  }

  ngOnInit() {
    this.receiptSvc.getAll(this.authSvc.currentUserValue.id).subscribe((data: ReceiptView[]) => {
      this.dataSource = new MatTableDataSource<ReceiptView>(data);
      this.changeDetectorRef.detectChanges();
      this.dataSource.paginator = this.paginator;
      this.observableData = this.dataSource.connect();
    });
  }

  ngOnDestroy() {
    if (this.dataSource) {
      this.dataSource.disconnect();
    }
  }

  onReceiptDelete(id: number) {
    this.receiptSvc.delete(id).subscribe(data => {
      this.dataSource.data = this.dataSource.data.filter(r => r.id !== id)
    });
  }

  openDialog(id: number): void {
    const dialogRef = this.dialog.open(ReceiptDialogComponent, {
      maxHeight: '700px',
      maxWidth: '900px',
      height: '90%',
      width: '70%',
      data: { receiptId: id, formMode: FormMode.Edit }
    });

    dialogRef.afterClosed().subscribe(() => this.refreshDataSource());
  }

  public refreshDataSource() {
    this.receiptSvc.getFilteredReceipts(this.filterCriteria).subscribe((data: ReceiptView[]) => {
      this.dataSourceValue = data;
    });
  }
}
