import { Component, ViewEncapsulation } from '@angular/core';
import { IncomeDataService } from '../../Services/income-data.service';
import { TransactionService } from '../../Services/transaction.service';

@Component({
  selector: 'app-tab-view',
  templateUrl: './tab-view.component.html',
  styleUrl: './tab-view.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class TabViewComponent {
  incomeData: any[] = [];
  transactionData: any[] = [];
  selectedTab = 0
  pageNumber: number = 1;
  pageSize: number = 10;

  constructor(
    private incomeDataService: IncomeDataService,
    private transactionService: TransactionService
  ) { }

  ngOnInit() {
    this.getAllIncomeData();
    this.getAllTransactionPaginated();
  }

  getAllIncomeData() {
    this.incomeDataService.getAllIncomeData('asc', this.pageNumber, this.pageSize)
      .subscribe(data => {
        console.log('DataIncome:', data);
        this.incomeData = data;
      }, error => {
        console.error('Error:', error);
      });
  }

  getAllTransactionPaginated() {
    this.transactionService.getAllTransactionPaginated(this.pageNumber, this.pageSize)
      .subscribe(data => {
        console.log('DataTransaction:', data);
        this.transactionData = data;
      }, error => {
        console.error('Error:', error);
      });
  }

  selectTab(index: number) {
    this.selectedTab = index
  }

  nextPage() {
    this.pageNumber++;
    this.getAllIncomeData();
  }

  prevPage() {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.getAllIncomeData();
    }
  }

}
