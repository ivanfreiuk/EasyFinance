import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ReceiptDialogComponent } from '../receipt-dialog/receipt-dialog.component';

@Component({
  selector: 'app-receipt-main',
  templateUrl: './receipt-main.component.html',
  styleUrls: ['./receipt-main.component.css']
})
export class ReceiptMainComponent implements OnInit {

  constructor(public dialog: MatDialog) { }

  ngOnInit() {
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(ReceiptDialogComponent, {
      height: '90%',
      width: '70%',
    });
  }
}
