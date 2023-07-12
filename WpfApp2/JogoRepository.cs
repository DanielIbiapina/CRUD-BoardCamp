using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class JogoRepository
    {
        private readonly IBancoDeDados bancoDeDados;

        public JogoRepository(IBancoDeDados bancoDeDados)
        {
            this.bancoDeDados = bancoDeDados;
        }

        public void Add(Jogo jogo)
        {
            bancoDeDados.AddJogo(jogo);
        }

        public void Remove(Jogo jogo)
        {
            bancoDeDados.RemoveJogo(jogo);
        }

        public void Update(Jogo jogo)
        {
            bancoDeDados.UpdateJogo(jogo);
        }

        public List<Jogo> GetAll()
        {
            return bancoDeDados.GetJogos();
        }
    }
}
