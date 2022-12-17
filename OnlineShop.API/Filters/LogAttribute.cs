using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace OnlineShop.API.Filters
{
    public class LogAttribute : ActionFilterAttribute, IActionFilter
    {
        private string fileName = "ActionLog.log";
        private Stopwatch Stopwatch = new Stopwatch();
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            File.AppendAllText(fileName,$@"
                                {DateTime.Now}: 
                                Action: {context.ActionDescriptor.DisplayName}
                                Controller Name: {context.ActionDescriptor.RouteValues["controller"]}
                                Action Completed in: {Stopwatch.ElapsedMilliseconds} ms
                                ");
            Stopwatch.Stop();
            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
            base.OnActionExecuting(context);
        }
    }
}
