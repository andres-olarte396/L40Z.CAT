using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Entities;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User GetById(int id)
        {
            var entity = _context.Users.Find(id);
            if (entity == null) return null;

            return new User { Id = entity.Id, Name = entity.Name, Email = entity.Email };
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Select(entity => new User { Id = entity.Id, Name = entity.Name, Email = entity.Email }).ToList();
        }

        public void Add(User user)
        {
            var entity = new UserEntity { Name = user.Name, Email = user.Email };
            _context.Users.Add(entity);
            _context.SaveChanges();
            user.Id = entity.Id;
        }

        public void Update(User user)
        {
            var entity = _context.Users.Find(user.Id);
            if (entity == null) return;

            entity.Name = user.Name;
            entity.Email = user.Email;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Users.Find(id);
            if (entity == null) return;

            _context.Users.Remove(entity);
            _context.SaveChanges();
        }
    }
}
