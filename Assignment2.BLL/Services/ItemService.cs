using Assignment2.DAL; // Access Item entity
using Assignment2.DAL.Repositories; // Access ItemRepository
using System;
using System.Collections.Generic;

namespace Assignment2.BLL.Services
{
    // Service layer for Item operations
    public class ItemService : IDisposable
    {
        private readonly ItemRepository _itemRepository;

        public ItemService(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        }
        public ItemService() : this(new ItemRepository())
        {
            
        }

        // Get all items - directly pass through for now, add validation later if needed
        public List<Item> GetAllItems()
        {
            return _itemRepository.GetAllItems();
        }

        // Get item by ID
        public Item GetItemById(int itemId)
        {
            // Basic validation
            if (itemId <= 0) return null;
            return _itemRepository.GetItemById(itemId);
        }

        // Add a new item - Add business rules here if needed
        public bool AddItem(string itemName, string size)
        {
            // Example Validation: Item name cannot be empty
            if (string.IsNullOrWhiteSpace(itemName))
            {
                return false; // Indicate failure
            }

            Item newItem = new Item
            {
                ItemName = itemName,
                Size = size // Size can be null/empty based on DB schema
            };

            try
            {
                _itemRepository.AddItem(newItem);
                return true; // Indicate success
            }
            catch (Exception ex)
            {
                // Log exception ex here in a real application
                return false; // Indicate failure
            }
        }

        // Update an existing item
        public bool UpdateItem(int itemId, string itemName, string size)
        {
            // Validation
            if (itemId <= 0 || string.IsNullOrWhiteSpace(itemName))
            {
                return false;
            }

            Item itemToUpdate = _itemRepository.GetItemById(itemId);
            if (itemToUpdate == null)
            {
                return false; // Item not found
            }

            itemToUpdate.ItemName = itemName;
            itemToUpdate.Size = size;

            try
            {
                _itemRepository.UpdateItem(itemToUpdate);
                return true; // Success
            }
            catch (Exception ex)
            {
                // Log exception ex
                return false; // Failure
            }
        }

        // Delete an item
        public bool DeleteItem(int itemId)
        {
            if (itemId <= 0)
            {
                return false;
            }

            try
            {
                // Optional: Add checks here if item is part of an order before deleting
                _itemRepository.DeleteItem(itemId);
                return true; // Success
            }
            catch (Exception ex)
            {
                // Log exception ex
                // Could fail due to Foreign Key constraints if item is in OrderDetail
                return false; // Failure
            }
        }


        // Implement IDisposable
        public void Dispose()
        {
            _itemRepository?.Dispose();
        }
        public List<object> GetBestSellingItems(int topCount = 10)
        {
            // Can add business logic/caching here later if needed
            return _itemRepository.GetBestSellingItems(topCount);
        }
        
        
    }
}
