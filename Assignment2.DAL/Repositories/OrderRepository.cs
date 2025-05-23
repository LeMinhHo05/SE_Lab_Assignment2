using System;
using System.Collections.Generic;
using System.Data.Entity; // For Include, State
using System.Linq;

namespace Assignment2.DAL.Repositories
{
    // Repository for Order and OrderDetail operations
    public class OrderRepository : IDisposable
    {
        private SE_Assignment2_DBEntities context; // Adjust name if needed

        public OrderRepository()
        {
            context = new SE_Assignment2_DBEntities(); // Adjust name if needed
        }

        // Get all orders (potentially with details and agent)
        // Use Include for eager loading related data
        public List<Order> GetAllOrders()
        {
            // Eager load Agent and OrderDetails along with Order
            return context.Orders
                          .Include(o => o.Agent) // Include Agent navigation property
                          .Include(o => o.OrderDetails.Select(od => od.Item)) // Include OrderDetails and their Items
                          .ToList();
        }

        // Get a single order by ID, including details
        public Order GetOrderById(int orderId)
        {
            return context.Orders
                          .Include(o => o.Agent)
                          .Include(o => o.OrderDetails.Select(od => od.Item))
                          .FirstOrDefault(o => o.OrderID == orderId);
        }

        // Add a new Order along with its OrderDetail items
        // Assumes the Order object passed in has its OrderDetails collection populated
        public void AddOrderWithDetails(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            if (order.OrderDetails == null || !order.OrderDetails.Any())
            {
                throw new InvalidOperationException("Order must have at least one detail line.");
            }

            // Add the Order header. EF will track this new entity.
            context.Orders.Add(order);

            // Since the OrderDetails collection is part of the 'order' object,
            // Entity Framework will automatically detect these as new related entities
            // when SaveChanges is called, provided the relationships are set up correctly
            // in the .edmx model (which they should be from DB First).

            context.SaveChanges(); // Saves the Order header and all associated OrderDetails in one transaction
        }

        // Delete an order and its details
        // Requires careful handling of related data
        public void DeleteOrder(int orderId)
        {
            Order orderToDelete = context.Orders
                                         .Include(o => o.OrderDetails) // MUST include details to delete them
                                         .FirstOrDefault(o => o.OrderID == orderId);

            if (orderToDelete != null)
            {
                // Remove detail lines first (or rely on cascade delete if set up in DB/EF model)
                // EF often handles this if the collection is loaded (due to Include)
                // Explicitly removing is safer if cascade delete isn't configured/guaranteed
                context.OrderDetails.RemoveRange(orderToDelete.OrderDetails);

                // Remove the order header
                context.Orders.Remove(orderToDelete);

                context.SaveChanges(); // Saves deletions in one transaction
            }
        }

        // NOTE: Updating Orders with Details can be complex.
        // You might need to fetch the existing order, clear its details,
        // add the new details, and mark the header as modified.
        // We are skipping Update for simplicity in this phase.


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
        public List<object> GetOrderDetailsByAgentId(int agentId)
        {
            var agentOrderDetails = context.OrderDetails
                .Include(od => od.Order) // Include Order to access OrderDate and AgentID
                .Include(od => od.Item)  // Include Item to access ItemName
                .Where(od => od.Order.AgentID == agentId) // Filter by AgentID on the Order
                .Select(od => new
                {
                    od.Order.OrderDate,
                    od.Item.ItemName,
                    od.Quantity,
                    od.UnitAmount,
                    LineTotal = od.Quantity * od.UnitAmount // Calculate line total
                })
                .OrderByDescending(result => result.OrderDate) // Show most recent first
                .ToList<object>();

            return agentOrderDetails;
        }
    }
}
