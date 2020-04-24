import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/services/user/authentication.service';
import { ReceiptFilterCriteria } from 'src/app/models/receipt-filter-criteria';
import { CategoryService } from 'src/app/services/core/category.service';
import { Category } from 'src/app/models';


@Component({
  selector: 'app-receipt-filter',
  templateUrl: './receipt-filter.component.html',
  styleUrls: ['./receipt-filter.component.css']
})
export class ReceiptFilterComponent implements OnInit {

  @Output() filterCriteriaChanged: EventEmitter<ReceiptFilterCriteria> = new EventEmitter<ReceiptFilterCriteria>();

  categorySource: Array<Category>;
  filterForm: FormGroup;
  filterCriteria: ReceiptFilterCriteria;
 
  constructor(private categorySvc: CategoryService,
    private authService: AuthenticationService,
     private formBuilder: FormBuilder) { 
      this.categorySvc.getAll().subscribe(data=>{
        this.categorySource = data;
      });

      this.filterCriteria = new ReceiptFilterCriteria();
      this.filterCriteria.userId = this.authService.currentUserValue.id;
      this.initializeFormGoup(this.filterCriteria);
    }

  ngOnInit() {
  }

  initializeFormGoup(filterCriteria: ReceiptFilterCriteria) {
    this.filterForm = this.formBuilder.group({
      dateFrom: [filterCriteria.dateFrom],
      dateTo: [filterCriteria.dateTo],
      totalFrom:[filterCriteria.totalFrom, [Validators.min(0), Validators.max(Number.MAX_VALUE)]],
      totalTo: [filterCriteria.totalTo, [Validators.min(0), Validators.max(Number.MAX_VALUE)]],
      category: [filterCriteria.categoryId]
    });
  }

  onSubmit() {
    this.populateFilterCriteria();
    this.filterCriteriaChanged.emit(this.filterCriteria);
  }

  private populateFilterCriteria() {
    const filterCriteriaValue = this.filterForm.value;
    this.filterCriteria.dateFrom = filterCriteriaValue.dateFrom;
    this.filterCriteria.dateTo = filterCriteriaValue.dateTo;
    this.filterCriteria.totalFrom = filterCriteriaValue.totalFrom;
    this.filterCriteria.totalTo = filterCriteriaValue.totalTo;
    this.filterCriteria.categoryId = filterCriteriaValue.categoryId;
  }

}
