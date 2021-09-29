using System;

namespace CarrosSQL
{
    public abstract class ACarro : ICarro
    {
        public int Id {get;set;}
        public string Nome {get; set;}
        public int Ano {get; set;}
        public string Modelo {get; set;}
    }
}
