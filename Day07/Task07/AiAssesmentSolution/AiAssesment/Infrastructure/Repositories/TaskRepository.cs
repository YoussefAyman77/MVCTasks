using Microsoft.EntityFrameworkCore;
using AiAssesment.Domain.Entities;
using AiAssesment.Domain.Interfaces;
using AiAssesment.Infrastructure.Data;

namespace AiAssesment.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetByCompletionStatusAsync(bool isCompleted)
        {
            return await _context.Tasks
                .Where(t => t.IsCompleted == isCompleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetSortedByDueDateAsync()
        {
            return await _context.Tasks
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<TaskItem> AddAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem> UpdateAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
