using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace Neko
{
    public class ChoosingSyllables
    {
        Form form;
        Cap thisCap;
        string NamePage;

        MyButton BackButton = new MyButton();
        MyButton StartButton = new MyButton();
        InformationBlock inf = new InformationBlock();
        InformationBlock[] Syllables = new InformationBlock[11];
        string[] SyllableNames = new string[]
        {
            "Ряд a", "Ряд ka", "Ряд sa", "Ряд ta", "Ряд na", "Ряд ha", 
            "Ряд ma", "Ряд ya", "Ряд ra", "Ряд wa", "n"
        };
        MySwitch[] Switches = new MySwitch[11];
        MySwitch gameMode = new MySwitch();
        MyButton AllSyllables = new MyButton();
        Label ModeOne = new Label();
        Label ModeTwo = new Label();

        public bool[] SyllablesOn = new bool[11];

        public ChoosingSyllables(string name, bool gM)
        {
            NamePage = name;
            gameMode.Chacked = gM;
        }

        public void DrawChoosingSyllable(Form f, Cap cap)
        {
            form = f;
            thisCap = cap;
            cap.NamePage = Cap.NamesPage.ChoosingSyllables;

            CreateInformationBlocksAndSwitches();

            SetSizeControls();

            var BackText = new MyText() { Text = "Назад" };
            BackButton.myText = BackText;
            BackButton.MouseClick += BackButton_MouseClick;

            var StartText = new MyText() { Text = "Старт" };
            StartButton.myText = StartText;
            StartButton.MouseClick += StartButton_MouseClick;

            var text = SyllablesOn.All(on => on == true) ? "Убрать" : "Все";
            var AllSyllablesText = new MyText() { Text = text };
            AllSyllables.myText = AllSyllablesText;
            AllSyllables.MouseClick += AllSyllables_MouseClick;

            inf.SizeText = 30f;
            inf.Text = "Выберите ряд для изучения";

            gameMode.backColorChange = Color.WhiteSmoke;

            ModeOne.Font = ModeTwo.Font = new Font("Arial", 20f, FontStyle.Regular);
            ModeOne.TextAlign = ModeTwo.TextAlign = ContentAlignment.MiddleCenter;
            ModeOne.Text = "слог =>\n транскрипция";
            ModeTwo.Text = "транскрипция\n => слог";
            ModeOne.ForeColor = ModeTwo.ForeColor = Color.DimGray;

            AddControls();
        }

        private void SetSizeControls()
        {
            BackButton.Size = new Size() { Height = 65, Width = 190 };
            BackButton.Location = new Point() { X = 10, Y = form.Height - 75 };

            StartButton.Size = new Size() { Height = 65, Width = 190 };
            StartButton.Location = new Point() { X = form.Width - 200, Y = form.Height - 75 };

            inf.Size = new Size() { Height = 120, Width = 700 };
            inf.Location = new Point() { X = form.Width / 2 - 350, Y = 55 };

            gameMode.Size = new Size() { Height = 50, Width = 110 };
            gameMode.Location = new Point() { X = form.Width / 2 - 55, Y = form.Height - 62 };

            ModeOne.Size = new Size() { Height = 65, Width = 200 };
            ModeOne.Location = new Point() { X = form.Width / 2 - 265, Y = form.Height - 75 };
            ModeTwo.Size = new Size() { Height = 65, Width = 200 };
            ModeTwo.Location = new Point() { X = form.Width / 2 + 65, Y = form.Height - 75 };

            var y = 185;
            var x = form.Width / 2 - 335;
            for (var i = 0; i < Syllables.Length; i++)
            {
                Syllables[i].Size = new Size() { Height = 65, Width = 165 };
                Syllables[i].Location = new Point() { X = x, Y = y };

                Switches[i].Size = new Size() { Height = 65, Width = 140 };
                Switches[i].Location = new Point() { X = x + 175, Y = y };
                if (Syllables[i].Text == "Ряд ha")
                {
                    y = 185;
                    x = form.Width / 2 + 25;
                }
                else
                    y = y + 75;
            }

            AllSyllables.Size = new Size() { Height = 65, Width = 165 };
            AllSyllables.Location = new Point() { X = x, Y = y };
        }

        private void AddControls()
        {
            form.Controls.Add(BackButton);
            form.Controls.Add(StartButton);
            form.Controls.Add(inf);
            form.Controls.Add(AllSyllables);
            form.Controls.Add(gameMode);
            form.Controls.Add(ModeOne);
            form.Controls.Add(ModeTwo);
            for (var i = 0; i < Syllables.Length; i++)
            {
                form.Controls.Add(Syllables[i]);
                form.Controls.Add(Switches[i]);
            }
        }

        private void CreateInformationBlocksAndSwitches()
        {
            for (var i = 0; i < Syllables.Length; i++)
            {
                Syllables[i] = new InformationBlock();
                Syllables[i].SizeText = 20f;
                Syllables[i].Text = SyllableNames[i];

                Switches[i] = new MySwitch();
                Switches[i].Chacked = SyllablesOn[i];
                var iRow = i;
                Switches[i].MouseClick += (sender, args) => SelectRow(iRow);
            }
        }

        private void SelectRow(int i)
        {
            SyllablesOn[i] = !SyllablesOn[i];
            AllSyllables.myText.Text = SyllablesOn.All(on => on == true) ? "Убрать" : "Все";
            AllSyllables.Invalidate();
        }

        private void AllSyllables_MouseClick(object sender, MouseEventArgs e)
        {
            var check = AllSyllables.myText.Text == "Все";
            for(var i = 0;i < Switches.Length;i++)
            {
                Switches[i].Chacked = check;
                Switches[i].Invalidate();
                SyllablesOn[i] = check;
            }
            AllSyllables.myText.Text = check ? "Убрать" : "Все";
            AllSyllables.Invalidate();
        }

        private void BackButton_MouseClick(object sender, MouseEventArgs e)
        {
            form.Controls.Clear();
            thisCap.abcBooks = new AbcBooks(NamePage);
            thisCap.abcBooks.DrawAbcBooks(form, thisCap);
        }

        private void StartButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (SyllablesOn.Any(on => on == true))
            {
                form.Controls.Clear();
                thisCap.game = new Game(SyllablesOn, NamePage, gameMode.Chacked);
                thisCap.game.DrawGame(form, thisCap);
            }
        }
    }
}
