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
    //not public
    partial class ConversionResultForm : Form
    {
        MCBlockVariant[,] blocks;
        Dictionary<MCBlockVariant, Bitmap> palette;

        public ConversionResultForm(MCBlockVariant[,] b, Dictionary<MCBlockVariant, Bitmap> p)
        {
            InitializeComponent();

            blocks = b;
            palette = p;

            Bitmap pixelArtImage = new Bitmap(blocks.GetLength(0) * 16, blocks.GetLength(1) * 16);
            Graphics g = Graphics.FromImage(pixelArtImage);
            for (int w = 0; w < blocks.GetLength(0); w++)
            {
                for (int h = 0; h < blocks.GetLength(1); h++)
                {
                    g.DrawImage(palette[blocks[w, h]], new Point(16 * w, 16 * h));
                }
            }

            pictureBox1.Image = pixelArtImage;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG files|*.png";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                pictureBox1.Image.Save(saveFileDialog.FileName);
        }

        private void btnExportToCsv_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files|*.csv";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            StreamWriter exportFile;
            //TODO P1 handle IOexceptions (ie cannot open because in use)
            using (exportFile = new StreamWriter(saveFileDialog.FileName))
            {
                for (int h = 0; h < blocks.GetLength(1); h++)
                {
                    if (h > 0)
                        exportFile.Write("\n");
                    for (int w = 0; w < blocks.GetLength(0); w++)
                    {
                        if (w > 0)
                            exportFile.Write(";");
                        exportFile.Write(blocks[w, h].BlockState.ToString());
                    }
                }
            }
        }
    }
}
