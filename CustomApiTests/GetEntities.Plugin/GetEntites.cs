using System;
using Microsoft.Xrm.Sdk;

namespace GetEntities.Plugin
{
    public class GetEntites : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context =
                (IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext));
            context.OutputParameters["entitiesList"] = new EntityCollection
            {
                Entities =
                {
                    new Entity
                    {
                        ["logicalName"] = "contact",
                        ["displayName"] = "Contact"
                    },
                    new Entity
                    {
                        ["logicalName"] = "account",
                        ["displayName"] = "Account"
                    }
                }
            };
        }
    }
}
