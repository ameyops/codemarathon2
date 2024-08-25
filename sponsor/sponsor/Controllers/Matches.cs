using Microsoft.AspNetCore.Mvc;
using sponsor.data;

namespace sponsor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly DAO _dao;

        public MatchesController(DAO dao)
        {
            _dao = dao;
        }

        [HttpGet("PaymentDetails")]
        public IActionResult GetMatchesWithPaymentDetails()
        {
            var matches = _dao.GetMatchesWithPaymentDetails();
            return Ok(matches);
        }
    }
}
