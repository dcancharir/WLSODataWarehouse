using Application.CommandsQueries.AssociateCQ;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlDomain;

namespace wlsomigratesdbdatawarehouseapi.Controllers;
[Route("api/Associates")]
[ApiController]
public class AssociatesController : ControllerBase {
    private readonly IMediator _mediator;
    public AssociatesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [Route("GetAll")]
    public async Task<IEnumerable<Associate>> GetAll() {
        var result = await _mediator.Send(new GetAssociatesQuery() { quantity = 0 });
        return result;
    }
}
