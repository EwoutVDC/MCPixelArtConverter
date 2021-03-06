﻿namespace MCPixelArtConverter
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
            this.pictureBox = new MCPixelArtConverter.PictureBoxWithSettings();
            this.btnLoadPicture = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.txtHeigth = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.scaleTrackBar = new System.Windows.Forms.TrackBar();
            this.lblScaleValue = new System.Windows.Forms.Label();
            this.btnSelectBlocks = new System.Windows.Forms.Button();
            this.cmbDitherers = new System.Windows.Forms.ComboBox();
            this.cbDithering = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleTrackBar)).BeginInit();
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
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pictureBox.Location = new System.Drawing.Point(16, 110);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            this.pictureBox.Size = new System.Drawing.Size(561, 388);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            // 
            // btnLoadPicture
            // 
            this.btnLoadPicture.Location = new System.Drawing.Point(112, 13);
            this.btnLoadPicture.Name = "btnLoadPicture";
            this.btnLoadPicture.Size = new System.Drawing.Size(112, 23);
            this.btnLoadPicture.TabIndex = 5;
            this.btnLoadPicture.Text = "Load image";
            this.btnLoadPicture.UseVisualStyleBackColor = true;
            this.btnLoadPicture.Click += new System.EventHandler(this.btnLoadPicture_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(230, 13);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(105, 23);
            this.btnConvert.TabIndex = 6;
            this.btnConvert.Text = "Convert image";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // txtHeigth
            // 
            this.txtHeigth.Location = new System.Drawing.Point(391, 30);
            this.txtHeigth.Name = "txtHeigth";
            this.txtHeigth.Size = new System.Drawing.Size(57, 20);
            this.txtHeigth.TabIndex = 13;
            this.txtHeigth.Leave += new System.EventHandler(this.txtHeigth_Leave);
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(392, 4);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(56, 20);
            this.txtWidth.TabIndex = 14;
            this.txtWidth.Leave += new System.EventHandler(this.txtWidth_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(351, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Width";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(351, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Height";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(351, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Scale";
            // 
            // scaleTrackBar
            // 
            this.scaleTrackBar.LargeChange = 10;
            this.scaleTrackBar.Location = new System.Drawing.Point(391, 59);
            this.scaleTrackBar.Maximum = 200;
            this.scaleTrackBar.Minimum = 1;
            this.scaleTrackBar.Name = "scaleTrackBar";
            this.scaleTrackBar.Size = new System.Drawing.Size(151, 45);
            this.scaleTrackBar.TabIndex = 18;
            this.scaleTrackBar.TickFrequency = 10;
            this.scaleTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.scaleTrackBar.Value = 100;
            this.scaleTrackBar.Scroll += new System.EventHandler(this.scaleTrackBar_Scroll);
            // 
            // lblScaleValue
            // 
            this.lblScaleValue.AutoSize = true;
            this.lblScaleValue.Location = new System.Drawing.Point(542, 69);
            this.lblScaleValue.Name = "lblScaleValue";
            this.lblScaleValue.Size = new System.Drawing.Size(36, 13);
            this.lblScaleValue.TabIndex = 19;
            this.lblScaleValue.Text = "100 %";
            // 
            // btnSelectBlocks
            // 
            this.btnSelectBlocks.Location = new System.Drawing.Point(13, 38);
            this.btnSelectBlocks.Name = "btnSelectBlocks";
            this.btnSelectBlocks.Size = new System.Drawing.Size(93, 23);
            this.btnSelectBlocks.TabIndex = 20;
            this.btnSelectBlocks.Text = "Select blocks";
            this.btnSelectBlocks.UseVisualStyleBackColor = true;
            this.btnSelectBlocks.Click += new System.EventHandler(this.btnSelectBlocks_Click);
            // 
            // cmbDitherers
            // 
            this.cmbDitherers.Enabled = false;
            this.cmbDitherers.FormattingEnabled = true;
            this.cmbDitherers.Location = new System.Drawing.Point(224, 40);
            this.cmbDitherers.Name = "cmbDitherers";
            this.cmbDitherers.Size = new System.Drawing.Size(121, 21);
            this.cmbDitherers.TabIndex = 21;
            // 
            // cbDithering
            // 
            this.cbDithering.AutoSize = true;
            this.cbDithering.Location = new System.Drawing.Point(112, 42);
            this.cbDithering.Name = "cbDithering";
            this.cbDithering.Size = new System.Drawing.Size(102, 17);
            this.cbDithering.TabIndex = 22;
            this.cbDithering.Text = "Enable dithering";
            this.cbDithering.UseVisualStyleBackColor = true;
            this.cbDithering.CheckedChanged += new System.EventHandler(this.cbDithering_CheckedChanged);
            // 
            // MCPACMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 510);
            this.Controls.Add(this.cbDithering);
            this.Controls.Add(this.cmbDitherers);
            this.Controls.Add(this.btnSelectBlocks);
            this.Controls.Add(this.lblScaleValue);
            this.Controls.Add(this.scaleTrackBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.txtHeigth);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.btnLoadPicture);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.LoadBlockInfoButton);
            this.Name = "MCPACMainForm";
            this.Text = "MC Pixel Art Converter";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadBlockInfoButton;
        private PictureBoxWithSettings pictureBox;
        private System.Windows.Forms.Button btnLoadPicture;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.TextBox txtHeigth;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar scaleTrackBar;
        private System.Windows.Forms.Label lblScaleValue;
        private System.Windows.Forms.Button btnSelectBlocks;
        private System.Windows.Forms.ComboBox cmbDitherers;
        private System.Windows.Forms.CheckBox cbDithering;
    }
}

