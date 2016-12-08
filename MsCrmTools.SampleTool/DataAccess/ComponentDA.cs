using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MsCrmTools.WorkflowExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsCrmTools.WorkflowExplorer
{
    class ComponentDA
    {
        public static DataCollection<Entity> RetrieveDependentComponents(Guid workflowId, string workflowName)
        {
            RetrieveDependentComponentsRequest request = new RetrieveDependentComponentsRequest
            {
                ObjectId = workflowId,
                ComponentType = 29
            };

            RetrieveDependentComponentsResponse response = (RetrieveDependentComponentsResponse)Context.Service.Execute(request);
            EntityCollection result = (EntityCollection)response.Results["EntityCollection"];

            return result.Entities;

        }

        public static DataCollection<Entity> RetrieveRequiredComponents(Guid workflowId, string workflowName)
        {


            RetrieveRequiredComponentsRequest request = new RetrieveRequiredComponentsRequest
            {
                ObjectId = workflowId,
                ComponentType = 29
            };

            RetrieveRequiredComponentsResponse response = (RetrieveRequiredComponentsResponse)Context.Service.Execute(request);
            EntityCollection result = (EntityCollection)response.Results["EntityCollection"];

            return result.Entities;

        }


        public static EntityCollection GetWorkflows()
        {
            QueryExpression query = new QueryExpression("workflow");
            query.ColumnSet.AddColumns(new string[] {"mode", "primaryentity","name","scope","statecode","type",
                                                     "uniquename","solutionid","category"});
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 1);
            query.Criteria.AddCondition("type", ConditionOperator.Equal, 1);
            //query.Criteria.AddCondition("category", ConditionOperator.In, 0, 3);

            return Context.Service.RetrieveMultiple(query);
        }

        public static Component GetPluginTypeById(Guid id)
        {

            Component pa = Context.PluginAssemblies.Where(x => x.Id == id).FirstOrDefault();

            if (pa == null)
            {
                Entity e = Context.Service.Retrieve("plugintype", id, new ColumnSet(new string[] { "name" }));

                pa = new Component();
                pa.Name = e.GetAttributeValue<string>("name");

                pa.ComponentType = Component.ComponentTypes.PluginType;

            }

            return pa;
        }

        public static Component GetComponentByType(int type, Guid id)
        {
            Component c = null;

            switch (type)
            {
                case 29: 
                    c = Context.WorkflowComponents.Where(x => x.Id == id).FirstOrDefault();
                    break;
                case 90:
                    c = ComponentDA.GetPluginTypeById(id);
                    c.ComponentType = Component.ComponentTypes.PluginType;
                    break;
            }

            return c;

        }

    }
}
