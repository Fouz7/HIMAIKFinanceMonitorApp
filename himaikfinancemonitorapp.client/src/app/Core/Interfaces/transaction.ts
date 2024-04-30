export interface Transaction {
  transactionId: number;
  debit: number;
  credit: number;
  balance: number;
  notes: string;
  createdBy: string;
  createdAt: Date;
}
