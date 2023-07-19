using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WpfApp2;
using Npgsql;
using System.Reflection;

namespace UnitTests
{
    [TestFixture]
    public class PostgresDBUnitTests
    {
        private List<Usuario>? usuarios;
        private List<Jogo>? jogos;
        private List<Aluguel>? alugueis;
        private int usuarioIdCounter; 
        private int jogoIdCounter;
        private int aluguelIdCounter;
        private Mock<IBancoDeDados> mockBancoDeDados;

        [SetUp]
        public void SetUp()
        {
            
            usuarios = new List<Usuario>
            {
                new Usuario { Id = GetNextUsuarioId(), Nome = "Daniel", Email = "danie@email.com", Senha = "1234"},
                new Usuario { Id = GetNextUsuarioId(), Nome = "Daniell", Email = "danie@gmail.com", Senha = "12345"},
            };

            jogos = new List<Jogo>
            {
                new Jogo { Id = GetNextJogoId(), NomeDoJogo = "D&D", Categoria = "RPG", QuantidadeDisponivel = 5 },
                new Jogo { Id = GetNextJogoId(), NomeDoJogo = "Pega Vareta", Categoria = "Adventure", QuantidadeDisponivel = 3 },
                new Jogo { Id = GetNextJogoId(), NomeDoJogo = "FIFA 24", Categoria = "Sports", QuantidadeDisponivel = 10 }
            };
            alugueis = new List<Aluguel>
            {
                new Aluguel { Id = 1, Usuario = usuarios[0], Jogo = jogos[0], DataAluguel = DateTime.Now },
                new Aluguel { Id = 2, Usuario = usuarios[1], Jogo = jogos[1], DataAluguel = DateTime.Now.AddDays(-1) },
            };

            mockBancoDeDados = new Mock<IBancoDeDados>();

            //Usuários
            mockBancoDeDados.Setup(db => db.GetUsuarios()).Returns(usuarios);
            mockBancoDeDados.Setup(db => db.AddUsuario(It.IsAny<Usuario>())).Callback<Usuario>(usuario =>
            {
                usuario.Id = GetNextUsuarioId();
                usuarios.Add(usuario);
            });
            mockBancoDeDados.Setup(db => db.RemoveUsuario(It.IsAny<Usuario>())).Callback<Usuario>(usuario =>
            {
                usuarios.Remove(usuario);
            });
            mockBancoDeDados.Setup(db => db.UpdateUsuario(It.IsAny<Usuario>())).Callback<Usuario>(usuario =>
            {
                var existingUsuario = usuarios.Find(u => u.Id == usuario.Id);
                if (existingUsuario != null)
                {
                    existingUsuario.Nome = usuario.Nome;
                    existingUsuario.Email = usuario.Email;
                    existingUsuario.Senha = usuario.Senha;
                }
            });

            //Jogos
            mockBancoDeDados.Setup(db => db.GetJogos()).Returns(jogos);
            mockBancoDeDados.Setup(db => db.AddJogo(It.IsAny<Jogo>())).Callback<Jogo>(jogo =>
            {
                jogo.Id = GetNextJogoId();
                jogos.Add(jogo);
            });
            mockBancoDeDados.Setup(db => db.RemoveJogo(It.IsAny<Jogo>())).Callback<Jogo>(jogo =>
            {
                jogos.Remove(jogo);
            });
            mockBancoDeDados.Setup(db => db.UpdateJogo(It.IsAny<Jogo>())).Callback<Jogo>(jogo =>
            {
                var existingJogo = jogos.Find(j => j.Id == jogo.Id);
                if (existingJogo != null)
                {
                    existingJogo.NomeDoJogo = jogo.NomeDoJogo;
                    existingJogo.Categoria = jogo.Categoria;
                    existingJogo.QuantidadeDisponivel = jogo.QuantidadeDisponivel;
                }
            });

            //Alugueis
            mockBancoDeDados.Setup(db => db.GetAlugueis()).Returns(alugueis);
            mockBancoDeDados.Setup(db => db.AddAluguel(It.IsAny<Aluguel>())).Callback<Aluguel>(aluguel =>
            {
                aluguel.Id = GetNextAluguelId();
                alugueis.Add(aluguel);
            });

        }
        private int GetNextUsuarioId()
        {
            return ++usuarioIdCounter;
        }

        private int GetNextJogoId()
        {
            return ++jogoIdCounter;
        }

        private int GetNextAluguelId()
        {
            return ++aluguelIdCounter;
        }

        [Test]
        public void GetUsuarios_ShouldReturnListOfUsuarios()
        {
            // Arrange
            var postgresBancoDeDados = mockBancoDeDados.Object;

            // Act
            var result = postgresBancoDeDados.GetUsuarios();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Usuario>>(result);
            Assert.That(result.Count, Is.EqualTo(2));
            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(usuarios[i], Is.EqualTo(result[i]));
            }
            
        }

