using System.Net;
using System.Text.Json;
using BookLibrary.Database;
using BookLibrary.Logic;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetEventsController : ControllerBase
    {
        private readonly GetMeetUpService _eventsService;

        public GetEventsController(GetMeetUpService eventsService)
        {
            _eventsService = eventsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] string location = "Oslo")
        {
            try
            {
                var events = await _eventsService.GetEventsAsync(location);
                return Ok(events);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($" API Error: {ex.Message}");
                return StatusCode(500, $"Event API error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($" JSON Parsing Error: {ex.Message}");
                return StatusCode(500, $"Error parsing event data: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Unknown Error: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}