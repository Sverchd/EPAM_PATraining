using System;
using System.Collections.Generic;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using NUnit.Framework;

namespace DeleteParentThenChild.Tests
{
    [TestFixture]
    public class DeleteParentThenChildPlugin
    {
        [Test]
        public void PreOperationOneParentTwoChild()
        {
            // Arrange
            var fakedContext = new XrmFakedContext();
            var parentEntity = new Entity("new_parententity")
            {
                Id = Guid.NewGuid(),
                ["new_name"] = "First Parent"
            };


            var childEntityFirst = new Entity("new_childentity")
            {
                Id = Guid.NewGuid(),
                ["new_name"] = "First Child",
                ["new_parententity"] = parentEntity.ToEntityReference()
            };


            var childEntitySecond = new Entity("new_childentity")
            {
                Id = Guid.NewGuid(),
                ["new_name"] = "Second Child",
                ["new_parententity"] = parentEntity.ToEntityReference()
            };


            fakedContext.Initialize(new List<Entity>
            {
                parentEntity,
                childEntityFirst,
                childEntitySecond
            });

            var inputParameters = new ParameterCollection();
            inputParameters.Add("Target", parentEntity.ToEntityReference());

            var plugCtx = fakedContext.GetDefaultPluginContext();
            plugCtx.MessageName = "Delete";
            plugCtx.Stage = 10;
            plugCtx.InputParameters = inputParameters;
            plugCtx.Depth = 1;

            // Act
            fakedContext.ExecutePluginWith<Plugin.DeleteParentThenChildPlugin>(plugCtx);

            // Assert
            Assert.NotNull(plugCtx.SharedVariables["toDelete"]);
            Assert.That(((EntityCollection) plugCtx.SharedVariables["toDelete"]).Entities, Has.Count.EqualTo(2));
        }

        [Test]
        public void PostOperationOneParentTwoChild()
        {
            //Arrange
            var fakedContext = new XrmFakedContext();
            var parentEntity = new Entity("new_parententity")
            {
                Id = Guid.NewGuid(),
                ["new_name"] = "First Parent"
            };

            var childEntityFirst = new Entity("new_childentity")
            {
                Id = Guid.NewGuid(),
                ["new_name"] = "First Child",
                ["new_parententity"] = new EntityReference(parentEntity.LogicalName, parentEntity.Id)
            };


            var childEntitySecond = new Entity("new_childentity")
            {
                Id = Guid.NewGuid(),
                ["new_name"] = "Second Child",
                ["new_parententity"] = new EntityReference(parentEntity.LogicalName, parentEntity.Id)
            };

            fakedContext.Initialize(new List<Entity>
            {
                parentEntity,
                childEntityFirst,
                childEntitySecond
            });

            var inputParameters = new ParameterCollection
            {
                {
                    "Target", new EntityReference(parentEntity.LogicalName, parentEntity.Id)
                }
            };

            var plugCtx = fakedContext.GetDefaultPluginContext();
            plugCtx.MessageName = "Delete";
            plugCtx.Stage = 40;
            plugCtx.InputParameters = inputParameters;
            plugCtx.ParentContext = new XrmFakedPluginExecutionContext
            {
                SharedVariables = new ParameterCollection
                {
                    ["toDelete"] = new EntityCollection
                    {
                        Entities =
                        {
                            childEntityFirst,
                            childEntitySecond
                        }
                    }
                }
            };

            // Act
            fakedContext.ExecutePluginWith<Plugin.DeleteParentThenChildPlugin>(plugCtx);

            // Assert
            var service = fakedContext.GetOrganizationService();
            var query = new QueryExpression("new_childentity")
            {
                ColumnSet = new ColumnSet(true)
            };
            query.Criteria.AddCondition(new ConditionExpression("new_parententity",
                ConditionOperator.Equal,
                parentEntity.Id));
            var result = service.RetrieveMultiple(query);
            Assert.IsEmpty(result.Entities);
        }
    }
}
