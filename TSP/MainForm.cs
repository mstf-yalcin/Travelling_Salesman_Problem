using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TSP
{
    internal class MainForm:Form
    {
        PictureBox pictureBox;

        ListView matrisListView;
        ListView resultListView;

        ComboBox comboBoxSource;
        ComboBox comboBoxDestination;

        TextBox totalCostBox;

        GroupBox groupBox1;

        Label graphLabel;
        Label matrixLabel;
        Label sourceLabel;
        Label resultLabel;
        Label destinationLabel;
        Label l1;

        Button calculateBtn;
        Button clearBtn;
        Button clearAnmBtn;

        List<Node> Nodes = new List<Node>();
        public List<Edge> Edges = new List<Edge>();

        public int[,] matrisArray;

        Point sourcePoint;
        Point destinationPoint;
        int sourceId;
        int destinationId;


        Node n1;
        Edge edge;
        bool isClick = false;
        public static int counter = 0;
        Graphics g;
        Point lastPoint; 

        public MainForm()
        {
            Width = 1440;
            Height = 900;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Yazlab";
            BackColor = Color.White;    

            pictureBox = new PictureBox();
            pictureBox.Width = 1000;
            pictureBox.Height = 800;
            pictureBox.SetBounds(10, 40, 900, 800);
            pictureBox.BackColor = Color.LightYellow;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(pictureBox);

            graphLabel = new Label();
            graphLabel.Font = new Font("Segoe UI Symbol", 10, FontStyle.Bold);
            graphLabel.Text = "- Graph View -";
            graphLabel.SetBounds(10, 10, 500, 200);
            Controls.Add(graphLabel);

            matrisListView = new ListView();
            matrisListView.SetBounds(915, 40, 500, 300);
            matrisListView.View = View.Details;
            matrisListView.GridLines = true;
            Controls.Add(matrisListView);

            matrixLabel = new Label();
            matrixLabel.Font = new Font("Segoe UI Symbol", 10, FontStyle.Bold);
            matrixLabel.Text = "- Matrix -";
            matrixLabel.SetBounds(910, 10, 500, 200);
            Controls.Add(matrixLabel);

            groupBox1 = new GroupBox();
            groupBox1.SetBounds(915, 350, 500, 180);
            groupBox1.Paint += groupBox1_Paint;
            groupBox1.Text = " Calculate ";
            groupBox1.Font = new Font("Segoe UI Symbol", 10, FontStyle.Bold);
            Controls.Add(groupBox1);

            comboBoxSource = new ComboBox();
            comboBoxSource.SelectedValueChanged += ComboBoxSource_SelectedValueChanged;
            comboBoxSource.SetBounds(95, 70, 121, 28);
            groupBox1.Controls.Add(comboBoxSource);

            //comboBoxDestination = new ComboBox();
            //comboBoxDestination.SetBounds(295, 70, 121, 28);
            //groupBox1.Controls.Add(comboBoxDestination);

            totalCostBox=new TextBox();
            totalCostBox.SetBounds(295, 70, 121, 28);
            totalCostBox.ReadOnly = true;
            groupBox1.Controls.Add(totalCostBox);

            l1 = new Label();
            l1.Text = "--";
            l1.Font = new Font("", 11);
            l1.SetBounds(243, 70, 40, 40);
            groupBox1.Controls.Add(l1);

            sourceLabel = new Label();
            sourceLabel.Text = "Source";
            sourceLabel.Font = new Font("", 9);
            sourceLabel.SetBounds(90, 45, 90, 50);
            groupBox1.Controls.Add(sourceLabel);


            destinationLabel = new Label();
            destinationLabel.Text = "Total-Cost";
            destinationLabel.Font = new Font("", 9);
            destinationLabel.SetBounds(290, 45, 90, 50);
            groupBox1. Controls.Add(destinationLabel);

            calculateBtn = new Button();
            calculateBtn.Text = "Calculate";
            calculateBtn.SetBounds(40, 130, 130, 35);
            groupBox1.Controls.Add(calculateBtn);

            clearBtn = new Button();
            clearBtn.Text = "Clear";
            clearBtn.SetBounds(190, 130, 130, 35);
            groupBox1.Controls.Add(clearBtn);

            clearAnmBtn=new Button();
            clearAnmBtn.Text = "Clear-Anm";
            clearAnmBtn.SetBounds(340, 130, 130, 35);
            groupBox1.Controls.Add(clearAnmBtn);

            resultLabel = new Label();
            resultLabel.Font = new Font("Segoe UI Symbol", 10, FontStyle.Bold);
            resultLabel.Text = "- Result -";
            resultLabel.SetBounds(910, 545, 500, 20);
            Controls.Add(resultLabel);

            resultListView = new ListView();
            resultListView.SetBounds(915, 580, 500, 260);
            resultListView.View = View.Details;
            resultListView.GridLines = true;
            Controls.Add(resultListView);

            resultListView.Columns.Add("Queue", 124);
            resultListView.Columns.Add("Source", 124);
            resultListView.Columns.Add("Destination", 124);
            resultListView.Columns.Add("Weight", 124);


            clearBtn.Click += ClearBtn_Click;
            calculateBtn.Click += CalculateBtn_Click;
            clearAnmBtn.Click += ClearAnmBtn_Click;

            pictureBox.Paint += PictureBox_Paint;
            //pictureBox.MouseClick += P1_MouseClick;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseUp += PictureBox_MouseUp;
            pictureBox.MouseMove += PictureBox_MouseMove;

        }

        private void ClearAnmBtn_Click(object? sender, EventArgs e)
        {
            foreach(Node node in Nodes)
                node.Visited = false;
            foreach(Edge edge in Edges)
                edge.Visited = false;

            pictureBox.Invalidate();

        }

        private void ComboBoxSource_SelectedValueChanged(object? sender, EventArgs e)
        {
            Node.selectId = comboBoxSource.SelectedIndex;
            pictureBox.Invalidate();
        }

        private void CalculateBtn_Click(object? sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(comboBoxSource.Text))
            {
                MessageBox.Show("Please select source node!");
                return;
            }

            Node.isClickCalculute = 1;

            resultListView.Clear();
            resultListView.Columns.Add("Queue", 124);
            resultListView.Columns.Add("Source", 124);
            resultListView.Columns.Add("Destination", 124);
            resultListView.Columns.Add("Weight", 124);

            int startNode = int.Parse(comboBoxSource.Text) - 1;

            Solve solver = new Solve(startNode, matrisArray);
            string s = "";

            int[] path = new int[counter+1];
            int pathCount = 0;
            foreach (var item in solver.getTour())
            {
                s +=","+ (item+1).ToString();
                path[pathCount] = (item+1);
                pathCount++;
            }
       
            for (int i = 0; i < path.Length; i++)
            {
                ListViewItem item = new ListViewItem();
                resultListView.Items.Add(item);
            }
            pathCount = 1;
            foreach (Node node in Nodes)
            {
                //MessageBox.Show("Next");
                Task.Delay(600).Wait();
                pictureBox.Invalidate();

                Node.visitNode(path[pathCount - 1] - 1, Nodes);
                Edge.visitEdge(path[pathCount - 1]-1, path[pathCount]-1, Edges);
                pathCount++;

                if (pathCount == path.Length)
                    break;

            }
           
            //**************************************************

            pathCount = 1;
            int totalcost = 0;
            int weight = 0;
            foreach (ListViewItem item in resultListView.Items)
            {
                item.SubItems.Clear();
                item.Text = pathCount.ToString();
                item.SubItems.Add(path[pathCount-1].ToString());
                item.SubItems.Add(path[pathCount].ToString());

                weight = Edge.getWeigth((path[pathCount - 1] - 1), (path[pathCount] - 1), Edges);
                totalcost += weight;

                item.SubItems.Add(weight.ToString());
                item.SubItems.Add(pathCount.ToString());

                pathCount++;
                if (pathCount == path.Length)
                    break;
            }

            totalCostBox.Text=totalcost.ToString();
        }

        private void ClearBtn_Click(object? sender, EventArgs e)
        {
            matrisListView.Clear();
            resultListView.Clear();
            resultListView.Columns.Add("Queue", 124);
            resultListView.Columns.Add("Source", 124);
            resultListView.Columns.Add("Destination", 124);
            resultListView.Columns.Add("Weight", 124);

            matrisArray = null;
            Edges.Clear();
            Nodes.Clear();
            n1 = null;
            edge = null;
            //comboBoxDestination.SelectedIndex = -1;
            comboBoxSource.SelectedIndex = -1;
            comboBoxSource.Items.Clear();
            totalCostBox.Clear();
            //comboBoxDestination.Items.Clear();
            Node.id = 0;
            counter = 0;
            Node.selectId = 0;
            Node.isClickCalculute = 0;
            matrisListView.Items.Clear();
            pictureBox.Invalidate();
            sourceId = -1;
            destinationId = -1;
            nodeId = -1;
 
        }

        private void PictureBox_MouseMove(object? sender, MouseEventArgs e)
        {
            Text ="Yazlab | "+ lastPoint.X+","+lastPoint.Y+ " | " + e.X + "," + e.Y;
            if (isClick == true)
            {
                destinationPoint = new Point(e.X, e.Y);
                pictureBox.Invalidate();
            }

        }

        private void PictureBox_MouseUp(object? sender, MouseEventArgs e)
        {
            int x = e.X / 40;
            int y = e.Y / 40;

            destinationId = Node.getNode(Nodes, x, y);
            int weight=0;

            if (destinationId!=-1 && sourceId!=-1 && destinationId != sourceId && isClick==true)
            {

                destinationPoint = new Point(Nodes[destinationId].X + 18, Nodes[destinationId].Y + 18);//centered

                int indexCount = 0;
                int indexControl = -1;
                if (Edges.Count!=0)
                {

                    foreach (Edge item in Edges)
                    {
                        if((item.SourceId==sourceId || item.SourceId == destinationId) && (item.DestinationId==sourceId || item.DestinationId==destinationId))
                        {
                            indexControl = 1;
                            break;
                        }
                        indexCount++;
                    }

                }

                //var index =
                //    from item in Edges
                //    where (item.SourceId == sourceId || item.SourceId == destinationId) && (item.DestinationId == sourceId || item.DestinationId == destinationId)
                //    select item;

                using(var Frm = new InputDialog())
                {
                    Frm.ShowDialog();
                    if (Frm.DialogResult == DialogResult.OK)
                        weight = int.Parse(Frm.distance);
                }


                if (indexControl != -1 && weight!=0 )
                {
                    Edges.Remove(Edges[indexCount]);

                    edge = new Edge();
                    edge.SourcePoint = sourcePoint;
                    edge.DestinationPoint = destinationPoint;
                    edge.SourceId = sourceId;
                    edge.DestinationId = destinationId;

                    edge.Weight = weight;

                    matrisArray[sourceId, destinationId]=weight;
                    matrisArray[destinationId, sourceId] = weight;

                    drawMatrix();

                    Edges.Add(edge);
                }
                else if(weight!=0)
                {
                    edge = new Edge();
                    edge.SourcePoint = sourcePoint;
                    edge.DestinationPoint = destinationPoint;
                    edge.SourceId = sourceId;
                    edge.DestinationId = destinationId;

                    edge.Weight = weight;

                    matrisArray[sourceId, destinationId] = weight;
                    matrisArray[destinationId, sourceId] = weight;

                    drawMatrix();

                    Edges.Add(edge);
                }

                sourcePoint = new Point(0, 0);
                destinationPoint = new Point(0, 0);

                pictureBox.Invalidate();

                isClick = false;
            }
            else
            {
                sourcePoint = new Point(0,0);
                destinationPoint = new Point(0, 0);
                pictureBox.Invalidate();

                isClick = false;

            }

        }

        int nodeId=-1;
        private void PictureBox_MouseDown(object? sender, MouseEventArgs e)
        {
            int x = e.X / 40;
            int y = e.Y / 40;

            if(Nodes.Count>0)
                nodeId = Node.getNode(Nodes, x, y);


            if(nodeId==-1)
            {
                n1 = new Node(e.X - 18, e.Y - 18, counter);
               
                lastPoint=new Point(e.X, e.Y);
                Node.id++;
                Nodes.Add(n1);

                pictureBox.Invalidate();
                counter++;

                comboBoxSource.Items.Add(counter.ToString());
                //comboBoxDestination.Items.Add(counter.ToString());

                matrisArray = createMatrix(counter);

                drawMatrix();

            }
            else
            {

                sourcePoint = new Point( Nodes[nodeId].X+18,Nodes[nodeId].Y+18);
                sourceId = nodeId;
                isClick = true;
            }
           
        }


        private void PictureBox_Paint(object? sender, PaintEventArgs e)
        {
            g = e.Graphics;
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            var pen = new Pen(Color.DarkBlue, 2)
            {
                DashPattern = new[]
                        {
                        4f,
                        4f,
                        4f,
                        4f
                    },
            };

            var brush = new SolidBrush(Color.Black);
            g.DrawLine(pen, sourcePoint, destinationPoint);

            foreach (var edge in Edges)
            {
                edge.DrawLine(g);
            }

            foreach (var node in Nodes)
            {
                node.drawNode(g);
            }

        }

        private int[,] createMatrix(int count)
        {

            var matrix = new int[count, count];

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    matrix[i, j] = 999;
                }
            }
            if (matrisArray != null)
            {
                for (int i = 0; i < matrisArray.GetLength(0); i++)
                {
                    for (int j = 0; j < matrisArray.GetLength(0); j++)
                    {
                        if (matrisArray[i, j] != 0 || matrisArray[i, j] != 999)
                            matrix[i, j] = matrisArray[i, j];
                    }
                }
            }
                
            return matrix;
        }

        private void drawMatrix()
        {
            matrisListView.Clear();
            matrisListView.Columns.Add("", 40);

            for (int i = 1; i <= counter; i++)
                matrisListView.Columns.Add(i.ToString(), 40);

            for (int i = 1; i <= counter; i++)
            {
                ListViewItem item = new ListViewItem();
                matrisListView.Items.Add(item);
            }

            int k = 1;
            foreach (ListViewItem item in matrisListView.Items)
            {
                item.SubItems.Clear();
                item.Text = k.ToString();
                for (int i = 0; i <= counter; ++i)
                {

                    item.SubItems.Add("-");
                }
                ++k;
            }

            for (int i = 0; i < counter; i++)
            {
                ListViewItem weight = new ListViewItem();

                for (int j = 0; j < counter; j++)
                {
                    if (matrisArray[i, j] != 999)
                        matrisListView.Items[i].SubItems[j + 1].Text = matrisArray[i,j].ToString();

                }
                matrisListView.Items.Add(weight);
            }

        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            GroupBox box = sender as GroupBox;
            DrawGroupBox(box, e.Graphics, Color.Black, Color.Black);
        }
        private void DrawGroupBox(GroupBox box, Graphics g, Color textColor, Color borderColor)
        {
            if (box != null)
            {
                Brush textBrush = new SolidBrush(textColor);
                Brush fillBrush = new SolidBrush(Color.White);

                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new Pen(borderBrush);
                Pen fillPen = new Pen(fillBrush);

                SizeF strSize = g.MeasureString(box.Text, box.Font);
                Rectangle rect = new Rectangle(box.ClientRectangle.X,
                                               box.ClientRectangle.Y + (int)(strSize.Height / 2),
                                               box.ClientRectangle.Width - 1,
                                               box.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);

                // Clear text and border
                g.Clear(this.BackColor);
                // Draw text
                g.DrawString(box.Text, box.Font, textBrush, box.Padding.Left, 0);
                // Drawing Border
                //Left
                g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                //Right
                g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Bottom
                g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Top1
                g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + box.Padding.Left, rect.Y));
                //Top2
                g.DrawLine(borderPen, new Point(rect.X + box.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));

                //g.DrawRectangle(fillPen, rect);
                //g.FillRectangle(fillBrush, rect);
            }
        }

    }
}
