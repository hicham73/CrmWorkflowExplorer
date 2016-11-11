// PROJECT : MsCrmTools.WorkflowExplorer
// This project was developed by Hicham Wahbi
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Windows.Forms;
using Microsoft.Crm.Sdk.Messages;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CrmExceptionHelper = XrmToolBox.CrmExceptionHelper;
using Helpers = MsCrmTools.WorkflowExplorer.Helpers;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Discovery;
using System.Linq;
using System.Drawing;

namespace MsCrmTools.WorkflowExplorer
{
    public partial class WorkflowExplorer : PluginControlBase, IGitHubPlugin, ICodePlexPlugin, IPayPalPlugin, IHelpPlugin, IStatusBarMessenger
    {

        #region Base tool implementation

        public WorkflowExplorer()
        {
            InitializeComponent();
        }

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;


        public void ProcessRetrieveWorkflows()
        {
            Context.Service = Service;

            tsbCancel.Enabled = true;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Retrieving Workflows...",
                Work = (bw, ea) =>
                {
                        if (bw.CancellationPending)
                        {
                            ea.Cancel = true;
                        }

                        Context.PluginAssemblies = new List<Component>();
                        Context.Workflows = new List<Component>();

                        EntityCollection wCollection = ComponentDA.GetWorkflows();
 
                        List<Entity> allWorkflows = new List<Entity>();

                        foreach (Entity w in wCollection.Entities)
                        {
 
                            Component workflow = new Component();
                            int category = w.GetAttributeValue<OptionSetValue>("category").Value;
                            if(category == 0)
                                workflow.Type = ComponentType.Workflow;
                            else
                                workflow.Type = ComponentType.Action;

                            workflow.Id = w.Id;
                            workflow.Name = w.GetAttributeValue<string>("name");
                            workflow.PrimaryEntityName = w.GetAttributeValue<string>("primaryentity");

                            Context.Workflows.Add(workflow);

                        }

                        foreach (var workflow in Context.Workflows)
                        {
                            DataCollection<Entity> requiredComponents = RetrieveRequiredComponents(workflow.Id, workflow.Name);

                            foreach (var rc in requiredComponents)
                            {
                                int componentType = rc.GetAttributeValue<OptionSetValue>("requiredcomponenttype").Value;
                                Guid requiredComponentId = rc.GetAttributeValue<Guid>("requiredcomponentobjectid");

                                if (componentType == 29 || componentType == 90)
                                {
                            
                                    Component w = ComponentDA.GetComponentById(requiredComponentId, componentType);

                                    if (w != null)
                                    {
                                        workflow.ChildComponents.Add(w);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Component with ID {0} not found", requiredComponentId);
                                    }
                                }
                            }


                        }

                        ea.Result = Context.Workflows;
 
                },
                PostWorkCallBack = ea =>
                {
                    Console.WriteLine("Work completed");
                    tsbCancel.Enabled = false;
                    List<Component> components = (List<Component>)ea.Result;
                    if (!ea.Cancelled)
                    {
                        DisplayByEntity(components);
                    }
                },
                AsyncArgument = null,
                IsCancelable = true,
                MessageWidth = 340,
                MessageHeight = 150
            });

        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void DisplayByEntity(List<Component> components)
        {

            Dictionary<string, List<Component>> map = new Dictionary<string, List<Component>>();

            foreach (var component in components)
            {
                List<Component> list;
                if (map.ContainsKey(component.PrimaryEntityName))
                {
                    list = map[component.PrimaryEntityName];
                }
                else
                {
                    list = new List<Component>();
                    map.Add(component.PrimaryEntityName, list);
                }

                list.Add(component);
            }

            foreach (var en in map.Keys)
            {
                var childWorkflows = map[en];
                ComponentTreeNode rootComponent = new ComponentTreeNode();
                rootComponent.Type = ComponentType.Entity;
                rootComponent.Text = en;
                tvWorkflows.Nodes.Add(rootComponent);
                foreach (var workflow in childWorkflows)
                {
                    AppendWorkflowNode(rootComponent, workflow, 5);

                }
            }


        }


        private void tsbWhoAmI_Click(object sender, EventArgs e)
        {
            ExecuteMethod(ProcessRetrieveWorkflows);
        }


