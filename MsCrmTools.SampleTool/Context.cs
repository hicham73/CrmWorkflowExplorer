using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsCrmTools.WorkflowExplorer
{
    class Context
    {

        public static List<Component> PluginAssemblies;
        public static List<Component> WorkflowComponents;
        public static IOrganizationService Service;


        public static bool HideAssemblies { get; internal set; } = true;

    }
}
