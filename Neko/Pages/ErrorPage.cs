using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Neko
{
    public class ErrorPage
    {
        InformationBlock aboutError = new InformationBlock();

        public void DrawErrorPage(Form f)
        {
            aboutError.Size = new Size() { Height = 300, Width = 800 };
            aboutError.Location = new Point() { X = f.Width / 2 - 400, 
                                                Y = f.Height / 2 - 150 };
            aboutError.SizeText = 30f;
            aboutError.Text = "Ошибка\nФайл Progress.txt был изменён";
            f.Controls.Add(aboutError);
        }

        public bool IsCorrect()
        {
            var progress = File.ReadAllLines(@"..\..\Progress.txt");
            if (progress.Length != 3)
                return false;
            foreach (var n in progress)
                foreach (var e in n)
                    if (!"0123456789".Contains(e.ToString()))
                        return false;
            return true;
        }
    }
}
