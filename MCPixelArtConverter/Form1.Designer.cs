namespace MCPixelArtConverter
{
    partial class MCPACMainForm
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
            this.LoadBlockInfoButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxAvailableBlocks = new System.Windows.Forms.ComboBox();
            this.btnShowTexture = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LoadBlockInfoButton
            // 
            this.LoadBlockInfoButton.Location = new System.Drawing.Point(13, 13);
            this.LoadBlockInfoButton.Name = "LoadBlockInfoButton";
            this.LoadBlockInfoButton.Size = new System.Drawing.Size(93, 23);
            this.LoadBlockInfoButton.TabIndex = 0;
            this.LoadBlockInfoButton.Text = "Load block info";
            this.LoadBlockInfoButton.UseVisualStyleBackColor = true;
            this.LoadBlockInfoButton.Click += new System.EventHandler(this.LoadBlockInfoButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Available blocks:";
            // 
            // comboBoxAvailableBlocks
            // 
            this.comboBoxAvailableBlocks.FormattingEnabled = true;
            this.comboBoxAvailableBlocks.Location = new System.Drawing.Point(13, 59);
            this.comboBoxAvailableBlocks.Name = "comboBoxAvailableBlocks";
            this.comboBoxAvailableBlocks.Size = new System.Drawing.Size(188, 21);
            this.comboBoxAvailableBlocks.TabIndex = 2;
            // 
            // btnShowTexture
            // 
            this.btnShowTexture.Location = new System.Drawing.Point(207, 59);
            this.btnShowTexture.Name = "btnShowTexture";
            this.btnShowTexture.Size = new System.Drawing.Size(129, 23);
            this.btnShowTexture.TabIndex = 3;
            this.btnShowTexture.Text = "Show selected texture";
            this.btnShowTexture.UseVisualStyleBackColor = true;
            this.btnShowTexture.Click += new System.EventHandler(this.btnShowTexture_Click);
            // 
            // MCPACMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 510);
            this.Controls.Add(this.btnShowTexture);
            this.Controls.Add(this.comboBoxAvailableBlocks);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LoadBlockInfoButton);
            this.Name = "MCPACMainForm";
            this.Text = "MC Pixel Art Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadBlockInfoButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxAvailableBlocks;
        private System.Windows.Forms.Button btnShowTexture;
    }
}

