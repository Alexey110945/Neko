using System.Windows.Forms;
using System.Drawing;

namespace Neko
{
    public class ScoreLevels
    {
        Form form;
        Cap thisCap;
        string NamePage;
        bool GameMode;
        string LevelOne = "От 0 до 100";
        string LevelTwo = "От 100 до 1000";
        string LevelThree = "От 1000 до 100000";

        MyButton backButton = new MyButton();
        MyButton startGame = new MyButton();
        InformationBlock Theory = new InformationBlock();
        InformationBlock TheoryTwo = new InformationBlock();
        MyButton[] NumbersButtons = new MyButton[22];
        string[] Numbers;


        public ScoreLevels(string name, bool gM)
        {
            NamePage = name;
            GameMode = gM;
            if (NamePage == LevelOne)
                Numbers = Properties.Resources.Score_0_100.Split('\n');
            else if (NamePage == LevelTwo)
                Numbers = Properties.Resources.Score_100_1000.Split('\n');
            else if (NamePage == LevelThree)
                Numbers = Properties.Resources.Score_1000_100000.Split('\n');
            for (var i = 0; i < Numbers.Length; i++)
                Numbers[i] = Numbers[i].Trim();
        }

        public void DrawScoreLevels(Form f, Cap cap)
        {
            form = f;
            thisCap = cap;
            cap.NamePage = Cap.NamesPage.ScoreLevels;

            CreateButtons();
            SetSizeControls();

            var backButtonText = new MyText() { Text = "Назад" };
            backButton.myText = backButtonText;
            backButton.MouseClick += BackButton_MouseClick;

            var startGameText = new MyText() { Text = "Играть" };
            startGame.myText = startGameText;
            startGame.MouseClick += startGame_MouseClick;

            Theory.SizeText = 22f;
            TheoryTwo.SizeText = 35f;
            if (NamePage == LevelOne)
            {
                Theory.Text =
                    "Числа в японском языке образуются почти так же как и в русском\n" +
                    "Например: На русском 23 будет Двадцать три т.е. сначала количество\n" +
                    "десятков и затем количество едениц: ДВА ДЦАТЬ ТРИ\n" +
                    "По японски 23 будет 二十三(ichi ju: san)\n二十 - два десятка  三 - 3 единицы\n" +
                    "Ниже можно посмотреть больше примеров";
                if (f.WindowState == FormWindowState.Maximized)
                    TheoryTwo.SizeText = 50f;
            }
            else if (NamePage == LevelTwo)
            {
                Theory.Text = "Сотни образуются так же как и десяки\n" +
                    "Например: 435 - Четыре ста три дцать пять\n" +
                    "435 - 四百三十五 (yon hyaku san ju: go)\n" +
                    "Обратите внимение на произношение таких чисел как 300, 600, 800";
                if (f.WindowState == FormWindowState.Maximized)
                    TheoryTwo.SizeText = 45f;
            }
            else if (NamePage == LevelThree)
            {
                Theory.Text = "Тысячи образуются так же как сотни и десяки\n" +
                    "Например: 2843 - 二千八百四十三 (ni sen happyaku yon ju: san)\n" +
                    "Отличие от русского счёта начинается с десятков тысяч\n" +
                    "10001 一万一 (ichi man ichi) Указывается колличество десятков тысяч\n" +
                    "В примере с 100000 видно отлиние от русского счётам\n" +
                    "Пишется не как 100000, а как 10 по 10000";
                if (f.WindowState == FormWindowState.Normal)
                    TheoryTwo.SizeText = 26f;
            }
            TheoryTwo.Text = "Выберите число";

            AddControls();
        }

        private void CreateButtons()
        {
            for (var i = 0; i < NumbersButtons.Length; i++)
            {
                NumbersButtons[i] = new MyButton();
                var number = Numbers[i].Split('_');
                var buttonText = new MyText() { Text = number[0] };
                buttonText.SizeText = NamePage == LevelThree ? 17f : 20f;
                NumbersButtons[i].myText = buttonText;
                var t = number[0];
                NumbersButtons[i].MouseClick += (sender, args) => CreateTheoryTwo(t);
            }
        }

        private void SetSizeControls()
        {
            backButton.Size = new Size() { Height = 65, Width = 190 };
            backButton.Location = new Point() { X = 10, Y = form.Height - 75 };

            startGame.Size = new Size() { Height = 65, Width = 190 };
            startGame.Location = new Point() { X = form.Width - 200, Y = form.Height - 75 };

            Theory.Size = new Size() { Height = 260, Width = form.Width - 20 };
            Theory.Location = new Point() { X = 10, Y = 30 };

            TheoryTwo.Size = new Size() { Height = 200, Width = form.Width - 20 };
            TheoryTwo.Location = new Point() { X = 10, Y = 440 };

            var loc = new Point() { X = 10, Y = 300 };
            for (var i = 0; i < NumbersButtons.Length; i++)
            {
                NumbersButtons[i].Location = loc;
                NumbersButtons[i].Size = new Size() { Height = 60, Width = 90 };
                if (i == 10)
                {
                    loc.X = 10;
                    loc.Y = loc.Y + 70;
                }
                else
                    loc.X += 99;
            }
        }

        private void AddControls()
        {
            form.Controls.Add(backButton);
            form.Controls.Add(startGame);
            form.Controls.Add(Theory);
            form.Controls.Add(TheoryTwo);
            foreach (var button in NumbersButtons)
                form.Controls.Add(button);
        }

        private void BackButton_MouseClick(object sender, MouseEventArgs e)
        {
            form.Controls.Clear();
            thisCap.choosingLevel = new ChoosingLevel();
            thisCap.choosingLevel.DrawChoosingLevel(form, thisCap);
        }

        private void startGame_MouseClick(object sender, MouseEventArgs e)
        {
            form.Controls.Clear();
            thisCap.gameScore = new GameScore(NamePage, GameMode);
            thisCap.gameScore.DrawGameScore(form, thisCap);
        }

        private void CreateTheoryTwo(string number)
        {
            form.Controls.Remove(TheoryTwo);
            var n = "";
            foreach (var e in Numbers)
                if (e.Split('_')[0] == number)
                {
                    n = e;
                    break;
                }
            var s = n.Split('_');
            var str1 = s[0] + " " + s[2] + "\n (" + s[1] + ")  " + s[3];
            var nText = "\nДалее будет использоваться ";
            var next = s[3].Contains("/") && int.Parse(s[0])<=10 ? nText + s[3].Split('/')[0]: "";
            TheoryTwo.Text = str1 + next;
            form.Controls.Add(TheoryTwo);
        }
    }
}

