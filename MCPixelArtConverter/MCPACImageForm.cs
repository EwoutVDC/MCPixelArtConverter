using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCPixelArtConverter
{
    public partial class MCPACImageForm : Form
    {
        public MCPACImageForm()
        {
            InitializeComponent();
        }

        public void SetImage(Bitmap image)
        {
            pictureBox1.Image = image;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG files|*.png";
            saveFileDialog.ShowDialog();

            pictureBox1.Image.Save(saveFileDialog.FileName);
        }
    }
}
