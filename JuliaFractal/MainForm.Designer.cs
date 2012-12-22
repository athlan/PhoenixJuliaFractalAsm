using System;
namespace PhoenixJuliaFractal
{
    partial class MainForm
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupProperties = new System.Windows.Forms.GroupBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.comboCoresExecution = new System.Windows.Forms.ComboBox();
            this.labelCoresExecution = new System.Windows.Forms.Label();
            this.labelCoresCount = new System.Windows.Forms.Label();
            this.labelCores = new System.Windows.Forms.Label();
            this.radioRenderModeAssembler = new System.Windows.Forms.RadioButton();
            this.radioRenderModeCSharp = new System.Windows.Forms.RadioButton();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonRender = new System.Windows.Forms.Button();
            this.textCIm = new System.Windows.Forms.TextBox();
            this.textCRe = new System.Windows.Forms.TextBox();
            this.labelCIm = new System.Windows.Forms.Label();
            this.labelCRe = new System.Windows.Forms.Label();
            this.textRangeYStop = new System.Windows.Forms.TextBox();
            this.textRangeYStart = new System.Windows.Forms.TextBox();
            this.labelYStop = new System.Windows.Forms.Label();
            this.labelYStart = new System.Windows.Forms.Label();
            this.textRangeXStop = new System.Windows.Forms.TextBox();
            this.textRangeXStart = new System.Windows.Forms.TextBox();
            this.labelXStop = new System.Windows.Forms.Label();
            this.labelXStart = new System.Windows.Forms.Label();
            this.groupRender = new System.Windows.Forms.GroupBox();
            this.selectionPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.debugModeCheckBox = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.groupProperties.SuspendLayout();
            this.groupRender.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusProgressBar,
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 631);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(832, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(220, 16);
            // 
            // statusLabel
            // 
            this.statusLabel.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(585, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupProperties
            // 
            this.groupProperties.Controls.Add(this.debugModeCheckBox);
            this.groupProperties.Controls.Add(this.progressBar);
            this.groupProperties.Controls.Add(this.comboCoresExecution);
            this.groupProperties.Controls.Add(this.labelCoresExecution);
            this.groupProperties.Controls.Add(this.labelCoresCount);
            this.groupProperties.Controls.Add(this.labelCores);
            this.groupProperties.Controls.Add(this.radioRenderModeAssembler);
            this.groupProperties.Controls.Add(this.radioRenderModeCSharp);
            this.groupProperties.Controls.Add(this.buttonReset);
            this.groupProperties.Controls.Add(this.buttonRender);
            this.groupProperties.Controls.Add(this.textCIm);
            this.groupProperties.Controls.Add(this.textCRe);
            this.groupProperties.Controls.Add(this.labelCIm);
            this.groupProperties.Controls.Add(this.labelCRe);
            this.groupProperties.Controls.Add(this.textRangeYStop);
            this.groupProperties.Controls.Add(this.textRangeYStart);
            this.groupProperties.Controls.Add(this.labelYStop);
            this.groupProperties.Controls.Add(this.labelYStart);
            this.groupProperties.Controls.Add(this.textRangeXStop);
            this.groupProperties.Controls.Add(this.textRangeXStart);
            this.groupProperties.Controls.Add(this.labelXStop);
            this.groupProperties.Controls.Add(this.labelXStart);
            this.groupProperties.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupProperties.Location = new System.Drawing.Point(0, 0);
            this.groupProperties.Name = "groupProperties";
            this.groupProperties.Size = new System.Drawing.Size(220, 631);
            this.groupProperties.TabIndex = 1;
            this.groupProperties.TabStop = false;
            this.groupProperties.Text = "Rendering properties";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(6, 323);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(208, 23);
            this.progressBar.TabIndex = 19;
            // 
            // comboCoresExecution
            // 
            this.comboCoresExecution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCoresExecution.FormattingEnabled = true;
            this.comboCoresExecution.Location = new System.Drawing.Point(114, 182);
            this.comboCoresExecution.Name = "comboCoresExecution";
            this.comboCoresExecution.Size = new System.Drawing.Size(100, 21);
            this.comboCoresExecution.TabIndex = 18;
            this.comboCoresExecution.SelectedValueChanged += new System.EventHandler(this.comboCoresExecution_SelectedValueChanged);
            // 
            // labelCoresExecution
            // 
            this.labelCoresExecution.Location = new System.Drawing.Point(3, 184);
            this.labelCoresExecution.Name = "labelCoresExecution";
            this.labelCoresExecution.Size = new System.Drawing.Size(105, 15);
            this.labelCoresExecution.TabIndex = 17;
            this.labelCoresExecution.Text = "Execute on:";
            this.labelCoresExecution.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelCoresCount
            // 
            this.labelCoresCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCoresCount.AutoSize = true;
            this.labelCoresCount.Location = new System.Drawing.Point(131, 611);
            this.labelCoresCount.MinimumSize = new System.Drawing.Size(50, 0);
            this.labelCoresCount.Name = "labelCoresCount";
            this.labelCoresCount.Size = new System.Drawing.Size(50, 13);
            this.labelCoresCount.TabIndex = 16;
            this.labelCoresCount.Text = "1";
            // 
            // labelCores
            // 
            this.labelCores.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCores.AutoSize = true;
            this.labelCores.Location = new System.Drawing.Point(5, 611);
            this.labelCores.MinimumSize = new System.Drawing.Size(120, 0);
            this.labelCores.Name = "labelCores";
            this.labelCores.Size = new System.Drawing.Size(120, 13);
            this.labelCores.TabIndex = 15;
            this.labelCores.Text = "Cores avaliable:";
            // 
            // radioRenderModeAssembler
            // 
            this.radioRenderModeAssembler.Checked = true;
            this.radioRenderModeAssembler.Location = new System.Drawing.Point(8, 232);
            this.radioRenderModeAssembler.Name = "radioRenderModeAssembler";
            this.radioRenderModeAssembler.Size = new System.Drawing.Size(206, 24);
            this.radioRenderModeAssembler.TabIndex = 14;
            this.radioRenderModeAssembler.TabStop = true;
            this.radioRenderModeAssembler.Text = "Render in Assembler";
            this.radioRenderModeAssembler.UseVisualStyleBackColor = true;
            // 
            // radioRenderModeCSharp
            // 
            this.radioRenderModeCSharp.Location = new System.Drawing.Point(8, 215);
            this.radioRenderModeCSharp.Name = "radioRenderModeCSharp";
            this.radioRenderModeCSharp.Size = new System.Drawing.Size(206, 24);
            this.radioRenderModeCSharp.TabIndex = 1;
            this.radioRenderModeCSharp.Text = "Render in C#";
            this.radioRenderModeCSharp.UseVisualStyleBackColor = true;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(6, 271);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(100, 23);
            this.buttonReset.TabIndex = 13;
            this.buttonReset.Text = "Reset to defaults";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonRender
            // 
            this.buttonRender.Location = new System.Drawing.Point(114, 271);
            this.buttonRender.Name = "buttonRender";
            this.buttonRender.Size = new System.Drawing.Size(100, 23);
            this.buttonRender.TabIndex = 12;
            this.buttonRender.Text = "Render";
            this.buttonRender.UseVisualStyleBackColor = true;
            this.buttonRender.Click += new System.EventHandler(this.buttonRender_Click);
            // 
            // textCIm
            // 
            this.textCIm.Location = new System.Drawing.Point(114, 155);
            this.textCIm.Name = "textCIm";
            this.textCIm.Size = new System.Drawing.Size(100, 20);
            this.textCIm.TabIndex = 11;
            // 
            // textCRe
            // 
            this.textCRe.Location = new System.Drawing.Point(114, 128);
            this.textCRe.Name = "textCRe";
            this.textCRe.Size = new System.Drawing.Size(100, 20);
            this.textCRe.TabIndex = 10;
            // 
            // labelCIm
            // 
            this.labelCIm.Location = new System.Drawing.Point(3, 155);
            this.labelCIm.Name = "labelCIm";
            this.labelCIm.Size = new System.Drawing.Size(105, 15);
            this.labelCIm.TabIndex = 9;
            this.labelCIm.Text = "C (Im):";
            this.labelCIm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelCRe
            // 
            this.labelCRe.Location = new System.Drawing.Point(3, 130);
            this.labelCRe.Name = "labelCRe";
            this.labelCRe.Size = new System.Drawing.Size(105, 15);
            this.labelCRe.TabIndex = 8;
            this.labelCRe.Text = "C (Re):";
            this.labelCRe.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textRangeYStop
            // 
            this.textRangeYStop.Location = new System.Drawing.Point(114, 102);
            this.textRangeYStop.Name = "textRangeYStop";
            this.textRangeYStop.Size = new System.Drawing.Size(100, 20);
            this.textRangeYStop.TabIndex = 7;
            // 
            // textRangeYStart
            // 
            this.textRangeYStart.Location = new System.Drawing.Point(114, 75);
            this.textRangeYStart.Name = "textRangeYStart";
            this.textRangeYStart.Size = new System.Drawing.Size(100, 20);
            this.textRangeYStart.TabIndex = 6;
            // 
            // labelYStop
            // 
            this.labelYStop.Location = new System.Drawing.Point(3, 102);
            this.labelYStop.Name = "labelYStop";
            this.labelYStop.Size = new System.Drawing.Size(105, 15);
            this.labelYStop.TabIndex = 5;
            this.labelYStop.Text = "Y range stop:";
            this.labelYStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelYStart
            // 
            this.labelYStart.Location = new System.Drawing.Point(3, 77);
            this.labelYStart.Name = "labelYStart";
            this.labelYStart.Size = new System.Drawing.Size(105, 15);
            this.labelYStart.TabIndex = 4;
            this.labelYStart.Text = "Y range start:";
            this.labelYStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textRangeXStop
            // 
            this.textRangeXStop.Location = new System.Drawing.Point(114, 49);
            this.textRangeXStop.Name = "textRangeXStop";
            this.textRangeXStop.Size = new System.Drawing.Size(100, 20);
            this.textRangeXStop.TabIndex = 3;
            // 
            // textRangeXStart
            // 
            this.textRangeXStart.Location = new System.Drawing.Point(114, 22);
            this.textRangeXStart.Name = "textRangeXStart";
            this.textRangeXStart.Size = new System.Drawing.Size(100, 20);
            this.textRangeXStart.TabIndex = 2;
            // 
            // labelXStop
            // 
            this.labelXStop.Location = new System.Drawing.Point(3, 49);
            this.labelXStop.Name = "labelXStop";
            this.labelXStop.Size = new System.Drawing.Size(105, 15);
            this.labelXStop.TabIndex = 1;
            this.labelXStop.Text = "X range stop:";
            this.labelXStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelXStart
            // 
            this.labelXStart.Location = new System.Drawing.Point(3, 24);
            this.labelXStart.Name = "labelXStart";
            this.labelXStart.Size = new System.Drawing.Size(105, 15);
            this.labelXStart.TabIndex = 0;
            this.labelXStart.Text = "X range start:";
            this.labelXStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupRender
            // 
            this.groupRender.Controls.Add(this.selectionPanel);
            this.groupRender.Controls.Add(this.pictureBox1);
            this.groupRender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupRender.Location = new System.Drawing.Point(220, 0);
            this.groupRender.Name = "groupRender";
            this.groupRender.Size = new System.Drawing.Size(612, 631);
            this.groupRender.TabIndex = 2;
            this.groupRender.TabStop = false;
            this.groupRender.Text = "Rendering output";
            // 
            // selectionPanel
            // 
            this.selectionPanel.BackColor = System.Drawing.SystemColors.HighlightText;
            this.selectionPanel.Location = new System.Drawing.Point(3, 16);
            this.selectionPanel.Name = "selectionPanel";
            this.selectionPanel.Size = new System.Drawing.Size(0, 0);
            this.selectionPanel.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(606, 612);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // debugModeCheckBox
            // 
            this.debugModeCheckBox.AutoSize = true;
            this.debugModeCheckBox.Location = new System.Drawing.Point(6, 300);
            this.debugModeCheckBox.Name = "debugModeCheckBox";
            this.debugModeCheckBox.Size = new System.Drawing.Size(148, 17);
            this.debugModeCheckBox.TabIndex = 20;
            this.debugModeCheckBox.Text = "Enable brute-debug mode";
            this.debugModeCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 653);
            this.Controls.Add(this.groupRender);
            this.Controls.Add(this.groupProperties);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phoenix Julia Fractal";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupProperties.ResumeLayout(false);
            this.groupProperties.PerformLayout();
            this.groupRender.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void comboCoresExecution_SelectedValueChanged(object sender, System.EventArgs e)
        {
            object val = ((System.Windows.Forms.ComboBox) this.comboCoresExecution).SelectedItem;

            if (val is PhoenixJuliaFractal.DataSource.CoresDataSource)
            {
                this.coresSelected = ((PhoenixJuliaFractal.DataSource.CoresDataSource) val).Value;
            }
        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
        private System.Windows.Forms.GroupBox groupProperties;
        private System.Windows.Forms.Label labelXStart;
        private System.Windows.Forms.Label labelXStop;
        private System.Windows.Forms.TextBox textRangeXStart;
        private System.Windows.Forms.TextBox textRangeXStop;
        private System.Windows.Forms.GroupBox groupRender;
        private System.Windows.Forms.TextBox textRangeYStop;
        private System.Windows.Forms.TextBox textRangeYStart;
        private System.Windows.Forms.Label labelYStop;
        private System.Windows.Forms.Label labelYStart;
        private System.Windows.Forms.TextBox textCIm;
        private System.Windows.Forms.TextBox textCRe;
        private System.Windows.Forms.Label labelCIm;
        private System.Windows.Forms.Label labelCRe;
        public System.Windows.Forms.Button buttonRender;
        private System.Windows.Forms.Button buttonReset;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Panel pictureBoxSelection;
        private System.Windows.Forms.RadioButton radioRenderModeCSharp;
        private System.Windows.Forms.RadioButton radioRenderModeAssembler;
        private System.Windows.Forms.Label labelCoresCount;
        private System.Windows.Forms.Label labelCores;
        private System.Windows.Forms.ComboBox comboCoresExecution;
        private System.Windows.Forms.Label labelCoresExecution;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel selectionPanel;
        private System.Windows.Forms.CheckBox debugModeCheckBox;
    }
}

