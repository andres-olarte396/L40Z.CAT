using Core.Application.DTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    /// <summary>
    /// Controlador para la gestión de usuarios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="userService">El servicio de usuario.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario.</param>
        /// <returns>El usuario solicitado.</returns>
        /// <response code="200">Retorna el usuario.</response>
        /// <response code="404">Si el usuario no es encontrado.</response>
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        /// <returns>Una lista de usuarios.</returns>
        /// <response code="200">Retorna el usuario.</response>
        /// <response code="404">Si el usuario no es encontrado.</response>
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="userDto">Los datos del usuario.</param>
        /// <returns>El usuario creado.</returns>
        /// <response code="201">Retorna el usuario creado.</response>
        /// <response code="400">Si los datos del usuario son inválidos.</response>
        [HttpPost]
        public ActionResult CreateUser([FromBody] UserDto userDto)
        {
            _userService.CreateUser(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
        }

        /// <summary>
        /// Actualiza un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario.</param>
        /// <param name="userDto">Los datos del usuario.</param>
        /// <returns>Respuesta sin contenido.</returns>
        /// <response code="204">Si el usuario es actualizado.</response>
        /// <response code="400">Si los datos del usuario son inválidos.</response>
        /// <response code="404">Si el usuario no es encontrado.</response>
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] UserDto userDto)
        {
            if (id != userDto.Id) return BadRequest();

            _userService.UpdateUser(userDto);
            return NoContent();
        }

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario.</param>
        /// <returns>Respuesta sin contenido.</returns>
        /// <response code="204">Si el usuario es eliminado.</response>
        /// <response code="404">Si el usuario no es encontrado.</response>
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
