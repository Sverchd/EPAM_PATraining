using System;
using Microsoft.Xrm.Sdk;

namespace GetFields.Plugin
{
    public class GetFields : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context =
                (IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext));
            context.OutputParameters["fieldsList"] = new EntityCollection
            {
                Entities =
                {
                    new Entity
                    {
                        ["logicalName"] = "fax",
                        ["displayName"] = "Fax"
                    },
                    new Entity
                    {
                        ["logicalName"] = "ownerid",
                        ["displayName"] = "Owner"
                    }
                }
            };
        }
    }
}
