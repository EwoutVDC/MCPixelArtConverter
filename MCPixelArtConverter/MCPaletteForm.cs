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
        MCResourcePack resourcePack;

        public MCPaletteForm(MCResourcePack rp)
        {
            InitializeComponent();
            
            resourcePack = rp;

            cmbSide.DataSource = Enum.GetValues(typeof(Sides));
            cmbSide.SelectedItem = resourcePack.SelectedSide;
            cmbSide.SelectedIndexChanged += cmbSide_SelectedIndexChanged;
            checkedListBoxBlockStates.Items.AddRange(resourcePack.getBlockStates().ToArray());
            
            for (int i = 0; i <= (checkedListBoxBlockStates.Items.Count - 1); i++)
                checkedListBoxBlockStates.SetItemCheckState(i, CheckState.Checked);
        }

        private void checkedListBoxBlockStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            checkedListBoxVariants.Items.Clear();
            MCBlockState blockState = (MCBlockState)checkedListBoxBlockStates.SelectedItem;
            MCBlockVariant[] variants = blockState.GetVariants().ToArray();
            checkedListBoxVariants.Items.AddRange(variants);

            for (int i = 0; i <= (checkedListBoxVariants.Items.Count - 1); i++)
                checkedListBoxVariants.SetItemCheckState(i, variants[i].Selected == true ? CheckState.Checked : CheckState.Unchecked);
        }

        private void checkedListBoxVariants_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTextureImage((MCBlockVariant)checkedListBoxVariants.SelectedItem);
        }

        private void SetTextureImage(MCBlockVariant selectedVariant)
        {
            if (selectedVariant == null)
                textureImage.Image = null;
            else
            {
                textureImage.Image = resourcePack.GetPalette()[selectedVariant];
            }
        }

        private void cmbSide_SelectedIndexChanged(object sender, EventArgs e)
        {
            resourcePack.SelectedSide = (Sides)cmbSide.SelectedValue;
            SetTextureImage((MCBlockVariant)checkedListBoxVariants.SelectedItem);
        }

        private void checkedListBoxBlockStates_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            MCBlockState blockState = (MCBlockState) checkedListBoxBlockStates.Items[e.Index];
            blockState.SetSelected(e.NewValue == CheckState.Checked);

            //just change checkbox, the variant change is already done from the blockstate
            checkedListBoxVariants.ItemCheck -= checkedListBoxVariants_ItemCheck;
            for (int i = 0; i <= (checkedListBoxVariants.Items.Count - 1); i++)
            { 
                checkedListBoxVariants.SetItemCheckState(i, e.NewValue);
            }
            checkedListBoxVariants.ItemCheck += checkedListBoxVariants_ItemCheck;
        }

        private void checkedListBoxVariants_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            MCBlockVariant variant = (MCBlockVariant)checkedListBoxVariants.Items[e.Index];
            variant.Selected = (e.NewValue == CheckState.Checked);
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            //TODO: allow saving and loading preset selections (none, all, survival, wool, custom)
            //loaded from plain text file with list of variant names
            for (int i = 0; i <= (checkedListBoxBlockStates.Items.Count - 1); i++)
            {
                checkedListBoxBlockStates.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
