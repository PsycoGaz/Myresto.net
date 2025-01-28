using Microsoft.AspNetCore.Mvc;
using myresto.Models;
using myresto.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myresto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentRepository _paymentRepository;

        public PaymentsController(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            return Ok(await _paymentRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
        {
            if (payment == null)
            {
                return BadRequest();
            }

            await _paymentRepository.AddAsync(payment);
            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            await _paymentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}