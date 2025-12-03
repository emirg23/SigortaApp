using Microsoft.AspNetCore.Mvc;
using SigortaApp.Extensions;

namespace SigortaApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtilsController : ControllerBase
    {
        public UtilsController() { }

        [HttpGet]
        public ActionResult Test()
        {
            var number = "12";
            var converted = int.Parse(number);
            number.ToInt();


            List<string> result = new List<string>();

            if(result != null && result.Count > 0)
            {
                // Do something
            }

            if(result.IsNullOrEmpty())
            {
                // Do something
            }
        }
    }
}
