using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommMVC.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class OrderDetailController : Controller
    {
        private IOrderDetailService _orderDetailService;
        private IOrderService _orderService;
        private IProductService _productService;

        public OrderDetailController(IOrderDetailService orderDetailService, IProductService productService, IOrderService orderService)
        {
            _orderDetailService = orderDetailService;
            _orderService = orderService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _orderDetailService.GetAllWithRefAsync());
        }

        // Create
        public async Task<IActionResult> Create()
        {
            ViewBag.OrderID = new SelectList(await _orderService.GetAllAsync(), "ID", "ID");
            ViewBag.ProductID = new SelectList(await _productService.GetAllAsync(), "ID", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderDetail orderDetail)
        {
            if (orderDetail != null)
            {
                if (orderDetail.ProductID != null)
                {
                    var products = await _productService.GetByIdAsync(orderDetail.ProductID.Value);

                    if (products.Quantity >= orderDetail.Quantity)
                    {
                        orderDetail.TotalPrice = (products.UnitPrice * orderDetail.Quantity) * (1 + orderDetail.VAT) * (1 - orderDetail.Discount);
                        products.Quantity = products.Quantity - orderDetail.Quantity;

                        orderDetail.UnitPrice = products.UnitPrice;
                        orderDetail.VAT = products.VAT;
                        orderDetail.Discount = products.Discount;

                        var order = await _orderService.GetByIdAsync(orderDetail.OrderID);
                        if (order != null)
                        {
                            order.Freight = orderDetail.TotalPrice;
                            await _orderService.UpdateAsync(order);
                        }

                        await _productService.UpdateAsync(products);
                        await _orderDetailService.CreateAsync(orderDetail);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        throw new Exception($"No stock this product! Stock: {products.Quantity}");
                    }
                }
                else
                {
                    throw new Exception("Product ID cannot be null.");
                }
            }
            else
            {
                throw new Exception($"Data cannot be null.");
            }
        }

        // Read
        public async Task<IActionResult> Details(int id)
        {
            return View(await _orderDetailService.GetByIdWithRefAsync(id));
        }

        // Update
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.OrderID = new SelectList(await _orderService.GetAllAsync(), "ID", "ID");
            ViewBag.ProductID = new SelectList(await _productService.GetAllAsync(), "ID", "Name");

            return View(await _orderDetailService.GetByIdAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderDetail orderDetail)
        {
            if (orderDetail != null)
            {
                if (orderDetail.ProductID.HasValue)
                {
                    var product = await _productService.GetByIdAsync(orderDetail.ProductID.Value);

                    if (product.Quantity >= orderDetail.Quantity)
                    {
                        orderDetail.TotalPrice = (product.UnitPrice * orderDetail.Quantity) * (1 + orderDetail.VAT) * (1 - orderDetail.Discount);
                        product.Quantity = product.Quantity - orderDetail.Quantity;

                        orderDetail.UnitPrice = product.UnitPrice;
                        orderDetail.VAT = product.VAT;
                        orderDetail.Discount = product.Discount;

                        var order = await _orderService.GetByIdAsync(orderDetail.OrderID);
                        if (order != null)
                        {
                            order.Freight = orderDetail.TotalPrice;
                            await _orderService.UpdateAsync(order);
                        }

                        await _productService.UpdateAsync(product);
                        await _orderDetailService.UpdateAsync(orderDetail);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        throw new Exception($"No stock this product! Stock: {product.Quantity}");
                    }
                }
                else
                {
                    throw new Exception("Product ID cannot be null.");
                }
            }
            else
            {
                throw new Exception($"Data cannot be null.");
            }
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var orderDetail = await _orderDetailService.GetByIdAsync(id);
            if (orderDetail != null)
            {
                var product = await _productService.GetByIdAsync(orderDetail.ProductID.Value);

                if (product != null)
                {
                    product.Quantity = product.Quantity + orderDetail.Quantity;
                    await _productService.UpdateAsync(product);
                }

                var order = await _orderService.GetByIdAsync(orderDetail.OrderID);
                if (order != null)
                {
                    order.Freight = orderDetail.TotalPrice;
                    await _orderService.UpdateAsync(order);
                }

                await _orderDetailService.DeleteAsync(id);
                return Json(new { success = true, message = "Delete success." });

            }
            else
            {
                throw new Exception("Order Detail not found.");
            }

            
        }
    }
}
