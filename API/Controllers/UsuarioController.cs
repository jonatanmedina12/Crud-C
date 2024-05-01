using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Dtos;
using Models.Entidades;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    
    public class UsuarioController : BaseApiController
    {
        private readonly ApplicationDbContext _db;
        private readonly ITokenServicio _tokenServicio;

        public UsuarioController(ApplicationDbContext db, ITokenServicio tokenServicio)
        {
            _db = db;
            _tokenServicio = tokenServicio; 
        }


        [HttpPost("registro")] // post api /usuario/registro
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroDto registroDto)
        {

            if(await UsuarioExiste(registroDto.Username))
            {
                return BadRequest("USERNAME YA REGISTRADO...");
            }
            using var hmac = new HMACSHA512();

            var usuario = new Usuario
            {
                Username= registroDto.Username.ToLower(),
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registroDto.Password)),
                passwordSalt = hmac.Key
            };
            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();

            return Ok(new UsuarioDto{
                Username = usuario.Username,
                Token = _tokenServicio.CreartToken(usuario)
            }); 




        }
        [HttpPost("login")]/// post api/usuario/login
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {

            var usuario = await _db.Usuarios.SingleOrDefaultAsync(x => x.Username == loginDto.Username);
            if (usuario == null) { 
                
                return Unauthorized("usuario no valido....");

            }
            using var hmac = new HMACSHA512(usuario.passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (var i = 0; i<computedHash.Length; i++)
            {
                if (computedHash[i] != usuario.passwordHash[i]) {
                    return Unauthorized("password no valido");
                }

            }
            return Ok(new UsuarioDto
            {
                Username = usuario.Username,
                Token = _tokenServicio.CreartToken(usuario)
            });



        }
        private async Task<bool> UsuarioExiste(string username) { 
            

            return await _db.Usuarios.AnyAsync(x => x.Username == username.ToLower());

        
        }

        [Authorize]
        [HttpGet] // api/usario
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        { 



            var usuario = await _db.Usuarios.ToListAsync ();


            return Ok (usuario); 


        }
        [Authorize]
        [HttpGet("{id}")]
        public  async  Task<ActionResult<Usuario>> GetUsuario(int id)
        {

            var usuario =   await _db.Usuarios.FindAsync (id);

            return Ok(usuario);
        }

    }
}
