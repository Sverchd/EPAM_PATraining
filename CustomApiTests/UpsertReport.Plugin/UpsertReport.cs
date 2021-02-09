using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace UpsertReport.Plugin
{
    public class UpsertReport : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context =
                (IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext));
            var serviceFactory =
                (IOrganizationServiceFactory) serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = serviceFactory.CreateOrganizationService(context.UserId);
            var altKey = new KeyAttributeCollection();
            altKey.Add("cr90f_name", context.InputParameters["name"]);

            var report = new Entity
            {
                KeyAttributes = altKey,
                LogicalName = "cr90f_report",
                ["cr90f_description"] = context.InputParameters["description"],
                ["cr90f_priority"] = context.InputParameters["priority"],
                ["cr90f_name"] = context.InputParameters["name"]
            };
            var request = new UpsertRequest
            {
                Target = report
            };
            var response = (UpsertResponse) service.Execute(request);
            var result = response.RecordCreated ? "Record created" : "Record updated";
            context.OutputParameters["result"] = result;
        }
    }
}
