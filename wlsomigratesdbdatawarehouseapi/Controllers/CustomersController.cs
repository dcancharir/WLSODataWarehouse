using Application.CommandsQueries.CustomerCQ;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlDomain;

namespace wlsomigratesdbdatawarehouseapi.Controllers;
[Route("api/Customers")]
[ApiController]
public class CustomersController : ControllerBase {
    private readonly IMediator _mediator;
    public CustomersController(IMediator mediator) {
        _mediator = mediator;
    }
    [HttpGet]
    [Route("GetAllPaginated/{page}/{pageSize}")]
    public async Task<IEnumerable<Customer>>GetAllPaginated(int page, int pageSize) {
        var result = await _mediator.Send(new GetListCustomersQuery() { page = page, pageSize = pageSize });
        return result;
    }
}
