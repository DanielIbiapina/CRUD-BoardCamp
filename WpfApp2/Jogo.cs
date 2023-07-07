using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Jogo : ICloneable
    {
        private string nomeDoJogo;
        private string categoria;
        private int quantidadeDisponivel;
        public Jogo()
        {

        }
        public Jogo(string nomeDoJogo, string categoria, int quantidadeDisponivel)
        {
            this.nomeDoJogo = nomeDoJogo;
            this.categoria = categoria;
            this.quantidadeDisponivel = quantidadeDisponivel;
        }

        public string NomeDoJogo
        {
            get { return nomeDoJogo; }
            set { nomeDoJogo = value; }
        }
        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        public int QuantidadeDisponivel
        {
            get { return quantidadeDisponivel; }
            set { quantidadeDisponivel = value; }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

