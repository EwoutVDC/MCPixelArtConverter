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
        //TODO: remove uses of this foldername and use resourcepack instead, save folder in form to resume there when loading another one
        public static String BaseFolderName = "F:\\My Documents\\Minecraft\\1.12\\assets\\minecraft\\";
        public static MCResourcePack resourcePack;
        public static Bitmap picture;
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
