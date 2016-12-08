using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MsCrmTools.WorkflowExplorer
{
    class TreeBuilder
    {

        #region Private Members

        private Color _FontColor = Color.Black;
        private int _BoxWidth = 120;
        private int _BoxHeight = 80; //60;
        private int _Margin = 20;
        private int _HorizontalSpace = 30;
        private int _VerticalSpace = 30;
        private int _FontSize = 9;
        private int imgWidth = 0;
        private int imgHeight = 0;
        private Graphics gr;
        private Color _LineColor = Color.Black;
        private float _LineWidth = 2;
        private Color _WorkflowBoxFillColor = Color.LightGray;
        private Color _VisitedBoxFillColor = Color.Pink;
        private Color _ActionBoxFillColor = Color.LightGreen;
        private Color _PluginBoxFillColor = Color.LightBlue;
        private Color _TitleBoxFillColor = Color.LightGray;
        private Color _BGColor = Color.White;


        public bool ShowAssemblies { get; set; } = true;

        #endregion

        public TreeBuilder()
        {

        }



        public MemoryStream GenerateTree(ComponentTreeNode rootNode, ref int Width, ref int Height, string StartFromNodeID, ImageFormat ImageType)
        {
            rootNode = (ComponentTreeNode)rootNode.DeepClone();
            TreeView tv = new TreeView();
            tv.Nodes.Add(rootNode);
            

            //reset image size
            imgHeight = 0;
            imgWidth = 0;
            MemoryStream Result = new MemoryStream();

            BuildTree(rootNode, 0);

            Bitmap bmp = new Bitmap(imgWidth, imgHeight);
            gr = Graphics.FromImage(bmp);
            gr.Clear(_BGColor);
            DrawChart(rootNode);

            Bitmap ResizedBMP = new Bitmap(bmp, new Size(imgWidth, imgHeight));
            ResizedBMP.Save(Result, ImageType);
            ResizedBMP.Dispose();
            bmp.Dispose();
            gr.Dispose();

            Width = imgWidth;
            Height = imgHeight;

            return Result;

        }


        private void BuildTree(ComponentTreeNode oNode, int y)
        {
            if (!ShowAssemblies && oNode.ComponentType == Component.ComponentTypes.PluginType)
                oNode.IsHidden = true;
                foreach (ComponentTreeNode childNode in oNode.Nodes)
            {
                    BuildTree(childNode, y + 1);
            }

            //build node data
            //after checking for nodes we can add the current node
            int StartX;
            int StartY;
            int[] ResultsArr = new int[] {  GetXPosByOwnChildren(oNode),
                                            GetXPosByParentPreviousSibling(oNode),
                                            GetXPosByPreviousSibling(oNode),
                                            _Margin };
            Array.Sort(ResultsArr);
            StartX = ResultsArr[3];
            StartY = (y * (_BoxHeight + _VerticalSpace)) + _Margin;
            int width = _BoxWidth;
            int height = _BoxHeight;
            //update the coordinates of this box into the matrix, for later calculations
            oNode.X = StartX;
            oNode.Y = StartY;

            //update the image size
            if (imgWidth < (StartX + width + _Margin))
            {
                imgWidth = StartX + width + _Margin;
            }
            if (imgHeight < (StartY + height + _Margin))
            {
                imgHeight = StartY + height + _Margin;
            }

        }

        public Rectangle GetMainRectangle(ComponentTreeNode oNode)
        {
            int X = oNode.X;
            int Y = oNode.Y + 20;

            Rectangle Result = new Rectangle(X, Y, (int)(_BoxWidth), (int)(_BoxHeight) - 20);
            return Result;
        }


        public Rectangle GetTitleRectangle(ComponentTreeNode oNode)
        {
            int X = oNode.X;
            int Y = oNode.Y;

            Rectangle Result = new Rectangle(X, Y, (int)(_BoxWidth), 20);
            return Result;
        }


        private void DrawChart(ComponentTreeNode oNode)
        {
            Console.WriteLine("Drawing component of type {0}", oNode.WorkflowCategory);
            // Create font and brush.
            Font drawFont = new Font("verdana", _FontSize);
            SolidBrush drawBrush = new SolidBrush(_FontColor);
            Pen boxPen = new Pen(_LineColor, _LineWidth);
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            //find children

            foreach (ComponentTreeNode childNode in oNode.Nodes)
            {

                if(!childNode.IsHidden)
                    DrawChart(childNode);
            }

            Rectangle outerRectangle = new Rectangle(oNode.X, oNode.Y, (int)(_BoxWidth), (int)(_BoxHeight));
            Rectangle titleRectangle = new Rectangle(oNode.X, oNode.Y, (int)(_BoxWidth), 18);
            Rectangle detailRectangle = new Rectangle(oNode.X, oNode.Y + 20, (int)(_BoxWidth), (int)(_BoxHeight) - 20);

            gr.DrawRectangle(boxPen, titleRectangle);
            gr.DrawRectangle(boxPen, detailRectangle);

            Console.WriteLine("Drawing Rectangle for Component Type {0}, Name {1}", oNode.WorkflowCategory, oNode.Text);

            Color detailFillColor = GetBoxColor(oNode);
            Color titleFillColor = Color.Silver;
            gr.FillRectangle(new SolidBrush(detailFillColor), detailRectangle);
            gr.FillRectangle(new SolidBrush(titleFillColor), titleRectangle);


            // Draw string to screen.
            gr.DrawString(oNode.PrimaryEntityName, drawFont, drawBrush, titleRectangle, drawFormat);
            gr.DrawString(oNode.Text, drawFont, drawBrush, detailRectangle, drawFormat);

            //draw connecting lines


            if (!oNode.IsRoot) // oNode.Parent != null && 
            {
                //all but the top box should have lines growing out of their top
                gr.DrawLine(boxPen, outerRectangle.Left + (_BoxWidth / 2),
                                    outerRectangle.Top,
                                    outerRectangle.Left + (_BoxWidth / 2),
                                    outerRectangle.Top - (_VerticalSpace / 2));
            }
            if (oNode.Nodes.Count > 0)
            {
                //all nodes which have nodes should have lines coming from bottom down
                gr.DrawLine(boxPen, outerRectangle.Left + (_BoxWidth / 2),
                                    outerRectangle.Top + _BoxHeight,
                                    outerRectangle.Left + (_BoxWidth / 2),
                                    outerRectangle.Top + _BoxHeight + (_VerticalSpace / 2));

            }
            if (!oNode.IsRoot && oNode.PrevNode != null)
            {
                //the prev node has the same boss - connect the 2 nodes
                ComponentTreeNode prevNode = (ComponentTreeNode)oNode.PrevNode;
                Rectangle prevOuterRectangle = new Rectangle(prevNode.X, prevNode.Y, (int)(_BoxWidth), (int)(_BoxHeight));
                gr.DrawLine(boxPen, prevOuterRectangle.Left + (_BoxWidth / 2) - (_LineWidth / 2),
                                    prevOuterRectangle.Top - (_VerticalSpace / 2),
                                    outerRectangle.Left + (_BoxWidth / 2) + (_LineWidth / 2),
                                    outerRectangle.Top - (_VerticalSpace / 2));
            }
        }


        private int GetXPosByPreviousSibling(ComponentTreeNode CurrentNode)
        {
            int Result = -1;
            int X = -1;

            if(!CurrentNode.IsRoot)
            {
                ComponentTreeNode PrevSibling = (ComponentTreeNode)CurrentNode.PrevNode;

                if (PrevSibling != null)
                {
                    if (PrevSibling.Nodes.Count > 0)
                    {
                        X = Convert.ToInt32(GetMaxXOfDescendants((ComponentTreeNode)PrevSibling.LastNode));
                        Result = X + _BoxWidth + _HorizontalSpace;
                    }
                    else
                    {
                        Result = PrevSibling.X + _BoxWidth + _HorizontalSpace;
                    }
                }
            }

            return Result;
        }

        private int GetMaxXOfDescendants(ComponentTreeNode CurrentNode)
        {
            while (CurrentNode.Nodes.Count > 0)
            {
                CurrentNode = (ComponentTreeNode)CurrentNode.LastNode;
            }

            return CurrentNode.X;
        }

        private int GetXPosByOwnChildren(ComponentTreeNode CurrentNode)
        {
            int Result = -1;

            if (CurrentNode.Nodes.Count > 0)
            {
                int lastChildX = ((ComponentTreeNode)CurrentNode.LastNode).X;
                int firstChildX = ((ComponentTreeNode)CurrentNode.FirstNode).X;
                Result = (((lastChildX + _BoxWidth) - firstChildX) / 2) - (_BoxWidth / 2) + firstChildX;


            }
            return Result;
        }

        private int GetXPosByParentPreviousSibling(ComponentTreeNode CurrentNode)
        {
            int Result = -1;
            int X = -1;

            ComponentTreeNode Parent = (ComponentTreeNode)CurrentNode.Parent;

            if (!CurrentNode.IsRoot && Parent != null && !Parent.IsRoot)
            {
                ComponentTreeNode ParentPrevSibling = (ComponentTreeNode)Parent.PrevNode;

                if (ParentPrevSibling != null)
                {
                    if (ParentPrevSibling.Nodes.Count > 0)
                    {

                        X = GetMaxXOfDescendants((ComponentTreeNode)ParentPrevSibling.LastNode);
                        Result = X + _BoxWidth + _HorizontalSpace;
                    }
                    else
                    {

                        X = ParentPrevSibling.X;
                        Result = X + _BoxWidth + _HorizontalSpace;
                    }
                }
                else
                {

                    if (!CurrentNode.IsRoot && CurrentNode.Parent != null) 
                    {
                        Result = GetXPosByParentPreviousSibling((ComponentTreeNode)CurrentNode.Parent);
                    }
                }
            }

            return Result;
        }

        Color GetBoxColor(ComponentTreeNode node)
        {
            Color color = Color.White;

            if (node.ComponentType == Component.ComponentTypes.Entity)
            {
                color = _VisitedBoxFillColor;
            }
            else if (node.ComponentType == Component.ComponentTypes.Workflow)
            {

                switch (node.WorkflowCategory)
                {
                    case Component.WorkflowCategories.Action:
                        color = _ActionBoxFillColor;
                        break;
                    case Component.WorkflowCategories.Workflow:
                        color = _WorkflowBoxFillColor;
                        break;
                    case Component.WorkflowCategories.Dialog:
                        color = Color.Pink;
                        break;
                    case Component.WorkflowCategories.BusinessRule:
                        color = Color.PeachPuff;
                        break;
                }

            }
            else if (node.ComponentType == Component.ComponentTypes.PluginType)
            {
                color = _PluginBoxFillColor;
            }

            return color;
        }
    }
}
