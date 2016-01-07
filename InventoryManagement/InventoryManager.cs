using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryManagement
{
    public class InventoryManager
    {
        #region constructor, private members and methods
        InventoryDBDataContext db = null;
        public InventoryManager()
        {
            db = new InventoryDBDataContext();
        }
        #endregion

        #region Public Methods
        public void AddItem(Inventory item)
        {
            db.Inventories.InsertOnSubmit(item);
            db.SubmitChanges();
        }
        public void DeleteItem(int ItemID)
        {
            var itemInInventoty = db.Inventories.Single(item => item.Id == ItemID);
            db.Inventories.DeleteOnSubmit(itemInInventoty);
            db.SubmitChanges();
        }
        public Inventory GetItem(int ItemID)
        {
            var itemInInventoty = db.Inventories.Single(item => item.Id == ItemID);
            return itemInInventoty;
        }
        public List<Inventory> GetAllItems()
        {
            return db.Inventories.ToList<Inventory>();
        }
        public List<Inventory> GetAllItemsByName(string name)
        {
            var itemsInInventoty = from item in db.Inventories
                                   where item.Name.Contains(name)
                                   select item;
            //db.Inventories.Where(item => item.Name.Contains(name));
            return itemsInInventoty.ToList<Inventory>();
        }
        public void UpdateItem(Inventory itemToUpdate)
        {
            var itemInInventoty = db.Inventories.Single(item => item.Id == itemToUpdate.Id);
            itemInInventoty.Name = itemToUpdate.Name;
            itemInInventoty.Manufacturer = itemToUpdate.Manufacturer;
            itemInInventoty.MRP = itemToUpdate.MRP;
            itemInInventoty.ExpiryDate = itemToUpdate.ExpiryDate;
            itemInInventoty.Description = itemToUpdate.Description;
            itemInInventoty.CostPrice = itemToUpdate.CostPrice;
            itemInInventoty.Quantity = itemToUpdate.Quantity;
            db.SubmitChanges();
        }
        #endregion
    }
}
