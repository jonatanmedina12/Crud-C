using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class RegistroDto
    {


        [Required(ErrorMessage ="Username es Requerido")]

        public string Username { get; set; }
        [Required(ErrorMessage = "password es Requerido")]
        [StringLength(10,MinimumLength =4,ErrorMessage ="el password debe ser minimo de 4 maximo 10 caracteres")]
        public string Password { get; set; }

    }
}
