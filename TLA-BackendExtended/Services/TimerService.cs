using TLA_BackendExtended.DTOs;
using TLA_BackendExtended.Exceptions;
using TLA_BackendExtended.Models;
using TimerModel = TLA_BackendExtended.Models.Timer;

namespace TLA_BackendExtended.Services
{
    public class TimerService : ITimerService
    {
        private static readonly List<TimerModel> _timers = new();
        private static int _nextId = 1;

        // Start a timer
        public Task<TimerModel> StartTimerAsync(string category)
        {
            
            var latest = _timers.OrderByDescending(t => t.Id).FirstOrDefault();

            //
            if (latest == null)
            {
                var firstTimer = new TimerModel
                {
                    Id = _nextId++,
                    Category = category,
                    CreatedAt = DateTime.Now,
                    TimeInterval = null
                };

                _timers.Add(firstTimer);
                return Task.FromResult(firstTimer);
            }

            // Check if the user has ended the latest added timer. 
            if (latest.TimeInterval == null)
                throw new TimerAlreadyInUseException("Timer already running.");
            
            // Create Timer
            var timer = new TimerModel
            {
                Id = _nextId++,
                Category = category,
                CreatedAt = DateTime.Now,
                TimeInterval = null
            };

            _timers.Add(timer);
            return Task.FromResult(timer);
        }

        // End a timer
        public Task<TimerModel> EndTimerAsync(decimal timeInterval)
        {

            var timer = _timers.OrderByDescending(t => t.Id).FirstOrDefault();

            if (timer == null || timer.TimeInterval != null)
                throw new TimerNotFoundException("No timer running.");

            timer.TimeInterval = timeInterval;
            return Task.FromResult(timer);
        }

        // Update timer
        public Task<TimerModel> UpdateTimerAsync(int id, decimal timeInterval, string category)
        {
            var timer = _timers.FirstOrDefault(t => t.Id == id);

            if (timer == null)
                throw new TimerNotFoundException($"Timer '{id}' not found.");

            timer.Category = category;
            timer.TimeInterval = timeInterval;

            return Task.FromResult(timer);
        }

        // Delete timer
        public Task DeleteTimerAsync(int id)
        {
            var timer = _timers.FirstOrDefault(t => t.Id == id);

            if (timer == null)
                throw new TimerNotFoundException($"Timer '{id}' not found.");

            _timers.Remove(timer);
            return Task.CompletedTask;
        }

        // Get latest timer
        public Task<TimerModel> GetLatestTimerAsync()
        {
            var timer = _timers.OrderByDescending(t => t.Id).FirstOrDefault();

            if (timer == null)
                throw new TimerNotFoundException("No timers found.");

            return Task.FromResult(timer);
        }

        // Get paged timers
        public async Task<PagedResponse<TimerResponse>> GetPagedTimersAsync(int page, int pageSize)
        {
            await Task.Delay(20);

            var totalCount = _timers.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = _timers
                .OrderByDescending(t => t.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TimerResponse(
                    t.Id,
                    t.TimeInterval,
                    t.Category,
                    t.CreatedAt
                ))
                .ToList();

            var meta = new PaginationMeta(page, pageSize, totalPages, totalCount, page < totalPages, page > 1);

            return new PagedResponse<TimerResponse>(items, meta);
        }
    }
}
