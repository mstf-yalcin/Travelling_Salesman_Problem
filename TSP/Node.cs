using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    internal class Node
    {
        public static int id;

        private int x=0;
        private int y=0;
        private int height= 40, width=40;
        private int nodeId;
        private bool visited=false;
        public static int selectId=0;
        public static int isClickCalculute = 0;


        public Node(int x,int y,int nodeId)
        {
           this.x = x;
           this.y = y;
           this.nodeId = nodeId;
        }
    
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public int NodeId
        {
            get { return nodeId; }
            set { nodeId = value; }
        }

        public bool Visited
        {
            get { return visited; }
            set { visited = value; }
        }

        Rectangle rect;
        public void drawNode(Graphics g)
        {
            rect = new Rectangle(x,y,width,height);
            Brush brush;
            Brush brushText;
            Pen pen;
            if (visited==false)
            {
                brush = new SolidBrush(Color.White);
                brushText = new SolidBrush(Color.Black);
                pen = new Pen(SystemColors.ActiveBorder, 5);

            }
            else
            {
                brush = new SolidBrush(Color.Blue);
                pen = new Pen(Color.Blue, 5);
                brushText = new SolidBrush(Color.White);
            }

            Font f = new Font("Times New Roman", 10);

            if (nodeId == selectId)
            {
                g.DrawRectangle(pen, rect);
                g.FillRectangle(brush, rect);
            }
            else
            {
                g.DrawEllipse(pen, rect);
                g.FillEllipse(brush, rect);
            }

            //g.DrawEllipse(pen, rect);
            //g.FillEllipse(brush, rect);


            if (nodeId <= 8)
            {
                g.DrawString((nodeId + 1).ToString(), f, brushText, X + 13, Y + 11);
            }
            else
            {
                g.DrawString((nodeId + 1).ToString(), f, brushText, X+9, Y+11);
            }

            brush.Dispose();

        }
        public static int getNode(List<Node> Nodes,int pointX,int pointY)
        {
           int counter = -1;


            //MessageBox.Show("x,y:"+x+",  "+y+"\n_x,_y:"+_x+",  "+_y);

            int c = 2;
            foreach (Node node in Nodes)
            {
                counter++;
                int _x = node.X / 40;
                int _y = node.Y / 40;

                if (_x == pointX && _y == pointY)
                {
                    return counter;
                }
                else
                {
                    if(_x > pointX)
                    {
                        for (int i = 1; i < c; i++)
                        {
                            if (_x != pointX)
                                _x -= i;
                        }
                    }
                    else if(_x < pointX)
                    {
                        for (int i = 1; i < c; i++)
                        {
                            if (_x != pointX)
                                _x += i;
                        }
                    }

                    if (_y > pointY)
                    {
                        for (int i = 1; i < c; i++)
                        {
                            if (_y != pointY)
                                _y -= i;

                        }
                    }
                    else if(_y < pointY)
                    {
                        for (int i = 1; i < c; i++)
                        {
                            if (_y != pointY)
                                _y += i;
                        }
                    }

                    if (_x == pointX && _y == pointY)
                    {
                        return counter;
                    }

                }
            }

            return -1;
          
        }

        public int getNodeEdge(List<Node> Nodes, int pointX, int pointY)
        {
            int counter = 0;

            int c = 2;
            foreach (Node node in Nodes)
            {
                counter++;
                int _x = node.X / 40;
                int _y = node.Y / 40;

                if (_x == pointX && _y == pointY)
                {
                    return counter;
                }
                else
                {

                    if (_x > pointX)
                    {
                        for (int i = 1; i < c; i++)
                        {
                            if (_x != pointX)
                                _x -= i;
                        }
                    }
                    else if (_x < pointX)
                    {
                        for (int i = 1; i < c; i++)
                        {
                            if (_x != pointX)
                                _x += i;

                        }
                    }

                    if (_y > pointY)
                    {
                        for (int i = 1; i < c; i++)
                        {
                            if (_y != pointY)
                                _y -= i;

                        }
                    }
                    else if (_y < pointY)
                    {
                        for (int i = 1; i < c; i++)
                        {
                            if (_y != pointY)
                                _y += i;

                        }
                    }

                    if (_x == pointX && _y == pointY)
                    {
                        return counter;
                    }

                }
            }

            return -1;
        }

        public static void visitNode(int id, List<Node> Nodes)
        {
            foreach(Node node in Nodes)
            {
                if (node.NodeId == id)
                {
                    node.visited = true;
                    return;
                }
            }
        }


    }

}
