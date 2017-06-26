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
    public partial class MCPACMainForm : Form
    {
        public MCPACMainForm()
        {
            InitializeComponent();
        }

        private void LoadBlockInfoButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (!String.IsNullOrEmpty(Program.BaseFolderName))
                folderBrowser.SelectedPath = Program.BaseFolderName;
            folderBrowser.ShowDialog();

            Program.BaseFolderName = folderBrowser.SelectedPath + "\\";

            Program.resourcePack = new MCResourcePack(Program.BaseFolderName);

            comboBoxAvailableBlocks.Items.AddRange(Program.resourcePack.getBlockNames().ToArray());
        }

        private void btnShowTexture_Click(object sender, EventArgs e)
        {
            Form textureForm = new Form();
            textureForm.Text = "Texture viewer";
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = Program.resourcePack.getState(comboBoxAvailableBlocks.SelectedItem.ToString()).GetTopView();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            textureForm.Controls.Add(pictureBox);
            textureForm.ShowDialog();
        }

        private void btnLoadPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileBrowser = new OpenFileDialog();
            fileBrowser.ShowDialog();

            if (fileBrowser.CheckFileExists)
            {
                Program.picture = new Bitmap(fileBrowser.FileName);
                pictureBox.Image = Program.picture;
            }
            
            scaleTrackBar.Value = 100;
            updateScaleValue(scaleTrackBar.Value);
        }

        private void scaleTrackBar_Scroll(object sender, EventArgs e)
        {
            updateScaleValue(scaleTrackBar.Value);
        }

        private void updateScaleValue(Double scale)
        {
            lblScaleValue.Text = scale.ToString("0.##") + "%";
            scaleTrackBar.Value = (int)scale;

            if (Program.picture != null)
            { 
                txtWidth.Text = (Program.picture.Width * (int)scale / 100).ToString();
                txtHeigth.Text = (Program.picture.Height * (int)scale / 100).ToString();
            }
        }

        private void txtWidth_Leave(object sender, EventArgs e)
        {
            Double width;

            if (Program.picture != null && Double.TryParse(txtWidth.Text, out width))
            {
                Double scale = width * 100 / Program.picture.Width;
                updateScaleValue(scale);
            }
        }

        private void txtHeigth_Leave(object sender, EventArgs e)
        {
            Double heigth;

            if (Program.picture != null && Double.TryParse(txtHeigth.Text, out heigth))
            {
                Double scale = heigth * 100 / Program.picture.Height;
                updateScaleValue(scale);
            }
        }

        private void MCPACMainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
