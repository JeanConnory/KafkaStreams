using Rabbit.Models.Entities;
using Rabbit.Repositories.Interfaces;
using Rabbit.Services.Interfaces;

namespace Rabbit.Services
{
    public class AppMensagemService : IAppMensagemService
    {
        private readonly IAppMensagemRepository _repository;

        public AppMensagemService(IAppMensagemRepository repository)
        {
            _repository = repository;
        }

        public void SendMensagem(AppMensagem mensagem)
        {
            _repository.SendMensagem(mensagem);
        }
    }
}
