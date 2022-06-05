using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;

namespace Neko
{
    public class Cap
    {
        Form form;

        bool MousePressed = false;
        Point ClickPosition;
        Point MoveStartPosition;

        Rectangle rectBtnClose;//Закрыть
        Rectangle rectBtnCollapse;//Свернуть
        bool BtnClose = false;
        bool BtnCollapse = false;

        public int CapHeignt = 24;

        public int HiraganaProgress = 0;
        public int KatakanaProgress = 0;
        public int ScoreProgress = 0;
        public bool error = false;

        public enum NamesPage
        {
            Menu,
            AboutGame,
            ScoreLevels,
            AbcBooks,
            AbcTable,
            ChoosingSyllables,
            Game,
            ChoosingLevel,
            GameScore,
            ErrorPage
        }

        public NamesPage NamePage;
        public Menu menu;
        public AboutGame aboutGame;
        public ScoreLevels scoreLevels;
        public ChoosingLevel choosingLevel;
        public AbcBooks abcBooks;
        public AbcTable abcTable;
        public ChoosingSyllables choosingSyllables;
        public Game game;
        public GameScore gameScore;

        public void GetProgress()
        {
            var file = File.ReadAllLines(@"..\..\Progress.txt");
            HiraganaProgress = int.Parse(file[0]);
            KatakanaProgress = int.Parse(file[1]);
            ScoreProgress = int.Parse(file[2]);
        }


        public void ChangeProgress(int h, int k, int s)
        {
            HiraganaProgress = HiraganaProgress + h;
            KatakanaProgress = KatakanaProgress + k;
            ScoreProgress = ScoreProgress + s;
        }

        public void SaveProgress()
        {
            using (FileStream f = new FileStream(@"..\..\Progress.txt", FileMode.Truncate))
            { }
            using (FileStream file = new FileStream(@"..\..\Progress.txt", FileMode.Open))
            {
                var result = HiraganaProgress.ToString() + "\n" +
                    KatakanaProgress.ToString() + "\n" +
                    ScoreProgress.ToString();
                var arrayBytes = System.Text.Encoding.Default.GetBytes(result);
                file.Write(arrayBytes, 0, arrayBytes.Length);
            }
        }

        public void CreateCap(Form f)
        {
            form = f;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Icon = Properties.Resources.NekoLogIcon;
            form.Paint += (sender, args) => DrawStyle(args.Graphics);
            form.MouseDown += Form_MouseDown;
            form.MouseUp += Form_MouseUp;
            form.MouseMove += Form_MouseMove;
            form.MouseLeave += Form_MouseLeave;
        }

        private void Form_MouseLeave(object sender, EventArgs e)
        {
            BtnClose = false;
            BtnCollapse = false;
            form.Invalidate();
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (MousePressed)
            {
                var frmOffSet = new Size(Point.Subtract(Cursor.Position, new Size(ClickPosition)));
                form.Location = Point.Add(MoveStartPosition, frmOffSet);
            }
            else if (rectBtnClose.Contains(e.Location))
            {
                if (!BtnClose)
                {
                    BtnClose = true;
                    form.Invalidate();
                }
            }
            else if (rectBtnCollapse.Contains(e.Location))
            {
                if (!BtnCollapse)
                {
                    BtnCollapse = true;
                    form.Invalidate();
                }
            }
            else
            {
                if (BtnClose || BtnCollapse)
                {
                    BtnClose = false;
                    BtnCollapse = false;
                    form.Invalidate();
                }
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            MousePressed = false;
            if (e.Button == MouseButtons.Left && rectBtnClose.Contains(e.Location))
            {
                if(!error)
                    SaveProgress();
                form.Close();
            }
            if (e.Button == MouseButtons.Left && rectBtnCollapse.Contains(e.Location))
                form.WindowState = FormWindowState.Minimized;
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Location.Y <= CapHeignt)
            {
                MousePressed = true;
                ClickPosition = Cursor.Position;
                MoveStartPosition = form.Location;
            }
        }

        private void DrawStyle(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            var rectHeader = new Rectangle(0, 0, form.Width - CapHeignt * 2 - 4, CapHeignt);
            var rectBorder = new Rectangle(0, 0, form.Width - 1, form.Height - 1);
            var rectIcon = new Rectangle(0, 0, CapHeignt, CapHeignt);

            rectBtnClose = new Rectangle(rectHeader.Width + CapHeignt + 2, 0, CapHeignt, CapHeignt);
            var rectCrosshair = new Rectangle(
                rectBtnClose.X + rectBtnClose.Width / 2 - 5,
                rectBtnClose.Height / 2 - 5,
                10, 10);

            //Шапка
            g.DrawRectangle(new Pen(Color.DimGray), rectHeader);
            g.FillRectangle(new SolidBrush(Color.DimGray), rectHeader);

            //Заголовок
            var font = new Font("Arial", 11f, FontStyle.Regular);
            var SF = new StringFormat();
            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
            g.DrawString("Neko", font, new SolidBrush(Color.White), rectHeader, SF);

            //Иконка
            var icon = Properties.Resources.NekoLog;
            g.DrawImage(icon, rectIcon);
            g.DrawRectangle(new Pen(Color.Black), rectIcon);

            //Кнопка закрытия
            g.DrawRectangle(new Pen(BtnClose ? Color.Red : Color.DimGray), rectBtnClose);
            g.FillRectangle(new SolidBrush(BtnClose ? Color.Red : Color.DimGray), rectBtnClose);
            var line = new Pen(Color.White) { Width = 1.55f };
            var x = rectCrosshair.X;
            var y = rectCrosshair.Y;
            g.DrawLine(line, x, y, x + rectCrosshair.Width, y + rectCrosshair.Height);
            g.DrawLine(line, x + rectCrosshair.Width, y, x, y + rectCrosshair.Height);

            //Кнопка сворачивиня
            rectBtnCollapse = new Rectangle(rectHeader.Width + 1, 0, CapHeignt, CapHeignt);
            var changeColor = Color.FromArgb(200, Color.Gray);

            g.DrawRectangle(new Pen(BtnCollapse ? changeColor : Color.DimGray), rectBtnCollapse);
            g.FillRectangle(new SolidBrush(BtnCollapse ? changeColor : Color.DimGray), rectBtnCollapse);
            g.DrawLine(line, rectBtnCollapse.X + 7, 17, rectBtnCollapse.X + 17, 17);

            //Обводка
            g.DrawRectangle(new Pen(Color.Black), rectBorder);
        }
    }
}
