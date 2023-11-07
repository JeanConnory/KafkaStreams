using Microsoft.AspNetCore.Mvc;
using Rabbit.Models.Entities;
using Rabbit.Services.Interfaces;

namespace Rabbit.Publisher.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppMensagensController : ControllerBase
    {
        private readonly IAppMensagemService _service;

        public AppMensagensController(IAppMensagemService service)
        {
            _service = service;
        }

        [HttpPost]
        public void AddMensagem(AppMensagem mensagem)
        {
            _service.SendMensagem(mensagem);
        }
    }
}
