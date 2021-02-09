using System;
using Microsoft.Xrm.Sdk;

namespace PreImageUsage.Plugin
{
    public class PreImageUsagePlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.MessageName.Equals("Update"))
            {
                var tracingService =
                    (ITracingService) serviceProvider.GetService(typeof(ITracingService));

                if (context.Stage == 40)
                {
                    tracingService.Trace("In Stage 40");
                    var target = (Entity) context.InputParameters["Target"];
                    var preMessageImage = context.PreEntityImages["NewPreImage"];

                    tracingService.Trace("Old value: " + preMessageImage["cr90f_description"]);
                    tracingService.Trace("New value: " + target["cr90f_description"]);
                }
            }
        }
    }
}
