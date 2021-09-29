using System;

namespace CarrosSQL
{
    public interface ICarro
    {
        int Id {get;set;}
        string Nome {get; set;}
        int Ano {get; set;}
        string Modelo {get; set;}
    }
}
