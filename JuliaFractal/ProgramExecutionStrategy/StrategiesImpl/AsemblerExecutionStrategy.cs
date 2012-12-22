using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PhoenixJuliaFractal.ProgramExecutionStrategy
{
    unsafe public class AsemblerExecutionStrategy : IProgramExecutionStrategy
    {
        [DllImport("JuliaFractalAssembly.dll")]
        public static extern int ProcessJulia(int* imageTab, int imageTabSize, int offsetStart, int offsetStop, int imageWidth, int imageHeight, double imageWidthQ, double imageHeightQ, double rangeXStart, double rangeXStop, double rangeYStart, double rangeYStop, double CRe, double CIm);
        
        public void execute(ref int[] imageBytes, int offsetStart, int offsetStop, int imageWidth, int imageHeight, double rangeXStart, double rangeXStop, double rangeYStart, double rangeYStop, double CRe, double CIm)
        {
            int result = 0;

            // The fixed statement prevents the garbage collector from
            // relocating a movable variable.
            // 
            // The fixed statement sets a pointer to a managed variable
            // and "pins" that variable during the execution of statement.
            // Without fixed, pointers to movable managed variables would
            // be of little use since garbage collection could relocate
            // the variables unpredictably.
            fixed (int* imageBytesPtr = &imageBytes[0])
            {
                try
                {
                    result = ProcessJulia(imageBytesPtr, imageBytes.Length, offsetStart, offsetStop, imageWidth, imageHeight, Convert.ToDouble(imageWidth), Convert.ToDouble(imageHeight), rangeXStart, rangeXStop, rangeYStart, rangeYStop, CRe, CIm);

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
    }
}