        [Test]
        public void AddUsuario_ValidUsuario_ShouldAddUsuarioToDatabase()
        {
            // Arrange
            var usuario = new Usuario
            {
                Nome = "John",
                Email = "john@example.com",
                Senha = "test123"
            };
            var postgresBancoDeDados = mockBancoDeDados.Object;

            // Act
            postgresBancoDeDados.AddUsuario(usuario);
            var result = postgresBancoDeDados.GetUsuarios();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Usuario>>(result);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.IsTrue(result.Contains(usuario));
        }



        [Test]
        public void RemoveUsuario_ExistingUsuario_ShouldRemoveUsuarioFromDatabase()
        {
            // Arrange
            var usuario = usuarios[0];
            var postgresBancoDeDados = mockBancoDeDados.Object;

            // Act
            postgresBancoDeDados.RemoveUsuario(usuario);
            var result = postgresBancoDeDados.GetUsuarios();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1)); 
            Assert.IsFalse(result.Contains(usuario)); 
        }

        [Test]
        public void EditUsuario_ValidUsuario_ShouldUpdateUsuarioInDatabase()
        {
            // Arrange
            var usuario = usuarios[0];
            usuario.Nome = "Novo Nome";
            usuario.Email = "novoemail@example.com";
            usuario.Senha = "novasenha";
            var postgresBancoDeDados = mockBancoDeDados.Object;

            // Act
            postgresBancoDeDados.UpdateUsuario(usuario);
            var result = postgresBancoDeDados.GetUsuarios();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2)); 
            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(usuarios[i], Is.EqualTo(result[i]));
            }
        }



        [Test]
        public void GetJogos_ShouldReturnListOfJogos()
        {
            // Arrange
            var postgresBancoDeDados = mockBancoDeDados.Object;

            // Act
            var result = postgresBancoDeDados.GetJogos();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Jogo>>(result);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(jogos[i], Is.EqualTo(result[i]));
            }
        }


        [Test]
        public void AddJogo_ValidJogo_ShouldAddJogoToDatabase()
        {
            // Arrange
            var jogo = new Jogo
            {
                NomeDoJogo = "Novo Jogo",
                Categoria = "Ação",
                QuantidadeDisponivel = 3
            };
            var postgresBancoDeDados = mockBancoDeDados.Object;

            // Act
            postgresBancoDeDados.AddJogo(jogo);
            var result = postgresBancoDeDados.GetJogos();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Jogo>>(result);
            Assert.That(result.Count, Is.EqualTo(4));
            Assert.IsTrue(result.Contains(jogo));
        }

        [Test]
        public void RemoveJogo_ExistingJogo_ShouldRemoveJogoFromDatabase()
        {
            // Arrange
            var jogo = jogos[0];
            var postgresBancoDeDados = mockBancoDeDados.Object;

            // Act
            postgresBancoDeDados.RemoveJogo(jogo);
            var result = postgresBancoDeDados.GetJogos();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2)); 
            Assert.IsFalse(result.Contains(jogo)); 
        }

        [Test]
        public void EditJogo_ValidJogo_ShouldUpdateJogoInDatabase()
        {
            // Arrange
            var jogo = jogos[0];
            jogo.NomeDoJogo = "Novo Nome do Jogo";
            jogo.Categoria = "Aventura";
            jogo.QuantidadeDisponivel = 7;
            var postgresBancoDeDados = mockBancoDeDados.Object;

            // Act
            postgresBancoDeDados.UpdateJogo(jogo);
            var result = postgresBancoDeDados.GetJogos();

            // Assert
            Assert.That(result.Count, Is.EqualTo(3)); 
            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(jogos[i], Is.EqualTo(result[i]));
            }
        }

        [Test]
        public void GetAlugueis_ShouldReturnListOfAlugueis()
        {
            // Arrange
            var postgresBancoDeDados = mockBancoDeDados.Object;

            // Act
            var result = postgresBancoDeDados.GetAlugueis();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Aluguel>>(result);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(alugueis[i], Is.EqualTo(result[i]));
            }
        }

        [Test]
        public void AddAluguel_ValidAluguel_ShouldAddAluguelToDatabase()
        {
            // Arrange
            var aluguel = new Aluguel
            {
                Id = 3,
                Usuario = usuarios[0],
                Jogo = jogos[2],
                DataAluguel = DateTime.Now
            };
            var postgresBancoDeDados = mockBancoDeDados.Object;

            // Act
            postgresBancoDeDados.AddAluguel(aluguel);
            var result = postgresBancoDeDados.GetAlugueis();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Aluguel>>(result);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.IsTrue(result.Contains(aluguel));
        }


    }
}