using System.Windows.Forms;
using System.Drawing;

namespace Neko
{
    public class AbcBooks
    {
        Form form;
        Cap thisCap;
        string NamePage;

        InformationBlock AboutSyllable = new InformationBlock();
        MyButton menuButton = new MyButton();
        MyButton startGame = new MyButton();
        MyButton AbcTable = new MyButton();
        MyButton[] SyllablesButtons = new MyButton[51];
        string[] AbcInf;
        FunctionalityButtons funcButtons;

        public AbcBooks(string name)
        {
            NamePage = name;
            if (NamePage == "Hiragana")
                AbcInf = Properties.Resources.Hiragana.Split('\n');
            else
                AbcInf = Properties.Resources.Katakana.Split('\n');

            for (var i = 0; i < AbcInf.Length; i++)
                AbcInf[i] = AbcInf[i].Trim();
        }

        public void DrawAbcBooks(Form f, Cap cap)
        {
            form = f;
            thisCap = cap;
            cap.NamePage = Cap.NamesPage.AbcBooks;
            funcButtons = new FunctionalityButtons(f, cap);

            CreateButtons();
            SetSizeControls();
                
            var menuText = new MyText() { Text = "Меню" };
            menuButton.myText = menuText;
            menuButton.MouseClick += (sender, args) => funcButtons.GetMenu();

            var startGameText = new MyText() { Text = "Играть" };
            startGame.myText = startGameText;
            startGame.MouseClick += StartGame_MouseClick;

            var AbcTableText = new MyText() { Text = "Написание" };
            AbcTable.myText = AbcTableText;
            AbcTable.MouseClick += AbcTable_MouseClick;

            AboutSyllable.SizeText = 40f;
            AboutSyllable.Text = "Выберите слог";

            AddControls();
        }

        private void SetSizeControls()
        {
            menuButton.Size = new Size() { Height = 65, Width = 190 };
            menuButton.Location = new Point() { X = form.Width - 200, Y = form.Height - 110 };

            startGame.Size = new Size() { Height = 65, Width = 190 };
            startGame.Location = new Point() { X = form.Width - 200, Y = form.Height - 185 };

            AbcTable.Size = new Size() { Height = 65, Width = 190 };
            AbcTable.Location = new Point() { X = form.Width - 200, Y = form.Height - 260 };

            AboutSyllable.Size = new Size() { Height = 270, Width = form.Width - 220 };
            AboutSyllable.Location = new Point() { X = 10, Y = 440 };

            var loc = new Point() { X = 10, Y = 40 };
            for (var i = 0; i < SyllablesButtons.Length; i++)
            {
                SyllablesButtons[i].Location = loc;
                SyllablesButtons[i].Size = new Size() { Height = 70, Width = 90 };
                if (i % 5 == 4)
                {
                    loc.Y = 40;
                    loc.X += 99;
                }
                else
                    loc.Y += 80;
            }
        }

        private void AddControls()
        {
            form.Controls.Add(menuButton);
            form.Controls.Add(startGame);
            form.Controls.Add(AboutSyllable);
            form.Controls.Add(AbcTable);
            foreach (var button in SyllablesButtons)
                if (button.myText.Text != "_\n_")
                    form.Controls.Add(button);
        }

        private void CreateButtons()
        {
            for (var i = 0; i < SyllablesButtons.Length; i++)
            {
                SyllablesButtons[i] = new MyButton();
                var syllableAll = AbcInf[i].Split();
                var syllable = syllableAll[0].Split('/');
                var buttonText = new MyText()
                {
                    Text = syllable[0] + "\n" + syllable[1],
                    SizeText = 21f,
                    ColorText = Color.White
                };
                SyllablesButtons[i].myText = buttonText;
                var s = syllable[0];
                SyllablesButtons[i].MouseClick += (sender, args) => CreateAboutSyllable(s);
            }
        }

        private void AbcTable_MouseClick(object sender, MouseEventArgs e)
        {
            form.Controls.Clear();
            thisCap.abcTable = new AbcTable(NamePage);
            thisCap.abcTable.DrawAbcTable(form, thisCap);
        }

        private void StartGame_MouseClick(object sender, MouseEventArgs e)
        {
            form.Controls.Clear();
            thisCap.choosingSyllables = new ChoosingSyllables(NamePage, false);
            thisCap.choosingSyllables.DrawChoosingSyllable(form, thisCap);
        }

        private void CreateAboutSyllable(string Syllable)
        {
            var s = "";
            foreach (var e in AbcInf)
                if (e.Split()[0].Split('/')[0] == Syllable)
                {
                    s = e;
                    break;
                }
            var syll = s.Split();
            var str1 = Syllable + " (" + syll[0].Split('/')[1];
            str1 += ") - " + syll[0].Split('/')[2] + "\n";
            var str2 = "Озвончение: ";
            var str3 = "Оглушение: ";
            if (syll.Length > 1)
            {
                str2 = str2 + syll[1].Split('/')[0] + " (" + syll[1].Split('/')[1]; 
                str2 += ") - " + syll[1].Split('/')[2] + "\n";
            }
            else
                str2 = str2 + "Нет\n";
            if (syll.Length == 3)
            {
                str3 = str3 + syll[2].Split('/')[0] + " (" + syll[2].Split('/')[1];
                str3 += ") - " + syll[2].Split('/')[2];
            }
            else
                str3 = str3 + "Нет";
            AboutSyllable.Text = str1 + str2 + str3;
            AboutSyllable.Invalidate();
        }
    }
}
