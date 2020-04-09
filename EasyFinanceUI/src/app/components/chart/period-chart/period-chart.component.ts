import { Component, OnInit } from '@angular/core';
import { ReceiptService } from 'src/app/services/core/receipt.service';

@Component({
  selector: 'app-period-chart',
  templateUrl: './period-chart.component.html',
  styleUrls: ['./period-chart.component.css']
})
export class PeriodChartComponent implements OnInit {
  // options
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  legendTitle = 'Дата витрат'
  showDataLabel = true;
  showXAxisLabel = true;
  xAxisLabel = 'Дата витрат';
  showYAxisLabel = true;
  yAxisLabel = 'Витрачена сума';

  dataSource: any[];

  constructor(private receiptSvc: ReceiptService) {
    this.receiptSvc.getExpensesByAllPeriod().subscribe((data: any[]) => this.dataSource = data);
  }

  ngOnInit() {
  }
}

