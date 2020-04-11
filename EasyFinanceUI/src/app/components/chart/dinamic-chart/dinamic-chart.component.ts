import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/user/authentication.service';
import { ReceiptService } from 'src/app/services/core/receipt.service';

@Component({
  selector: 'app-dinamic-chart',
  templateUrl: './dinamic-chart.component.html',
  styleUrls: ['./dinamic-chart.component.css']
})
export class DinamicChartComponent implements OnInit {
 
  // options
  legend: boolean = true;
  legendTitle: string = 'Витрати користувача'
  showLabels: boolean = true;
  animations: boolean = true;
  xAxis: boolean = true;
  yAxis: boolean = true;
  showYAxisLabel: boolean = true;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = 'Дата витрат';
  yAxisLabel: string = 'Витрачена сума';
  timeline: boolean = true;

  dataSource: any[];

  constructor(private authSvc: AuthenticationService, private receiptSvc: ReceiptService) { 
    this.receiptSvc.getExpensesForPeriod(this.authSvc.currentUserValue.id).subscribe((data: any[]) => {
      this.dataSource = [{
        name: `${this.authSvc.currentUserValue.firstName} ${this.authSvc.currentUserValue.lastName}`,
        series: data
      }];      
    });
  }

  ngOnInit() {
  }
}
