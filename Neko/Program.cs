using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Neko
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var f = new Form1() { ClientSize = new Size(1100, 724) };
            f.BackColor = Color.White;

            Cap cap = new Cap();
            cap.CreateCap(f);
            var errorPage = new ErrorPage();

            if (!File.Exists(@"..\..\Progress.txt"))
                File.AppendAllLines(@"..\..\Progress.txt", new string[] { "0", "0", "0" });

            if (errorPage.IsCorrect())
            {
                cap.GetProgress();
                cap.menu = new Menu();
                cap.menu.DrawMenu(f, cap);
            }
            else
            {
                cap.error = true;
                errorPage = new ErrorPage();
                errorPage.DrawErrorPage(f);
            }

            Application.Run(f);
        }
    }
}