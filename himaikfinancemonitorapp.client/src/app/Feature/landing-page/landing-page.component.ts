import { Component } from '@angular/core';
import { TransactionService } from '../../Core/Services/transaction.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.css'
})
export class LandingPageComponent {
  latestBalance: number = 0
  totalIncome: number = 0
  totalOutcome: number = 0

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

        this.latestBalance = data[data.length - 1].balance;

        data.forEach(transaction => {
          this.totalIncome += transaction.credit;
          this.totalOutcome += transaction.debit;
        })
      }, error => {
        console.error('Error:', error);
      });
  }
}
