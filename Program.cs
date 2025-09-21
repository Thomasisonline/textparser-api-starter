using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();

[ApiController]
[Route("api/[controller]")]
public class TextParserController : ControllerBase
{
    [HttpPost("parse")]
    public IActionResult Parse([FromBody] ParseTextRequest req)
    {
        var dict = new Dictionary<string, int>();
        if (!string.IsNullOrEmpty(req.Text))
        {
            var words = req.Text.Split(new[] { ' ', '.', ',', ';', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                var key = word.ToLower();
                if (dict.ContainsKey(key))
                    dict[key]++;
                else
                    dict[key] = 1;
            }
        }
        return Ok(new { WordCounts = dict, UniqueWords = dict.Count });
    }
}

public class ParseTextRequest
{
    public string Text { get; set; }
}
