using Rabbit.Models.Entities;

namespace Rabbit.Services.Interfaces
{
    public interface IAppMensagemService
    {
        void SendMensagem(AppMensagem mensagem);
    }
}
