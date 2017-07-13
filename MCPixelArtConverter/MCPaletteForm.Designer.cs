namespace MCPixelArtConverter
{
    partial class MCPaletteForm
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
            this.textureImage = new MCPixelArtConverter.PictureBoxWithSettings();
            this.checkedListBoxVariants = new System.Windows.Forms.CheckedListBox();
            this.lblSide = new System.Windows.Forms.Label();
            this.cmbSide = new System.Windows.Forms.ComboBox();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.btnSaveSelection = new System.Windows.Forms.Button();
            this.btnLoadSelection = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.textureImage)).BeginInit();
            this.SuspendLayout();
            // 
            // checkedListBoxBlockStates
            // 
            this.checkedListBoxBlockStates.FormattingEnabled = true;
            this.checkedListBoxBlockStates.Location = new System.Drawing.Point(12, 42);
            this.checkedListBoxBlockStates.Name = "checkedListBoxBlockStates";
            this.checkedListBoxBlockStates.Size = new System.Drawing.Size(217, 499);
            this.checkedListBoxBlockStates.TabIndex = 2;
            this.checkedListBoxBlockStates.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxBlockStates_ItemCheck);
            this.checkedListBoxBlockStates.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxBlockStates_SelectedIndexChanged);
            // 
            // textureImage
            // 
            this.textureImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textureImage.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.textureImage.Location = new System.Drawing.Point(458, 44);
            this.textureImage.Name = "textureImage";
            this.textureImage.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default;
            this.textureImage.Size = new System.Drawing.Size(320, 320);
            this.textureImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.textureImage.TabIndex = 3;
            this.textureImage.TabStop = false;
            // 
            // checkedListBoxVariants
            // 
            this.checkedListBoxVariants.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxVariants.FormattingEnabled = true;
            this.checkedListBoxVariants.Location = new System.Drawing.Point(235, 42);
            this.checkedListBoxVariants.Name = "checkedListBoxVariants";
            this.checkedListBoxVariants.Size = new System.Drawing.Size(217, 499);
            this.checkedListBoxVariants.TabIndex = 4;
            this.checkedListBoxVariants.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxVariants_ItemCheck);
            this.checkedListBoxVariants.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxVariants_SelectedIndexChanged);
            // 
            // lblSide
            // 
            this.lblSide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSide.AutoSize = true;
            this.lblSide.Location = new System.Drawing.Point(463, 17);
            this.lblSide.Name = "lblSide";
            this.lblSide.Size = new System.Drawing.Size(56, 13);
            this.lblSide.TabIndex = 24;
            this.lblSide.Text = "Block side";
            // 
            // cmbSide
            // 
            this.cmbSide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSide.FormattingEnabled = true;
            this.cmbSide.Location = new System.Drawing.Point(525, 12);
            this.cmbSide.Name = "cmbSide";
            this.cmbSide.Size = new System.Drawing.Size(105, 21);
            this.cmbSide.TabIndex = 23;
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.Location = new System.Drawing.Point(93, 12);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(75, 23);
            this.btnUnselectAll.TabIndex = 25;
            this.btnUnselectAll.Text = "Unselect All";
            this.btnUnselectAll.UseVisualStyleBackColor = true;
            this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
            // 
            // btnSaveSelection
            // 
            this.btnSaveSelection.Location = new System.Drawing.Point(268, 12);
            this.btnSaveSelection.Name = "btnSaveSelection";
            this.btnSaveSelection.Size = new System.Drawing.Size(89, 23);
            this.btnSaveSelection.TabIndex = 26;
            this.btnSaveSelection.Text = "Save selection";
            this.btnSaveSelection.UseVisualStyleBackColor = true;
            this.btnSaveSelection.Click += new System.EventHandler(this.btnSaveSelection_Click);
            // 
            // btnLoadSelection
            // 
            this.btnLoadSelection.Location = new System.Drawing.Point(174, 12);
            this.btnLoadSelection.Name = "btnLoadSelection";
            this.btnLoadSelection.Size = new System.Drawing.Size(88, 23);
            this.btnLoadSelection.TabIndex = 28;
            this.btnLoadSelection.Text = "Load selection";
            this.btnLoadSelection.UseVisualStyleBackColor = true;
            this.btnLoadSelection.Click += new System.EventHandler(this.btnLoadSelection_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(12, 12);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 29;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // MCPaletteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 567);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnLoadSelection);
            this.Controls.Add(this.btnSaveSelection);
            this.Controls.Add(this.btnUnselectAll);
            this.Controls.Add(this.lblSide);
            this.Controls.Add(this.cmbSide);
            this.Controls.Add(this.checkedListBoxVariants);
            this.Controls.Add(this.textureImage);
            this.Controls.Add(this.checkedListBoxBlockStates);
            this.Name = "MCPaletteForm";
            this.Text = "MCPaletteForm";
            ((System.ComponentModel.ISupportInitialize)(this.textureImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxBlockStates;
        private PictureBoxWithSettings textureImage;
        private System.Windows.Forms.CheckedListBox checkedListBoxVariants;
        private System.Windows.Forms.Label lblSide;
        private System.Windows.Forms.ComboBox cmbSide;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.Button btnSaveSelection;
        private System.Windows.Forms.Button btnLoadSelection;
        private System.Windows.Forms.Button btnSelectAll;
    }
}