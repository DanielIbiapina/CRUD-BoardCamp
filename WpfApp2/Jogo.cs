using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WpfApp2
{
    public class Jogo : ICloneable, INotifyPropertyChanged
    {
        private int id;
        private string nomeDoJogo;
        private string categoria;
        private int quantidadeDisponivel;

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public string NomeDoJogo
        {
            get { return nomeDoJogo; }
            set
            {
                if (nomeDoJogo != value)
                {
                    nomeDoJogo = value;
                    OnPropertyChanged(nameof(NomeDoJogo));
                }
            }
        }

        public string Categoria
        {
            get { return categoria; }
            set
            {
                if (categoria != value)
                {
                    categoria = value;
                    OnPropertyChanged(nameof(Categoria));
                }
            }
        }

        public int QuantidadeDisponivel
        {
            get { return quantidadeDisponivel; }
            set
            {
                if (quantidadeDisponivel != value)
                {
                    quantidadeDisponivel = value;
                    OnPropertyChanged(nameof(QuantidadeDisponivel));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

