using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;



namespace WpfApp2
{
    public class PostgresBancoDeDados : IBancoDeDados
    {
        private readonly string connectionString;

        public PostgresBancoDeDados(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddUsuario(Usuario usuario)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "INSERT INTO \"Users\" (name, email, password) VALUES (@nome, @email, @senha)";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@nome", usuario.Nome);
                        command.Parameters.AddWithValue("@email", usuario.Email);
                        command.Parameters.AddWithValue("@senha", usuario.Senha);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }catch(Exception ex) { }
                }
            }
        }

        public void RemoveUsuario(Usuario usuario)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "DELETE FROM \"Users\" WHERE id = @id";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    try { 
                    command.Parameters.AddWithValue("@id", usuario.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { }
                }
            }
        }

        public void UpdateUsuario(Usuario usuario)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "UPDATE \"Users\" SET name = @nome, email = @email, password = @senha WHERE id = @id";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    try { 
                    command.Parameters.AddWithValue("@nome", usuario.Nome);
                    command.Parameters.AddWithValue("@email", usuario.Email);
                    command.Parameters.AddWithValue("@senha", usuario.Senha);
                    command.Parameters.AddWithValue("@id", usuario.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { }
                }
            }
        }

        public List<Usuario> GetUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "SELECT * FROM \"Users\"";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    connection.Open();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        try { 
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
                        catch (Exception ex) { }
                    }
                }
            }

            return usuarios;
        }

        public void AddJogo(Jogo jogo)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "INSERT INTO \"Games\" (name, category, quantity) VALUES (@nome, @categoria, @quantidade)";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    try { 
                    command.Parameters.AddWithValue("@nome", jogo.NomeDoJogo);
                    command.Parameters.AddWithValue("@categoria", jogo.Categoria);
                    command.Parameters.AddWithValue("@quantidade", jogo.QuantidadeDisponivel);

                    connection.Open();
                    command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { }
                }
            }
        }

        public void RemoveJogo(Jogo jogo)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "DELETE FROM \"Games\" WHERE id = @id";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    try {
                    command.Parameters.AddWithValue("@id", jogo.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { }
                }
            }
        }

        public void UpdateJogo(Jogo jogo)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "UPDATE \"Games\" SET name = @nome, category = @categoria, quantity = @quantidade WHERE id = @id";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    try { 
                    command.Parameters.AddWithValue("@nome", jogo.NomeDoJogo);
                    command.Parameters.AddWithValue("@categoria", jogo.Categoria);
                    command.Parameters.AddWithValue("@quantidade", jogo.QuantidadeDisponivel);
                    command.Parameters.AddWithValue("@id", jogo.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { }
                }
            }
        }

        public List<Jogo> GetJogos()
        {
            List<Jogo> jogos = new List<Jogo>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "SELECT * FROM \"Games\"";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    connection.Open();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        try { 
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
                        catch (Exception ex) { }
                    }
                }
            }

            return jogos;
        }

        public void AddAluguel(Aluguel aluguel)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "INSERT INTO \"Rentals\" (username, gamename, userid, gameid, rentaldate) VALUES (@usuario, @jogo, @usuarioId, @jogoId, @dataAluguel)";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    try { 
                    command.Parameters.AddWithValue("@usuario", aluguel.Usuario.Nome);
                    command.Parameters.AddWithValue("@jogo", aluguel.Jogo.NomeDoJogo);
                    command.Parameters.AddWithValue("@usuarioId", aluguel.Usuario.Id);
                    command.Parameters.AddWithValue("@jogoId", aluguel.Jogo.Id);
                    command.Parameters.AddWithValue("@dataAluguel", aluguel.DataAluguel);

                    connection.Open();
                    command.ExecuteNonQuery();

                    UpdateJogo(aluguel.Jogo);
                    }
                    catch (Exception ex) { }
                }
            }
        }

        public List<Aluguel> GetAlugueis()
        {
            List<Aluguel> alugueis = new List<Aluguel>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "SELECT r.id, u.name, j.name, r.rentaldate " +
                               "FROM \"Rentals\" r " +
                               "JOIN \"Users\" u ON r.userid = u.id " +
                               "JOIN \"Games\" j ON r.gameid = j.id";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    connection.Open();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        try { 
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
                        catch (Exception ex) { }
                    }
                }
            }

            return alugueis;
        }
    }

}
