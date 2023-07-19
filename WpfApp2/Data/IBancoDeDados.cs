using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public interface IBancoDeDados
    {
        void AddUsuario(Usuario usuario);
        void RemoveUsuario(Usuario usuario);
        void UpdateUsuario(Usuario usuario);
        List<Usuario> GetUsuarios();

        void AddJogo(Jogo jogo);
        void RemoveJogo(Jogo jogo);
        void UpdateJogo(Jogo jogo);
        List<Jogo> GetJogos();

        void AddAluguel(Aluguel aluguel);
        List<Aluguel> GetAlugueis();
    }

}
