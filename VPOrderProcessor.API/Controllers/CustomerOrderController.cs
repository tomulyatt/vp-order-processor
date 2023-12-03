using Microsoft.AspNetCore.Mvc;
using VPOrderProcessor.Application.Orders;
using VPOrderProcessor.Contracts.Orders;
using VPOrderProcessor.Domain.Orders;

namespace VPOrderProcessor.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerOrderController : ControllerBase
    {
        
        private readonly ILogger<CustomerOrderController> _logger;
        private readonly IOrderProcessor _orderProcessor;

        public CustomerOrderController(
            ILogger<CustomerOrderController> logger,
            IOrderProcessor orderProcessor
            )
        {
            _logger = logger;
            _orderProcessor = orderProcessor;
        }

        //Depending on entry-point, may also want to consider adding token authorisation

        [HttpPost(Name = "Post")]
        public async Task<IActionResult> PostAsync(CreateOrderRequest request, CancellationToken cancelToken)
        {
            _logger.LogDebug("Handling submission of order with external refernce: [{customerOrderId}]", request.ExternalOrderId);

            Order order = await _orderProcessor.ProcessOrderRequestAsync(request, cancelToken);
            return Ok(order);
        }
    }
}