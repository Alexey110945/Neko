using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Neko
{
    public class InformationBlock : Control
    {
        private StringFormat SF = new StringFormat();
        public float SizeText = 14f;
        public Color TextColor = Color.DimGray;

        public InformationBlock()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(50, 100);
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

            g.DrawPath(new Pen(Color.Tomato), rectGP);

            var myText = new MyText() { Text = Text, SizeText = SizeText, ColorText = TextColor };
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
    }
}
