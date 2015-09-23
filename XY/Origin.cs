using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathFun
{
    class Origin
    {
        public static float X { get; set; }
        public static float Y { get; set; }
       
        public Origin(float x, float y)
        {
            X = x;
            Y = y;           
        }
    }
}
