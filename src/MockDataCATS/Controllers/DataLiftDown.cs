using Microsoft.AspNetCore.Mvc;

namespace MockDataCATS.Controllers;

[ApiController]
[Route("[controller]")]
public class DataLiftDown : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<DataLiftDown> _logger;

    public DataLiftDown(ILogger<DataLiftDown> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] string? filename = null)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            return BadRequest("Filename cannot be null or whitespace.");
        }

        switch (filename)
        {
            case "SaleOffice.csv":
                _logger.LogInformation($"Received filename: {filename}, StatusCode: 200, ExitCode: 0");
                Response.Headers.Append("ExitCode", "0");
                return Ok();
            case "SendCardInfo.csv":
                _logger.LogInformation($"Received filename: {filename}, StatusCode: 200, ExitCode: 999");
                Response.Headers.Append("ExitCode", "0");
                return Ok();
            default:
                _logger.LogWarning($"Received unknown filename: {filename}, StatusCode: 404, ExitCode: 1");
                Response.Headers.Append("ExitCode", "1");
                return NotFound($"File '{filename}' not found.");
        }
    }
}
