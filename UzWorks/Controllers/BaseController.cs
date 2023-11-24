﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UzWorks.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class BaseController : ControllerBase
{
}
