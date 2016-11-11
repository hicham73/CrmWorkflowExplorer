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


        public static EntityCollection GetWorkflows()
        {

            QueryExpression query = new QueryExpression("workflow");
            query.ColumnSet.AddColumns(new string[] {"mode", "primaryentity","name","scope","statecode","type",
                                                     "uniquename","solutionid","category"});
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 1);
            query.Criteria.AddCondition("type", ConditionOperator.Equal, 1);
            query.Criteria.AddCondition("category", ConditionOperator.In, 0, 3);

            //query.Criteria.AddCondition("rendererobjecttypecode", ConditionOperator.Null);

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

                pa.Type = ComponentType.PluginAssembly;

            }

            return pa;
        }

        public static Component GetComponentById(Guid id, int type)
        {
            Component c = null;
            switch (type)
            {
                case 29:
                    c = Context.Workflows.Where(x => x.Id == id).FirstOrDefault();
                    break;
                case 90:
                    c = ComponentDA.GetPluginTypeById(id);
                    break;
            }

            return c;

        }

    }
}
