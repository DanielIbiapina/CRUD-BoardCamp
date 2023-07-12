using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
    public class MensagensErro
    {
    
        public static void PreenchaTodosCampos()
        {
            MessageBox.Show("Todos os campos devem ser preenchidos.", "Erro");
            
        }
        public static void EmailJaExistente()
        {
            MessageBox.Show("O email informado já está em uso.", "Erro");
        }
        public static void JogosEsgotados()
        {
            MessageBox.Show("Esse jogo está esgotado", "Erro");
        }
    }
}
