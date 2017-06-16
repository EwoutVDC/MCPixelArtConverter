using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing;

namespace MCPixelArtConverter
{
    static class Program
    {
        public const String BaseFolderName = "F:\\My Documents\\Minecraft\\1.12\\assets\\minecraft\\";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MCPACMainForm());
        }
    }
}
