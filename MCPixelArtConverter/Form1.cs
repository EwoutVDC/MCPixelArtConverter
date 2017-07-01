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
        //string baseFolderName = "C:\\Users\\evandeca\\AppData\\Roaming\\.minecraft\\versions\\1.12\\1.12\\assets\\minecraft";
        string baseFolderName = "F:\\My Documents\\Minecraft\\1.12\\assets\\minecraft\\";
        MCResourcePack resourcePack = null;
        Bitmap image;
        Size scaledSize;
        Dictionary<Sides, Dictionary<MCBlockVariant, Bitmap>> palettes = new Dictionary<Sides, Dictionary<MCBlockVariant, Bitmap>>();
        
        public MCPACMainForm()
        {
            InitializeComponent();

            cmbFacing.DataSource = Enum.GetValues(typeof(Sides));
        }

        private void LoadBlockInfoButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(baseFolderName))
                folderBrowser.SelectedPath = baseFolderName;

            if (folderBrowser.ShowDialog() == DialogResult.Cancel)
                return;

            baseFolderName = folderBrowser.SelectedPath + "\\";

            resourcePack = new MCResourcePack(baseFolderName);

            comboBox1.Items.AddRange(resourcePack.getBlockNames().ToArray());

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
            Sides side;

            if (!CheckResourcePack())
                return;

            if (!TryGetSide(out side))
                return;

            //TODO: construct converter and keep (in map, lazy loaded) when changing combobox. Discard when reloading block info
            Dictionary<MCBlockVariant, Bitmap> palette;
            if (!palettes.TryGetValue(side, out palette))
            {
                palette = resourcePack.GetPalette(side);
                palettes.Add(side, palette);
            }
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

        private bool TryGetSide(out Sides side)
        {
            if (!Enum.TryParse<Sides>(cmbFacing.SelectedValue.ToString(), out side))
            {
                MessageBox.Show("Could not parse block side " + cmbFacing.SelectedValue);
                return false;
            }
            return true;
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

        private Dictionary<MCBlockVariant, Bitmap> GetPalette(Sides side)
        {
            if (!CheckResourcePack())
                return new Dictionary<MCBlockVariant, Bitmap>();

            Dictionary<MCBlockVariant, Bitmap> palette;
            if (!palettes.TryGetValue(side, out palette))
            {
                palette = resourcePack.GetPalette(side);
                palettes.Add(side, palette);
            }
            return palette;
        }

        private void btnSelectBlocks_Click(object sender, EventArgs e)
        {
            Sides side;

            if (!CheckResourcePack())
                return;

            if (!TryGetSide(out side))
                return;

            MCPaletteForm paletteForm = new MCPaletteForm();
            paletteForm.SetPalette(GetPalette(side));

            paletteForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MCPACImageForm textureForm = new MCPACImageForm();

            Sides side;
            if (!TryGetSide(out side))
                return;

            textureForm.SetImage(resourcePack.getBlockState(comboBox1.SelectedItem.ToString()).GetSideImages(side).First().Value);

            textureForm.Show();
        }
    }
}
