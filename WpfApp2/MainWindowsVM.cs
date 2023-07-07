using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp2
{
    public class MainWindowsVM
    {
        public ObservableCollection<Usuario> listaUsuarios { get; set; }
        public ObservableCollection<Jogo> listaJogos { get; set; }
        public ObservableCollection<Aluguel> listaAlugueis { get; set; }

        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Edita { get; private set; }
        public ICommand AddJogo { get; private set; }
        public ICommand RemoveJogo { get; private set; }
        public ICommand EditaJogo { get; private set; }
        public ICommand AddAluguel { get; private set; }
        public ICommand FinalizaAluguel { get; private set; }
        public Usuario UsuarioSelecionado { get; set; }
        public Jogo JogoSelecionado { get; set; }
        public Aluguel AluguelSelecionado { get; set; }
        public MainWindowsVM() { 
            listaUsuarios = new ObservableCollection<Usuario>()
            {
                new Usuario()
                {
                    Nome = "Daniel",
                    Email = "dandan@email.com",
                    Senha = "1234" 

                }
            };

            listaJogos = new ObservableCollection<Jogo>()
            {
                new Jogo()
                {
                    NomeDoJogo = "Jogo 1",
                    Categoria = "Estratégia",
                    QuantidadeDisponivel = 5
                },
                new Jogo()
                {
                    NomeDoJogo = "Jogo 2",
                    Categoria = "Blefe",
                    QuantidadeDisponivel = 3
                }
            };

            listaAlugueis = new ObservableCollection<Aluguel>()
            {
                
            };

            IniciaComandos();
        }
        public void IniciaComandos()
        {
            Add = new RelayCommand((object _) =>
            {
                Usuario userCadastro = new Usuario();

                CadastroUsuario tela = new CadastroUsuario();
                tela.DataContext = userCadastro;
                tela.ShowDialog();

                if (tela.DialogResult.HasValue && tela.DialogResult.Value)
                {
                    listaUsuarios.Add(userCadastro);
                }
            });

            Remove = new RelayCommand((object _) =>
            {
                listaUsuarios.Remove(UsuarioSelecionado);
            });

            Edita = new RelayCommand(_ =>
            {
                if (UsuarioSelecionado != null)
                {
                    Usuario usuarioEditado = (Usuario)UsuarioSelecionado.Clone();

                    CadastroUsuario tela = new CadastroUsuario();
                    tela.DataContext = usuarioEditado;
                    tela.ShowDialog();

                    if (tela.DialogResult.HasValue && tela.DialogResult.Value && !usuarioEditado.Equals(UsuarioSelecionado))
                    {
                        int index = listaUsuarios.IndexOf(UsuarioSelecionado);

                        if (index != -1)
                        {
                            listaUsuarios[index] = usuarioEditado;
                        }
                    }
                }
            });

            AddJogo = new RelayCommand((object _) =>
            {
                Jogo jogoCadastro = new Jogo();

                CadastroJogo tela = new CadastroJogo();
                tela.DataContext = jogoCadastro;
                tela.ShowDialog();

                if (tela.DialogResult.HasValue && tela.DialogResult.Value)
                {
                    listaJogos.Add(jogoCadastro);
                }
            });
            RemoveJogo = new RelayCommand((object _) =>
            {
                listaJogos.Remove(JogoSelecionado);
            });

            EditaJogo = new RelayCommand((object _) =>
            {
                if (JogoSelecionado != null)
                {
                    Jogo jogoEditado = (Jogo)JogoSelecionado.Clone();

                    CadastroJogo tela = new CadastroJogo();
                    tela.DataContext = jogoEditado;
                    tela.ShowDialog();

                    if (tela.DialogResult.HasValue && tela.DialogResult.Value && !jogoEditado.Equals(JogoSelecionado))
                    {
                        int index = listaJogos.IndexOf(JogoSelecionado);

                        if (index != -1)
                        {
                            listaJogos[index] = jogoEditado;
                        }
                    }
                    

                }
            });

            AddAluguel = new RelayCommand(_ =>
            {
                if (UsuarioSelecionado != null && JogoSelecionado != null)
                    if (JogoSelecionado.QuantidadeDisponivel < 1)
                    {
                        MessageBox.Show("Não há jogos disponíveis para aluguel.", "Alerta");
                    }else
                    {
                    Aluguel novoAluguel = new Aluguel
                    {
                        Usuario = UsuarioSelecionado,
                        Jogo = JogoSelecionado,
                        DataAluguel = DateTime.Now 
                    };

                    listaAlugueis.Add(novoAluguel);

                    JogoSelecionado.QuantidadeDisponivel--;
                    Jogo jogoEditado = (Jogo)JogoSelecionado.Clone();
                    int index = listaJogos.IndexOf(JogoSelecionado);
                    listaJogos[index] = jogoEditado;
                }
            });

            FinalizaAluguel = new RelayCommand((object _) =>
            {
                listaAlugueis.Remove(AluguelSelecionado);
            });


        }


    }
}
//git hub desktop
//vs e vscode
//wsl
//docker
//wiseclone
//verificar MVVM, modificar funçao edit
