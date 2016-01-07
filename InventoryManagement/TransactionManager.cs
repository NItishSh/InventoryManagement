using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace InventoryManagement
{
    public class TransactionManager
    {
        #region constructor, private members and methods
        InventoryDBDataContext db = null;
        public TransactionManager()
        {
            db = new InventoryDBDataContext();
        }
        private decimal getMRPForInventoryItem(int ItemId)
        {
            var inveontoryItem = db.Inventories.Single(x => x.Id == ItemId);
            return inveontoryItem.MRP;
        }
        #endregion
        
        #region Public Methods
        public void AddTransaction(Transaction transaction)
        {
            if(transaction.SellingPrice < 0)
                transaction.SellingPrice = getMRPForInventoryItem(transaction.InventoryItem);
            if (transaction.SaleDate == null)
                transaction.SaleDate = DateTime.Now;
            db.Transactions.InsertOnSubmit(transaction);
            db.SubmitChanges();
        }   
        public void UpdateTransaction(Transaction transaction)
        {
            Transaction transactionToUpdate = db.Transactions.Single(item => item.Id == transaction.Id);
            transactionToUpdate.Quantity = transaction.Quantity;
            transactionToUpdate.SaleDate = transaction.SaleDate;
            transactionToUpdate.InventoryItem = transaction.InventoryItem;
            if (transaction.SellingPrice < 0)
                transactionToUpdate.SellingPrice = getMRPForInventoryItem(transaction.InventoryItem);
            else
                transactionToUpdate.SellingPrice = transaction.SellingPrice;
            db.SubmitChanges();
        }   
        public void DeleteTransaction(int transactionId)
        {
            var transactionToDelete = db.Transactions.Single(x => x.Id == transactionId);
            db.Transactions.DeleteOnSubmit(transactionToDelete);
            db.SubmitChanges();
        }   
        public List<Transaction> GetAllTransactions()
        {
            return db.Transactions.ToList<Transaction>();
        }
        public Transaction GetTransactionById(int transactionId)
        {
            var transaction = db.Transactions.Single(x => x.Id == transactionId);
            return transaction;
        }
        public List<Transaction> GetTransactionsForInventoryItem(Inventory inventoryItem)
        {
            var transactions = db.Transactions.Where(x => x.InventoryItem == inventoryItem.Id).ToList<Transaction>();
            return transactions;
        }
        public List<Transaction> GetTransactionsOnDate(DateTime date)
        {
            var transactions = db.Transactions.Where(x => x.SaleDate.Date == date.Date).ToList<Transaction>();
            return transactions;
        }
        public List<Transaction> GetTransactionsInDateRange(DateTime fromDate, DateTime toDate)
        {
            var transactions = db.Transactions.Where(x => x.SaleDate.Date >= fromDate.Date && x.SaleDate.Date <= toDate.Date).ToList<Transaction>();
            return transactions;
        }
        #endregion
    }    
}
