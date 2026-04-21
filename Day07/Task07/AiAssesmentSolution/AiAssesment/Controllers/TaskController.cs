using Microsoft.AspNetCore.Mvc;
using AiAssesment.Domain.Entities;
using AiAssesment.Domain.Interfaces;

namespace AiAssesment.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // GET: Task
        public async Task<IActionResult> Index()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return View(tasks);
        }

        // GET: Task/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // GET: Task/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,IsCompleted,DueDate")] TaskItem task)
        {
            if (ModelState.IsValid)
            {
                await _taskRepository.AddAsync(task);
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Task/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,IsCompleted,CreatedAt,DueDate")] TaskItem task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _taskRepository.UpdateAsync(task);
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Task/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _taskRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Task/Filter/true
        public async Task<IActionResult> Filter(bool isCompleted)
        {
            var tasks = await _taskRepository.GetByCompletionStatusAsync(isCompleted);
            return View("Index", tasks);
        }

        // GET: Task/Sorted
        public async Task<IActionResult> Sorted()
        {
            var tasks = await _taskRepository.GetSortedByDueDateAsync();
            return View("Index", tasks);
        }

        // POST: Task/ToggleComplete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            task.IsCompleted = !task.IsCompleted;
            await _taskRepository.UpdateAsync(task);
            return RedirectToAction(nameof(Index));
        }
    }
}
