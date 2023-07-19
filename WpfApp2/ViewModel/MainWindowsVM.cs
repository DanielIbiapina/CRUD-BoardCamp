using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp2
{
    public class MainWindowsVM : INotifyPropertyChanged
    {
        public ObservableCollection<Usuario> ListaUsuarios { get; set; }
        public ObservableCollection<Jogo> ListaJogos { get; set; }
        public ObservableCollection<Aluguel> ListaAlugueis { get; set; }

        public ICommand AddUsuarioCommand { get; private set; }
        public ICommand RemoveUsuarioCommand { get; private set; }
        public ICommand EditarUsuarioCommand { get; private set; }
        public ICommand AddJogoCommand { get; private set; }
        public ICommand RemoveJogoCommand { get; private set; }
        public ICommand EditarJogoCommand { get; private set; }
        public ICommand RealizarAluguelCommand { get; private set; }

        public Usuario UsuarioSelecionado { get; set; }
        public Jogo JogoSelecionado { get; set; }
        public Aluguel AluguelSelecionado { get; set; }
        public IBancoDeDados bancoDeDados { get; set; }

        //private readonly IBancoDeDados bancoDeDados;

        public MainWindowsVM()
        {
            bancoDeDados = new PostgresBancoDeDados();

            InicializarComandos();
            CarregarDados();
        }

        private void InicializarComandos()
        {
            AddUsuarioCommand = new RelayCommand((object _) =>
            {
                Usuario novoUsuario = new Usuario();
                CadastroUsuario tela = new CadastroUsuario();
                tela.DataContext = novoUsuario;
                tela.ShowDialog();

                if (tela.DialogResult.HasValue && tela.DialogResult.Value)
                {
                    if (ValidarUsuario(novoUsuario))
                    {
                        bancoDeDados.AddUsuario(novoUsuario);
                        CarregarUsuarios();
                    }
                    else
                    {
                        MensagensErro.PreenchaTodosCampos();
                    }
                }
            });

            RemoveUsuarioCommand = new RelayCommand((object _) =>
            {
                if (UsuarioSelecionado != null)
                {
                    bancoDeDados.RemoveUsuario(UsuarioSelecionado);
                    CarregarUsuarios();
                }
                
                
            });

            EditarUsuarioCommand = new RelayCommand(_ =>
            {
                if (UsuarioSelecionado != null)
                {
                    Usuario usuarioEditado = (Usuario)UsuarioSelecionado.Clone();
                    CadastroUsuario tela = new CadastroUsuario();
                    tela.DataContext = usuarioEditado;
                    tela.ShowDialog();

                    if (tela.DialogResult.HasValue && tela.DialogResult.Value)
                    {
                        if (ValidarUsuario(usuarioEditado))
                        {
                            bancoDeDados.UpdateUsuario(usuarioEditado);
                            CarregarUsuarios();
                        }
                        else
                        {
                            MensagensErro.PreenchaTodosCampos();
                        }
                    }
                }
            });

            AddJogoCommand = new RelayCommand((object _) =>
            {
                Jogo novoJogo = new Jogo();
                CadastroJogo tela = new CadastroJogo();
                tela.DataContext = novoJogo;
                tela.ShowDialog();

                if (tela.DialogResult.HasValue && tela.DialogResult.Value)
                {
                    if (ValidarJogo(novoJogo))
                    {
                        bancoDeDados.AddJogo(novoJogo);
                        CarregarJogos();
                    }
                    else
                    {
                        MensagensErro.PreenchaTodosCampos();
                    }
                }
            });

            RemoveJogoCommand = new RelayCommand((object _) =>
            {
                if (JogoSelecionado != null)
                {
                    bancoDeDados.RemoveJogo(JogoSelecionado);
                    CarregarJogos();
                }
            });

            EditarJogoCommand = new RelayCommand((object _) =>
            {
                if (JogoSelecionado != null)
                {
                    Jogo jogoEditado = (Jogo)JogoSelecionado.Clone();
                    CadastroJogo tela = new CadastroJogo();
                    tela.DataContext = jogoEditado;
                    tela.ShowDialog();

                    if (tela.DialogResult.HasValue && tela.DialogResult.Value)
                    {
                        if (ValidarJogo(jogoEditado))
                        {
                            bancoDeDados.UpdateJogo(jogoEditado);
                            CarregarJogos();
                        }
                        else
                        {
                            MensagensErro.PreenchaTodosCampos();
                        }
                    }
                }
            });

            RealizarAluguelCommand = new RelayCommand(_ =>
            {
                if (UsuarioSelecionado != null && JogoSelecionado != null)
                {
                    if (JogoSelecionado.QuantidadeDisponivel < 1)
                    {
                        MensagensErro.JogosEsgotados();
                    }
                    else
                    {
                        Aluguel novoAluguel = new Aluguel
                        {
                            Usuario = UsuarioSelecionado,
                            Jogo = JogoSelecionado,
                            DataAluguel = DateTime.Now
                        };

                        bancoDeDados.AddAluguel(novoAluguel);
                        CarregarAlugueis();

                        JogoSelecionado.QuantidadeDisponivel--;
                        bancoDeDados.UpdateJogo(JogoSelecionado);
                        CarregarJogos();
                    }
                }
            });
        }

        public bool ValidarUsuario(Usuario usuario)
        {
            return !string.IsNullOrEmpty(usuario.Nome) && !string.IsNullOrEmpty(usuario.Email) && !string.IsNullOrEmpty(usuario.Senha);
        }

        public bool ValidarJogo(Jogo jogo)
        {
            return !string.IsNullOrEmpty(jogo.NomeDoJogo) && !string.IsNullOrEmpty(jogo.Categoria) && jogo.QuantidadeDisponivel >= 0;
        }

        public void CarregarDados()
        {
            CarregarUsuarios();
            CarregarJogos();
            CarregarAlugueis();
        }

        public void CarregarUsuarios()
        {
            ListaUsuarios = new ObservableCollection<Usuario>(bancoDeDados.GetUsuarios());
            OnPropertyChanged(nameof(ListaUsuarios));
        }


        public void CarregarJogos()
        {
            ListaJogos = new ObservableCollection<Jogo>(bancoDeDados.GetJogos());
            OnPropertyChanged(nameof(ListaJogos));
        }

        public void CarregarAlugueis()
        {
            ListaAlugueis = new ObservableCollection<Aluguel>(bancoDeDados.GetAlugueis());
            OnPropertyChanged(nameof(ListaAlugueis));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }


}

//git hub desktop
//vs e vscode
//wsl
//docker
//wiseclone
//verificar MVVM, modificar funçao edit
