using UrlShortening.Models;
using UrlShortening.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace UrlShortening.Controllers;

[ApiController]
[Route("shorten")]
public class UrlController : ControllerBase
{
    private readonly UrlShorteningService _urlShorteningService;


    public UrlController(UrlShorteningService urlShorteningService)
    {
        _urlShorteningService = urlShorteningService;
    }

    [HttpGet]
    [Route("{shortCode}")]
    public async Task<ActionResult<Url>> GetShortCode(string shortCode)
    {
        var url = await _urlShorteningService.GetByShortCode(shortCode);

        if (url == null)
        {
            return NotFound();
        }

        return Ok(url);
    }

    [HttpPost]
    public async Task<ActionResult<Url>> PostAsync(string newUrl)
    {
        try
        {
            Url url = new Url
            {
                ShortCode = GenerateUrlCode(newUrl),
                OriginalUrl = newUrl,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            await _urlShorteningService.Create(url);

            return Ok("201 Created");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    static string GenerateUrlCode(string url)
    {
        return Guid.NewGuid().ToString("N").Substring(0, 6);
    }
}
