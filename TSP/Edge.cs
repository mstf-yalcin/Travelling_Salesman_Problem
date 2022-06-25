using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    internal class Edge
    {
       private int sourceId;
       private int destinationId;
       private Point sourcePoint;
       private Point destinationPoint;
       private int weight;
       private bool visited = false;

       public Point SourcePoint
        {
            get { return sourcePoint; }
            set { sourcePoint = value; }
        } 
        public Point DestinationPoint
        {
            get { return destinationPoint; }
            set { destinationPoint = value; }
        }
        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public int SourceId
        {
            get { return sourceId; }
            set { sourceId = value; }
        }

        public int DestinationId
        {
            get { return destinationId; }
            set { destinationId = value; }    
        }

        public bool Visited
        { get { return visited; } 
          set { visited = value; } 
        }

        public void DrawLine(Graphics e)
        {

            Brush brush;

            int x = Math.Abs((sourcePoint.X - destinationPoint.X) / 2);
            int y = Math.Abs((destinationPoint.Y - sourcePoint.Y) / 2);

            int newX;
            int newY;
            if (sourcePoint.X>destinationPoint.X)
                newX = Math.Abs(x - sourcePoint.X);
            else
                newX = Math.Abs(x + sourcePoint.X);

            if (sourcePoint.Y>destinationPoint.Y)
                newY = Math.Abs(y - SourcePoint.Y);
            else
                newY = Math.Abs(y + SourcePoint.Y);


            e.SmoothingMode =System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            if(Visited == true)
            {
             brush = new SolidBrush(Color.Red);
             e.DrawLine(new Pen(Color.Green, 5f), sourcePoint, destinationPoint);
             e.DrawString(Weight.ToString(), new Font("Arial", 10), brush, newX + 4, newY + 4);
            }
            else
            {
            brush = new SolidBrush(Color.Black);
            e.DrawLine(new Pen(Color.Red, 2.3f), sourcePoint, destinationPoint);
            e.DrawString(Weight.ToString(), new Font("Arial", 10), brush, newX + 4, newY + 4);
            }
        }

        public static int getWeigth(int start,int dest,List<Edge> edges)
        {

            foreach(Edge e in edges)
            {
                if(e.sourceId==start && e.destinationId==dest || e.sourceId == dest && e.destinationId == start)
                    return e.weight;
            }

            return -1;
        }
        
        public static void visitEdge(int start,int dest, List<Edge> Edges)
        {
            foreach (var e in Edges)
            {
                if(e.sourceId == start && e.destinationId == dest || e.sourceId == dest && e.destinationId == start)
                {
                    e.Visited = true;
                    return;
                }
            }
        }

    
    }
}
