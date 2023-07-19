/*using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace WpfApp2
{
    public class MariaDbBancoDeDados : IBancoDeDados
    {
        pprivate readonly string connectionString;


        public PostgresBancoDeDados()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void AddUsuario(Usuario usuario)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (name, email, password) VALUES (@nome, @email, @senha)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", usuario.Nome);
                    command.Parameters.AddWithValue("@email", usuario.Email);
                    command.Parameters.AddWithValue("@senha", usuario.Senha);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveUsuario(Usuario usuario)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM Users WHERE id = @id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", usuario.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUsuario(Usuario usuario)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "UPDATE Users SET name = @nome, email = @email, password = @senha WHERE id = @id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", usuario.Nome);
                    command.Parameters.AddWithValue("@email", usuario.Email);
                    command.Parameters.AddWithValue("@senha", usuario.Senha);
                    command.Parameters.AddWithValue("@id", usuario.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Usuario> GetUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usuario = new Usuario
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nome = reader["name"].ToString(),
                                Email = reader["email"].ToString(),
                                Senha = reader["password"].ToString()
                            };

                            usuarios.Add(usuario);
                        }
                    }
                }
            }

            return usuarios;
        }

        public void AddJogo(Jogo jogo)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO Games (name, category, quantity) VALUES (@nome, @categoria, @quantidade)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", jogo.NomeDoJogo);
                    command.Parameters.AddWithValue("@categoria", jogo.Categoria);
                    command.Parameters.AddWithValue("@quantidade", jogo.QuantidadeDisponivel);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveJogo(Jogo jogo)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM Games WHERE id = @id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", jogo.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateJogo(Jogo jogo)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "UPDATE Games SET name = @nome, category = @categoria, quantity = @quantidade WHERE id = @id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", jogo.NomeDoJogo);
                    command.Parameters.AddWithValue("@categoria", jogo.Categoria);
                    command.Parameters.AddWithValue("@quantidade", jogo.QuantidadeDisponivel);
                    command.Parameters.AddWithValue("@id", jogo.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Jogo> GetJogos()
        {
            List<Jogo> jogos = new List<Jogo>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Games";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Jogo jogo = new Jogo
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                NomeDoJogo = reader["name"].ToString(),
                                Categoria = reader["category"].ToString(),
                                QuantidadeDisponivel = Convert.ToInt32(reader["quantity"])
                            };

                            jogos.Add(jogo);
                        }
                    }
                }
            }

            return jogos;
        }

        public void AddAluguel(Aluguel aluguel)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO Rentals (username, gamename, userid, gameid, rentaldate) VALUES (@usuario, @jogo, @usuarioId, @jogoId, @dataAluguel)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@usuario", aluguel.Usuario.Nome);
                    command.Parameters.AddWithValue("@jogo", aluguel.Jogo.NomeDoJogo);
                    command.Parameters.AddWithValue("@usuarioId", aluguel.Usuario.Id);
                    command.Parameters.AddWithValue("@jogoId", aluguel.Jogo.Id);
                    command.Parameters.AddWithValue("@dataAluguel", aluguel.DataAluguel);

                    connection.Open();
                    command.ExecuteNonQuery();

                    UpdateJogo(aluguel.Jogo);
                }
            }
        }

        public List<Aluguel> GetAlugueis()
        {
            List<Aluguel> alugueis = new List<Aluguel>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT r.id, u.name, j.name, r.rentaldate " +
                               "FROM Rentals r " +
                               "JOIN Users u ON r.userid = u.id " +
                               "JOIN Games j ON r.gameid = j.id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);

                            Usuario usuario = new Usuario()
                            {
                                Nome = reader["name"].ToString()
                            };

                            Jogo jogo = new Jogo()
                            {
                                NomeDoJogo = reader["name"].ToString()
                            };

                            DateTime dataAluguel = Convert.ToDateTime(reader["rentaldate"]);

                            Aluguel aluguel = new Aluguel(usuario, jogo, dataAluguel);
                            aluguel.Id = id;
                            alugueis.Add(aluguel);
                        }
                    }
                }
            }

            return alugueis;
        }
    }
}
*/