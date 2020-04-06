import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { ReceiptService } from 'src/app/services/receipt.service';
import { FileHelper } from 'src/app/helpers/file-helper';
import { ReceiptView } from 'src/app/models/receipt-view';
import { formatDate } from "@angular/common";
import { ReceiptPhotoService } from 'src/app/services/receipt-photo.service';
import { MatDialog } from '@angular/material/dialog';
import { ReceiptDialogComponent } from '../receipt-dialog/receipt-dialog.component';
import { FormMode } from 'src/app/constants/form-mode';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-receipt-list',
  templateUrl: './receipt-list.component.html',
  styleUrls: ['./receipt-list.component.css']
})
export class ReceiptListComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  observableData: Observable<ReceiptView[]>;
  public dataSource: MatTableDataSource<ReceiptView> = new MatTableDataSource<ReceiptView>();


  constructor(public dialog: MatDialog,
    private changeDetectorRef: ChangeDetectorRef,
    private receiptSvc: ReceiptService,
    private photoSvc: ReceiptPhotoService,
    private fileHelper: FileHelper) {

  }

  ngOnInit() {
    this.receiptSvc.getAll().subscribe((data: ReceiptView[]) => {
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

    dialogRef.afterClosed().subscribe(i => this.refreshDataSource());
  }

  public refreshDataSource() {
    this.receiptSvc.getAll().subscribe((data: ReceiptView[]) => {
      this.dataSource.data = data;
    });
  }
}
