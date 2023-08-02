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
    partial class MCPACBlockSelectionForm : Form
    {
        MCResourcePack resourcePack;

        public MCPACBlockSelectionForm(MCResourcePack rp)
        {
            InitializeComponent();

            resourcePack = rp;

            cmbSide.DataSource = Enum.GetValues(typeof(Sides));
            cmbSide.SelectedItem = resourcePack.SelectedSide;
            cmbSide.SelectedIndexChanged += cmbSide_SelectedIndexChanged;

            var dt = new DataTable();

            dt.Columns.Add("Item", typeof(string));
            dt.Columns.Add("BlockState", typeof(MCBlockState));
            dt.Columns.Add("Checked", typeof(bool));

            foreach (var item in resourcePack.getBlockStates().ToArray())
            {
                // BlockStates without variants mess with the .GetSelected() method, which 
                // causes problems when filtering the list of BlockStates and updating the
                // "select all/none" checkbox to match.

                // Temporarily skipping items with no variants, until multipart blockstates are
                // handled. There won't be any variants included, so there wouldn't be
                // any bitmap to render anyway.

                if (item.GetVariants().Count < 1)
                    continue;
                dt.Rows.Add(item.FileName, item, false);
            }

            dt.AcceptChanges();

            DataView dv = dt.DefaultView;
            dv.Sort = "Item";

            checkedListBoxBlockStates.DataSource = dv;
            checkedListBoxBlockStates.DisplayMember = "Item";
            checkedListBoxBlockStates.ValueMember = "Item";

            UpdateBlockStatesChecked();

            // If not already done by the designer...
            checkedListBoxBlockStates.BindingContext[dv].CurrentChanged += checkedListBoxBlockStates_BindingContext_CurrentChanged;
        }

        private void UpdateBlockStatesChecked()
        {
            //Don't change selection of MCBlockVariants
            checkedListBoxBlockStates.ItemCheck -= checkedListBoxBlockStates_ItemCheck;
            for (int i = 0; i <= (checkedListBoxBlockStates.Items.Count - 1); i++)
            {
                checkedListBoxBlockStates.SetItemChecked(i, ((MCBlockState)((DataRowView)checkedListBoxBlockStates.Items[i])["BlockState"]).GetSelected());
            }
            checkedListBoxBlockStates.ItemCheck += checkedListBoxBlockStates_ItemCheck;

            UpdateCheckboxSelectAll();

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

        private void checkedListBoxBlockStates_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateBlockVariants();
        }

        private void UpdateBlockVariants(MCBlockState definedState = null)
        {
            checkedListBoxVariants.Items.Clear();
            MCBlockState blockState = (MCBlockState)((DataRowView)checkedListBoxBlockStates.SelectedItem)["BlockState"];
            if (definedState != null)
                blockState = definedState;
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

            ShowFirstSelectedVariantImage();
        }

        private void checkedListBoxBlockStates_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            MCBlockState blockState = (MCBlockState)((DataView)checkedListBoxBlockStates.DataSource)[e.Index]["BlockState"];
            blockState.SetSelected(e.NewValue == CheckState.Checked);

            UpdateBlockStatesChecked();

            // This is already happening. Unsure whether this is needed at all?
            //just change checkbox, the MCBlockVariant selection is already done in the blockState.SetSelected
            //checkedListBoxVariants.ItemCheck -= checkedListBoxVariants_ItemCheck;
            //for (int i = 0; i <= (checkedListBoxVariants.Items.Count - 1); i++)
            //{
            //    checkedListBoxVariants.SetItemCheckState(i, e.NewValue);
            //}
            //checkedListBoxVariants.ItemCheck += checkedListBoxVariants_ItemCheck;
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

        private void btnSaveSelection_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            saveFileDialog.Filter = "Plain text files|*.txt";


            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                resourcePack.SaveBlockSelection(saveFileDialog.FileName);
        }

        private void btnLoadSelection_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            fileDialog.Filter = "Plain text files|*.txt";

            if (fileDialog.ShowDialog() != DialogResult.OK)
                return;

            if (!File.Exists(fileDialog.FileName))
            {
                MessageBox.Show("Could not load file '" + fileDialog.FileName + "'");
                return;
            }

            resourcePack.LoadBlockSelection(fileDialog.FileName);
            UpdateBlockStatesChecked();
            UpdateBlockVariantChecked();
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            var dv = checkedListBoxBlockStates.DataSource as DataView;
            var filter = textBoxFilter.Text.Trim().Length > 0
                ? $"Item LIKE '*{textBoxFilter.Text}*'"
                : null;

            dv.RowFilter = filter;
            UpdateBlockStatesChecked();
            //UpdateBlockVariants();
        }

        private void checkBoxSelectAllBlockStates_CheckStateChanged(object sender, EventArgs e)
        {

            // Ensure that the user only has two states,
            // even thouth the tristate is used to represent a partial selection
            checkBoxSelectAllBlockStates.CheckStateChanged -= checkBoxSelectAllBlockStates_CheckStateChanged;
            if (checkBoxSelectAllBlockStates.CheckState == CheckState.Indeterminate)
            {
                checkBoxSelectAllBlockStates.CheckState = CheckState.Unchecked;
            }
            checkBoxSelectAllBlockStates.CheckStateChanged += checkBoxSelectAllBlockStates_CheckStateChanged;

            checkedListBoxBlockStates.ItemCheck -= checkedListBoxBlockStates_ItemCheck;
            checkedListBoxBlockStates.ItemCheck -= checkedListBoxBlockStates_ItemCheck;

            var chk = checkBoxSelectAllBlockStates.CheckState == CheckState.Checked ? true : false;
            for (var i = 0; i < checkedListBoxBlockStates.Items.Count; i++)
            {
                checkedListBoxBlockStates.SetItemChecked(i, chk);
                var drv = checkedListBoxBlockStates.Items[i] as DataRowView;
                MCBlockState blockstate = (MCBlockState)drv["BlockState"];
                blockstate.SetSelected(chk);
            }

            checkedListBoxBlockStates.ItemCheck += checkedListBoxBlockStates_ItemCheck;

        }

        private void UpdateCheckboxSelectAll()
        {

            checkBoxSelectAllBlockStates.CheckStateChanged -= checkBoxSelectAllBlockStates_CheckStateChanged;

            var selected = 0;

            for (var i = 0; i < checkedListBoxBlockStates.Items.Count; i++)
            {
                selected += checkedListBoxBlockStates.GetItemCheckState(i) == CheckState.Checked ? 1 : 0;
            }

            if (selected == 0)
            {
                checkBoxSelectAllBlockStates.CheckState = CheckState.Unchecked;
            } else if (selected == checkedListBoxBlockStates.Items.Count)
            {
                checkBoxSelectAllBlockStates.CheckState = CheckState.Checked;
            } else
            {
                checkBoxSelectAllBlockStates.CheckState = CheckState.Indeterminate;
            }

            checkBoxSelectAllBlockStates.CheckStateChanged += checkBoxSelectAllBlockStates_CheckStateChanged;

        }
        
        private void checkedListBoxBlockStates_BindingContext_CurrentChanged(object sender, EventArgs e)
        {
            MCBlockState blockState =
                ((MCBlockState)(
                    (DataRowView)
                        (
                            (CurrencyManager)sender
                        ).Current
                )["BlockState"]);

            UpdateBlockVariants(blockState);
        }

        private void ShowFirstSelectedVariantImage()
        {
            SetTextureImage((MCBlockVariant)checkedListBoxVariants.SelectedItem);
            for (int i = 0; i < checkedListBoxVariants.Items.Count; i++)
            {
                if (((MCBlockVariant)checkedListBoxVariants.Items[i]).Selected)
                {
                    checkedListBoxVariants.SelectedIndex = i;
                    break;
                }
            }

        }

    }
}
