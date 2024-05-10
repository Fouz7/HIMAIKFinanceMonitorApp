import { Component } from '@angular/core';
import {IncomeDataService} from "../../Services/income-data.service";

@Component({
  selector: 'app-income-table',
  templateUrl: './income-table.component.html',
  styleUrl: './income-table.component.css'
})
export class IncomeTableComponent {
  incomeData: any[] = [];

  constructor(
    private incomeDataService: IncomeDataService
  ) { }

  ngOnInit() {
    this.getAllIncomeData();
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

}
