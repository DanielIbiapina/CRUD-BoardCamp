using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class UsuarioRepository
    {
        private readonly IBancoDeDados bancoDeDados;

        public UsuarioRepository(IBancoDeDados bancoDeDados)
        {
            this.bancoDeDados = bancoDeDados;
        }

        public void Add(Usuario usuario)
        {
            bancoDeDados.AddUsuario(usuario);
        }

        public void Remove(Usuario usuario)
        {
            bancoDeDados.RemoveUsuario(usuario);
        }

        public void Update(Usuario usuario)
        {
            bancoDeDados.UpdateUsuario(usuario);
        }

        public List<Usuario> GetAll()
        {
            return bancoDeDados.GetUsuarios();
        }
    }

}
