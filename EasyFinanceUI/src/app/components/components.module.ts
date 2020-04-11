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
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatChipsModule } from '@angular/material/chips';
import { MatListModule } from '@angular/material/list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTabsModule } from '@angular/material/tabs';
import {MatMenuModule} from '@angular/material/menu';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { NavbarComponent } from './common/navbar/navbar.component';
import { ReceiptMainComponent } from './receipt/receipt-main/receipt-main.component';
import { ReceiptDialogComponent } from './receipt/receipt-dialog/receipt-dialog.component';
import { ReceiptListComponent } from './receipt/receipt-list/receipt-list.component';
import { SidenavComponent } from './common/sidenav/sidenav.component';
import { MatNativeDateModule, MAT_DATE_LOCALE } from '@angular/material/core';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { ReceiptFilterComponent } from './receipt/receipt-filter/receipt-filter.component';
import { CategoryChartComponent } from './chart/category-chart/category-chart.component';
import { ChartMainComponent } from './chart/chart-main/chart-main.component';
import { PeriodChartComponent } from './chart/period-chart/period-chart.component';
import { AppRoutingModule } from '../app-routing.module';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { DinamicChartComponent } from './chart/dinamic-chart/dinamic-chart.component';

@NgModule({
  declarations: [
    NavbarComponent,
    ReceiptMainComponent,
    ReceiptDialogComponent,
    ReceiptListComponent,
    SidenavComponent,
    ReceiptFilterComponent,
    CategoryChartComponent,
    ChartMainComponent,
    PeriodChartComponent,
    LoginComponent,
    RegisterComponent,
    DinamicChartComponent
  ],
  imports: [
    AppRoutingModule,
    CommonModule,
    ReactiveFormsModule,
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
    MatSelectModule,
    MatCardModule,
    MatSidenavModule,
    MatPaginatorModule,
    MatSnackBarModule,
    MatChipsModule,
    MatListModule,
    MatExpansionModule,
    MatTabsModule,
    MatMenuModule,
    NgxChartsModule
  ],
  exports: [
    HttpClientModule,
    NavbarComponent,
    LoginComponent,
    RegisterComponent,
    SidenavComponent,
    ReceiptDialogComponent,
    ReceiptListComponent,
    ReceiptMainComponent,
    PeriodChartComponent,   
    DinamicChartComponent, 
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    MatDividerModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatSelectModule,
    MatCardModule,
    MatSidenavModule,
    MatPaginatorModule,
    MatSnackBarModule,
    MatChipsModule,
    MatListModule,
    MatExpansionModule,
    MatTabsModule,
    MatMenuModule,
    NgxChartsModule
  ],
  entryComponents: [
    ReceiptDialogComponent
  ],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'uk-UA' }
  ]
})
export class ComponentsModule { }
