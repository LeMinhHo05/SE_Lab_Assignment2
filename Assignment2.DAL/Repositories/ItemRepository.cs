using System;
using System.Collections.Generic;
using System.Data.Entity; // Required for Include, State etc.
using System.Linq;

namespace Assignment2.DAL.Repositories
{
    // Repository for Item entity operations
    public class ItemRepository : IDisposable
    {
        private SE_Assignment2_DBEntities context; // Adjust name if needed

        public ItemRepository()
        {
            context = new SE_Assignment2_DBEntities(); // Adjust name if needed
        }

        // Get all items
        public virtual List<Item> GetAllItems()
        {
            return context.Items.ToList();
        }

        // Get a single item by its ID
        public virtual Item GetItemById(int itemId)
        {
            return context.Items.Find(itemId);
        }

        // Add a new item
        public virtual void AddItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            context.Items.Add(item);
            context.SaveChanges(); // Save changes to the database
        }

        // Update an existing item
        public virtual void UpdateItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            // Attach the item to the context and mark it as modified
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges(); // Save changes
        }

        // Delete an item by its ID
        public virtual void DeleteItem(int itemId)
        {
            Item itemToDelete = context.Items.Find(itemId);
            if (itemToDelete != null)
            {
                context.Items.Remove(itemToDelete);
                context.SaveChanges(); // Save changes
            }
            // Optional: else throw an exception or return a status
        }


        // Implement IDisposable
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public List<object> GetBestSellingItems(int topCount = 10) // Get top 10 by default
        {
            var bestSellers = context.OrderDetails
                .GroupBy(od => od.Item) // Group by the related Item entity
                .Select(g => new
                {
                    ItemID = g.Key.ItemID,
                    ItemName = g.Key.ItemName,
                    TotalQuantity = g.Sum(od => od.Quantity) // Sum quantity for each item
                })
                .OrderByDescending(result => result.TotalQuantity) // Order by most sold
                .Take(topCount) // Take the top N items
                .ToList<object>(); // Convert to List<object> for easy DataGridView binding

            return bestSellers;
        }
    }
}