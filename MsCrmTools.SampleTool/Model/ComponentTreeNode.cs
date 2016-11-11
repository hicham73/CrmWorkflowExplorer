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

        private ComponentType type;
        public ComponentType Type {
            get
            {
                return type;
            }

            set
            {
                this.type = value;
                this.ImageIndex = 0;
                switch (value)
                {
                    case ComponentType.Entity:
                        this.ImageIndex = 0;
                        break;
                    case ComponentType.Workflow:
                        this.ImageIndex = 1;
                        break;
                    case ComponentType.Action:
                        this.ImageIndex = 2;
                        break;
                    case ComponentType.PluginAssembly:
                        this.ImageIndex = 3;
                        break;

                }
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
                return this.Type == ComponentType.Workflow;
            }
        }
        public bool IsAssembly
        {
            get
            {
                return this.Type == ComponentType.PluginAssembly;
            }
        }

        public bool IsEntity
        {
            get
            {
                return this.Type == ComponentType.Entity;
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
                Type = this.Type,
                Text = this.Text,
                PrimaryEntityName = this.PrimaryEntityName,
                IsVisited = this.IsVisited

            };

            foreach (ComponentTreeNode childNode in this.Nodes)
            {
                if (Context.HideAssemblies || childNode.Type != ComponentType.PluginAssembly)
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
