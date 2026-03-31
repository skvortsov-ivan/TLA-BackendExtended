using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace TLA_BackendExtended.Filters
{
    public class ExecutionTimeFilter : IActionFilter
    {
        private Stopwatch _stopwatch;

        // INNAN controllern
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
        }

        // EFTER controllern
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            var elapsed = _stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"[LOGG] Action {context.ActionDescriptor.DisplayName} tog {elapsed}ms");
        }
    }
}
