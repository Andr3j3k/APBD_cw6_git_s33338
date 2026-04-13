using APBD_cw6_git_s33338.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APBD_cw6_git_s33338.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(IRoomService service) : ControllerBase
{
    
}
