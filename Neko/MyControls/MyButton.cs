using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Neko
{
    public class MyButton : Control
    {
        private StringFormat SF = new StringFormat();
        private bool MouseEntered = false;
        private bool MousePressed = false;
        public MyText myText;

        public MyButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(50, 100);
            BackColor = Color.Tomato;

            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            g.Clear(Parent.BackColor);

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            var rectGP = RoundCorners(rect, rect.Height / 4);

            g.DrawPath(new Pen(MouseEntered ? Color.FromArgb(150, BackColor) : BackColor), rectGP);
            g.FillPath(new SolidBrush(MouseEntered ? Color.FromArgb(150, BackColor) : BackColor), rectGP);

            if (MousePressed)
            {
                g.DrawPath(new Pen(Color.FromArgb(30, Color.Black)), rectGP);
                g.FillPath(new SolidBrush(Color.FromArgb(30, Color.Black)), rectGP);
            }

            Font = new Font("Arial", myText.SizeText, FontStyle.Regular);
            g.DrawString(myText.Text, Font, new SolidBrush(myText.ColorText), rect, SF);
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

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            MouseEntered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            MouseEntered = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            MousePressed = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            MousePressed = false;
            Invalidate();
        }

    }
}
