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
  displayedColumns: string[] = ['name', 'nominal', 'transferDate']
  selectedTab = 0

  constructor(
    private incomeDataService: IncomeDataService,
    private transactionService: TransactionService
  ) { }

  ngOnInit() {
    this.getAllIncomeData();
    this.getAllTransaction();
  }

  getAllIncomeData() {
    this.incomeDataService.getAllIncomeData('asc', '', '')
      .subscribe(data => {
        console.log('DataIncome:', data);
        this.incomeData = data;
      }, error => {
        console.error('Error:', error);
      });
  }

  getAllTransaction() {
    this.transactionService.getAllTransaction()
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
}
