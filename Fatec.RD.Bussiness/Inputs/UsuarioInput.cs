using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatec.RD.Bussiness.Inputs
{
    public class UsuarioInput
    {
        public string CarteiraTrabalho { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string senha { get; set; }
        public int Tipo { get; set; }
        public string Nome { get; set; }
    }
}
