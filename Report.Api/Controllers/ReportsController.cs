using MediatR;
using Microsoft.AspNetCore.Mvc;
using Report.Application.Commands.CreateNewReport;

namespace Report.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ReportsController> _logger;

    public ReportsController(ILogger<ReportsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateNewReportCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}