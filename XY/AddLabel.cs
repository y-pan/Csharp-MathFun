using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathFun
{
    class AddLabel
    {
        public PointF P { get; set; }//RU_value
        public PointF C { get; set; }

        //RU-value: x, y   <-----  relative: (x*U,y*U) -----> abs: (Origin.X + x*U, Orirgin.Y - y*U) 
        public AddLabel(PointF pointToLocate, PointF origin, Panel canvas, string textToShow = "")
        {
            P = pointToLocate;//RU
            C = origin;

            Label lb = new Label();
            lb.AutoSize = true;
            lb.Location = new Point((int)(C.X + P.X * Unit.X + 8), (int)(C.Y - P.Y * Unit.Y - lb.Height / 3));//abs-values
            if (textToShow == "")// empty for single point
            {
                lb.Text = string.Format("({0},{1})", P.X, P.Y);
            }
            else  //some string for line
            {
                lb.Text = textToShow;
            }
            lb.Parent = canvas;            
        }

        public AddLabel(float x1, float y1, float originX, float originY, Panel canvas, string textToShow = "")
        {
            P = new PointF(x1, y1);
            C = new PointF(originX, originY);

            Label lb = new Label();
            lb.AutoSize = true;
            lb.Location = new Point((int)(originX + P.X * Unit.X + 8), (int)(C.Y - P.Y * Unit.Y - lb.Height / 3));
            if (textToShow == "")// empty for single point
            {
                lb.Text = string.Format("({0},{1})", P.X, P.Y);
            }
            else  //some string for line
            {
                lb.Text = textToShow;
            }
            lb.Parent = canvas;         
        }
    }
}
