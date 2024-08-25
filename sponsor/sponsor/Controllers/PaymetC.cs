using Microsoft.AspNetCore.Mvc;
using sponsor.data;

using sponsor.Model;

namespace sponsor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly DAO _dao;

        public PaymentsController(DAO dao)
        {
            _dao = dao;
        }

        [HttpPost]
        public IActionResult AddPayment([FromBody] Payment payment)
        {
            bool isValidMatch = _dao.ValidatePaymentForMatch(payment.ContractID); 

            if (!isValidMatch)
            {
                return BadRequest("Match cannot be created as PaymentDate and/or AmountPaid do not exist.");
            }
            _dao.AddPayment(payment);
            return Ok("Payment added successfully.");
        }
    }
}
