using Fatec.RD.SharedKernel.Validacao;
using System;


namespace Fatec.RD.Dominio.Modelos
{
    public class Usuario
    {
        public int Id { get; set; }
        public string CarteiraTrabalho { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Tipo { get; set; }
        public string Nome { get; set; }

        public void Validar()
        {
            new Convalidare()
                 .NotNullOrEmpty("CarteiraTrabalho", this.CarteiraTrabalho)
                 .NotNullOrEmpty("Email",this.Email)
                 .NotNullOrEmpty("Senha",this.Senha)
                 .NotNullOrEmpty("Nome",this.Nome)
                 .Validate();
        }
    }
}
