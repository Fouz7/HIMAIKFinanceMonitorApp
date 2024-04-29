CREATE TABLE FinanceSchema.IncomeData(
	id INT IDENTITY(1,1) PRIMARY KEY,
	name VARCHAR(255),
	nominal DECIMAL(10, 2),
	transfer_date DATE,
	createdby VARCHAR(255),
	createdAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE FinanceSchema.Transactions(
  transactionId INT IDENTITY(1,1) PRIMARY KEY,
  debit DECIMAL(10, 2),
  credit DECIMAL(10, 2),
  balance DECIMAL(10, 2),
  notes TEXT,
  createdBy VARCHAR(255),
  createdAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE FinanceSchema.Users(
  userId INT IDENTITY(1,1) PRIMARY KEY,
  username VARCHAR(255),
  fullname VARCHAR(255),
  NIM VARCHAR(255),
  pic VARCHAR(255),
  password VARCHAR(255),
  role VARCHAR(255),
  createdAt DATETIME DEFAULT GETDATE()
);
