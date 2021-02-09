using System;
using Microsoft.Xrm.Sdk;

namespace RollbackTransaction.Plugin
{
    public class ThrowException : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext));

            var tracingService =
                (ITracingService) serviceProvider.GetService(typeof(ITracingService));
            tracingService.Trace("In Stage 40");
            var target = (Entity) context.InputParameters["Target"];

            if ((int) target["cr90f_priority"] == 2)
            {
                tracingService.Trace("Throwing exception");
                throw new InvalidPluginExecutionException("Revert");
            }
        }
    }
}
