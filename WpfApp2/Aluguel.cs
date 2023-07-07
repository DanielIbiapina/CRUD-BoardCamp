using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Aluguel : ICloneable
    {
        private Usuario usuario;
        private Jogo jogo;
        private DateTime dataAluguel;
        public Aluguel()
        {

        }
        public Aluguel(Usuario usuario, Jogo jogo, DateTime dataAluguel)
        {
            this.usuario = usuario;
            this.jogo = jogo;
            this.dataAluguel = dataAluguel;
        }

        public Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
        public Jogo Jogo
        {
            get { return jogo; }
            set { jogo = value; }
        }
        public DateTime DataAluguel
        {
            get { return dataAluguel; }
            set { dataAluguel = value; }
        }




        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}





