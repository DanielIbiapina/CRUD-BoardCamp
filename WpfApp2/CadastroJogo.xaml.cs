using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Lógica interna para CadastroJogo.xaml
    /// </summary>
    public partial class CadastroJogo : Window
    {
        public CadastroJogo()
        {
            InitializeComponent();
        }
        private void bnt_SalvarJogo(object sender, RoutedEventArgs e)
        {

            DialogResult = true;

        }
    }
}
