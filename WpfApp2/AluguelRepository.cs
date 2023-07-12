using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class AluguelRepository
    {
        private readonly IBancoDeDados bancoDeDados;

        public AluguelRepository(IBancoDeDados bancoDeDados)
        {
            this.bancoDeDados = bancoDeDados;
        }

        public void Add(Aluguel aluguel)
        {
            bancoDeDados.AddAluguel(aluguel);
        }

        public List<Aluguel> GetAll()
        {
            return bancoDeDados.GetAlugueis();
        }
    }
}
