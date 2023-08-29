CREATE TABLE TransactionData (
    Id bigint Identity(1,1) PRIMARY KEY,
    TransactionId nvarchar(50) NOT NULL,
    Amount decimal(26,9) NOT NULL,
    CurrencyCode varchar(3) NOT NULL,
    TransactionDatetime datetime2  NOT NULL,
    [Status] char(1)  NOT NULL
);