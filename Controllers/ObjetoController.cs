using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Data;
using Microsoft.AspNetCore.Mvc;

namespace LockAi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ObjetoController : ControllerBase
    {
        private readonly DataContext _context;

        public ObjetoController(DataContext context)
        {
            _context = context;
        }
        // Na inclusão de um novo objeto o status será definido como indisponivel.
        // quem pode incluir Objtos.

    }
}