using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuletaAPI.Data;
using RuletaAPI.Models;
using RuletaAPI.Responses; // Importa el namespace para las clases de respuesta
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuletaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuletaController : ControllerBase
    {
        private readonly RuletaContext _context;

        private static readonly Dictionary<int, string> NumeroColores = new Dictionary<int, string>
        {
            { 0, "verde" },
            { 32, "negro" },
            { 15, "rojo" },
            { 19, "negro" },
            { 4, "rojo" },
            { 21, "negro" },
            { 2, "rojo" },
            { 25, "negro" },
            { 17, "rojo" },
            { 34, "negro" },
            { 6, "rojo" },
            { 27, "negro" },
            { 13, "rojo" },
            { 36, "negro" },
            { 11, "rojo" },
            { 30, "negro" },
            { 8, "rojo" },
            { 23, "negro" },
            { 10, "rojo" },
            { 5, "negro" },
            { 24, "rojo" },
            { 16, "negro" },
            { 33, "rojo" },
            { 1, "negro" },
            { 20, "rojo" },
            { 14, "negro" },
            { 31, "rojo" },
            { 9, "negro" },
            { 22, "rojo" },
            { 18, "rojo" },
            { 29, "negro" },
            { 7, "rojo" },
            { 28, "negro" },
            { 12, "rojo" },
            { 35, "negro" },
            { 3, "rojo" },
            { 26, "negro" }
        };

        public RuletaController(RuletaContext context)
        {
            _context = context;
        }

        [HttpGet("random")]
        public ActionResult GetRandomNumber()
        {
            var random = new Random();
            int number = random.Next(0, 37);
            string color = NumeroColores[number];
            string paridad = (number % 2 == 0) ? "par" : "impar";
            return Ok(new { number, color, paridad });
        }

        [HttpPost("guardar")]
        public async Task<ActionResult> GuardarUsuario([FromBody] Usuario usuario)
        {
            var existingUser = _context.Usuarios
                .Include(u => u.ApuestasTemporales)
                .AsEnumerable()
                .FirstOrDefault(u => u.Nombre.ToLower() == usuario.Nombre.ToLower());
            if (existingUser != null)
            {
                existingUser.Saldo += usuario.Saldo;
                existingUser.ApuestasTemporales.AddRange(usuario.ApuestasTemporales);
                usuario.ApuestasTemporales.Clear();
            }
            else
            {
                _context.Usuarios.Add(usuario);
            }

            await _context.SaveChangesAsync();
            return Ok(usuario);
        }


        [HttpGet("buscar-usuario/{nombre}")]
        public async Task<ActionResult> BuscarUsuario(string nombre)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre.ToLower() == nombre.ToLower());
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("crear-usuario")]
        public async Task<ActionResult> CrearUsuario([FromBody] Usuario nuevoUsuario)
        {
            var existingUser = _context.Usuarios
                .AsEnumerable()
                .FirstOrDefault(u => u.Nombre.ToLower() == nuevoUsuario.Nombre.ToLower());
            if (existingUser != null)
            {
                return Conflict("El usuario ya existe.");
            }

            nuevoUsuario.Saldo = 0;
            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            var usuarioResponse = new UsuarioResponse
            {
                Id = nuevoUsuario.Id,
                Nombre = nuevoUsuario.Nombre,
                Saldo = nuevoUsuario.Saldo
            };

            return Ok(usuarioResponse);
        }


        [HttpPost("recargar-saldo/{nombre}")]
        public async Task<ActionResult> RecargarSaldo(string nombre, [FromBody] RecargaRequest recarga)
        {
            var user = _context.Usuarios
                .AsEnumerable()
                .FirstOrDefault(u => u.Nombre.ToLower() == nombre.ToLower());
            if (user == null)
            {
                return NotFound();
            }

            user.Saldo += recarga.Monto;
            await _context.SaveChangesAsync();
            return Ok(new { user.Nombre, user.Saldo });
        }

        [HttpGet("saldo/{nombre}")]
        public async Task<ActionResult> GetSaldo(string nombre)
        {
            var user = _context.Usuarios
                .AsEnumerable()
                .FirstOrDefault(u => u.Nombre.ToLower() == nombre.ToLower());
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.Saldo);
        }

        [HttpPost("apuesta")]
        public async Task<ActionResult> RealizarApuesta([FromBody] ApuestaTemporal apuesta)
        {
            var user = _context.Usuarios
                .Include(u => u.ApuestasTemporales)
                .FirstOrDefault(u => u.Id == apuesta.UsuarioId);
            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            var random = new Random();
            int number = random.Next(0, 37);
            string color = NumeroColores[number];
            string paridad = (number % 2 == 0) ? "par" : "impar";
            decimal premio = 0;
            bool gano = false;

            if (apuesta.Tipo == "color" && color == apuesta.Valor)
            {
                premio = apuesta.Monto / 2;
                gano = true;
            }
            else if (apuesta.Tipo == "paridad" && paridad == apuesta.Valor)
            {
                premio = apuesta.Monto;
                gano = true;
            }
            else if (apuesta.Tipo == "color_y_paridad" && color == apuesta.Color && paridad == apuesta.Paridad)
            {
                premio = apuesta.Monto * 3;
                gano = true;
            }
            else
            {
                premio = -apuesta.Monto;
            }

            apuesta.Resultado = premio;

            // Solo agregar la apuesta temporal si el monto es mayor que 0
            if (apuesta.Monto > 0)
            {
                user.ApuestasTemporales.Add(apuesta);
            }

            await _context.SaveChangesAsync();

            return Ok(new { number, color, paridad, premio, gano });
        }


        [HttpDelete("borrar-apuestas-temporales/{nombre}")]
        public async Task<ActionResult> BorrarApuestasTemporales(string nombre)
        {
            var user = await _context.Usuarios
                .Include(u => u.ApuestasTemporales)
                .FirstOrDefaultAsync(u => u.Nombre.ToLower() == nombre.ToLower());
            if (user == null)
            {
                return NotFound();
            }

            var apuestasTemporales = _context.ApuestasTemporales
                .Where(a => a.UsuarioId == user.Id);

            _context.ApuestasTemporales.RemoveRange(apuestasTemporales);

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("guardar-apuestas/{nombre}")]
        public async Task<ActionResult> GuardarApuestas(string nombre)
        {
            var user = _context.Usuarios
                .Include(u => u.ApuestasTemporales)
                .FirstOrDefault(u => u.Nombre.ToLower() == nombre.ToLower());
            if (user == null)
            {
                return NotFound();
            }

            decimal saldoTemporal = 0;
            foreach (var apuesta in user.ApuestasTemporales.ToList())
            {
                var apuestaFinal = new Apuesta
                {
                    UsuarioId = apuesta.UsuarioId,
                    Tipo = apuesta.Tipo,
                    Valor = apuesta.Valor,
                    Monto = apuesta.Monto,
                    Color = apuesta.Color,
                    Paridad = apuesta.Paridad,
                    Resultado = apuesta.Resultado
                };

                _context.Apuestas.Add(apuestaFinal);
                saldoTemporal += apuesta.Resultado;
            }

            user.Saldo += saldoTemporal;
            _context.ApuestasTemporales.RemoveRange(user.ApuestasTemporales);

            await _context.SaveChangesAsync();
            return Ok(new { user.Nombre, user.Saldo });
        }





        [HttpGet("apuestas-temporales/{nombre}")]
        public async Task<ActionResult> GetApuestasTemporales(string nombre)
        {
            var user = await _context.Usuarios
                .Include(u => u.ApuestasTemporales)
                .FirstOrDefaultAsync(u => u.Nombre.ToLower() == nombre.ToLower());
            if (user == null)
            {
                return NotFound();
            }

            decimal saldoTemporal = user.Saldo + user.ApuestasTemporales.Sum(a => a.Resultado);

            var apuestasResponse = user.ApuestasTemporales.Select(a => new ApuestaResponse
            {
                Tipo = a.Tipo,
                Valor = a.Valor,
                Monto = a.Monto,
                Color = a.Color,
                Paridad = a.Paridad,
                Resultado = a.Resultado
            }).ToList();

            var response = new ApuestasTemporalesResponse
            {
                UsuarioId = user.Id,
                SaldoTemporal = saldoTemporal,
                Apuestas = apuestasResponse
            };

            return Ok(response);
        }
    }
}
