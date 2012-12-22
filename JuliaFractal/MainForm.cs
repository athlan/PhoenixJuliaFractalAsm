using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PhoenixJuliaFractal.ProgramExecutionStrategy;
using PhoenixJuliaFractal.Model;

namespace PhoenixJuliaFractal
{
    public partial class MainForm : Form
    {
        protected PhoenixJuliaFractal.Program parent;

        protected int coresSelected;

        public MainForm(PhoenixJuliaFractal.Program parent)
        {
            this.parent = parent;
            
            InitializeComponent();
        }

        public ProgramExecutionStrategyType ExecutionMode
        {
            get
            {
                if (this.radioRenderModeAssembler.Checked)
                    return ProgramExecutionStrategyType.ASEMBLER;

                return ProgramExecutionStrategyType.CSHARP;
            }
        }


        public int ParamCores
        {
            get
            {
                return coresSelected;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.resetDefaults();
            this.displayExecutionTime(0);
        }

        private void buttonRender_Click(object sender, EventArgs e)
        {
            this.parent.runExecution();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            this.resetDefaults();
        }

        private int selectionStartX;
        private int selectionStartY;
        private bool selectionPerformed;

        private void pictureBox1_MouseDown(Object sender, MouseEventArgs e)
        {
            selectionStartX = e.Location.X;
            selectionStartY = e.Location.Y;
            selectionPerformed = true;
            this.selectionPanel.Visible = true;
        }

        private void pictureBox1_MouseUp(Object sender, MouseEventArgs e)
        {
            selectionPerformed = false;
            this.selectionPanel.Visible = false;

            double ratioWidthX = (double)this.selectionPanel.Width / (double)this.pictureBox1.Width;
            double ratioWidthY = (double)this.selectionPanel.Height / (double)this.pictureBox1.Height;

            double ratioStartX = (double)this.selectionPanel.Left / (double)this.pictureBox1.Width;
            double ratioStartY = (double)this.selectionPanel.Top / (double)this.pictureBox1.Height;

            double rangeX = (this.RangeXStop - this.RangeXStart);
            double rangeY = (this.RangeYStop - this.RangeYStart);

            double paramXStart = rangeX * ratioStartX + this.RangeXStart;
            double paramYStart = rangeY * ratioStartY + this.RangeYStart;
            double paramXStop = paramXStart + rangeX * ratioWidthX;
            double paramYStop = paramYStart + rangeY * ratioWidthY;

            this.RangeXStart = paramXStart;
            this.RangeXStop = paramXStop;
            this.RangeYStart = paramYStart;
            this.RangeYStop = paramYStop;

            this.buttonRender.PerformClick();
        }

        private void pictureBox1_MouseMove(Object sender, MouseEventArgs e)
        {
            if (selectionPerformed != true)
                return;

            int drawStartX = Math.Min(e.Location.X, selectionStartX);
            int drawStartY = Math.Min(e.Location.Y, selectionStartY);
            int drawBoxX = Math.Abs(e.Location.X - selectionStartX);
            int drawBoxY = Math.Abs(e.Location.Y - selectionStartY);

            this.selectionPanel.Top = this.pictureBox1.Top + drawStartY;
            this.selectionPanel.Left = this.pictureBox1.Left + drawStartX;
            this.selectionPanel.Width = drawBoxX;
            this.selectionPanel.Height = drawBoxY;
        }

        public void setExecution(bool state)
        {
            if (state)
            {
                Action buttonRenderAction = () => this.buttonRender.Enabled = false;
                this.buttonRender.Invoke(buttonRenderAction);

                Action progressBarAction = () => this.progressBar.Style = ProgressBarStyle.Marquee;
                this.progressBar.Invoke(progressBarAction);
            }
            else
            {
                Action buttonRenderAction = () => this.buttonRender.Enabled = true;
                this.buttonRender.Invoke(buttonRenderAction);

                Action progressBarAction = () => this.progressBar.Style = ProgressBarStyle.Blocks;
                this.progressBar.Invoke(progressBarAction);
            }
        }

        public void displayExecutionTime(long timeMilisecs)
        {
            this.statusLabel.Text = "Total Execution time: " + timeMilisecs + " ms.";
        }

        public void resetDefaults()
        {
            CoresModel model = new CoresModel();

            int cores = model.getCoresCount();

            this.comboCoresExecution.DataSource = model.getCoresExecutionModeDataSource();
            this.comboCoresExecution.DisplayMember = "Label";
            this.comboCoresExecution.ValueMember = "Value";

            this.comboCoresExecution.Select(0, 1);

            this.textRangeXStart.Text = "-1,5";
            this.textRangeXStop.Text = "1,5";

            this.textRangeYStart.Text = "-1,5";
            this.textRangeYStop.Text = "1,5";

            this.textCRe.Text = "0,56667";
            this.textCIm.Text = "-0,5";

            this.labelCoresCount.Text = cores.ToString();
        }

        public double RangeXStart
        {
            get
            {
                return Double.Parse(this.textRangeXStart.Text);
            }
            set
            {
                this.textRangeXStart.Text = value.ToString();
            }
        }

        public double RangeXStop
        {
            get
            {
                return Double.Parse(this.textRangeXStop.Text);
            }
            set
            {
                this.textRangeXStop.Text = value.ToString();
            }
        }

        public double RangeYStart
        {
            get
            {
                return Double.Parse(this.textRangeYStart.Text);
            }
            set
            {
                this.textRangeYStart.Text = value.ToString();
            }
        }

        public double RangeYStop
        {
            get
            {
                return Double.Parse(this.textRangeYStop.Text);
            }
            set
            {
                this.textRangeYStop.Text = value.ToString();
            }
        }

        public double ParamCRe
        {
            get
            {
                return Double.Parse(this.textCRe.Text);
            }
        }

        public double ParamCIm
        {
            get
            {
                return Double.Parse(this.textCIm.Text);
            }
        }

        public bool DebugMode
        {
            get
            {
                return this.debugModeCheckBox.Checked;
            }
        }
    }
}
