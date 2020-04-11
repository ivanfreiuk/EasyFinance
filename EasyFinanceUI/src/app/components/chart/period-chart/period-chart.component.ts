import { Component, OnInit } from '@angular/core';
import { ReceiptService } from 'src/app/services/core/receipt.service';
import { AuthenticationService } from 'src/app/services/user/authentication.service';

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

  constructor(private authSvc: AuthenticationService, private receiptSvc: ReceiptService) {
    this.receiptSvc.getExpensesForPeriod(this.authSvc.currentUserValue.id).subscribe((data: any[]) => this.dataSource = data);
  }

  ngOnInit() {
  }
}

