using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using PhoenixJuliaFractal.ProgramExecutionStrategy;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.ComponentModel;
using PhoenixJuliaFractal.ProgramExecutionStrategy.Strategies;

namespace PhoenixJuliaFractal
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Program runner = new Program();
        }

        private MainForm form;
        private Stopwatch stopwatch;

        protected int[] imageBytes;

        protected int imageWidth = 600;
        protected int imageHeight = 600;

        public Program()
        {
            this.form = new MainForm(this);
            this.stopwatch = new Stopwatch();

            bw.WorkerReportsProgress = false;
            bw.WorkerSupportsCancellation = false;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            
            Application.Run(this.form);
        }

        public void ThreadStartHaha(object o)
        {
            if (!(o is ProgramExecutionStartegyParams))
                return;
            
            ProgramExecutionStartegyParams param = o as ProgramExecutionStartegyParams;
            //Thread.Sleep(param.ThradId * 1000);
            //return;
            int offsetStart = param.ExecutionOffsetStart;
            int offsetStop  = param.ExecutionOffsetStop;

            int[] imageBytes     = param.ImageBytes;
            int imageWidth       = param.ImageWidth;
            int imageHeight      = param.ImageHeight;
            double RangeXStart   = param.RangeXStart;
            double RangeXStop    = param.RangeXStop;
            double RangeYStart   = param.RangeYStart;
            double RangeYStop    = param.RangeYStop;
            double CRe           = param.ParamCRe;
            double CIm           = param.ParamCIm;

            param.Strategy.execute(ref imageBytes, offsetStart, offsetStop, imageWidth, imageHeight, RangeXStart, RangeXStop, RangeYStart, RangeYStop, CRe, CIm);

            //this.runExecutionCompleted();

        }

        public void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            IProgramExecutionStrategy strategy = ProgramExecutionStrategyFactory.getStrategy(this.form.ExecutionMode);
            int coresCount = this.form.ParamCores;

            this.form.setExecution(true);

            Thread[] threadPool = new Thread[coresCount];
            // MessageBox.Show("cores selected: "  + threadPool.Length.ToString());
            double RangeXStart = this.form.RangeXStart;
            double RangeXStop = this.form.RangeXStop;
            double RangeYStart = this.form.RangeYStart;
            double RangeYStop = this.form.RangeYStop;
            double CRe = this.form.ParamCRe;
            double CIm = this.form.ParamCIm;

            imageBytes = new int[imageWidth * imageHeight];

            for (int i = 0; i < imageBytes.Length; ++i)
                imageBytes[i] = 5;
            
            ProgramExecutionStartegyParams threadParams;

            int ExecutionStepsStop = imageHeight;
            int ExecutionStepsPerProcess = (int)Math.Ceiling((double)ExecutionStepsStop / threadPool.Length);
            int ExecutionStepsTotal = 0;

            for (int i = 0; i < threadPool.Length; ++i)
            {
                threadParams = new ProgramExecutionStartegyParams();
                threadParams.ThradId = i + 1;
                //threadParams.Offset = 1;
                threadParams.Strategy = strategy;

                ExecutionStepsTotal += ExecutionStepsPerProcess;

                threadParams.ExecutionOffsetStart = i * ExecutionStepsPerProcess;

                if (threadParams.ThradId == threadPool.Length)
                    threadParams.ExecutionOffsetStop = ExecutionStepsStop;
                else
                    threadParams.ExecutionOffsetStop = ExecutionStepsTotal;

                //int[] imageBytes2 = new int[imageWidth * imageHeight];

                threadParams.ImageBytes = imageBytes;
                threadParams.ImageWidth = imageWidth;
                threadParams.ImageHeight = imageHeight;
                threadParams.RangeXStart = RangeXStart;
                threadParams.RangeXStop = RangeXStop;
                threadParams.RangeYStart = RangeYStart;
                threadParams.RangeYStop = RangeYStop;
                threadParams.ParamCRe = CRe;
                threadParams.ParamCIm = CIm;

                threadPool[i] = new Thread(new ParameterizedThreadStart(ThreadStartHaha));
                threadPool[i].Start(threadParams);
            }

            stopwatch.Restart();

            for (int i = 0; i < threadPool.Length; ++i)
            {
                threadPool[i].Join();
            }

            stopwatch.Stop();

        }

        public void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
            }
            else
            {
            }

            stopwatch.Stop();

            this.form.displayExecutionTime(stopwatch.ElapsedMilliseconds);
            
            this.form.pictureBox1.Image = new Bitmap(imageWidth, imageHeight);
            Graphics graph = Graphics.FromImage(this.form.pictureBox1.Image);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, imageWidth, imageHeight);

            int x;
            int y;
            int colorValue;
            Pen pen = new Pen(Color.Black);

            for (int i = 0; i < imageWidth * imageHeight; ++i)
            {
                y = (int)Math.Floor((double)(i / imageWidth));
                x = i % imageWidth;

                colorValue = imageBytes[i];

                pen.Color = Color.FromArgb((colorValue >> 16) & 255, (colorValue >> 8) & 255, colorValue & 255);

                graph.DrawRectangle(pen, x, y, 1, 1);
            }
            
            this.form.setExecution(false);
        }

        protected BackgroundWorker bw = new BackgroundWorker();

        public void runExecution()
        {
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
            }
            else
            {
                if (bw.WorkerSupportsCancellation == true)
                {
                    bw.CancelAsync();
                }
            }
        }
    }
}
