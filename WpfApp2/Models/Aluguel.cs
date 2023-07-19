using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Aluguel : ICloneable, INotifyPropertyChanged
    {
        private int id;
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

        public Usuario Usuario
        {
            get { return usuario; }
            set
            {
                if (usuario != value)
                {
                    usuario = value;
                    OnPropertyChanged(nameof(Usuario));
                }
            }
        }

        public Jogo Jogo
        {
            get { return jogo; }
            set
            {
                if (jogo != value)
                {
                    jogo = value;
                    OnPropertyChanged(nameof(Jogo));
                }
            }
        }

        public DateTime DataAluguel
        {
            get { return dataAluguel; }
            set
            {
                if (dataAluguel != value)
                {
                    dataAluguel = value;
                    OnPropertyChanged(nameof(DataAluguel));
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





