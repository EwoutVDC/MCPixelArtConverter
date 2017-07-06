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
    partial class MCPACMainForm : Form
    {
        //TODO: save/load baseFolderName to/from config json file?
        //TODO: use minecraft jar + resource pack folders instead of unzipped folders
        //string baseFolderName = "C:\\Users\\evandeca\\AppData\\Roaming\\.minecraft\\versions\\1.12\\1.12\\assets\\minecraft";
        string baseFolderName = "F:\\My Documents\\Minecraft\\1.12\\assets\\minecraft\\";
        MCResourcePack resourcePack = null;
        Bitmap image;
        Size scaledSize;
        Sides selectedSide = Sides.Up;
        
        public MCPACMainForm()
        {
            InitializeComponent();
        }

        public void SetSelectedSide(Sides side)
        {
            selectedSide = side;
        }

        private void LoadBlockInfoButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(baseFolderName))
                folderBrowser.SelectedPath = baseFolderName;

            if (folderBrowser.ShowDialog() == DialogResult.Cancel)
                return;

            baseFolderName = folderBrowser.SelectedPath + "\\";

            try
            {
                resourcePack = new MCResourcePack(baseFolderName);
            } catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }

            Console.WriteLine("Done loading block info");
        }

        private void btnLoadPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileBrowser = new OpenFileDialog();
            if (fileBrowser.ShowDialog() == DialogResult.Cancel)
                return;

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

            //TODO: fix rounding here - values change after repeatedly changing focus >.<
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
            if (!CheckResourcePack())
                return;

            //TODO: construct converter and keep (in Dictionary<Sides, ImageConverter>, lazy loaded) when changing selected side.
            //Discard when reloading block info
            Dictionary<MCBlockVariant, Bitmap> palette = resourcePack.GetPalette();
            ImageConverter imageConverter = new ImageConverterAverage(palette);

            MCBlockVariant[,] blocks = imageConverter.Convert(image, scaledSize);

            Bitmap pixelArtImage = new Bitmap(scaledSize.Width * 16, scaledSize.Height * 16);
            Graphics g = Graphics.FromImage(pixelArtImage);
            for (int w = 0; w < scaledSize.Width; w++)
            {
                for (int h = 0; h < scaledSize.Height; h++)
                {
                    g.DrawImage(palette[blocks[w, h]], new Point(16 * w, 16 * h));
                }
            }

            MCPACImageForm form = new MCPACImageForm();
            form.SetImage(pixelArtImage);
            form.Show();
        }

        private bool CheckResourcePack()
        {
            if (resourcePack == null)
            {
                MessageBox.Show("Load block info first!");
                return false;
            }
            return true;
        }

        private void btnSelectBlocks_Click(object sender, EventArgs e)
        {
            if (!CheckResourcePack())
                return;

            MCPaletteForm paletteForm = new MCPaletteForm(resourcePack);

            paletteForm.Show();
        }
    }
}
