using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace ReturnPhoneNumber.Plugin
{
    public class ReturnPhoneNumber : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context =
                (IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext));
            var serviceFactory =
                (IOrganizationServiceFactory) serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            var target = (EntityReference) context.InputParameters["Target"];
            Entity entity = service.Retrieve(target.LogicalName, target.Id, new ColumnSet("telephone1"));
            context.OutputParameters["new_phonenumber"] = entity["telephone1"];
        }
    }
}
