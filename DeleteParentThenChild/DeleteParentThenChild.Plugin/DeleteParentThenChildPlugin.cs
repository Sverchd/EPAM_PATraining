using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace DeleteParentThenChild.Plugin
{
    public class DeleteParentThenChildPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.MessageName.Equals("Delete"))
            {
                var serviceFactory =
                    (IOrganizationServiceFactory) serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                var service = serviceFactory.CreateOrganizationService(context.UserId);
                var tracingService =
                    (ITracingService) serviceProvider.GetService(typeof(ITracingService));

                if (context.Stage == 10)
                {
                    tracingService.Trace("In Stage 10");
                    var target = (EntityReference) context.InputParameters["Target"];
                    var query = new QueryExpression("new_childentity")
                    {
                        ColumnSet = new ColumnSet("new_childentityid"),
                        NoLock = true
                    };

                    context.SharedVariables.Add("testvar", 12);
                    query.Criteria.AddCondition(new ConditionExpression("new_parententity",
                        ConditionOperator.Equal,
                        target.Id));

                    tracingService.Trace("Target id: " + target.Id);

                    tracingService.Trace("Depth: " + context.Depth);

                    var toDelete = service.RetrieveMultiple(query);
                    tracingService.Trace("Found child records count: " + toDelete.Entities.Count);
                    if (toDelete.Entities.Count > 0) context.SharedVariables.Add("toDelete", toDelete);
                }
                else if (context.Stage == 40)
                {
                    tracingService.Trace("In Stage 40");
                    tracingService.Trace("Depth: " + context.Depth);
                    var toRemove = new EntityCollection();
                    tracingService.Trace("Shared variables: ");
                    foreach (var atr in context.SharedVariables) tracingService.Trace(atr.Key);

                    if (context.ParentContext != null)
                        if (context.ParentContext.SharedVariables.Contains("toDelete"))
                        {
                            tracingService.Trace("Shared variable contains to delete");
                            toRemove = (EntityCollection) context.ParentContext.SharedVariables["toDelete"];
                        }

                    foreach (var relatingEntity in toRemove.Entities)
                        service.Delete(relatingEntity.LogicalName, relatingEntity.Id);
                }
            }
            else
            {
                throw new InvalidPluginExecutionException("Wrong message");
            }
        }
    }
}
