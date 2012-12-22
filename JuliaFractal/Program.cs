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

        protected int imageWidth = 512;
        protected int imageHeight = 512;

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

        public void FireAllThreads(object o)
        {
            if (!(o is ProgramExecutionStartegyParams))
                return;
            
            ProgramExecutionStartegyParams param = o as ProgramExecutionStartegyParams;
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
            bool debugMode       = param.debugMode;

            param.Strategy.execute(ref imageBytes, offsetStart, offsetStop, imageWidth, imageHeight, RangeXStart, RangeXStop, RangeYStart, RangeYStop, CRe, CIm, debugMode);

        }

        public void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            IProgramExecutionStrategy strategy = ProgramExecutionStrategyFactory.getStrategy(this.form.ExecutionMode);
            int coresCount = this.form.ParamCores;

            this.form.setExecution(true);

            Thread[] threadPool = new Thread[coresCount];
            ProgramExecutionStartegyParams[] threadParamsStack = new ProgramExecutionStartegyParams[coresCount];

            double RangeXStart = this.form.RangeXStart;
            double RangeXStop = this.form.RangeXStop;
            double RangeYStart = this.form.RangeYStart;
            double RangeYStop = this.form.RangeYStop;
            double CRe = this.form.ParamCRe;
            double CIm = this.form.ParamCIm;
            bool debugModeEnabled = this.form.DebugMode;

            imageBytes = new int[imageWidth * imageHeight];

            for (int i = 0; i < imageBytes.Length; ++i)
                imageBytes[i] = 0;
            
            int ExecutionStepsStop = imageHeight;
            int ExecutionStepsPerProcess = (int)Math.Ceiling((double)ExecutionStepsStop / threadPool.Length);
            int ExecutionStepsTotal = 0;

            for (int i = 0; i < threadPool.Length; ++i)
            {
                threadParamsStack[i] = new ProgramExecutionStartegyParams();
                threadParamsStack[i].ThradId = i + 1;
                threadParamsStack[i].Strategy = ProgramExecutionStrategyFactory.getStrategy(this.form.ExecutionMode);

                ExecutionStepsTotal += ExecutionStepsPerProcess;

                threadParamsStack[i].ExecutionOffsetStart = i * ExecutionStepsPerProcess;

                if (threadParamsStack[i].ThradId == threadPool.Length)
                    threadParamsStack[i].ExecutionOffsetStop = ExecutionStepsStop;
                else
                    threadParamsStack[i].ExecutionOffsetStop = ExecutionStepsTotal;

                threadParamsStack[i].ImageBytes = new int[imageWidth * imageHeight];
                threadParamsStack[i].ImageWidth = imageWidth;
                threadParamsStack[i].ImageHeight = imageHeight;
                threadParamsStack[i].RangeXStart = RangeXStart;
                threadParamsStack[i].RangeXStop = RangeXStop;
                threadParamsStack[i].RangeYStart = RangeYStart;
                threadParamsStack[i].RangeYStop = RangeYStop;
                threadParamsStack[i].ParamCRe = CRe;
                threadParamsStack[i].ParamCIm = CIm;
                threadParamsStack[i].debugMode = debugModeEnabled;

                threadPool[i] = new Thread(new ParameterizedThreadStart(FireAllThreads));
            }

            for (int i = 0; i < threadPool.Length; ++i)
            {
                threadPool[i].Start(threadParamsStack[i]);
            }

            stopwatch.Restart();

            for (int i = 0; i < threadPool.Length; ++i)
            {
                threadPool[i].Join();
            }

            stopwatch.Stop();

            // merge data from threads
            for (int i = 0; i < threadPool.Length; ++i)
            {
                for (int n = threadParamsStack[i].ExecutionOffsetStart * threadParamsStack[i].ImageHeight; n < threadParamsStack[i].ExecutionOffsetStop * threadParamsStack[i].ImageHeight; ++n)
                {
                    imageBytes[n] = threadParamsStack[i].ImageBytes[n];
                }
            }
            
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
