using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaEscola.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }
        public string CPF { get; set; }
    }
}