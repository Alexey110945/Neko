using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System;

namespace Neko
{
    public class Game
    {
        Form form;
        Cap thisCap;
        bool[] Rows;
        string NamePage;
        bool rightAnswer = true;

        MyButton backButton = new MyButton();
        MyButton nextButton = new MyButton();
        MyButton menuButton = new MyButton();
        MyButton[] answerButtons = new MyButton[9];
        string[] allSyllables;
        FunctionalityButtons funcButtons;
        Label information = new Label();
        Label progress = new Label();
        string[] answer;
        bool gameMode;

        InformationBlock inf = new InformationBlock();

        public Game(bool[] rows, string name, bool gM)
        {
            Rows = rows;
            NamePage = name;
            gameMode = gM;
            if (NamePage == "Hiragana")
                allSyllables = Properties.Resources.Hiragana.Split('\n');
            else
                allSyllables = Properties.Resources.Katakana.Split('\n');
            for (var i = 0; i < allSyllables.Length; i++)
                allSyllables[i] = allSyllables[i].Trim();
        }

        public void DrawGame(Form f, Cap cap)
        {
            form = f;
            thisCap = cap;
            cap.NamePage = Cap.NamesPage.Game;
            funcButtons = new FunctionalityButtons(f, cap);

            var t = RandomSyllables(GetSyllablesForLevel());
            CreateButtons(t);

            var rnd = new Random();
            answer = t[rnd.Next(0, t.Length)].Split('/');

            inf.SizeText = 40f;
            inf.Text = gameMode ? answer[1] : answer[0];

            SetSizeControls();

            var menuText = new MyText() { Text = "Меню" };
            menuButton.myText = menuText;
            menuButton.MouseClick += (sender, args) => funcButtons.GetMenu();

            var backButtonText = new MyText() { Text = "Назад" };
            backButton.myText = backButtonText;
            backButton.MouseClick += BackButton_MouseClick;

            var nextButtonText = new MyText() { Text = "Далее" };
            nextButton.myText = nextButtonText;
            nextButton.MouseClick += NextButton_MouseClick;

            information.Font = new Font("Arial", 40f, FontStyle.Regular);
            information.TextAlign = ContentAlignment.MiddleCenter;
            information.Text = gameMode ? "Выберите слог для транскрипции" :
                                          "Выберите транскрипцию для слога";
            information.ForeColor = Color.DimGray;

            progress.Font = new Font("Arial", 20f, FontStyle.Regular);
            progress.TextAlign = ContentAlignment.MiddleCenter;
            var p = NamePage == "Hiragana" ? thisCap.HiraganaProgress : thisCap.KatakanaProgress;
            progress.Text = "Набранно баллов: " + p.ToString();
            progress.ForeColor = Color.DimGray;
            AddControls();
        }

        private void NextButton_MouseClick(object sender, MouseEventArgs e)
        {
            form.Controls.Clear();
            thisCap.game = new Game(Rows, NamePage, gameMode);
            thisCap.game.DrawGame(form, thisCap);
        }

        private void BackButton_MouseClick(object sender, MouseEventArgs e)
        {
            form.Controls.Clear();
            thisCap.choosingSyllables = new ChoosingSyllables(NamePage, gameMode);
            thisCap.choosingSyllables.SyllablesOn = Rows;
            thisCap.choosingSyllables.DrawChoosingSyllable(form, thisCap);
        }
       
        private void AddControls()
        {
            form.Controls.Add(menuButton);
            form.Controls.Add(backButton);
            form.Controls.Add(nextButton);
            form.Controls.Add(inf);
            form.Controls.Add(information);
            form.Controls.Add(progress);
            foreach (var button in answerButtons)
                form.Controls.Add(button);
        }

