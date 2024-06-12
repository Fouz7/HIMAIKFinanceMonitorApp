import { Component } from '@angular/core';
import {TransactionService} from "../../Services/transaction.service";

@Component({
  selector: 'app-transaction-table',
  templateUrl: './transaction-table.component.html',
  styleUrl: './transaction-table.component.css'
})
export class TransactionTableComponent {
  transactionData: any[] = [];
  pageNumber: number = 1;
  pageSize: number = 10;

  constructor(
    private transactionService: TransactionService
  ) { }

  ngOnInit() {
    this.getAllTransactionPaginated();
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

}
