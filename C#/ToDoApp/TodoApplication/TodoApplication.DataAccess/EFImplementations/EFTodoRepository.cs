using Microsoft.EntityFrameworkCore;
using TodoApplication.DataAccess.Interfaces;
using TodoApplication.Domain;

namespace TodoApplication.DataAccess.EFImplementations
{
    public class EFTodoRepository : ITodoRepository
    {
        private readonly TodoAppDbContext _context;

        public EFTodoRepository(TodoAppDbContext context)
        {
            _context = context;
        }

        public EFTodoRepository()
        {
        }

        public IEnumerable<Todo> GetAll()
        {
            return _context.Todo
                .Include(t => t.Category)
                .Include(t => t.Status)
                .ToList();
        }
        public Todo GetById(int id)
        {
            return _context.Todo
                .Include(t => t.Status)
                .Include(t => t.Category)
                .FirstOrDefault(t => t.Id == id)!;
        }
        public void Add(Todo entity)
        {
            _context.Todo.Add(entity);
            _context.SaveChanges();
        }
        public void Update(Todo entity)
        {
            _context.Todo.Update(entity);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Todo todoDb = GetById(id);
            if (todoDb != null)
            {
                _context.Todo.Remove(todoDb);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Todo> GetCompletedTodos()
        {
            return _context.Todo
                .Include(t => t.Status)
                .Include(t => t.Category)
                .Where(t => t.Status.Name == "Completed")
                .ToList();
        }
    }
}
