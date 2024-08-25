using Microsoft.AspNetCore.Mvc;
using sponsor.data;
using sponsor.Model;

namespace sponsor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SponsorsController : ControllerBase
    {
        private readonly DAO _dao;

        public SponsorsController(DAO dao)
        {
            _dao = dao;
        }

        [HttpGet("PaymentDetails")]
        public IActionResult GetSponsorsWithPaymentDetails()
        {
            var sponsors = _dao.GetSponsorsWithPaymentDetails();
            return Ok(sponsors);
        }

        [HttpGet("ByYear")]
        public IActionResult GetSponsorsWithMatchCountByYear([FromQuery] int year)
        {
            var sponsors = _dao.GetSponsorsWithMatchCountByYear(year);
            return Ok(sponsors);
        }
    }
}
