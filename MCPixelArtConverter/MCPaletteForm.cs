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
        Dictionary<string, List<MCBlockVariant>> variantsForBlockState = new Dictionary<string, List<MCBlockVariant>>();

        MCPACMainForm parentForm;
        MCResourcePack resourcePack;

        Sides selectedSide;

        public MCPaletteForm(MCPACMainForm parentForm, MCResourcePack rp)
        {
            InitializeComponent();

            this.parentForm = parentForm;
            resourcePack = rp;

            cmbFacing.DataSource = Enum.GetValues(typeof(Sides));
            cmbFacing.SelectedItem = Sides.Up;
            selectedSide = Sides.Up;
            checkedListBoxBlockStates.Items.AddRange(resourcePack.getBlockNames().ToArray());
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

        private void SetPalette(Dictionary<MCBlockVariant, Bitmap> palette)
        {
            this.palette.Clear();
            foreach (KeyValuePair<MCBlockVariant, Bitmap> kv in palette)
            {
                if (!variantsForBlockState.ContainsKey(kv.Key.blockState.ToString()))
                {
                    variantsForBlockState.Add(kv.Key.blockState.ToString(), new List<MCBlockVariant>());
                }
                variantsForBlockState[kv.Key.blockState.ToString()].Add(kv.Key);

                this.palette.Add(kv.Key.ToString(), kv.Value);
            }
        }

        private void checkedListBoxBlockVariants_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBoxVariants.Items.Clear();
            List<MCBlockVariant> variantsForBlockState;
            if (!this.variantsForBlockState.TryGetValue(checkedListBoxBlockStates.SelectedItem.ToString(), out variantsForBlockState))
                return;
            checkedListBoxVariants.Items.AddRange(variantsForBlockState.ToArray());
        }

        private void checkedListBoxVariants_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTextureImage(checkedListBoxVariants.SelectedItem);
        }

        private void SetTextureImage(object selectedVariant)
        {
            if (selectedVariant == null)
                textureImage.Image = null;
            else
                textureImage.Image = palette[selectedVariant.ToString()];
        }

        private void cmbFacing_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO: this is not yet returned to the main form for the image converter. Can we use the resourcePack for that??
            if (!TryGetSide(out selectedSide))
                return;
            SetPalette(resourcePack.GetPalette(selectedSide));
            SetTextureImage(checkedListBoxVariants.SelectedItem);
            parentForm.SetSelectedSide(selectedSide);
        }
    }
}
