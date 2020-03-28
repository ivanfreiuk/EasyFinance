import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import {MatSelectModule} from '@angular/material/select'
import { NavbarComponent } from './common/navbar/navbar.component';
import { ReceiptMainComponent } from './receipt/receipt-main/receipt-main.component';
import { ReceiptDialogComponent } from './receipt/receipt-dialog/receipt-dialog.component';
import { ReceiptListComponent } from './receipt/receipt-list/receipt-list.component';
import { SidenavComponent } from './common/sidenav/sidenav.component';
import { MatNativeDateModule } from '@angular/material/core';
import { HttpClientModule } from '@angular/common/http';
@NgModule({
  declarations: [
    NavbarComponent,
    ReceiptMainComponent,
    ReceiptDialogComponent,
    ReceiptListComponent,
    SidenavComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    MatDividerModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule
  ],
  exports: [
    HttpClientModule,
    NavbarComponent,
    ReceiptDialogComponent,
    ReceiptMainComponent,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    MatDividerModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatSelectModule
  ],
  entryComponents: [
    ReceiptDialogComponent
  ],
  providers: [
    MatDatepickerModule
  ]
})
export class ComponentsModule { }