        private void SetSizeControls()
        {
            inf.Size = new Size() { Height = 80, Width = 700 };
            inf.Location = new Point() { X = 200, Y = 200 };

            information.Size = new Size() { Height = 150, Width = form.Width - 20 };
            information.Location = new Point() { X = 10, Y = thisCap.CapHeignt + 20 };

            progress.Size = new Size() { Height = 60, Width = 400 };
            var locX = form.Width / 2 - 200;
            var locY = form.Height - 150;
            progress.Location = new Point() { X = locX, Y = locY };

            menuButton.Size = new Size() { Height = 65, Width = 190 };
            locX = form.Width / 2 - 95;
            locY = form.Height - 75;
            menuButton.Location = new Point() { X = locX, Y = locY };

            backButton.Size = new Size() { Height = 65, Width = 190 };
            backButton.Location = new Point() { X = 10, Y = locY };

            nextButton.Size = new Size() { Height = 65, Width = 190 };
            nextButton.Location = new Point() { X = form.Width - 200, Y = locY };

            var loc = new Point() { X = 210, Y = 290 };
            for (var i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].Size = new Size() { Height = 80, Width = 220 }; ;
                answerButtons[i].Location = loc;
                if (i == 2 || i == 5)
                {
                    loc.X = 210;
                    loc.Y += 90;
                }
                else
                    loc.X += 230;
            }
        }

        public string[] GetSyllablesForLevel()
        {
            var result = "";
            var index = 0;
            for (int i = 0; i < Rows.Length; i++)
                if (Rows[i])
                    for (int j = 0; j < 5; j++)
                    {
                        if (allSyllables[index] != "_/_")
                            result += allSyllables[index] + " ";
                        index += 1;
                        if (i == Rows.Length - 1)
                            break;
                    }
                else
                    index += 5;
            return result.Trim().Split();
        }

        private string[] RandomSyllables(string[] syllables)
        {
            var rnd = new Random();
            var result = new string[syllables.Length >= 9 ? 9 : syllables.Length];
            var count = 0;
            while(count < result.Length)
            {
                var n = rnd.Next(0, syllables.Length);
                if (!result.Any(s => s == syllables[n]))
                {
                    result[count] = syllables[n];
                    count++;
                }
            }
            return result;
        }

        private string[] CreateSyllablesForLevel(string[] allSyl)
        {
            var result = new string[allSyl.Length + 25];
            var ind = 0;
            string[] s;
            for (int i = 0;i < allSyl.Length;i++)
            {
                if (!allSyl[i].Contains(" "))
                {
                    result[ind] = allSyl[i];
                    ind++;
                }
                else
                {
                    s = allSyl[i].Split();
                    foreach(var e in s)
                    {
                        result[ind] = e;
                        ind++;
                    }
                }
            }
            return result;
        }

        private string[] AddSyllables(string[] syllables, string[]  allSyl)
        {
            var syllablesForLevel = CreateSyllablesForLevel(allSyl);
            var result = new string[9];
            var rnd = new Random();
            for (int i = 0; i < result.Length; i++)
                if (i < syllables.Length)
                    result[i] = syllables[i];
                else
                    while(true)
                    {
                        var n = rnd.Next(0, syllablesForLevel.Length);
                        var cond = !result.Any(x => x == syllablesForLevel[n]);
                        if (cond && syllablesForLevel[n] != "_/_")
                        {
                            result[i] = syllablesForLevel[n];
                            break;
                        }
                    }
            return result;
        }

        private void CreateButtons(string[] syllables)
        {
            if (syllables.Length < 9)
                syllables = AddSyllables(syllables, allSyllables);

            for (int i = 0; i < syllables.Length; i++)
            {
                answerButtons[i] = new MyButton();
                var syl = syllables[i].Split('/');
                var text = new MyText() { Text = gameMode ? syl[0] : syl[1], SizeText = 25f};
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
                    if (NamePage == "Hiragana")
                        thisCap.ChangeProgress(1, 0, 0);
                    else
                        thisCap.ChangeProgress(0, 1, 0);
                var p = NamePage == "Hiragana" ? thisCap.HiraganaProgress : thisCap.KatakanaProgress;
                progress.Text = "Набранно баллов: " + p.ToString();
            }
            else
            {
                answerButtons[i].BackColor = Color.Red;
                rightAnswer = false;
            }
            answerButtons[i].Invalidate();
        }
    }
}
