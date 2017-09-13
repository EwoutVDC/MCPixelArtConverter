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

            UpdateBlockStateCheckBoxes();

        }

        private void UpdateBlockStateCheckBoxes()
        {
            MCBlockState[] blockStates = resourcePack.getBlockStates().ToArray();

            int selectedIndex = checkedListBoxBlockStates.SelectedIndex;
            checkedListBoxBlockStates.Items.Clear();
            checkedListBoxBlockStates.Items.AddRange(blockStates);

            for (int i = 0; i <= (checkedListBoxBlockStates.Items.Count - 1); i++)
            {
                if (blockStates[i].GetSelected())
                    checkedListBoxBlockStates.SetItemCheckState(i, CheckState.Checked);
                else
                    checkedListBoxBlockStates.SetItemCheckState(i, CheckState.Unchecked);
            }
            
            if (selectedIndex != -1)
                checkedListBoxBlockStates.SetSelected(selectedIndex, true);
            else if (checkedListBoxBlockStates.Items.Count > 0)
                checkedListBoxBlockStates.SetSelected(0, true);
        }

        private void checkedListBoxBlockStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            UpdateBlockVariantCheckBoxes(false);
        }

        private void UpdateBlockVariantCheckBoxes(bool restoreSelection)
        {
            int selectedIndex = checkedListBoxVariants.SelectedIndex;

            checkedListBoxVariants.Items.Clear();
            MCBlockState blockState = (MCBlockState)checkedListBoxBlockStates.SelectedItem;
            if (blockState == null)
                return;

            MCBlockVariant[] variants = blockState.GetVariants().ToArray();
            checkedListBoxVariants.Items.AddRange(variants);

            for (int i = 0; i <= (checkedListBoxVariants.Items.Count - 1); i++)
                checkedListBoxVariants.SetItemCheckState(i, variants[i].Selected ? CheckState.Checked : CheckState.Unchecked);

            if (restoreSelection && selectedIndex != -1)
                checkedListBoxVariants.SetSelected(selectedIndex, true);
            else if (checkedListBoxVariants.Items.Count > 0)
                checkedListBoxVariants.SetSelected(0, true);

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

            //just change checkbox, the resource pack change is already done from the blockstate
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
            UpdateBlockStateCheckBoxes();
            UpdateBlockVariantCheckBoxes(true);
        }
    }
}
