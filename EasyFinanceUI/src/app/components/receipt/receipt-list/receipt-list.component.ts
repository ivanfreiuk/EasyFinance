import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { ReceiptService } from 'src/app/services/receipt.service';
import { Receipt } from 'src/app/models';
import { FileHelper } from 'src/app/helpers/file-helper';
import { ReceiptView } from 'src/app/models/receipt-view';
import { map } from 'rxjs/operators';
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
  obs: Observable<any>;
  dataSource: MatTableDataSource<ReceiptView>;
  
  receiptSource: ReceiptView[];
  

  constructor(public dialog: MatDialog,
    private changeDetectorRef: ChangeDetectorRef,
    private receiptSvc: ReceiptService,
    private photoSvc: ReceiptPhotoService,
    private fileHelper: FileHelper) { 
      
       this.receiptSvc.getAll().subscribe((data: ReceiptView[])=>{
         this.receiptSource=data;
         this.dataSource = new MatTableDataSource<ReceiptView>(this.receiptSource);
       });
  }

  ngOnInit() {
    this.changeDetectorRef.detectChanges();     
    //this.dataSource.paginator = this.paginator;
    //this.obs = this.dataSource.connect();
  }

  ngOnDestroy() {
    if (this.dataSource) { 
      this.dataSource.disconnect(); 
    }
  }

  onReceiptDelete(id: number) {
    this.receiptSvc.delete(id).subscribe(data=>{
      this.receiptSource = this.receiptSource.filter(r=> r.id !== id)
    });
  }

  openDialog(id:number): void {
    const dialogRef = this.dialog.open(ReceiptDialogComponent, {
      maxHeight: '700px',
      maxWidth: '900px',
      height: '90%',
      width: '70%',
      data: { receiptId: id, formMode: FormMode.Edit}
    });
    console.log('refresh')

    dialogRef.afterClosed().subscribe(t=>this.refreshReceiptSource())
    this.refreshReceiptSource()
  }

refreshReceiptSource() {
  this.receiptSvc.getAll().subscribe((data: ReceiptView[])=>{
    this.receiptSource=data;
  });
  console.log('refresh')
}


  public setImageURL(file: File | Blob, imageURL: any) {
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = (_event) => {
        imageURL = reader.result;     
    };
}
}
