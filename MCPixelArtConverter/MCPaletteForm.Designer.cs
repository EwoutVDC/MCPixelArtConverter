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
            this.cmbFacing = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.textureImage)).BeginInit();
            this.SuspendLayout();
            // 
            // checkedListBoxBlockStates
            // 
            this.checkedListBoxBlockStates.FormattingEnabled = true;
            this.checkedListBoxBlockStates.Location = new System.Drawing.Point(12, 12);
            this.checkedListBoxBlockStates.Name = "checkedListBoxBlockStates";
            this.checkedListBoxBlockStates.Size = new System.Drawing.Size(217, 529);
            this.checkedListBoxBlockStates.TabIndex = 2;
            this.checkedListBoxBlockStates.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxBlockVariants_SelectedIndexChanged);
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
            this.checkedListBoxVariants.Location = new System.Drawing.Point(235, 12);
            this.checkedListBoxVariants.Name = "checkedListBoxVariants";
            this.checkedListBoxVariants.Size = new System.Drawing.Size(217, 529);
            this.checkedListBoxVariants.TabIndex = 4;
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
            // cmbFacing
            // 
            this.cmbFacing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFacing.FormattingEnabled = true;
            this.cmbFacing.Location = new System.Drawing.Point(525, 12);
            this.cmbFacing.Name = "cmbFacing";
            this.cmbFacing.Size = new System.Drawing.Size(105, 21);
            this.cmbFacing.TabIndex = 23;
            this.cmbFacing.SelectedIndexChanged += new System.EventHandler(this.cmbFacing_SelectedIndexChanged);
            // 
            // MCPaletteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 567);
            this.Controls.Add(this.lblSide);
            this.Controls.Add(this.cmbFacing);
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
        private System.Windows.Forms.ComboBox cmbFacing;
    }
}