using System.Windows.Forms;
using System.Drawing;

namespace Neko
{
    public class ChoosingLevel
    {
        Form form;
        Cap thisCap;

        MyButton firstLevel = new MyButton();
        MyButton secondLevel = new MyButton();
        MyButton thridLevel = new MyButton();
        MyButton menuButton = new MyButton();
        FunctionalityButtons funcButtons;

        public void DrawChoosingLevel(Form f, Cap cap)
        {
            form = f;
            thisCap = cap;
            cap.NamePage = Cap.NamesPage.ChoosingLevel;
            funcButtons = new FunctionalityButtons(f, cap);

            SetSizeControls();

            var firstLevelText = new MyText() { Text = "От 0 до 100" };
            firstLevel.myText = firstLevelText;
            firstLevel.MouseClick += (sender, args) => CreateLevel(firstLevel.myText.Text);

            var secondLevelText = new MyText() { Text = "От 100 до 1000" };
            secondLevel.myText = secondLevelText;
            secondLevel.MouseClick += (sender, args) => CreateLevel(secondLevel.myText.Text);

            var thridLevelText = new MyText() { Text = "От 1000 до 100000" };
            thridLevel.myText = thridLevelText;
            thridLevel.MouseClick += (sender, args) => CreateLevel(thridLevel.myText.Text);

            var menuText = new MyText() { Text = "Меню" };
            menuButton.myText = menuText;
            menuButton.MouseClick += (sender, args) => funcButtons.GetMenu();

            AddControls();
        }

        private void SetSizeControls()
        {
            var x = form.Width / 2 - 125;
            var y = form.Height / 2 - 175;
            var size = new Size() { Height = 80, Width = 250 };
            firstLevel.Size = size;
            firstLevel.Location = new Point() { X = x, Y = y };

            secondLevel.Size  = size;
            secondLevel.Location = new Point() { X = x, Y = y + 90 };

            thridLevel.Size = size;
            thridLevel.Location = new Point() { X = x, Y = y + 180 };

            menuButton.Size = size;
            menuButton.Location = new Point() { X = x, Y = y + 270 };
        }

        private void AddControls()
        {
            form.Controls.Add(firstLevel);
            form.Controls.Add(secondLevel);
            form.Controls.Add(thridLevel);
            form.Controls.Add(menuButton);
        }

        private void CreateLevel(string nameLevel)
        {
            form.Controls.Clear();
            thisCap.scoreLevels = new ScoreLevels(nameLevel, false);
            thisCap.scoreLevels.DrawScoreLevels(form, thisCap);
        }
    }
}

