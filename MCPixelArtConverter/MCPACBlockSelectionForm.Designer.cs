namespace MCPixelArtConverter
{
    partial class MCPACBlockSelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkedListBoxBlockStates = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxVariants = new System.Windows.Forms.CheckedListBox();
            this.lblSide = new System.Windows.Forms.Label();
            this.cmbSide = new System.Windows.Forms.ComboBox();
            this.btnSaveSelection = new System.Windows.Forms.Button();
            this.btnLoadSelection = new System.Windows.Forms.Button();
            this.labelFilter = new System.Windows.Forms.Label();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.checkBoxSelectAllBlockStates = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textureImage = new MCPixelArtConverter.PictureBoxWithSettings();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textureImage)).BeginInit();
            this.SuspendLayout();
            // 
            // checkedListBoxBlockStates
            // 
            this.checkedListBoxBlockStates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.checkedListBoxBlockStates.IntegralHeight = false;
            this.checkedListBoxBlockStates.Location = new System.Drawing.Point(38, 131);
            this.checkedListBoxBlockStates.Name = "checkedListBoxBlockStates";
            this.checkedListBoxBlockStates.Size = new System.Drawing.Size(566, 676);
            this.checkedListBoxBlockStates.TabIndex = 32;
            this.checkedListBoxBlockStates.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxBlockStates_ItemCheck);
            this.checkedListBoxBlockStates.SelectedValueChanged += new System.EventHandler(this.checkedListBoxBlockStates_SelectedValueChanged);
            // 
            // checkedListBoxVariants
            // 
            this.checkedListBoxVariants.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxVariants.FormattingEnabled = true;
            this.checkedListBoxVariants.IntegralHeight = false;
            this.checkedListBoxVariants.Location = new System.Drawing.Point(0, 0);
            this.checkedListBoxVariants.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.checkedListBoxVariants.Name = "checkedListBoxVariants";
            this.checkedListBoxVariants.Size = new System.Drawing.Size(561, 676);
            this.checkedListBoxVariants.Sorted = true;
            this.checkedListBoxVariants.TabIndex = 7;
            this.checkedListBoxVariants.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxVariants_ItemCheck);
            this.checkedListBoxVariants.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxVariants_SelectedIndexChanged);
            // 
            // lblSide
            // 
            this.lblSide.AutoSize = true;
            this.lblSide.Location = new System.Drawing.Point(615, 88);
            this.lblSide.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblSide.Name = "lblSide";
            this.lblSide.Size = new System.Drawing.Size(152, 32);
            this.lblSide.TabIndex = 24;
            this.lblSide.Text = "Block side:";
            // 
            // cmbSide
            // 
            this.cmbSide.FormattingEnabled = true;
            this.cmbSide.Location = new System.Drawing.Point(783, 78);
            this.cmbSide.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.cmbSide.Name = "cmbSide";
            this.cmbSide.Size = new System.Drawing.Size(336, 39);
            this.cmbSide.TabIndex = 6;
            // 
            // btnSaveSelection
            // 
            this.btnSaveSelection.Location = new System.Drawing.Point(289, 16);
            this.btnSaveSelection.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnSaveSelection.Name = "btnSaveSelection";
            this.btnSaveSelection.Size = new System.Drawing.Size(237, 55);
            this.btnSaveSelection.TabIndex = 1;
            this.btnSaveSelection.Text = "Save selection";
            this.btnSaveSelection.UseVisualStyleBackColor = true;
            this.btnSaveSelection.Click += new System.EventHandler(this.btnSaveSelection_Click);
            // 
            // btnLoadSelection
            // 
            this.btnLoadSelection.Location = new System.Drawing.Point(38, 16);
            this.btnLoadSelection.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnLoadSelection.Name = "btnLoadSelection";
            this.btnLoadSelection.Size = new System.Drawing.Size(235, 55);
            this.btnLoadSelection.TabIndex = 0;
            this.btnLoadSelection.Text = "Load selection";
            this.btnLoadSelection.UseVisualStyleBackColor = true;
            this.btnLoadSelection.Click += new System.EventHandler(this.btnLoadSelection_Click);
            // 
            // labelFilter
            // 
            this.labelFilter.AutoSize = true;
            this.labelFilter.Location = new System.Drawing.Point(86, 88);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(86, 32);
            this.labelFilter.TabIndex = 30;
            this.labelFilter.Text = "Filter:";
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Location = new System.Drawing.Point(178, 83);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(426, 38);
            this.textBoxFilter.TabIndex = 4;
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // checkBoxSelectAllBlockStates
            // 
            this.checkBoxSelectAllBlockStates.AutoSize = true;
            this.checkBoxSelectAllBlockStates.Location = new System.Drawing.Point(46, 87);
            this.checkBoxSelectAllBlockStates.Name = "checkBoxSelectAllBlockStates";
            this.checkBoxSelectAllBlockStates.Size = new System.Drawing.Size(34, 33);
            this.checkBoxSelectAllBlockStates.TabIndex = 3;
            this.checkBoxSelectAllBlockStates.ThreeState = true;
            this.checkBoxSelectAllBlockStates.UseVisualStyleBackColor = true;
            this.checkBoxSelectAllBlockStates.CheckStateChanged += new System.EventHandler(this.checkBoxSelectAllBlockStates_CheckStateChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(621, 131);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.checkedListBoxVariants);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textureImage);
            this.splitContainer1.Size = new System.Drawing.Size(901, 701);
            this.splitContainer1.SplitterDistance = 569;
            this.splitContainer1.TabIndex = 31;
            // 
            // textureImage
            // 
            this.textureImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textureImage.BackColor = System.Drawing.SystemColors.Window;
            this.textureImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.textureImage.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.textureImage.Location = new System.Drawing.Point(4, 0);
            this.textureImage.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.textureImage.Name = "textureImage";
            this.textureImage.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            this.textureImage.Size = new System.Drawing.Size(316, 316);
            this.textureImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.textureImage.TabIndex = 3;
            this.textureImage.TabStop = false;
            // 
            // MCPACBlockSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1853, 893);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.checkBoxSelectAllBlockStates);
            this.Controls.Add(this.checkedListBoxBlockStates);
            this.Controls.Add(this.textBoxFilter);
            this.Controls.Add(this.labelFilter);
            this.Controls.Add(this.btnLoadSelection);
            this.Controls.Add(this.btnSaveSelection);
            this.Controls.Add(this.lblSide);
            this.Controls.Add(this.cmbSide);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "MCPACBlockSelectionForm";
            this.Text = "MC Pixel Art Block Selection";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textureImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private PictureBoxWithSettings textureImage;
        private System.Windows.Forms.CheckedListBox checkedListBoxVariants;
        private System.Windows.Forms.Label lblSide;
        private System.Windows.Forms.ComboBox cmbSide;
        private System.Windows.Forms.Button btnSaveSelection;
        private System.Windows.Forms.Button btnLoadSelection;
        private System.Windows.Forms.Label labelFilter;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.CheckedListBox checkedListBoxBlockStates;
        private System.Windows.Forms.CheckBox checkBoxSelectAllBlockStates;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}