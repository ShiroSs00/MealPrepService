using MealPrepService.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealPrepService.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly UserService _userService;

        public OrderController(OrderService orderService, UserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        // Create order (POST) - minimal flow: create then redirect to payment page
        [HttpPost]
        public IActionResult Create(int userId, int nutritionProfileId, int? subscriptionPlanId, decimal totalAmount)
        {
            // basic validation
            var user = _userService.GetUserById(userId);
            if (user == null) return NotFound();

            var order = _orderService.CreateOrder(userId, nutritionProfileId, subscriptionPlanId, totalAmount);

            return RedirectToAction(nameof(Payment), new { orderId = order.Id });
        }

        // Show payment mock screen
        [HttpGet]
        public IActionResult Payment(int orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            if (order == null) return NotFound();
            return View(order); // create a simple view that shows Order and a "Pay (mock)" button
        }

        // Process mock payment
        [HttpPost]
        public IActionResult ProcessPayment(int orderId, string paymentMethod = "MockPay")
        {
            var order = _orderService.MockPayment(orderId, paymentMethod);
            // after payment redirect to order details or dashboard
            return RedirectToAction(nameof(Details), new { orderId = order.Id });
        }

        [HttpGet]
        public IActionResult Details(int orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            if (order == null) return NotFound();
            return View(order);
        }

        // Endpoint to get QR text (can be used client-side)
        [HttpGet]
        public IActionResult QrText(int orderId)
        {
            try
            {
                var qr = _orderService.GetQrTextForOrder(orderId);
                return Ok(new { qr });
            }
            catch (System.Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
