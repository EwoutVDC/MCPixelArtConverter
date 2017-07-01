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
    partial class MCPaletteForm : Form
    {
        Dictionary<string, Bitmap> palette = new Dictionary<string, Bitmap>();

        public MCPaletteForm()
        {
            InitializeComponent();
        }

        public void SetPalette(Dictionary<MCBlockVariant, Bitmap> palette)
        {
            //TODO: this list is way to big, perhaps split blockstate and variant to a different list?
            foreach (KeyValuePair<MCBlockVariant, Bitmap> kv in palette)
            {
                this.palette.Add(kv.Key.ToString(), kv.Value);
            }
        }

        private void checkedListBoxBlockVariants_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = palette[checkedListBoxBlockVariants.SelectedItem.ToString()];
        }
    }
}
