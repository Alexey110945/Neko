using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System;

namespace Neko
{
    public class GameScore
    {
        Form form;
        Cap thisCap;
        string NamePage;

        MyButton backButton = new MyButton();
        MyButton nextButton = new MyButton();
        MyButton menuButton = new MyButton();
        MyButton[] answerButtons = new MyButton[4];
        Label progress = new Label();
        MySwitch gameMode = new MySwitch();
        Label ModeOne = new Label();
        Label ModeTwo = new Label();
        FunctionalityButtons funcButtons;
        InformationBlock inf = new InformationBlock();
        string[] allNumbers;
        string[] answer;
        bool rightAnswer = true;

        public GameScore(string name, bool gM)
        {
            NamePage = name;
            gameMode.Chacked = gM;
        }

        public void DrawGameScore(Form f, Cap cap)
        {
            form = f;
            thisCap = cap;
            cap.NamePage = Cap.NamesPage.GameScore;
            funcButtons = new FunctionalityButtons(f, cap);

            CreateNubbers();
            
            inf.SizeText = 40f;
            var rnd = new Random();
            answer = allNumbers[rnd.Next(0, 4)].Split('_');
            inf.Text = gameMode.Chacked ? answer[1] : answer[0];

            CreateButtons(allNumbers);

            SetSizeControls();

            var menuText = new MyText() { Text = "Меню" };
            menuButton.myText = menuText;
            menuButton.MouseClick += (sender, args) => funcButtons.GetMenu();

            var backButtonText = new MyText() { Text = "Назад" };
            backButton.myText = backButtonText;
            backButton.MouseClick += BackButton_MouseClick;

            var nextButtonText = new MyText() { Text = "Далее" };
            nextButton.myText = nextButtonText;
            nextButton.MouseClick += NextAndGameMode_MouseClick;

            gameMode.backColorChange = Color.WhiteSmoke;
            gameMode.MouseClick += NextAndGameMode_MouseClick;

            progress.Font = new Font("Arial", 20f, FontStyle.Regular);
            progress.TextAlign = ContentAlignment.MiddleCenter;
            progress.Text = "Набранно баллов: " + thisCap.ScoreProgress.ToString();
            progress.ForeColor = Color.DimGray;

            ModeOne.Font = ModeTwo.Font = new Font("Arial", 20f, FontStyle.Regular);
            ModeOne.TextAlign = ModeTwo.TextAlign = ContentAlignment.MiddleCenter;
            ModeOne.Text = "число => написание";
            ModeTwo.Text = "написание => число";
            ModeOne.ForeColor = ModeTwo.ForeColor = Color.DimGray;
            AddControls();
        }

        private void NextAndGameMode_MouseClick(object sender, MouseEventArgs e)
        {
            form.Controls.Clear();
            thisCap.gameScore = new GameScore(NamePage, gameMode.Chacked);
            thisCap.gameScore.DrawGameScore(form, thisCap);
        }

        private void BackButton_MouseClick(object sender, MouseEventArgs e)
        {
            form.Controls.Clear();
            thisCap.scoreLevels = new ScoreLevels(NamePage, gameMode.Chacked);
            thisCap.scoreLevels.DrawScoreLevels(form, thisCap);
        }

        private void AddControls()
        {
            form.Controls.Add(menuButton);
            form.Controls.Add(backButton);
            form.Controls.Add(nextButton);
            form.Controls.Add(gameMode);
            form.Controls.Add(progress);
            form.Controls.Add(ModeOne);
            form.Controls.Add(ModeTwo);
            form.Controls.Add(inf);
            foreach (var button in answerButtons)
                form.Controls.Add(button);
        }

