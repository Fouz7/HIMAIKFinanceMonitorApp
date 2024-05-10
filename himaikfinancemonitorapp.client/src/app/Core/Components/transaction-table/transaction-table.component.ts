import { Component } from '@angular/core';
import {TransactionService} from "../../Services/transaction.service";

@Component({
  selector: 'app-transaction-table',
  templateUrl: './transaction-table.component.html',
  styleUrl: './transaction-table.component.css'
})
export class TransactionTableComponent {
  transactionData: any[] = [];

  constructor(
    private transactionService: TransactionService
  ) { }

  ngOnInit() {
    this.getAllTransaction();
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

}
