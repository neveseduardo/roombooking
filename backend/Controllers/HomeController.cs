using Microsoft.AspNetCore.Mvc;

[Route("/")]
[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        var api = new {
            name = "Api RoomBooking",
            version = "1.0.0",
            docs = "https://localhost:44392/swagger",
        };
        return Ok(api);
    }
}
