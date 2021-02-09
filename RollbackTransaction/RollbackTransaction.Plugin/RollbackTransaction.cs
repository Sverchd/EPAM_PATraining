using System;
using Microsoft.Xrm.Sdk;

namespace RollbackTransaction.Plugin
{
    public class RollbackTransaction : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext));

            var serviceFactory =
                (IOrganizationServiceFactory) serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = serviceFactory.CreateOrganizationService(context.UserId);
            var tracingService =
                (ITracingService) serviceProvider.GetService(typeof(ITracingService));

            var target = (Entity) context.InputParameters["Target"];
            var report = new Entity
            {
                LogicalName = "cr90f_report",
                Id = Guid.NewGuid(),
                ["cr90f_description"] = target["cr90f_description"],
                ["cr90f_priority"] = target["cr90f_priority"],
                ["cr90f_name"] = target["cr90f_name"]
            };
            service.Create(report);
            tracingService.Trace("In Stage 20 New report created");
        }
    }
}
