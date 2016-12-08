using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MsCrmTools.WorkflowExplorer
{


    public class Component
    {

        public enum WorkflowCategories
        {
            Workflow = 0,
            Dialog = 1,
            BusinessRule = 2,
            Action = 3,
            BusinessProcessFlow = 4,
        };

        public enum ComponentTypes // check the complete list from a metadata browser
        {
            Entity = 1,
            Workflow = 29,
            PluginType = 90

        }

        #region "Properties"

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PrimaryEntityName { get; set; }

        public List<Component> ChildComponents { get; set; }

        public WorkflowCategories WorkflowCategory { get; set; }

        public ComponentTypes ComponentType { get; set; }

        public bool IsVisited { get; set; } = false;

        public bool IsWorkflow
        {
            get
            {
                return this.WorkflowCategory == WorkflowCategories.Workflow;
            }
        }
        public bool IsAssembly
        {
            get
            {
                return this.ComponentType == Component.ComponentTypes.PluginType;
            }
        }

        public bool IsEntity
        {
            get
            {
                return this.ComponentType == Component.ComponentTypes.Entity;
            }
        }
        #endregion

        #region "Constructors"

        public Component()
        {
            ChildComponents = new List<Component>();
        }

        #endregion

        #region "Methods"

        public static WorkflowCategories GetComponentTypeByWorkflowCategory(int category)
        {
            WorkflowCategories type;
            switch (category)
            {
                case 0:
                    type = WorkflowCategories.Workflow;
                    break;
                case 1:
                    type = WorkflowCategories.Dialog;
                    break;
                case 2:
                    type = WorkflowCategories.BusinessRule;
                    break;
                case 3:
                    type = WorkflowCategories.Action;
                    break;
                default:
                    type = WorkflowCategories.Workflow;
                    break;



            }

            return type;
        }
        #endregion 


    }
}
