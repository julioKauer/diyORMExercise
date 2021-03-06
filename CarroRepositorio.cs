using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CarrosSQL
{
    public class CarroRepositorio
    {
        public CarroRepositorio(Type type)
        {
            this.type = type;
        }

        private Type type;

        public List<ICarro> Todos()
        {
            var carros = new List<ICarro>();

            using (MySqlConnection connection = new MySqlConnection(Program.SqlCNN))
            {
                connection.Open();
                var sql = $"select * from carros where tipo = @tipo limit 1000";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@tipo", type.Name);

                    var dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        var carro = (ICarro)Activator.CreateInstance(type);
                        carro.Id = Convert.ToInt32(dr["id"]);
                        carro.Nome = dr["nome"].ToString();
                        carro.Ano = Convert.ToInt32(dr["ano"]);
                        carro.Modelo = dr["modelo"].ToString();

                        carros.Add(carro);
                    }
                }

                connection.Close();
            }

            return carros;
        }

        public void Salvar(ICarro carro)
        {
            using (MySqlConnection connection = new MySqlConnection(Program.SqlCNN))
            {
                connection.Open();
                if (carro.Id == 0)
                {
                    var sql = $"insert into carros(nome, ano, tipo, modelo)values(@nome, @ano, @tipo, @modelo)";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", carro.Nome);
                        command.Parameters.AddWithValue("@ano", carro.Ano);
                        command.Parameters.AddWithValue("@tipo", carro.GetType().Name);
                        command.Parameters.AddWithValue("@modelo", carro.Modelo);


                        carro.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                else
                {
                    var sql = $"update carros set nome = @nome, ano = @ano, tipo = @tipo, modelo = @modelo where id = @id";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", carro.Id);
                        command.Parameters.AddWithValue("@nome", carro.Nome);
                        command.Parameters.AddWithValue("@ano", carro.Ano);
                        command.Parameters.AddWithValue("@tipo", carro.GetType().Name);
                        command.Parameters.AddWithValue("@modelo", carro.Modelo);

                        carro.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                }

                connection.Close();
            }
        }

        public void Delete(ICarro carro)
        {
            using (MySqlConnection connection = new MySqlConnection(Program.SqlCNN))
            {
                connection.Open();
                var sql = $"delete from carros where id = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", carro.Id);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
