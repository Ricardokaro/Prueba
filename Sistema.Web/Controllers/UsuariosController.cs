using Microsoft.AspNetCore.Mvc;
using Sistema.Datos;
using Sistema.Web.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prueba.Entidades.Usuarios;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DbContextPrueba _context;        

        public UsuariosController(DbContextPrueba context)
        {
            _context = context;            
        }

        // GET: api/Usuarios/Listar        
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioViewModel>> Listar()
        {
            var usuario = await _context.Usuarios.Where(u => u.condicion == true).ToListAsync();

            return usuario.Select(u => new UsuarioViewModel
            {
                id = u.id,                                
                nombre = u.nombre,
                tipo_documento = u.tipo_documento,
                num_documento = u.num_documento,
                direccion = u.direccion,
                telefono = u.telefono,
                email = u.email,                
                condicion = u.condicion
            });

        }

        // POST: api/Usuarios/Crear        
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.email.ToLower();

            if (await _context.Usuarios.AnyAsync(u => u.email == email))
            {
                return BadRequest("El email ya existe");
            }            

            Usuario usuario = new Usuario
            {                
                nombre = model.nombre,
                tipo_documento = model.tipo_documento,
                num_documento = model.num_documento,
                direccion = model.direccion,
                telefono = model.telefono,
                email = model.email.ToLower(),                
                condicion = true
            };

            _context.Usuarios.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }


        // PUT: api/Usuarios/Actualizar        
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.id == model.id);

            if (usuario == null)
            {
                return NotFound();
            }

            
            usuario.nombre = model.nombre;
            usuario.tipo_documento = model.tipo_documento;
            usuario.num_documento = model.num_documento;
            usuario.direccion = model.direccion;
            usuario.telefono = model.telefono;
            usuario.email = model.email.ToLower();           

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {                
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Usuarios/Desactivar/1        
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.condicion = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Usuarios/Activar/1        
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.condicion = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {                
                return BadRequest();
            }

            return Ok();
        }
    }
}
