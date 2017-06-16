using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCPixelArtConverter
{
    public partial class MCPACMainForm : Form
    {
        public MCPACMainForm()
        {
            InitializeComponent();
        }

        private void LoadBlockInfoButton_Click(object sender, EventArgs e)
        {
            //CONTINUE HERE: start FolderBrowserDialog for loading 

            MCBlockState blackWool = new MCBlockState(Program.BaseFolderName + "blockstates\\black_wool.json");

            MCBlockState birchFenceGate = new MCBlockState(Program.BaseFolderName + "blockstates\\birch_fence_gate.json");

            Form textureForm = new Form();
            textureForm.Text = "Texture viewer";
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = blackWool.GetTopView();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            textureForm.Controls.Add(pictureBox);
            textureForm.ShowDialog();
        }
    }
}
