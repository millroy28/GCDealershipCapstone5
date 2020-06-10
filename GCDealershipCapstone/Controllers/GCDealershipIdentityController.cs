using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GCDealershipCapstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GCDealershipCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GCDealershipIdentityController : ControllerBase
    {
        private readonly GCDealershipIdentityContext _context;

        public GCDealershipIdentityController(GCDealershipIdentityContext context)
        {
            _context = context;
        }
    }
}
