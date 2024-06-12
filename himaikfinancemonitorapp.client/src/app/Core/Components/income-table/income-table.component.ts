import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {IncomeDataService} from "../../Services/income-data.service";

@Component({
  selector: 'app-income-table',
  templateUrl: './income-table.component.html',
  styleUrl: './income-table.component.css'
})
export class IncomeTableComponent {
  incomeData: any[] = [];
  incomeForm: FormGroup;
  pageNumber: number = 1;
  pageSize: number = 10;

  constructor(
    private incomeDataService: IncomeDataService,
    private formBuilder: FormBuilder
  ) {
    this.incomeForm = this.formBuilder.group({
      name: ['', Validators.required],
      nominal: ['', Validators.required],
      transferDate: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.getAllIncomeData();
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

  addIncome() {
    if (this.incomeForm.valid) {
      this.incomeDataService.addIncomeData(this.incomeForm.value)
        .subscribe(response => {
          console.log('Income added:', response);
          this.getAllIncomeData(); // Refresh the table
          this.closeModal(); // Close the modal
        }, error => {
          console.error('Error:', error);
        });
    }
  }



  openModal() {
    const modal = document.getElementById('myModal')
      if (modal) {
        modal.style.display = 'block'
        document.addEventListener('click', (event) => {
          if (event.target === modal) {
            this.closeModal()
          }
        })
      }
  }

  closeModal() {
    const modal = document.getElementById('myModal')
      if (modal) {
        modal.style.display = 'none'
      }
  }


}
