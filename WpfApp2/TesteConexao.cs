using Npgsql;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace WpfApp2
{
    public static class TesteConexao
    {
        public static void TestarConexao()
        {
            string connectionString = "Server=localhost;Port=5432;Database=CRUD;User Id=postgres;Password=dibmm11111;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Conexão bem-sucedida!");
                    string query = "SELECT * FROM \"Users\"";


                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                
                                string email = reader["email"].ToString();
                                //Console.WriteLine($"email: {email}");
                                MessageBox.Show($"{ email}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao conectar ao banco de dados: {ex.Message}");
                }
            }
        }
    }
}
