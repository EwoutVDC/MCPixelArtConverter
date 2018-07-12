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
        string defaultFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".minecraft\\versions");

        MCResourcePack resourcePack = null;
        Bitmap image;
        Size scaledSize;
        Sides selectedSide = Sides.Up;
        
        public MCPACMainForm()
        {
            InitializeComponent();
            List<ImageDitherer> ditherers = new List<ImageDitherer>();
            
            ditherers.Add(new ImageDithererErrorDiffuser("Sierra", new double[,]
                                                        { { 0,      0,      0,      5/32.0, 3/32.0 },
                                                          { 2/32.0, 4/32.0, 5/32.0, 4/32.0, 2/32.0 },
                                                          { 0,      2/32.0, 3/32.0, 2/32.0, 0      } }));
            ditherers.Add(new ImageDithererErrorDiffuser("Sierra Two row", new double[,]
                                                        { { 0,      0,      0,      4/16.0, 3/16.0 },
                                                          { 1/16.0, 2/16.0, 3/16.0, 2/16.0, 1/16.0 } }));
            ditherers.Add(new ImageDithererErrorDiffuser("Sierra Lite", new double[,]
                                                        { { 0,      0,      1/4.0},
                                                          { 1/4.0,  2/4.0,  1/4.0} }));
            ditherers.Add(new ImageDithererErrorDiffuser("Floyd-Steinberg", new double[,]
                                                        { { 0,      0,      7/16.0 },
                                                          { 3/16.0, 5/16.0, 1/16.0 } }));
            ditherers.Add(new ImageDithererErrorDiffuser("Jarvis, Judice and Ninke", new double[,]
                                                        { { 0,      0,      0,      7/48.0, 5/48.0 },
                                                          { 3/48.0, 5/48.0, 7/48.0, 5/48.0, 3/48.0 },
                                                          { 1/48.0, 3/48.0, 5/48.0, 3/48.0, 1/48.0 } }));
            ditherers.Add(new ImageDithererErrorDiffuser("Stucki", new double[,]
                                                        { { 0,      0,      0,      8/42.0, 4/42.0 },
                                                          { 2/42.0, 4/42.0, 8/42.0, 4/42.0, 2/42.0 },
                                                          { 1/42.0, 2/42.0, 4/42.0, 2/42.0, 1/42.0 } }));
            ditherers.Add(new ImageDithererErrorDiffuser("Atkinson", new double[,]
                                                        { { 0,      0,      0,      1/8.0,  1/8.0 },
                                                          { 0,      1/8.0,  1/8.0,  1/8.0,  0     },
                                                          { 0,      0,      1/8.0,  0,      0     } }));
            ditherers.Add(new ImageDithererErrorDiffuser("Burkes", new double[,]
                                                        { { 0,      0,      0,      8/32.0, 4/32.0 },
                                                          { 2/32.0, 4/32.0, 8/32.0, 4/32.0, 2/32.0 } }));
            ditherers.Sort();
            cmbDitherers.DisplayMember = "Name";
            cmbDitherers.DataSource = ditherers;
            cmbDitherers.SelectedIndex = ditherers.FindIndex(d => d.Name == "Sierra");
        }

        public void SetSelectedSide(Sides side)
        {
            selectedSide = side;
        }

        private void LoadBlockInfoButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select minecraft .jar file";
            if (Directory.Exists(defaultFolderPath))
                fileDialog.InitialDirectory = defaultFolderPath;
            else
                fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (fileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            
            if (!fileDialog.CheckFileExists)
                return;

            try
            {
                resourcePack = new MCResourcePack(fileDialog.FileName);
            } catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }

            Console.WriteLine("Done loading block info");
            //resourcePack.LoadBlockSelection("C:\\Users\\evandeca\\Pictures\\carpet_blocks.txt");
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

            //TODO: P2 construct converter and keep (in Dictionary<Sides, ImageConverter>, lazy loaded) when changing selected side.
            //Discard when reloading block info
            Dictionary<MCBlockVariant, Bitmap> palette = resourcePack.GetPalette();
            ImageConverter imageConverter = new ImageConverterAverage(palette);

            ImageDitherer d = null;

            if (cbDithering.Checked)
            {
                d = (ImageDitherer)cmbDitherers.SelectedValue;
                d.Reset(scaledSize);
            }

            MCBlockVariant[,] blocks = imageConverter.Convert(image, scaledSize, d);

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

        private void cbDithering_CheckedChanged(object sender, EventArgs e)
        {
            cmbDitherers.Enabled = cbDithering.Checked;
        }
    }
}
