using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            IBancoDeDados bancoDeDados = new PostgresBancoDeDados("Server=localhost;Port=5432;Database=CRUD;User Id=postgres;Password=dibmm11111;");
            // Ou
            // IBancoDeDados bancoDeDados = new MariaDbBancoDeDados("sua_connection_string_do_MariaDB");

            UsuarioRepository usuarioRepository = new UsuarioRepository(bancoDeDados);
            JogoRepository jogoRepository = new JogoRepository(bancoDeDados);
            AluguelRepository aluguelRepository = new AluguelRepository(bancoDeDados);

            InitializeComponent();
            DataContext = new MainWindowsVM(usuarioRepository, jogoRepository, aluguelRepository);
        }

        public object WebApplication { get; private set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
        }
    }
}
