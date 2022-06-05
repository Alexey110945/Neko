using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Neko
{
    public class MySwitch : Control
    {
        public bool Chacked = false;
        public Color backColorChange = Color.YellowGreen;

        public MySwitch()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(40, 15);
            BackColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.Clear(Parent.BackColor);

            var TSPen = new Pen(Color.DimGray, 1);
            var TSPenToggle = new Pen(Color.DimGray, 5);

            var rect = new Rectangle(2, 2, Width - 5, Height - 5);
            var rectGP = RoundCorners(rect, rect.Height);
            var rectToggle = new Rectangle(rect.X, rect.Y, rect.Height, rect.Height);

            g.DrawPath(TSPen, rectGP);

            if (Chacked)
            {
                rectToggle.Location = new Point(rect.Width - rect.Height, rect.Y);
                g.FillPath(new SolidBrush(backColorChange), rectGP);
            }
            else
            {
                rectToggle.Location = new Point(rect.X, rect.Y);
                g.FillPath(new SolidBrush(Color.WhiteSmoke), rectGP);
            }

            g.DrawEllipse(TSPenToggle, rectToggle);
            g.FillEllipse(new SolidBrush(Color.Tomato), rectToggle);
        }

        private GraphicsPath RoundCorners(Rectangle rect, int roundSize)
        {
            var gp = new GraphicsPath();

            gp.AddArc(rect.X, rect.Y, roundSize, roundSize, 180, 90);
            gp.AddArc(rect.X + rect.Width - roundSize, rect.Y, roundSize, roundSize, 270, 90);
            gp.AddArc(rect.X + rect.Width - roundSize, rect.Y + rect.Height - roundSize, roundSize, roundSize, 0, 90);
            gp.AddArc(rect.X, rect.Y + rect.Height - roundSize, roundSize, roundSize, 90, 90);

            gp.CloseFigure();
            return gp;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            SwitchToggle();
        }

        private void SwitchToggle()
        {
            Chacked = !Chacked;
            Invalidate();
        }
    }
}
