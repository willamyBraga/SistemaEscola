using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaEscola.Models
{
    [DisplayName("Matricula")]
    public class Matricula
    {
        public int Id { get; set; }

        [DataType(DataType.Date), Display(Name = "Data da Matricula")]
        public DateTime DataMatricula { get; set; }

        [Display(Name = "Aluno")]
        public Aluno Aluno { get; set; }

        [Display(Name = "Turma")]
        public Turma Turma { get; set; }
    }
}