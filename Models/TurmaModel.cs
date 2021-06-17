using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaEscola.Models
{
    [DisplayName("Turma")]
    public class Turma
    {
        public int Id { get; set; }
        [Display(Name="Sala")]
        public string Sala { get; set; }

        [Display(Name="Professor")]
        public Professor Professor { get; set; }
    }
}