using AiAssesment.Domain.Entities;

namespace AiAssesment.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<IEnumerable<TaskItem>> GetByCompletionStatusAsync(bool isCompleted);
        Task<IEnumerable<TaskItem>> GetSortedByDueDateAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task<TaskItem> AddAsync(TaskItem task);
        Task<TaskItem> UpdateAsync(TaskItem task);
        Task DeleteAsync(int id);
    }
}
