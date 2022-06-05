using System.Windows.Forms;
using System.Drawing;

namespace Neko
{
    public class AboutGame
    {
        MyButton menuButton = new MyButton();
        InformationBlock Title = new InformationBlock();
        FunctionalityButtons funcButtons;
        InformationBlock information = new InformationBlock();

        public void DrawPageAboutGame(Form f, Cap cap)
        {
            cap.NamePage = Cap.NamesPage.AboutGame;
            funcButtons = new FunctionalityButtons(f, cap);

            menuButton.Size = new Size() { Height = 65, Width = 190 };
            menuButton.Location = new Point() { X = f.Width / 2 - 95, Y = f.Height - 75 };

            var menuText = new MyText() { Text = "Меню" };
            menuButton.myText = menuText;
            menuButton.MouseClick += (sender, args) => funcButtons.GetMenu();

            Title.Size = new Size() { Height = 100, Width = f.Width - 20 };
            Title.Location = new Point() { X = 10, Y = 34 };
            Title.Text = "ね  Neko  こ";
            Title.SizeText = 36f;
            Title.TextColor = Color.Black;

            information.Size = new Size() { Height = f.Height - 250, Width = f.Width - 20 };
            information.Location = new Point() { X = 10, Y = 150 };
            information.Text = Properties.Resources.aboutGame; ;
            information.SizeText = 19f;

            f.Controls.Add(menuButton);
            f.Controls.Add(Title);
            f.Controls.Add(information);
        }
    }
}
