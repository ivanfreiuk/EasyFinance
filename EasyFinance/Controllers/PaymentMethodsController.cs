using System.Threading.Tasks;
using EasyFinance.BusinessLogic.Interfaces;
using EasyFinance.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EasyFinance.Controllers
{
    [Route("api/paymentmethods")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentService;

        public PaymentMethodsController(IPaymentMethodService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentMethodAsync(int id)
        {
            var paymentMethod = await _paymentService.GetPaymentMethodAsync(id);

            if (paymentMethod == null)
            {
                return NotFound();
            }

            return Ok(paymentMethod);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentMethodsAsync()
        {
            var paymentMethods = await _paymentService.GetPaymentMethodsAsync();

            if (paymentMethods == null)
            {
                return NoContent();
            }

            return Ok(paymentMethods);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            await _paymentService.AddPaymentMethodAsync(paymentMethod);

            return Ok(new { paymentMethod.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentMethodAsync(int id, PaymentMethod paymentMethod)
        {
            var paymentMethodFromDb = await _paymentService.GetPaymentMethodAsync(id);

            if (paymentMethodFromDb == null)
            {
                return BadRequest();
            }

            await _paymentService.UpdatePaymentMethodAsync(paymentMethodFromDb);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethodAsync(int id)
        {
            var paymentMethodFromDb = await _paymentService.GetPaymentMethodAsync(id);

            if (paymentMethodFromDb == null)
            {
                return BadRequest();
            }

            await _paymentService.RemovePaymentMethodAsync(paymentMethodFromDb);

            return Ok();
        }
    }
}