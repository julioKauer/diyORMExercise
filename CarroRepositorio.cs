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

        //  private string getObjectConverted(PropertyInfo pi)
        // {
        //     var dataType = pi.PropertyType.Name.ToLower();
        //     if (dataType == "nullable`1") dataType = GetIntDataType(pi);

        //     if (dataType == "string")
        //         return "txt" + pi.Name + ".Text";
        //     else if (dataType == "int")
        //         return "int.Parse(txt" + pi.Name + ".Text)";
        //     else if (dataType == "datetime")
        //         return "DateTime.Parse(txt" + pi.Name + ".Text)";
        //     else if (dataType == "int32")
        //         return "int.Parse(txt" + pi.Name + ".Text)";
        //     else if (dataType == "int64")
        //         return "long.Parse(txt" + pi.Name + ".Text)";
        //     else if (dataType == "double")
        //         return "double.Parse(txt" + pi.Name + ".Text)";
        //     else if (dataType == "single")
        //         return "float.Parse(txt" + pi.Name + ".Text)";
        //     else if (dataType == "boolean")
        //         return "bool.Parse(txt" + pi.Name + ".Text)";

        //     return "txt" + pi.Name + ".Text";
        // }

        public List<ICarro> Todos()
        {
            var carros = new List<ICarro>();

            using (MySqlConnection connection = new MySqlConnection(Program.SqlCNN))
            {
                connection.Open();
                var sql = $"select * from {this.type.Name} limit 1000";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    var dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        var carro = (ICarro)Activator.CreateInstance(type);

                        foreach(var pi in carro.GetType().GetProperties())
                        {
                            pi.SetValue(carro, dr[pi.Name]);
                        }
                        // carro.Id = Convert.ToInt32(dr["id"]);
                        // carro.Nome = dr["nome"].ToString();
                        // carro.Ano = Convert.ToInt32(dr["ano"]);
                        // carro.Modelo = dr["modelo"].ToString();

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
                    var sql = $"insert into {this.type.Name} (nome, ano, modelo) values (@nome, @ano, @modelo)";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", carro.Nome);
                        command.Parameters.AddWithValue("@ano", carro.Ano);
                        command.Parameters.AddWithValue("@modelo", carro.Modelo);


                        carro.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                else
                {
                    var sql = $"update {this.type.Name} set nome = @nome, ano = @ano, modelo = @modelo where id = @id";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", carro.Id);
                        command.Parameters.AddWithValue("@nome", carro.Nome);
                        command.Parameters.AddWithValue("@ano", carro.Ano);
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
                var sql = $"delete from {this.type.Name} where id = @id";
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
