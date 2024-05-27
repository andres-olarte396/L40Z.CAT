using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Entities;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repositorio de usuarios
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtener un usuario por su Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetById(int id)
        {
            var entity = _context.Users.Find(id);
            if (entity == null) return null;

            return new User { Id = entity.Id, Name = entity.Name, Email = entity.Email };
        }

        /// <summary>
        /// Obtener todos los usuarios 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAll()
        {
            return _context.Users.Select(entity => new User { Id = entity.Id, Name = entity.Name, Email = entity.Email }).ToList();
        }

        /// <summary>
        /// Agregar un usuario
        /// </summary>
        /// <param name="user"></param>
        public void Add(User user)
        {
            var entity = new UserEntity { Name = user.Name, Email = user.Email, CreatedBy = "system" }; // Asignar el usuario
            _context.Users.Add(entity);
            _context.SaveChanges();
            user.Id = entity.Id;
        }

        /// <summary>
        /// Actualizar un usuario 
        /// </summary>
        /// <param name="user"></param>
        public void Update(User user)
        {
            var entity = _context.Users.Find(user.Id);
            if (entity == null) return;

            entity.Name = user.Name;
            entity.Email = user.Email;
            entity.ModifiedBy = "system"; // Asignar el usuario
            _context.SaveChanges();
        }

        /// <summary>
        /// Eliminar un usuario
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var entity = _context.Users.Find(id);
            if (entity == null) return;

            _context.Users.Remove(entity);
            _context.SaveChanges();
        }
    }
}