        void AppendWorkflowNode(ComponentTreeNode node, Component w, int depth)
        {
            depth--;
            if (depth < 0)
                return;               
            ComponentTreeNode childNode = new ComponentTreeNode();
            childNode.Text = w.Name;
            childNode.Id = w.Id;
            childNode.Type = w.Type;
            childNode.PrimaryEntityName = w.PrimaryEntityName;
            node.Nodes.Add(childNode);
            childNode.IsVisited = IsComponentVisited(childNode, childNode.Id);

            if (!childNode.IsVisited)
            {
                foreach (var childWorkflow in w.ChildComponents)
                {
                    AppendWorkflowNode(childNode, childWorkflow, depth);
                }
            }

        }

        public bool IsComponentVisited(ComponentTreeNode node, Guid id)
        {

            ComponentTreeNode parent = (ComponentTreeNode)node.Parent;
            if (parent != null)
            {
                if (parent.Id == id)
                    return true;
                else
                    return IsComponentVisited(parent, id);
            }

            return false;

        }
        DataCollection<Entity> RetrieveDependentComponents(Guid workflowId, string workflowName)
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


        DataCollection<Entity> RetrieveRequiredComponents(Guid workflowId, string workflowName)
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


        private void tsbCancel_Click(object sender, EventArgs e)
        {
            CancelWorker();
            tsbCancel.Enabled = false;
            MessageBox.Show("Cancelled");
        }

        #endregion Base tool implementation

        #region Github implementation

        public string RepositoryName
        {
            get { return "GithubRepositoryName"; }
        }

        public string UserName
        {
            get { return "GithubUserName"; }
        }

        #endregion Github implementation

        #region CodePlex implementation

        public string CodePlexUrlName
        {
            get { return "CodePlex"; }
        }

        #endregion CodePlex implementation

        #region PayPal implementation

        public string DonationDescription
        {
            get { return "paypal description"; }
        }

        public string EmailAccount
        {
            get { return "paypal@paypal.com"; }
        }

        #endregion PayPal implementation

        #region Help implementation

        public string HelpUrl
        {
            get { return "http://www.google.com"; }
        }

        #endregion Help implementation

        private void tvWorkflows_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DisplayGraph();
        }
        public void DisplayGraph()
        {

            ComponentTreeNode selectedNode = (ComponentTreeNode)tvWorkflows.SelectedNode;
            selectedNode.IsRoot = true;
            TreeBuilder myTree = new TreeBuilder();

            int w = -1;
            int h = -1;

            Image img = Image.FromStream(myTree.GenerateTree(selectedNode, ref w, ref h, "1", System.Drawing.Imaging.ImageFormat.Bmp));
            pbImage.Image = (Image)(new Bitmap(img, new Size(w, h)));

            selectedNode.IsRoot = false;
        }


        private void NavigateToWorkflow()
        {
            ComponentTreeNode selectedNode = (ComponentTreeNode)tvWorkflows.SelectedNode;
            selectedNode.IsRoot = true;
            string id = selectedNode.Id.ToString(); // "6858AA7B-5835-4D7E-B619-62C595281233";
            webBrowser1.Url = new Uri(String.Format("https://crm2016fe2dev.dev.teksavvy.ca/TSR1SP62016/sfa/workflow/edit.aspx?id=%7b{0}%7d", id));

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            /*
            ComponentTreeNode rootNode = (ComponentTreeNode)tvWorkflows.SelectedNode;
            rootNode.IsRoot = true;
            TreeBuilder myTree = new TreeBuilder();
            pbImage.Image = Image.FromStream(myTree.GenerateTree(rootNode, - 1, -1, "1", System.Drawing.Imaging.ImageFormat.Bmp));
            rootNode.IsRoot = false;
            */
        }

        private void btnToggleAssemblies_Click(object sender, EventArgs e)
        {
            Context.HideAssemblies = !Context.HideAssemblies;

            if (Context.HideAssemblies)
                btnToggleAssemblies.Text = "Show Assemblies";
            else
                btnToggleAssemblies.Text = "Hide Assemblies";

            DisplayGraph();

        }
    }
}