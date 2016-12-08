using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MsCrmTools.WorkflowExplorer
{
    public class ComponentTreeNode : TreeNode
    {

        #region "Properties"

        public Guid Id { get; set; }
        public string ComponentName { get; set;}
        public string PrimaryEntityName { get; set; }

        private Component.WorkflowCategories _workflowCategory;
        public Component.WorkflowCategories WorkflowCategory
        {
            get
            {
                return _workflowCategory;
            }
            set
            {
                this._workflowCategory = value;
                SetImageIndex();
            }
        }
        private Component.ComponentTypes _componentType;
        public Component.ComponentTypes ComponentType
        {
            get
            {
                return _componentType;
            }
            set
            {
                this._componentType = value;
                SetImageIndex();
            }
        }


        public void SetImageIndex()
        {
            if (ComponentType == Component.ComponentTypes.Workflow)
            {
                switch (WorkflowCategory)
                {
                    case Component.WorkflowCategories.Workflow:
                        this.ImageIndex = 1;
                        break;
                    case Component.WorkflowCategories.Action:
                        this.ImageIndex = 2;
                        break;
                    case Component.WorkflowCategories.Dialog:
                        this.ImageIndex = 4;
                        break;
                    case Component.WorkflowCategories.BusinessRule:
                        this.ImageIndex = 5;
                        break;

                }
            }
            else if (ComponentType == Component.ComponentTypes.Entity)
            {
                this.ImageIndex = 0;

            }
            else if (ComponentType == Component.ComponentTypes.PluginType)
            {
                this.ImageIndex = 3;

            }
        }

        public int X { get; set; }
        public int Y { get; set; }
        public bool IsRoot { get; set; } = false;
        public bool IsHidden { get; set; } = false;
        public bool IsVisited { get; set; } = false;
        public bool IsWorkflow
        {
            get
            {
                return this.ComponentType == Component.ComponentTypes.Workflow;
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

        public ComponentTreeNode()
        {
            
        }

        public ComponentTreeNode DeepClone()
        {
            ComponentTreeNode newNode = new ComponentTreeNode()
            {
                Name = this.Name,
                Id = this.Id,
                IsHidden = this.IsHidden,
                WorkflowCategory = this.WorkflowCategory,
                ComponentType = this.ComponentType,
                Text = this.Text,
                PrimaryEntityName = this.PrimaryEntityName,
                IsVisited = this.IsVisited

            };

            foreach (ComponentTreeNode childNode in this.Nodes)
            {
                if (Context.HideAssemblies || childNode.ComponentType != Component.ComponentTypes.PluginType)
                {
                    ComponentTreeNode newChildNode = childNode.DeepClone();
                    newNode.Nodes.Add(newChildNode);
                }
            }

            return newNode;

        }

        #endregion

        #region "Methods"

        #endregion 


    }
}
