using LLama.Common;
using LLama.WebAPI.Models;
using LLama.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;


namespace LLama.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;

        public ChatController(ILogger<ChatController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Send")]
        public Task<string> SendMessage([FromBody] SendMessageInput input, [FromServices] StatefulChatService _service)
        {
            return _service.Send(input);
        }

        [HttpPost("Send/Stream")]
        public async Task SendMessageStream([FromBody] SendMessageInput input, [FromServices] StatefulChatService _service, CancellationToken cancellationToken)
        {

            Response.ContentType = "text/event-stream";

            await foreach (var r in _service.SendStream(input))
            {
                await Response.WriteAsync("data:" + r + "\n\n", cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);
            }

            await Response.CompleteAsync();
        }

        [HttpPost("History")]
        public async Task<string> SendHistory([FromBody] HistoryInput input, [FromServices] StatelessChatService _service)
        {
            var history = new ChatHistory();

            var messages = input.Messages.Select(m => new ChatHistory.Message(Enum.Parse<AuthorRole>(m.Role), m.Content));

            history.Messages.AddRange(messages);

            return await _service.SendAsync(history);
        }
    }
}


[Route("api/[controller]")]
[ApiController]
public class CodeExecutionController : ControllerBase
{
    [HttpPost("execute")]
    public async Task<IActionResult> ExecutePythonCode([FromBody] CodeExecutionRequest request)
    {
        if (request.Language.ToLower() != "python")
        {
            return BadRequest("Unsupported language");
        }

        var randomFileName = Path.GetRandomFileName().Replace(".", "") + ".py";
        var filePath = Path.Combine(Path.GetTempPath(), randomFileName);

        await System.IO.File.WriteAllTextAsync(filePath, request.Code);

        var outputDir = Path.GetTempPath();
        var command = $"docker run --rm -v {filePath}:/usr/src/app/script.py -v {outputDir}:/usr/src/app/output python-executor python /usr/src/app/script.py";

        var startInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/c {command}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = startInfo };
        process.Start();

        var output = await process.StandardOutput.ReadToEndAsync();
        var error = await process.StandardError.ReadToEndAsync();

        process.WaitForExit();

        System.IO.File.Delete(filePath);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        var imagePath = Path.Combine(outputDir, "quantum_circuit.png");
        if (System.IO.File.Exists(imagePath))
        {
            var imageBytes = await System.IO.File.ReadAllBytesAsync(imagePath);
            System.IO.File.Delete(imagePath);
            return File(imageBytes, "image/png");
        }

        return Ok(output);
    }
}

