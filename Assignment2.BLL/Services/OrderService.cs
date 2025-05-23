using Assignment2.DAL; // Access Order, OrderDetail etc.
using Assignment2.DAL.Repositories; // Access Repositories
using System;
using System.Collections.Generic;
using System.Linq; // For Any()

namespace Assignment2.BLL.Services
{
    // Service layer for Order operations
    public class OrderService : IDisposable
    {
        // Need repositories for Orders, and potentially Agents/Items for validation/lookup
        private OrderRepository _orderRepository;
        private AgentRepository _agentRepository; // Needed to validate AgentID
        private ItemRepository _itemRepository;   // Needed to validate ItemID

        public OrderService()
        {
            _orderRepository = new OrderRepository();
            _agentRepository = new AgentRepository();
            _itemRepository = new ItemRepository();
        }

        // Get all orders
        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        // Get order by ID
        public Order GetOrderById(int orderId)
        {
            if (orderId <= 0) return null;
            return _orderRepository.GetOrderById(orderId);
        }


        // Add a new order with details
        public bool AddOrder(DateTime orderDate, int agentId, List<OrderDetail> details)
        {
            // --- Validation ---
            if (agentId <= 0 || details == null || !details.Any())
            {
                return false; // Basic validation
            }

            // Validate Agent exists
            if (_agentRepository.GetAgentById(agentId) == null)
            {
                return false; // Invalid Agent ID
            }

            // Validate all items in details exist and quantities/amounts are valid
            foreach (var detail in details)
            {
                if (detail.ItemID <= 0 || _itemRepository.GetItemById(detail.ItemID) == null)
                {
                    return false; // Invalid Item ID
                }
                if (detail.Quantity <= 0 || detail.UnitAmount < 0)
                {
                    return false; // Invalid quantity or amount
                }
                // NOTE: UnitAmount here is what's *recorded* in the order,
                // it doesn't necessarily have to match the current item price in Item table.
            }
            // --- End Validation ---


            // Create the Order header object
            Order newOrder = new Order
            {
                OrderDate = orderDate,
                AgentID = agentId,
                OrderDetails = details // Assign the validated details list
            };

            try
            {
                _orderRepository.AddOrderWithDetails(newOrder);
                return true; // Success
            }
            catch (Exception ex)
            {
                // Log exception ex
                return false; // Failure
            }
        }

        // Delete an order
        public bool DeleteOrder(int orderId)
        {
            if (orderId <= 0)
            {
                return false;
            }
            try
            {
                _orderRepository.DeleteOrder(orderId);
                return true;
            }
            catch (Exception ex)
            {
                // Log ex
                return false;
            }
        }

        // Implement IDisposable
        public void Dispose()
        {
            _orderRepository?.Dispose();
            _agentRepository?.Dispose();
            _itemRepository?.Dispose();
        }
        public List<object> GetOrderDetailsForAgent(int agentId)
        {
            if (agentId <= 0)
            {
                return new List<object>(); // Return empty list for invalid ID
            }
            // Can add business logic/caching here later if needed
            return _orderRepository.GetOrderDetailsByAgentId(agentId);
        }
        public List<object> GetAllOrdersBasicInfo()
        {
            // Use the existing GetAllOrders which already includes Agent
            // Then select only the needed fields for the list view
            return _orderRepository.GetAllOrders() // Assuming GetAllOrders includes Agent
                .Select(o => new {
                    o.OrderID,
                    o.OrderDate,
                    AgentName = o.Agent != null ? o.Agent.AgentName : "N/A" // Handle potential null agent
                })
                .OrderByDescending(o => o.OrderDate) // Show newest first
                .ToList<object>();
        }
    }
}