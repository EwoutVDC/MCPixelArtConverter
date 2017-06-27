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
        //TODO: remove uses of this foldername and use resourcepack instead, save folder in form to resume there when loading another one
        //TODO: use minecraft jar + resource pack folders instead of unzipped folders
        String baseFolderName = "C:\\Users\\evandeca\\AppData\\Roaming\\.minecraft\\versions\\1.12\\1.12\\assets\\minecraft";
        MCResourcePack resourcePack;
        Bitmap image;
        Size scaledSize;
        
        public MCPACMainForm()
        {
            InitializeComponent();
        }

        private void LoadBlockInfoButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (!String.IsNullOrEmpty(baseFolderName))
                folderBrowser.SelectedPath = baseFolderName;
            folderBrowser.ShowDialog();

            baseFolderName = folderBrowser.SelectedPath + "\\";

            resourcePack = new MCResourcePack(baseFolderName);

            comboBoxAvailableBlocks.Items.AddRange(resourcePack.getBlockNames().ToArray());
        }

        private void btnShowTexture_Click(object sender, EventArgs e)
        {
            MCTextureShower textureForm = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f is MCTextureShower)
                {
                    textureForm = (MCTextureShower)f;
                }
            }
            if (textureForm == null)
            {
                textureForm = new MCTextureShower();
            }
            
            textureForm.setImage(resourcePack.getState(comboBoxAvailableBlocks.SelectedItem.ToString()).GetTopView());
            
            textureForm.Show(); //TODO: fix cannot access disposed object after closing window
            textureForm.Focus();
        }

        private void btnLoadPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileBrowser = new OpenFileDialog();
            fileBrowser.ShowDialog();

            if (fileBrowser.CheckFileExists)
            {
                image = new Bitmap(fileBrowser.FileName);
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

            //TODO: fix rounding here
            scaledSize = new Size((Int32) (image.Width * scale / 100), (Int32) (image.Height * scale / 100));

            if (image != null)
            { 
                txtWidth.Text = scaledSize.Width.ToString();
                txtHeigth.Text = scaledSize.Height.ToString();
            }

            pictureBox.Image = new Bitmap(image, scaledSize);
        }

        private void txtWidth_Leave(object sender, EventArgs e)
        {
            Double width;

            //TODO: This shouldn't change the value: ie 128/250 -> 127

            if (image != null && Double.TryParse(txtWidth.Text, out width))
            {
                Double scale = width * 100 / image.Width;
                updateScaleValue(scale);
            }
        }

        private void txtHeigth_Leave(object sender, EventArgs e)
        {
            Double heigth;

            if (image != null && Double.TryParse(txtHeigth.Text, out heigth))
            {
                Double scale = heigth * 100 / image.Height;
                updateScaleValue(scale);
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            ImageConverter imageConverter = new ImageConverterAverage(resourcePack);
            MCBlockState[,] blocks = imageConverter.Convert(image, scaledSize);
            Bitmap pixelArtImage = new Bitmap(scaledSize.Width * 16, scaledSize.Height * 16);

        }
    }
}
