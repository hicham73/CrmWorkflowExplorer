namespace MsCrmTools.WorkflowExplorer
{
    partial class WorkflowExplorer
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkflowExplorer));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLoadWorkflows = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.tvWorkflows = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabDiagramme = new System.Windows.Forms.TabPage();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnToggleAssemblies = new System.Windows.Forms.ToolStripButton();
            this.toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabDiagramme.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator2,
            this.btnLoadWorkflows,
            this.toolStripSeparator1,
            this.toolStripSeparator3,
            this.toolStripButton1,
            this.tsbCancel});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(1143, 27);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(24, 24);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // btnLoadWorkflows
            // 
            this.btnLoadWorkflows.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadWorkflows.Image")));
            this.btnLoadWorkflows.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadWorkflows.Name = "btnLoadWorkflows";
            this.btnLoadWorkflows.Size = new System.Drawing.Size(116, 24);
            this.btnLoadWorkflows.Text = "Load Workflows";
            this.btnLoadWorkflows.ToolTipText = "Perfomrs a Who I Am request";
            this.btnLoadWorkflows.Click += new System.EventHandler(this.tsbWhoAmI_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 24);
            // 
            // tsbCancel
            // 
            this.tsbCancel.Enabled = false;
            this.tsbCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancel.Image")));
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(67, 24);
            this.tsbCancel.Text = "Cancel";
            this.tsbCancel.ToolTipText = "Cancel the current request";
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // tvWorkflows
            // 
            this.tvWorkflows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvWorkflows.ImageIndex = 0;
            this.tvWorkflows.ImageList = this.imageList1;
            this.tvWorkflows.Location = new System.Drawing.Point(0, 0);
            this.tvWorkflows.Name = "tvWorkflows";
            this.tvWorkflows.SelectedImageIndex = 0;
            this.tvWorkflows.Size = new System.Drawing.Size(315, 563);
            this.tvWorkflows.TabIndex = 5;
            this.tvWorkflows.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvWorkflows_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Letter-E-blue-icon.png");
            this.imageList1.Images.SetKeyName(1, "Letter-W-black-icon.png");
            this.imageList1.Images.SetKeyName(2, "Letter-A-lg-icon.png");
            this.imageList1.Images.SetKeyName(3, "Letter-P-red-icon.png");
            this.imageList1.Images.SetKeyName(4, "Letter-D-violet-icon.png");
            this.imageList1.Images.SetKeyName(5, "Letter-R-pink-icon.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvWorkflows);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Size = new System.Drawing.Size(1143, 563);
            this.splitContainer1.SplitterDistance = 315;
            this.splitContainer1.TabIndex = 6;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabDiagramme);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(824, 563);
            this.tabControl.TabIndex = 1;
            // 
            // tabDiagramme
            // 
            this.tabDiagramme.AutoScroll = true;
            this.tabDiagramme.Controls.Add(this.pbImage);
            this.tabDiagramme.Controls.Add(this.toolStrip1);
            this.tabDiagramme.Location = new System.Drawing.Point(4, 22);
            this.tabDiagramme.Name = "tabDiagramme";
            this.tabDiagramme.Padding = new System.Windows.Forms.Padding(3);
            this.tabDiagramme.Size = new System.Drawing.Size(816, 537);
            this.tabDiagramme.TabIndex = 0;
            this.tabDiagramme.Text = "Diagramme";
            this.tabDiagramme.UseVisualStyleBackColor = true;
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(0, 31);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(495, 371);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            this.pbImage.Click += new System.EventHandler(this.pbImage_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnToggleAssemblies});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(810, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnToggleAssemblies
            // 
            this.btnToggleAssemblies.Image = ((System.Drawing.Image)(resources.GetObject("btnToggleAssemblies.Image")));
            this.btnToggleAssemblies.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnToggleAssemblies.Name = "btnToggleAssemblies";
            this.btnToggleAssemblies.Size = new System.Drawing.Size(114, 22);
            this.btnToggleAssemblies.Text = "Hide Assemblies";
            this.btnToggleAssemblies.ToolTipText = "Show Assemblies";
            this.btnToggleAssemblies.Click += new System.EventHandler(this.btnToggleAssemblies_Click);
            // 
            // WorkflowExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "WorkflowExplorer";
            this.Size = new System.Drawing.Size(1143, 590);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabDiagramme.ResumeLayout(false);
            this.tabDiagramme.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnLoadWorkflows;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TreeView tvWorkflows;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabDiagramme;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnToggleAssemblies;
    }
}
