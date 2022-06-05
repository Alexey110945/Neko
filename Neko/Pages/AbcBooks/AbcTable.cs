using System.Windows.Forms;
using System.Drawing;

namespace Neko
{
    public class AbcTable
    {
        Form form;
        Cap thisCap;
        PictureBox image;
        MyButton backButton = new MyButton();
        string NamePage;

        public AbcTable(string name)
        {
            NamePage = name;
        }

        public void DrawAbcTable(Form f, Cap cap)
        {
            form = f;
            thisCap = cap;
            cap.NamePage = Cap.NamesPage.AbcTable;

            image = AddPicture(form.Width - 10, form.Height - 115, 5, 30);
            backButton.Size = new Size() { Height = 65, Width = 190 };
            backButton.Location = new Point() { X = f.Width / 2 - 95, Y = f.Height - 75 };

            var backButtonText = new MyText() { Text = "Назад" };
            backButton.myText = backButtonText;
            backButton.MouseClick += MenuButton_backButton;

            f.Controls.Add(image);
            f.Controls.Add(backButton);
        }

        private void MenuButton_backButton(object sender, MouseEventArgs e)
        {
            form.Controls.Clear();
            thisCap.abcBooks.DrawAbcBooks(form, thisCap);
        }

        private PictureBox AddPicture(int height, int width, int locheight, int locwidth)
        {
            var x = new PictureBox()
            {
                Location = new Point(locheight, locwidth),
                Size = new Size(height - 1, width - 1),
                SizeMode = PictureBoxSizeMode.StretchImage//картинка под размер окна
            };
            if (NamePage == "Hiragana")
                x.Image = Properties.Resources.HiraganaTable;
            else
                x.Image = Properties.Resources.KatakanaTable;
            return x;
        }
    }
}
