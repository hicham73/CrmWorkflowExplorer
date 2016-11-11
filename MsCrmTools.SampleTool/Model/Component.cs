using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MsCrmTools.WorkflowExplorer
{
    public enum ComponentType
    {
        Entity,
        Workflow,
        Action,
        PluginAssembly
    };

    public class Component
    {

        #region "Properties"

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PrimaryEntityName { get; set; }
        public List<Component> ChildComponents { get; set; }
        public ComponentType Type { get; set; }
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

        public Component()
        {
            ChildComponents = new List<Component>();
        }

        #endregion

        #region "Methods"

        #endregion 


    }
}
