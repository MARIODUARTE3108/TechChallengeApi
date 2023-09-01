using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.Application.Models
{
    public class NoticiaModel
    {
        [Required(ErrorMessage = "O campo Título é obrigatório.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo Chapeu é obrigatório.")]
        public string Chapeu { get; set; }

        [Required(ErrorMessage = "O campo Data de Publicação é obrigatório.")]
        [DataType(DataType.DateTime)]
        public DateTime DataPublicacao { get; set; }

        [Required(ErrorMessage = "O campo Autor é obrigatório.")]
        public string Autor { get; set; }
    }
}
