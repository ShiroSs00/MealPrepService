using MealPrepService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPrepService.BLL.Services
{
    public class OrderService
    {
        private readonly MealPrepDbContext _context;

        public OrderService(MealPrepDbContext context)
        {
            _context = context;
        }

        public Order CreateOrder(int userId, int nutritionProfileId, int? subscriptionPlanId, decimal totalAmount, string? menuJson = "[]", string? menuPdfPath = "")
        {
            var order = new Order
            {
                UserId = userId,
                NutritionProfileId = nutritionProfileId,
                SubscriptionPlanId = subscriptionPlanId,
                TotalAmount = totalAmount,
                WeekNumber = GetCurrentIsoWeekOfYear(),
                OrderCode = GenerateOrderCode(),
                Status = "Pending",
                MenuJsonData = menuJson ?? "[]",
                MenuPdfPath = menuPdfPath ?? "",
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Find(orderId);
        }

        public IEnumerable<Order> GetOrdersByUser(int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        // Mock payment — updates order as paid with a fake transaction id
        public Order MockPayment(int orderId, string paymentMethod)
        {
            var order = _context.Orders.Find(orderId) ?? throw new Exception("Order not found");

            if (order.Status == "Paid")
                return order;

            order.PaymentMethod = paymentMethod;
            order.PaymentTransactionId = $"MOCK-{Guid.NewGuid():N}";
            order.PaidAt = DateTime.UtcNow;
            order.Status = "Paid";
            order.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
            return order;
        }

        // QR text is simply the OrderCode
        public string GetQrTextForOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId) ?? throw new Exception("Order not found");
            return order.OrderCode;
        }

        private string GenerateOrderCode()
        {
            // Format: MEAL-ORDER-20260126-<6chars>
            var suffix = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpperInvariant();
            return $"MEAL-ORDER-{DateTime.UtcNow:yyyyMMddHHmmss}-{suffix}";
        }

        private int GetCurrentIsoWeekOfYear()
        {
            var ci = CultureInfo.InvariantCulture;
            var cal = ci.Calendar;
            return cal.GetWeekOfYear(DateTime.UtcNow, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
