using Microsoft.AspNetCore.Mvc.Filters;

namespace MVC_Assignment.Filter
{
    public class ActionLogFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // This runs before the About action
            Console.WriteLine("About action is executing...");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // This runs after the About action
            Console.WriteLine("About action execution completed.");
        }
    }
}
