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
            this.checkedListBoxBlockVariants = new System.Windows.Forms.CheckedListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkedListBoxBlockVariants
            // 
            this.checkedListBoxBlockVariants.FormattingEnabled = true;
            this.checkedListBoxBlockVariants.Location = new System.Drawing.Point(12, 12);
            this.checkedListBoxBlockVariants.Name = "checkedListBoxBlockVariants";
            this.checkedListBoxBlockVariants.Size = new System.Drawing.Size(217, 319);
            this.checkedListBoxBlockVariants.TabIndex = 2;
            this.checkedListBoxBlockVariants.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxBlockVariants_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(235, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 320);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // MCPaletteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 349);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkedListBoxBlockVariants);
            this.Name = "MCPaletteForm";
            this.Text = "MCPaletteForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxBlockVariants;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}