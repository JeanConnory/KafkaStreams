using Rabbit.Models.Entities;

namespace Rabbit.Repositories.Interfaces
{
    public interface IAppMensagemRepository
    {
        void SendMensagem(AppMensagem mensagem);
    }
}
