using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamenProgra6_Assent_JozefMoraAraya.Models;
using ExamenProgra6_Assent_JozefMoraAraya.ModelsDTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExamenProgra6_Assent_JozefMoraAraya.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AnswersDbContext _context;

        public UsersController(AnswersDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("GetUserData")]
        public ActionResult<IEnumerable<UserDTO>> GetUserData(string pUserName)
        {
            var query = (from us in _context.Users
                         join ur in _context.UserRoles on us.UserRoleId equals ur.UserRoleId
                         where us.UserName == pUserName && us.Active == true
                         select new
                         {
                             idusuario = us.UserId,
                             cedula = us.UserName,
                             nombre = us.FirstName,
                             apellido = us.LastName,
                             telefono = us.PhoneNumber,
                             contrasennia = us.UserPassword,
                             huelgas = us.StrikeCount,
                             gmail = us.BackUpEmail,
                             trabajo = us.JobDescription,
                             idpuesto = us.UserStatusId,
                             cedularegional = us.CountryId,
                             idapartamento = us.UserRoleId,
                             
                         }
                         ).ToList();
            List<UserDTO> listausuarios = new List<UserDTO>();

            foreach (var item in query)
            {
                UserDTO newUser = new UserDTO()
                {
                    IdUsuario = item.IdUsuario,
                    Cedula = item.cedula,
                    Apellido = item.apellido,
                    Telefono = item.telefono,
                    Contrsennia = item.contrsennia,
                    Huelgas = item.huelgas,
                    Gmail = item.gmail,
                    Trabajo = item.trabajo,
                    IdPuesto = item.idPuesto,
                    CedulaRegional = item.cedularegional,
                    IdApartamento = item.idApartamento,
                };
                listausuarios.Add(newUser);
            }
            if(listausuarios==null)
            {
                return NotFound();
            }
            return listausuarios;
        }


        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
