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
using NUnit.Framework;
using Moq;
using WpfApp2;

namespace UnitTests
{
    //procurar metodos de extensao doc microsoft
    [TestFixture]
    public class MainWindowsVMUnitTests
    {
        private MainWindowsVM mainWindowsVM;
        private Mock<IBancoDeDados> mockBancoDeDados;

        [SetUp]
        public void SetUp()
        {
            mockBancoDeDados = new Mock<IBancoDeDados>();
            mainWindowsVM = new MainWindowsVM();
            mainWindowsVM.bancoDeDados = mockBancoDeDados.Object;
        }

        [Test]
        public void AddUsuarioCommand_ValidUsuario_ShouldAddUsuarioToDatabase()
        {
            // Arrange
            var usuarios = new List<Usuario>
            {
              new Usuario { Nome = "John", Email = "john@example.com", Senha = "test123" },

            };
            mockBancoDeDados.Setup(db => db.GetUsuarios()).Returns(usuarios);


            // Act
            mainWindowsVM.bancoDeDados.AddUsuario(usuarios[0]);
            mainWindowsVM.CarregarUsuarios();

            // Assert
            mockBancoDeDados.Verify(db => db.AddUsuario(usuarios[0]), Times.Once);
            Assert.AreEqual(usuarios.Count, mainWindowsVM.ListaUsuarios.Count);
            Assert.IsTrue(mainWindowsVM.ListaUsuarios.Contains(usuarios[0]));
        }

        [Test]
        public void CarregarUsuarios_ShouldLoadUsersFromDatabase()
        {
            // Arrange
            var usuarios = new List<Usuario>
            {
               new Usuario { Nome = "John", Email = "john@example.com", Senha = "test123" },
        
            };
            mockBancoDeDados.Setup(db => db.GetUsuarios()).Returns(usuarios);

            // Act
            mainWindowsVM.CarregarUsuarios();

            // Assert
            Assert.IsNotNull(mainWindowsVM.ListaUsuarios);
            Assert.AreEqual(usuarios.Count, mainWindowsVM.ListaUsuarios.Count);
            
        }


        [Test]
        public void ValidarUsuario_AllFieldsFilled_ShouldReturnTrue()
        {
            // Arrange
            var usuario = new Usuario
            {
                Nome = "John",
                Email = "john@example.com",
                Senha = "test123"
            };

            // Act
            bool result = mainWindowsVM.ValidarUsuario(usuario);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidarUsuario_NotAllFieldsFilled_ShouldReturnFalse()
        {
            // Arrange
            var usuario = new Usuario
            {
                Nome = "",
                Email = "john@example.com",
                Senha = "test123"
            };

            // Act
            bool result = mainWindowsVM.ValidarUsuario(usuario);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void AddJogoCommand_ValidJogo_ShouldAddJogoToDatabase()
        {
            // Arrange
            var jogos = new List<Jogo>
            {
              new Jogo { NomeDoJogo = "Fifa 25", Categoria = "Futebol", QuantidadeDisponivel = 14 },

            };
            mockBancoDeDados.Setup(db => db.GetJogos()).Returns(jogos);


            // Act
            mainWindowsVM.bancoDeDados.AddJogo(jogos[0]);
            mainWindowsVM.CarregarJogos();

            // Assert
            mockBancoDeDados.Verify(db => db.AddJogo(jogos[0]), Times.Once);
            Assert.AreEqual(jogos.Count, mainWindowsVM.ListaJogos.Count);
            Assert.IsTrue(mainWindowsVM.ListaJogos.Contains(jogos[0]));
        }

        [Test]
        public void CarregarJogos_ShouldLoadGamesFromDatabase()
        {
            // Arrange
            var jogos = new List<Jogo>
            {
              new Jogo { NomeDoJogo = "Fifa 25", Categoria = "Futebol", QuantidadeDisponivel = 14 },

            };
            mockBancoDeDados.Setup(db => db.GetJogos()).Returns(jogos);

            // Act
            mainWindowsVM.CarregarJogos();

            // Assert
            Assert.IsNotNull(mainWindowsVM.ListaJogos);
            Assert.AreEqual(jogos.Count, mainWindowsVM.ListaJogos.Count);

        }


        [Test]
        public void ValidarJogo_AllFieldsFilled_ShouldReturnTrue()
        {
            // Arrange
            var jogo = new Jogo
            {
                NomeDoJogo = "Fifa 29",
                Categoria = "Futebol",
                QuantidadeDisponivel = 23
            };

            // Act
            bool result = mainWindowsVM.ValidarJogo(jogo);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidarJogo_NotAllFieldsFilled_ShouldReturnFalse()
        {
            // Arrange
            var jogo = new Jogo
            {
                NomeDoJogo = "Fifa 29",
                Categoria = "",
                QuantidadeDisponivel = 23
            };

            // Act
            bool result = mainWindowsVM.ValidarJogo(jogo);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void AddValidAluguel_ShouldAddAluguelToDatabase()
        {
            // Arrange
            var alugueis = new List<Aluguel>
    {
        new Aluguel
        {
            Usuario = new Usuario { Nome = "John", Email = "john@example.com", Senha = "test123" },
            Jogo = new Jogo { NomeDoJogo = "Fifa 25", Categoria = "Futebol", QuantidadeDisponivel = 14 },
            DataAluguel = DateTime.Now
        }
    };
            mockBancoDeDados.Setup(db => db.GetAlugueis()).Returns(alugueis);

            // Act
            mainWindowsVM.bancoDeDados.AddAluguel(alugueis[0]);
            mainWindowsVM.CarregarAlugueis();

            // Assert
            mockBancoDeDados.Verify(db => db.AddAluguel(alugueis[0]), Times.Once);
            Assert.AreEqual(alugueis.Count, mainWindowsVM.ListaAlugueis.Count);
            Assert.IsTrue(mainWindowsVM.ListaAlugueis.Contains(alugueis[0]));
        }

        [Test]
        public void CarregarAlugueis_ShouldLoadAlugueisFromDatabase()
        {
            // Arrange
            var alugueis = new List<Aluguel>
    {
        new Aluguel
        {
            Usuario = new Usuario { Nome = "John", Email = "john@example.com", Senha = "test123" },
            Jogo = new Jogo { NomeDoJogo = "Fifa 25", Categoria = "Futebol", QuantidadeDisponivel = 14 },
            DataAluguel = DateTime.Now
        }
    };
            mockBancoDeDados.Setup(db => db.GetAlugueis()).Returns(alugueis);

            // Act
            mainWindowsVM.CarregarAlugueis();

            // Assert
            Assert.IsNotNull(mainWindowsVM.ListaAlugueis);
            Assert.AreEqual(alugueis.Count, mainWindowsVM.ListaAlugueis.Count);
        }

    }

}
