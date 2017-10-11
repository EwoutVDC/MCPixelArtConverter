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
            UpdateBlockStatesChecked();
        }

        private void UpdateBlockStatesChecked()
        {
            //Don't change selection of MCBlockVariants
            checkedListBoxBlockStates.ItemCheck -= checkedListBoxBlockStates_ItemCheck;
            for (int i = 0; i <= (checkedListBoxBlockStates.Items.Count - 1); i++)
            {
                checkedListBoxBlockStates.SetItemChecked(i, ((MCBlockState)checkedListBoxBlockStates.Items[i]).GetSelected());
            }
            checkedListBoxBlockStates.ItemCheck += checkedListBoxBlockStates_ItemCheck;
        }

        private void UpdateBlockStateChecked(MCBlockState blockState)
        {
            int index = checkedListBoxBlockStates.Items.IndexOf(blockState);
            if (index == -1)
                return;

            checkedListBoxBlockStates.ItemCheck -= checkedListBoxBlockStates_ItemCheck;
            checkedListBoxBlockStates.SetItemChecked(index, blockState.GetSelected());
            checkedListBoxBlockStates.ItemCheck += checkedListBoxBlockStates_ItemCheck;
        }

        private void checkedListBoxBlockStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            UpdateBlockVariants();
        }

        private void UpdateBlockVariants()
        {
            checkedListBoxVariants.Items.Clear();
            MCBlockState blockState = (MCBlockState)checkedListBoxBlockStates.SelectedItem;
            if (blockState == null)
                return;
            
            checkedListBoxVariants.Items.AddRange(blockState.GetVariants().ToArray());
            UpdateBlockVariantChecked();
        }

        private void UpdateBlockVariantChecked()
        {
            //Only update the checked value, don't update the MCBlockVariant or the block state selection box
            checkedListBoxVariants.ItemCheck -= checkedListBoxVariants_ItemCheck;
            for (int i = 0; i <= (checkedListBoxVariants.Items.Count - 1); i++)
                checkedListBoxVariants.SetItemChecked(i, ((MCBlockVariant)checkedListBoxVariants.Items[i]).Selected);
            checkedListBoxVariants.ItemCheck += checkedListBoxVariants_ItemCheck;
        }

        private void checkedListBoxBlockStates_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            MCBlockState blockState = (MCBlockState)checkedListBoxBlockStates.Items[e.Index];
            blockState.SetSelected(e.NewValue == CheckState.Checked);

            //just change checkbox, the MCBlockVariant selection is already done in the blockState.SetSelected
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
            //update blockstate checked property if one or none of the variants are selected
            //CheckedItems is not updated until after the event handler
            if ((checkedListBoxVariants.CheckedItems.Count == 0 && variant.Selected) ||
                (checkedListBoxVariants.CheckedItems.Count == 1 && !variant.Selected))
            {
                UpdateBlockStateChecked(variant.BlockState);
            }
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
                textureImage.Image = resourcePack.GetPalette()[selectedVariant];
        }

        private void cmbSide_SelectedIndexChanged(object sender, EventArgs e)
        {
            resourcePack.SelectedSide = (Sides)cmbSide.SelectedValue;
            SetTextureImage((MCBlockVariant)checkedListBoxVariants.SelectedItem);
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= (checkedListBoxBlockStates.Items.Count - 1); i++)
            {
                checkedListBoxBlockStates.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= (checkedListBoxBlockStates.Items.Count - 1); i++)
            {
                checkedListBoxBlockStates.SetItemCheckState(i, CheckState.Checked);
            }
        }

        private void btnSaveSelection_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Plain text files|*.txt";
            saveFileDialog.ShowDialog();

            //TODO: check dialog result first
            resourcePack.SaveBlockSelection(saveFileDialog.FileName);
        }

        private void btnLoadSelection_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Plain text files|*.txt";
            fileDialog.ShowDialog();

            if (!File.Exists(fileDialog.FileName))
            {
                MessageBox.Show("Could not load file '" + fileDialog.FileName + "'");
                return;
            }

            resourcePack.LoadBlockSelection(fileDialog.FileName);
            UpdateBlockStatesChecked();
            UpdateBlockVariantChecked();
        }
    }
}