        private void SetSizeControls()
        {
            inf.Size = new Size() { Height = 80, Width = 700 };
            inf.Location = new Point() { X = form.Width / 2 - 350, Y = 200 };

            gameMode.Size = new Size() { Height = 50, Width = 110 };
            gameMode.Location = new Point() { X = form.Width / 2 - 55, Y = 40 };

            progress.Size = new Size() { Height = 60, Width = 400 };
            progress.Location = new Point() { X = form.Width / 2 - 200, Y = form.Height - 150 };

            ModeOne.Size = new Size() { Height = 65, Width = 300 };
            ModeOne.Location = new Point() { X = form.Width / 2 - 365, Y = 30 };
            ModeTwo.Size = new Size() { Height = 65, Width = 300 };
            ModeTwo.Location = new Point() { X = form.Width / 2 + 65, Y = 30 };

            menuButton.Size = new Size() { Height = 65, Width = 190 };
            menuButton.Location = new Point() { X = form.Width / 2 - 95, Y = form.Height - 75 };

            backButton.Size = new Size() { Height = 65, Width = 190 };
            backButton.Location = new Point() { X = 10, Y = form.Height - 75 };

            nextButton.Size = new Size() { Height = 65, Width = 190 };
            nextButton.Location = new Point() { X = form.Width - 200, Y = form.Height - 75 };

            var loc = new Point() { X = form.Width / 2 - 340, Y = 290 };
            for (var i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].Size = new Size() { Height = 80, Width = 335 }; ;
                answerButtons[i].Location = loc;
                if (i == 1)
                {
                    loc.X = form.Width / 2 - 340;
                    loc.Y += 90;
                }
                else
                    loc.X += 345;
            }
        }

        private void CreateButtons(string[] numbers)
        {
            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i] = new MyButton();
                var text = new MyText() { 
                    Text = gameMode.Chacked ? numbers[i].Split('_')[0] : numbers[i].Split('_')[1],
                    SizeText = gameMode.Chacked ? 30f : 20f};
                answerButtons[i].myText = text;
                var nI = i;
                answerButtons[i].MouseClick += (sender, args) => AnswerButtonsMouseClick(nI);
            }
            
        }

        private void AnswerButtonsMouseClick(int i)
        { 
            var ans = answerButtons[i].myText.Text == answer[0] ||
                      answerButtons[i].myText.Text == answer[1];
            if (ans)
            {
                answerButtons[i].BackColor = Color.YellowGreen;
                if (rightAnswer)
                    thisCap.ChangeProgress(0, 0, 1);
                progress.Text = "Набранно баллов: " + thisCap.ScoreProgress.ToString();
            }
            else
            {
                answerButtons[i].BackColor = Color.Red;
                rightAnswer = false;
            }
            answerButtons[i].Invalidate();
        }

        private void CreateNubbers()
        {
            var resultInt = new int[4];
            allNumbers = new string[4];
            var rnd = new Random();
            var left = int.Parse(NamePage.Split()[1]);
            var right = int.Parse(NamePage.Split()[3]);
            for (var i = 0; i < resultInt.Length;)
            {
                var next = rnd.Next(left, right+1);
                if (!resultInt.Contains(next))
                {
                    resultInt[i] = next;
                    i++;
                }
            }
            for (var i = 0; i < allNumbers.Length; i++)
                allNumbers[i] = resultInt[i].ToString() + "_" + CreateNuber(resultInt[i]);
        }

        public string CreateNuber(int n)
        {
            var strNumbers = new string[]
            {
                "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "百", "千"
            };
            var result = "";
            var num = n.ToString();
            if (n == 0)
                return "零";
            while (num.Length != 6)
                num = "0" + num;
            for (var i = 0; i < num.Length; i++)
            {
                if (num[i] != '0')
                {
                    if (i == 0)
                        return "十万";
                    else if (i == 1)
                        result += strNumbers[int.Parse(num[i].ToString())] + "万";
                    else if (i == 2)
                        if (num[i] == '1')
                            result += "千";
                        else
                            result += strNumbers[int.Parse(num[i].ToString())]  + "千";
                    else if (i == 3)
                        if (num[i] == '1')
                            result += "百";
                        else
                            result += strNumbers[int.Parse(num[i].ToString())] + "百";
                    else if (i == 4)
                        if (num[i] == '1')
                            result = result + "十";
                        else
                            result = result + strNumbers[int.Parse(num[i].ToString())] + "十";
                    else if (i == 5)
                            result = result + strNumbers[int.Parse(num[i].ToString())];
                }
            }
            return result;
        }
    }
}
