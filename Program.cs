using System;

namespace CarrosSQL
{
    class Program
    {
        public static readonly string SqlCNN = "Server=localhost;database=aula_xp;user=root;password=root;SslMode=none";

        static void Main(string[] args)
        {
            var repoGM = new CarroRepositorio(typeof(GM));
            var repoVW = new CarroRepositorio(typeof(VW));
            var repoFord = new CarroRepositorio(typeof(Ford));

            repoGM.Salvar(new GM()
            {
                Nome = "Opalão",
                Modelo = "SS",
                Ano = 1974,
            });

            repoVW.Salvar(new VW()
            {
                Nome = "SP2",
                Modelo = "Coupé",
                Ano = 1972,
            });

            repoFord.Salvar(new Ford()
            {
                Nome = "Maverick",
                Modelo = "GT",
                Ano = 1972,
            });



            // Console.WriteLine("------------[Clientes]-------------");
            // foreach (var cliente in repoCliente.Todos())
            // {
            //     Console.WriteLine($"Id {cliente.Id}");
            //     Console.WriteLine($"Nome {cliente.Nome}");
            //     Console.WriteLine($"Telefone {cliente.Telefone}");
            //     Console.WriteLine("----------------------------");
            // }

            // Console.WriteLine("\n");

            // Console.WriteLine("------------[Fornecedores]----------------");
            // foreach (var cliente in repoFornecedor.Todos())
            // {
            //     Console.WriteLine($"Id {cliente.Id}");
            //     Console.WriteLine($"Nome {cliente.Nome}");
            //     Console.WriteLine($"Telefone {cliente.Telefone}");
            //     Console.WriteLine("----------------------------");
            // }

            // //Cliente.DeletePorId(5);


            // Console.WriteLine("Pessoas inseridas na base");
        }
    }
}
