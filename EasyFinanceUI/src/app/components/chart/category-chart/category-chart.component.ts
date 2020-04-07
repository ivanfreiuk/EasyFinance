import { Component, OnInit } from '@angular/core';
import { ReceiptService } from 'src/app/services/receipt.service';

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

  constructor(private receiptSvc: ReceiptService) {
    this.receiptSvc.getExpensesByCategories().subscribe((data: any[]) => {
    this.dataSource = data
    });

  }

  ngOnInit() {
  }
}

