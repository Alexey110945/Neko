using System.Windows.Forms;
using System.Drawing;

namespace Neko
{
    public class FunctionalityButtons
    {
        Form form;
        Cap cap;

        public FunctionalityButtons(Form f, Cap c)
        {
            form = f;
            cap = c;
        }

        public void GetMenu()
        {
            form.Controls.Clear();
            cap.menu = new Menu();
            cap.menu.DrawMenu(form, cap);
        }
    }
}
