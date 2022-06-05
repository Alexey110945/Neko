using System.Windows.Forms;
using System.Drawing;

namespace Neko
{
    public class Menu
    {
        Form form;
        Cap thisCap;
        PictureBox image;
        MyButton hiragana = new MyButton();
        MyButton katakana = new MyButton();
        MyButton score = new MyButton();
        MyButton aboutGame = new MyButton();

        InformationBlock hiraganaProgress = new InformationBlock();
        InformationBlock katakanaProgress = new InformationBlock();
        InformationBlock scoreProgress = new InformationBlock();
        string openNextLevel = "100";

        public void DrawMenu(Form f, Cap cap)
        {
            thisCap = cap;
            form = f;
            cap.NamePage = Cap.NamesPage.Menu;

            SetSizeControls();

            var hiraganaText = new MyText() { Text = "Хирагана", SizeText = 20f };
            hiragana.myText = hiraganaText;
            hiragana.MouseClick += Hiragana_MouseClick;

            var katakanaText = new MyText() { Text = "Катакана", SizeText = 20f };
            katakana.myText = katakanaText;
            if (thisCap.HiraganaProgress >= int.Parse(openNextLevel))
                katakana.MouseClick += Katakana_MouseClick;

            var scoreText = new MyText() { Text = "Счёт", SizeText = 20f };
            score.myText = scoreText;
            score.MouseClick += Score_MouseClick;

            var aboutGameText = new MyText() { Text = "Об игре", SizeText = 20f };
            aboutGame.myText = aboutGameText;
            aboutGame.MouseClick += AboutGame_MouseClick;

            var Hp = thisCap.HiraganaProgress;
            hiraganaProgress.Text = "Набрано баллов:\n" + Hp.ToString();
            hiraganaProgress.Invalidate();
            var Kp = thisCap.KatakanaProgress;
            katakanaProgress.Text = "Набрано баллов:\n" + Kp.ToString();
            var Sp = thisCap.ScoreProgress;
            scoreProgress.Text = "Набрано баллов:\n" + Sp.ToString();

            AddControls();

            //всплывающие подсказки
            CreatePromptings();
        }
        private void SetSizeControls()
        {
            image = AddPicture(700, 650, 10, 50);
            hiragana.Size = new Size() { Height = 150, Width = 180 };
            hiragana.Location = new Point() { X = 720, Y = 70 };
            hiraganaProgress.Size = new Size() { Height = 150, Width = 180 };
            hiraganaProgress.Location = new Point() { X = 910, Y = 70 };

            katakana.Size = new Size() { Height = 150, Width = 180 };
            katakana.Location = new Point() { X = 720, Y = 235 };
            katakanaProgress.Size = new Size() { Height = 150, Width = 180 };
            katakanaProgress.Location = new Point() { X = 910, Y = 235 };

            score.Size = new Size() { Height = 150, Width = 180 };
            score.Location = new Point() { X = 720, Y = 400 };
            scoreProgress.Size = new Size() { Height = 150, Width = 180 };
            scoreProgress.Location = new Point() { X = 910, Y = 400 };

            aboutGame.Size = new Size() { Height = 110, Width = 210 };
            aboutGame.Location = new Point() { X = 800, Y = 565 };
        }

        private void AddControls()
        {
            form.Controls.Add(image);
            form.Controls.Add(hiragana);
            form.Controls.Add(hiraganaProgress);
            form.Controls.Add(katakana);
            form.Controls.Add(katakanaProgress);
            form.Controls.Add(score);
            form.Controls.Add(scoreProgress);
            form.Controls.Add(aboutGame);
        }

        private void Score_MouseClick(object sender, MouseEventArgs e)
        {
            form.Controls.Clear();
            thisCap.choosingLevel = new ChoosingLevel();
            thisCap.choosingLevel.DrawChoosingLevel(form, thisCap);
        }

        private void Katakana_MouseClick(object sender, MouseEventArgs e)
        {
            form.Controls.Clear();
            thisCap.abcBooks = new AbcBooks("Katakana");
            thisCap.abcBooks.DrawAbcBooks(form, thisCap);
        }

        private void Hiragana_MouseClick(object sender, MouseEventArgs e)
        {
            form.Controls.Clear();
            thisCap.abcBooks = new AbcBooks("Hiragana");
            thisCap.abcBooks.DrawAbcBooks(form, thisCap);
        }

        private void AboutGame_MouseClick(object sender, MouseEventArgs e)
        {
            form.Controls.Clear();
            thisCap.aboutGame = new AboutGame();
            thisCap.aboutGame.DrawPageAboutGame(form, thisCap);
        }

        private void CreatePromptings()
        {
            var hiraganaPrompting = new ToolTip();
            var textH = "Слоговая азбука, одна из\nсоставляющих японской письменности";
            hiraganaPrompting.SetToolTip(hiragana, textH);

            var condition = "Для открытия этого уровня нужно набрать\n" +
                openNextLevel + " баллов в предыдущем";
            var katakanaPrompting = new ToolTip();
            var textK = "Вторая азбука японского языка\n" + condition;
            katakanaPrompting.SetToolTip(katakana, textK);
            katakanaPrompting.AutoPopDelay = 6500;

            var scorePrompting = new ToolTip();
            var textS = "Правила японского счёта";
            scorePrompting.SetToolTip(score, textS);
        }

        private PictureBox AddPicture(int height, int width, int locheight, int locwidth)
        {
            var image = Properties.Resources.NekoLog;
            var x = new PictureBox()
            {
                Image = image,
                Location = new Point(locheight, locwidth),
                Size = new Size(height - 1, width - 1),
                SizeMode = PictureBoxSizeMode.StretchImage//картинка под размер окна
            };
            return x;
        }
    }
}
