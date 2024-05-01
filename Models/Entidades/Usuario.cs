using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class Usuario
    {
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }

        public int Id { get; set; }

        public string Username { get; set; }



    }
}
