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
        Dictionary<string, List<MCBlockVariant>> variants = new Dictionary<string, List<MCBlockVariant>>();

        public MCPaletteForm()
        {
            InitializeComponent();
        }

        public void SetPalette(Dictionary<MCBlockVariant, Bitmap> palette)
        {
            //TODO: this list is way to big, perhaps split blockstate and variant to a different list?
            foreach (KeyValuePair<MCBlockVariant, Bitmap> kv in palette)
            {
                if (!variants.ContainsKey(kv.Key.blockState.ToString()))
                {
                    variants.Add(kv.Key.blockState.ToString(), new List<MCBlockVariant>());
                    checkedListBoxBlockStates.Items.Add(kv.Key.blockState.ToString());
                }
                variants[kv.Key.blockState.ToString()].Add(kv.Key);

                this.palette.Add(kv.Key.ToString(), kv.Value);
            }
        }

        private void checkedListBoxBlockVariants_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBoxVariants.Items.Clear();
            checkedListBoxVariants.Items.AddRange(variants[checkedListBoxBlockStates.SelectedItem.ToString()].ToArray());
        }

        private void checkedListBoxVariants_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = palette[checkedListBoxVariants.SelectedItem.ToString()];
        }
    }
}
