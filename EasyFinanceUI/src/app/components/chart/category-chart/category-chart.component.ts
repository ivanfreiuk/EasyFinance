import { Component, OnInit } from '@angular/core';
import { ReceiptService } from 'src/app/services/core/receipt.service';
import { AuthenticationService } from 'src/app/services/user/authentication.service';

@Component({
  selector: 'app-category-chart',
  templateUrl: './category-chart.component.html',
  styleUrls: ['./category-chart.component.css']
})
export class CategoryChartComponent implements OnInit {
  // options
  gradient: boolean = true;
  showLabels: boolean = true;
  isDoughnut: boolean = false;
  explodeSlices: boolean = true;
  showLegend: boolean = true;
  legendPosition: string = 'right';
  legendTitle: string = 'Категорії';

  dataSource: any[];

  constructor(private authSvc: AuthenticationService, private receiptSvc: ReceiptService) {
    this.receiptSvc.getExpensesByCategories(this.authSvc.currentUserValue.id).subscribe((data: any[]) => {
    this.dataSource = data
    });

  }

  ngOnInit() {
  }
}

