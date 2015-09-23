using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
//Introduction: This app is to draw points, lines, and complex functions. All values passed into methods are relative values(normal values for user)
//the built method g.DrawLine() using absolute values
//RU-value: x, y   <-----  relative: (x*Unit.X, y*Unit.Y) -----> abs: (Origin.X + x*Unit.X, Orirgin.Y - y*Unit.Y)   

namespace MathFun
{
    public partial class MathFun : Form
    {
        
        Graphics g;
        Pen blackPen;
        bool IsOriginSetted = false;
        bool isHelpOn = false;

        public MathFun()
        {
            MessageBox.Show("Welcome to use this product. It's not perfect but interesting : )");
            InitializeComponent();
            new Unit();
        }
        private void canvas_Resize(object sender, EventArgs e)
        {
            btnGo.PerformClick();
        }
        private void btnSetDefault_Click(object sender, EventArgs e)
        {
            canvas.Controls.Clear();

            IsOriginSetted = false;
            Origin.X = canvas.Width / 2;
            Origin.Y = canvas.Height / 2;
         
            new Unit();
            btnGo.PerformClick();
            //canvas.Refresh();
        }       
        private void btnOriginIncrease_Click(object sender, EventArgs e)
        {           
            float value = 0;
            float.TryParse(tbOriginSet.Text, out value);
            if (value != 0)
            {
                canvas.Controls.Clear();

                if (rbX.Checked == true) Origin.X += value * Unit.X;
                else Origin.Y -= value * Unit.Y;
                IsOriginSetted = true;
                
                btnGo.PerformClick();
                //canvas.Refresh();
            }
        }
        private void btnOriginDecrease_Click(object sender, EventArgs e)
        {
            float value = 0;
            float.TryParse(tbOriginSet.Text, out value);
            if (value != 0)
            {
                canvas.Controls.Clear();

                if (rbX.Checked == true) Origin.X -= value * Unit.X;
                else Origin.Y += value * Unit.Y;
                IsOriginSetted = true;

                btnGo.PerformClick();
                //canvas.Refresh();
            }
        }
        private void btnUnitTimesBy_Click(object sender, EventArgs e)
        {
            float value = 0;
            float.TryParse(tbUnitSet.Text, out value);
            if(value > 0 && value != 1)
            {
                canvas.Controls.Clear();

                if (rbUnitX.Checked == true) Unit.X *= value;
                else Unit.Y *= value;
                
                btnGo.PerformClick();
                //canvas.Refresh();
            }            
        }
        private void btnUnitDividBy_Click(object sender, EventArgs e)
        {
            float value = 0;
            float.TryParse(tbUnitSet.Text, out value);
            if (value > 0 && value != 1)
            {
                canvas.Controls.Clear();

                if (rbUnitX.Checked == true) Unit.X /= value;
                else Unit.Y /= value;

                btnGo.PerformClick();
                //canvas.Refresh();
            }            
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {            
            //draw x,y Axis
            if (IsOriginSetted == false)
            {
                Origin.X = canvas.Width / 2;
                Origin.Y = canvas.Height / 2;
            }
            
            g = canvas.CreateGraphics();
            blackPen = new Pen(Color.Black, 1f);
            g.DrawLine(blackPen, 0, Origin.Y, canvas.Width, Origin.Y);
            g.DrawLine(blackPen, Origin.X, 0, Origin.X, canvas.Height);

            //define and draw unitX, unitmark
            
            int unitDividend = 10;
            int count = 0;
            int heightOfMark = 10;
            // marks on x
            for (float x = Origin.X; x <= canvas.Width; x += Unit.X / unitDividend) //x: relative value without U
            {
                if (x != Origin.X)
                {
                    if (count % 10 == 0)
                    {
                        g.DrawLine(blackPen, x, Origin.Y, x, Origin.Y - heightOfMark); // whole 
                        
                        //label
                        string text = ((int)((x - Origin.X)/Unit.X)).ToString();
                        Label l = new Label();
                        l.Text = text;
                        l.AutoSize = true;                        
                        l.Location = new Point((int)(x - l.Width /15), (int)(Origin.Y + l.Height/5));
                        l.Parent = canvas;                       
                    }
                    else if (count % 5 == 0) g.DrawLine(blackPen, x, Origin.Y, x, Origin.Y - heightOfMark * 2 / 3);//half
                    else g.DrawLine(blackPen, x, Origin.Y, x, Origin.Y - heightOfMark / 3);//min
                }
                count++;
            }
            count = 0;
            for (float x = Origin.X; x >= 0; x -= Unit.X / unitDividend)
            {
                if (x != 0)
                {
                    if (count % 10 == 0)
                    {
                        g.DrawLine(blackPen, x, Origin.Y, x, Origin.Y - heightOfMark); // 1u, 2u, 3u  

                        //label
                        string text = ((int)((x - Origin.X) / Unit.X)).ToString();
                        if (text != "0")
                        {
                            Label l = new Label();
                            l.Text = text;
                            l.AutoSize = true;
                            l.Location = new Point((int)(x - l.Width / 11), (int)(Origin.Y + l.Height / 5));
                            l.Parent = canvas;
                        } 
                    }
                    else if (count % 5 == 0) g.DrawLine(blackPen, x, Origin.Y, x, Origin.Y - heightOfMark * 2 / 3);
                    else g.DrawLine(blackPen, x, Origin.Y, x, Origin.Y - heightOfMark / 3);
                }
                count++;
            }

            //define and draw unitY, unitmarks
            count = 0;
            for (float y = Origin.Y; y <= canvas.Height; y += Unit.Y / unitDividend)
            {
                if (y != Origin.Y)
                {
                    if (count % 10 == 0)
                    {
                        g.DrawLine(blackPen, Origin.X, y, Origin.X + heightOfMark, y); // 1u, 2u, 3u  

                         //label
                        string text = ((int)((Origin.Y - y) / Unit.Y)).ToString();
                        if (text != "0")
                        {
                            Label l = new Label();
                            l.Text = text;
                            l.AutoSize = true;
                            l.Location = new Point((int)(Origin.X - l.Width / 3),(int)(y - l.Height /5));
                            l.Parent = canvas;
                        }
                    }
                    else if (count % 5 == 0) g.DrawLine(blackPen, Origin.X, y, Origin.X + heightOfMark * 2 / 3, y);
                    else g.DrawLine(blackPen, Origin.X, y, Origin.X + heightOfMark / 3, y);
                }
                count++;
            }
            count = 0;
            for (float y = Origin.Y; y >= 0; y -= Unit.Y / unitDividend)
            {
                if (y != Origin.Y)
                {
                    if (count % 10 == 0)
                    {
                        g.DrawLine(blackPen, Origin.X, y, Origin.X + heightOfMark, y); // 1u, 2u, 3u  

                        //label
                        string text = ((int)((Origin.Y - y) / Unit.Y)).ToString();
                        if (text != "0")
                        {
                            Label l = new Label();
                            l.Text = text;
                            l.AutoSize = true;
                            l.Location = new Point((int)(Origin.X - l.Width / 3), (int)(y - l.Height / 5));
                            l.Parent = canvas;
                        }
                    }
                    else if (count % 5 == 0) g.DrawLine(blackPen, Origin.X, y, Origin.X + heightOfMark * 2 / 3, y);
                    else g.DrawLine(blackPen, Origin.X, y, Origin.X + heightOfMark / 3, y);
                }
                count++;
            }
        }



        private void btnGo_Click(object sender, EventArgs e)
        {
            canvas.Controls.Clear();
            canvas.Refresh();
            try
            {
                if (cbSimple.Checked)
                {
                    float a, b;
                    string description;
                    char operendB;
                    a = float.Parse(tb1_a.Text);
                    b = float.Parse(tb1_b.Text);
                    operendB = (b >= 0) ? '+' : '-';
                    description = string.Format("y={0}x{1}{2}", a, operendB, Math.Abs(b));
                    
                    DrawSimpleEquation(a, b, description);
                }

                if(cbQuadratic.Checked)
                {
                    float a, b, c;
                    string description;
                    char operendB,operendC;
                    a = float.Parse(tb2_a.Text);
                    b = float.Parse(tb2_b.Text);
                    c = float.Parse(tb2_c.Text);
                    operendB = (b>=0)?'+':'-';
                    operendC = (c>=0)?'+':'-';
                    description = string.Format("y={0}x^2 {1}{2}x {3}{4}", a, operendB, b, operendC,c);

                    DrawQuadraticEquation(a, b, c, description);
                }

                if(cbPoint.Checked)
                {
                    float x, y;
                    string description;
                    x = float.Parse(tbPointX.Text);
                    y = float.Parse(tbPointY.Text);
                    description = string.Format("Point({0},{1})", x, y);

                    DrawPoint(x, y, description,Color.Blue, 2, 2);
                }

                if(cbSin.Checked)
                {
                    float a, b, c;
                    string description;
                    char operendC;
                    a = float.Parse(tbSin_a.Text);
                    b = float.Parse(tbSin_b.Text);
                    c = float.Parse(tbSin_c.Text);
                    operendC = (c >= 0) ? '+' : '-';
                    description = string.Format("y={0}Sin({1}x+{2}{3})", a, b, operendC, c);
                    DrawSin(a, b, c);
                    //AddLabel ?
                }

                if(cbCos.Checked)
                {
                    {
                        float a, b, c;
                        string description;
                        char operendC;
                        a = float.Parse(tbCos_a.Text);
                        b = float.Parse(tbCos_b.Text);
                        c = float.Parse(tbCos_c.Text);
                        operendC = (c >= 0) ? '+' : '-';
                        description = string.Format("y={0}Cos({1}x+{2}{3})", a, b, operendC, c);
                        DrawCos(a, b, c);
                        //AddLabel ?
                    }
                }

                if (cbTan.Checked)
                {
                    {
                        float a, b, c;
                        string description;
                        char operendC;
                        a = float.Parse(tbTan_a.Text);
                        b = float.Parse(tbTan_b.Text);
                        c = float.Parse(tbTan_c.Text);
                        operendC = (c >= 0) ? '+' : '-';
                        description = string.Format("y={0}Tan({1}x+{2}{3})", a, b, operendC, c);
                        DrawTan(a, b, c);
                        //AddLabel ?
                    }
                }

                if (cbCot.Checked)
                {
                    {
                        float a, b, c;
                        string description;
                        char operendC;
                        a = float.Parse(tbCot_a.Text);
                        b = float.Parse(tbCot_b.Text);
                        c = float.Parse(tbCot_c.Text);
                        operendC = (c >= 0) ? '+' : '-';
                        description = string.Format("y={0}Cot({1}x+{2}{3})", a, b, operendC, c);
                        DrawCot(a, b, c);
                        //AddLabel ?
                    }
                }

                if(cbNew.Checked)
                {
                    double x, y = 0, maxX, step = 0.005;
                    maxX = (canvas.Width - Origin.X) / Unit.X;
                    for (x = (0 - Origin.X) / Unit.X; x <= maxX; x += step )
                    { 
                        foreach (var v in newXs)
                        {
                            if((string)v.Tag == "Sin")
                            {
                                y += double.Parse(v.Text) * Math.Sin(x);
                            }
                            else
                            {
                                y += double.Parse(v.Text) * Math.Pow(x, Convert.ToDouble(v.Tag));
                            }                            

                        }
                        DrawPoint((float)x, (float)y);
                        y = 0;
                    }                       
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        int newXPos =  166, newYPos = 115, newXCount = 0;
        List<TextBox> newXs = new List<TextBox>();
        List<Label> newLabels = new List<Label>();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //generate object 
            switch(cmb.Text)
            {
                case "X":
                    TextBox newX1 = new TextBox();
                    newX1.Name = "newX1_" + newXCount.ToString();
                    newX1.Tag = "1";
                    newX1.Text = "1";
                    newX1.Size = new System.Drawing.Size(30,30);
                    newX1.Location = new Point(newXPos, newYPos);
                    newX1.TextAlign = HorizontalAlignment.Right;
                    newX1.Parent = gbInput;
                    newXPos += newX1.Size.Width;
                    newXCount++;
                    newXs.Add(newX1);

                    Label lb1 = new Label();
                    lb1.Location = new Point(newXPos, newYPos + 3);
                    lb1.Size = new System.Drawing.Size(25, 30);
                    lb1.Text = "x +";
                    lb1.Parent = gbInput;
                    newXPos += lb1.Size.Width;
                    newLabels.Add(lb1);
                    break;

                case "X^2":
                    TextBox newX2 = new TextBox();
                    newX2.Name = "newX2_" + newXCount.ToString();
                    newX2.Tag = "2";
                    newX2.Text = "1"; 
                    newX2.Size = new System.Drawing.Size(30, 30);                    
                    newX2.Location = new Point(newXPos, newYPos);
                    newX2.TextAlign = HorizontalAlignment.Right;
                    newX2.Parent = gbInput;
                    newXPos += newX2.Size.Width;
                    newXs.Add(newX2);
                    newXCount++;

                    Label lb2 = new Label();
                    lb2.Location = new Point(newXPos, newYPos + 3);
                    lb2.Size = new System.Drawing.Size(35, 30);
                    lb2.Text = "x^2 +";
                    lb2.Parent = gbInput;
                    newXPos += lb2.Size.Width;
                    newLabels.Add(lb2);
                    break;

                case "X^3":
                    TextBox newX3 = new TextBox();
                    newX3.Name = "newX3_" + newXCount.ToString();
                    newX3.Tag = "3";
                    newX3.Text = "1";
                    newX3.Size = new System.Drawing.Size(30, 30);
                    newX3.Location = new Point(newXPos, newYPos);
                    newX3.TextAlign = HorizontalAlignment.Right;
                    newX3.Parent = gbInput;
                    newXPos += newX3.Size.Width;
                    newXs.Add(newX3);
                    newXCount++;

                    Label lb3 = new Label();
                    lb3.Location = new Point(newXPos, newYPos + 3);
                    lb3.Size = new System.Drawing.Size(35, 30);
                    lb3.Text = "x^3 +";
                    lb3.Parent = gbInput;
                    newXPos += lb3.Size.Width;
                    newLabels.Add(lb3);
                    break;

                case "SinX":
                    TextBox newXSin = new TextBox();
                    newXSin.Name = "newXSin_" + newXCount.ToString();
                    newXSin.Tag = "Sin";
                    newXSin.Text = "1";
                    newXSin.Size = new System.Drawing.Size(30, 30);
                    newXSin.Location = new Point(newXPos, newYPos);
                    newXSin.TextAlign = HorizontalAlignment.Right;
                    newXSin.Parent = gbInput;
                    newXPos += newXSin.Size.Width;
                    newXs.Add(newXSin);
                    newXCount++;

                    Label lbSin = new Label();
                    lbSin.Location = new Point(newXPos, newYPos + 3);
                    lbSin.Size = new System.Drawing.Size(38, 30);
                    lbSin.Text = "SinX +";
                    lbSin.Parent = gbInput;
                    newXPos += lbSin.Size.Width;
                    newLabels.Add(lbSin);
                    break;

                default:
                    break;

            }          
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (newXs.Count - 1 >= 0)
            {
                for (int i = newXs.Count - 1; i >= 0; i--)
                {
                    newXPos -= newXs[i].Width;
                    gbInput.Controls.Remove(newXs[i]);
                    newXs.RemoveAt(i);
                }
                for (int j = newLabels.Count - 1; j >= 0; j-- )
                {
                    newXPos -= newLabels[j].Width;
                    gbInput.Controls.Remove(newLabels[j]);
                    newLabels.RemoveAt(j);
                }        
            }

              
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (newXs.Count - 1 >= 0)
            {
                newXPos -= newXs[newXs.Count - 1].Width;
                gbInput.Controls.Remove(newXs[newXs.Count - 1]);
                newXs.RemoveAt(newXs.Count - 1);                
            }
            if (newLabels.Count - 1 >= 0)
            {
                newXPos -= newLabels[newLabels.Count - 1].Width;
                gbInput.Controls.Remove(newLabels[newLabels.Count - 1]);
                newLabels.RemoveAt(newLabels.Count - 1);
            }
        }

        //
        //generate aX^p, c, (),+,-,*,/
        int newXPos2 = 166, newYPos2 = 170, newXCount2 = 0;
        List<TextBox> boxes_a = new List<TextBox>();
        List<TextBox> boxes_p = new List<TextBox>();
        List<Label> labels_x = new List<Label>();
        List<ComboBox> cboxes_op = new List<ComboBox>();
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            //textBox for a
            TextBox bA = new TextBox();
            bA.Name = "bA_" + newXCount2;
            bA.Size = new System.Drawing.Size(30,30);
            bA.Location = new Point(newXPos2, newYPos2);
            bA.TextAlign = HorizontalAlignment.Right;
            bA.Parent = gbInput;
            newXPos2 += bA.Width;
            
            //label for X
            Label lbX = new Label();
            lbX.Name = "lbX_" + newXCount2;
            lbX.Size = new System.Drawing.Size(15, 30);
            lbX.Location = new Point(newXPos2, newYPos2);
            lbX.Text = "X";
            lbX.Parent = gbInput;
            newXPos2 += lbX.Width;            

            TextBox bP = new TextBox();
            bP.Name = "bPX_" + newXCount2;
            bP.Size = new System.Drawing.Size(20, 20);
            bP.Location = new Point(newXPos2, newYPos2 - lbX.Height*2/3);
            bP.Parent = gbInput;
            newXPos2 += bP.Width;

            ComboBox ope = new ComboBox();
            ope.Name = "ope_" + newXCount2;
            ope.Size = new System.Drawing.Size(32, 30);
            ope.Location = new Point(newXPos2, newYPos2);
            ope.Items.Add("+");
            ope.Items.Add("-");
            ope.Items.Add("*");
            ope.Items.Add("/");
            ope.Font = new Font(ope.Font.FontFamily,12);
            ope.DropDownStyle = ComboBoxStyle.DropDownList;
            ope.Parent = gbInput;
            newXPos2 += ope.Width;

            newXCount2++;
        }


        //level 1 methods-process RU-values, and use built-in method using abs-value
        private void DrawPoint(float x, float y, string description = "", Color? color = null, float height = 0.05f, float width = 0.05f)
        {
            Pen pen = new Pen(color ?? Color.Red, height); //1f for vertical height, acts as height of point
            PointF p_Abs = new PointF(Origin.X + x * Unit.X, Origin.Y - y * Unit.Y);
            PointF _p_Abs = new PointF(p_Abs.X + width, p_Abs.Y);//1f acts as with of point 
            g.DrawLine(pen, p_Abs, _p_Abs);

            if (description != "")
            {
                AddLabel lb = new AddLabel(new PointF(x, y), new PointF(Origin.X, Origin.Y), canvas, description);
            }
        }
        private void DrawPoint(PointF p, string description = "", Color? color = null, float height = 0.05f, float width = 0.05f)
        {
            Pen pen = new Pen(color ?? Color.Red, height);
            PointF pA = new PointF(Origin.X + p.X * Unit.X, Origin.Y - p.Y * Unit.Y);
            PointF pa = new PointF(pA.X + width, pA.Y);
            g.DrawLine(pen, pA, pa);//this is a built-in method, needs absolute value

            if (description != "")
            {
                AddLabel lb = new AddLabel(p, new PointF(Origin.X, Origin.Y), canvas, description);
            }
        }
        private void DrawSegmentByTwoPoints(float x1, float y1, float x2, float y2, string description, Color? color = null, float width = 1f)
        {
            Pen pen = new Pen(color ?? Color.Red, width);
            PointF p1 = new PointF(Origin.X + x1 * Unit.X, Origin.Y - y1 * Unit.Y);
            PointF p2 = new PointF(Origin.X + x2 * Unit.X, Origin.Y - y2 * Unit.Y);
            g.DrawLine(pen, p1, p2);

            AddLabel lb1 = new AddLabel(new PointF(x1, y1), new PointF(Origin.X, Origin.Y), canvas, description);
        }
        private void DrawSegmentByTwoPoints(PointF p1, PointF p2, string description = "", Color? color = null, float width = 1f)
        {
            Pen pen = new Pen(color ?? Color.Red, width);
            PointF _p1 = new PointF(Origin.X + p1.X * Unit.X, Origin.Y - p1.Y * Unit.Y);
            PointF _p2 = new PointF(Origin.X + p2.X * Unit.X, Origin.Y - p2.Y * Unit.Y);
            g.DrawLine(pen, _p1, _p2);

            AddLabel lb1 = new AddLabel(p1, new PointF(Origin.X, Origin.Y), canvas, description);
        }


        //level 2 methods-process RU-values, and use level 1 method using RU-values
        private void DrawSimpleEquation(float a, float b, string description = "", float step = 0.05f)
        {
            bool GetP1 = false;
            bool GetP2 = false;
            float x, y, maxX, maxY, x1 = 0, y1 = 0, x2 = 0, y2 = 0;  //RU_VALUE
            maxX = (canvas.Width - Origin.X) / Unit.X;//RU_VALUE
            maxY = (canvas.Height - Origin.Y) / Unit.Y; // RU_VALUE
            for (x = (0 - Origin.X) / Unit.X; x <= maxX; x += step)
            {
                y = a * x + b;
                DrawPoint(x, y);//RU_VALUE

                //get 2 locations(about 2/3 of range) for label.
                if (x >= maxX * 2f / 3f && !GetP1)
                {
                    x1 = x;
                    y1 = y;
                    GetP1 = true;
                }
                if (y >= maxY * 2f / 3f && !GetP2)
                {
                    x2 = x;
                    y2 = y;
                    GetP2 = true;
                }
            }
            //choose 1 good location from 2
            if (y1 < maxY && y1 > 0 && !GetP1)
            {
                AddLabel lb = new AddLabel(x1, y1, Origin.X, Origin.Y, canvas, description);
            }
            else
            {
                AddLabel lb = new AddLabel(x2, y2, Origin.X, Origin.Y, canvas, description);
            }
        }
        //RU-value: x, y   <-----  relative: (x*Unit.X, y*Unit.Y) -----> abs: (Origin.X + x*Unit.X, Orirgin.Y - y*Unit.Y)   

        private void DrawQuadraticEquation(float a, float b, float c, string description = "", float step = 0.005f)
        {
            bool GetP1 = false, GetP2 = false;
            float x, y, maxX, maxY, x1 = 0, x2 = 0, y1 = 0, y2 = 0;  //RU-VALUE
            maxX = (canvas.Width - Origin.X) / Unit.X;//RU-VALUE
            maxY = (canvas.Height - Origin.Y) / Unit.Y;
            for (x = (0 - Origin.X) / Unit.X; x <= maxX; x += step)
            {
                y = a * x * x + b * x + c;
                DrawPoint(x, y);//RU

                //get 2 locations(about 2/3 of range).
                if (x >= maxX * 2f / 3f && !GetP1)
                {
                    x1 = x;
                    y1 = y;
                    GetP1 = true;
                }
                if (y >= maxY * 2f / 3f && !GetP2)
                {
                    x2 = x;
                    y2 = y;
                    GetP2 = true;
                }
            }

            //choose 1 good location from 2
            if (y1 < maxY && y1 > 0 && GetP1)
            {
                AddLabel lb = new AddLabel(x1, y1, Origin.X, Origin.Y, canvas, description);
            }
            else
            {
                AddLabel lb = new AddLabel(x2, y2, Origin.X, Origin.Y, canvas, description);
            }
        }

        private void DrawSin(float a, float b, float c, float step = 0.005f)
        {
            float x, y, max;  //RU-VALUE
            max = (canvas.Width - Origin.X) / Unit.X;
            for (x = (0 - Origin.X) / Unit.X; x <= max; x += step)
            {
                y = a * (float)(Math.Sin(Convert.ToDouble(b * x + c)));
                DrawPoint(x, y); //don't add label here, to much label              
            }
            //addlabel here?
        }
        private void DrawCos(float a, float b, float c, float step = 0.005f)
        {
            float x, y, max;  //RU-VALUE
            max = (canvas.Width - Origin.X) / Unit.X;
            for (x = (0 - Origin.X) / Unit.X; x <= max; x += step)
            {
                y = a * (float)(Math.Cos(Convert.ToDouble(b * x + c)));
                DrawPoint(x, y);
            }
        }
        private void DrawTan(float a, float b, float c, float step = 0.005f)
        {
            float x, y, max;  //RU-VALUE
            max = (canvas.Width - Origin.X) / Unit.X;
            for (x = (0 - Origin.X) / Unit.X; x <= max; x += step)
            {
                y = a * (float)(Math.Tan(Convert.ToDouble(b * x + c)));
                DrawPoint(x, y);
            }
        }
        private void DrawCot(float a, float b, float c, float step = 0.005f)
        {
            float x, y, max;  //RU-VALUE
            max = (canvas.Width - Origin.X) / Unit.X;
            for (x = (0 - Origin.X) / Unit.X; x <= max; x += step)
            {
                y = a * (float)(1 / Math.Tan(Convert.ToDouble(b * x + c)));
                DrawPoint(x, y);
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            isHelpOn = !isHelpOn;
            lbForHelp.Text = "Button ' + ': To see more area on the right/top\nButton ' - ': To see more area on the left/bottom\nButton ' * ': To increase unit\nButton ' / ': To decrease unit";
            lbForHelp.Font = new Font("Consolas",8);
            if(isHelpOn)
            {
                lbForHelp.Visible = true;
                btnHelp.ForeColor = Color.Blue;
            }
            else
            {
                lbForHelp.Visible = false;
                btnHelp.ForeColor = Color.Black;
            }
            
        }

    }
}
