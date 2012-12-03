using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

namespace PhoenixJuliaFractal.ProgramExecutionStrategy
{
    unsafe public class AsemblerExecutionStrategy : IProgramExecutionStrategy
    {
        //[DllImport("JuliaFractalAssembly.dll")]
        //public static extern int Dodaj(int a, int b);
        [DllImport("JuliaFractalAssembly.dll")]
        public static extern int ProcessJulia(int* imageTab, int imageTabSize, int imageWidth, int imageHeight, double imageWidthQ, double imageHeightQ, double rangeXStart, double rangeXStop, double rangeYStart, double rangeYStop, double CRe, double CIm);
        //public static extern int ProcessJuliaLocal(int* imageTab, int imageTabSize, int imageWidth, int imageHeight, double imageWidthQ, double imageHeightQ, double rangeXStart, double rangeXStop, double rangeYStart, double rangeYStop, double CRe, double CIm);

        public void execute(ref int[] imageBytes, int offsetStart, int offsetStop, int imageWidth, int imageHeight, double rangeXStart, double rangeXStop, double rangeYStart, double rangeYStop, double CRe, double CIm)
        {
            /*Thread.Sleep(50);
            return;*/
            //int result = Dodaj(3, 5);
            int result = 0;
            //imageBytes = new int[imageWidth * imageHeight];

            fixed (int* imageBytesPtr = &imageBytes[0])
            {
                //for (int e = 0; e < 10; ++e)
                result = ProcessJulia(imageBytesPtr, imageBytes.Length, imageWidth, imageHeight, Convert.ToDouble(imageWidth), Convert.ToDouble(imageHeight), rangeXStart, rangeXStop, rangeYStart, rangeYStop, CRe, CIm);
            }

            // render
            int iteration = 0;
            int color = 0;
            for (int i = 0; i < imageBytes.Length; ++i)
            {
                iteration = imageBytes[i];

                color = 0;

                color += (int)(255.0 * iteration / 120);
                color = color << 8;

                color += (int)(255.0 * iteration / 120);
                color = color << 8;

                color += (int)(255.0 * Math.Log(iteration) / Math.Log(120));
                imageBytes[i] = color;
            }

            //System.Console.WriteLine(result.ToString());
            
            //Thread.Sleep(100);
        }
    }
}
