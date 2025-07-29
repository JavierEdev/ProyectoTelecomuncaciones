using Microsoft.AspNetCore.Mvc;
using System.IO.Ports;

[ApiController]
[Route("api/[controller]")]
public class LuxController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            using var serial = new SerialPort("COM6", 9600)
            {
                ReadTimeout = 1000
            };

            serial.Open();

            string line = serial.ReadLine().Trim();

            if (float.TryParse(line, out float lux))
            {
                return Ok(new { lux });
            }
            else
            {
                return BadRequest("Dato recibido no es un número válido.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al leer el puerto serial: {ex.Message}");
        }
    }
}
