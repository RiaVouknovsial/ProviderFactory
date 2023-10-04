using System;
using System.Windows.Forms;
using WindowsFormsApp20;  // Add this using directive

namespace WindowsFormsApp20
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create an instance of Form1 from the correct namespace
            Application.Run(new Form1());
        }
    }
}
