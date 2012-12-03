using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoenixJuliaFractal.ProgramExecutionStrategy
{
    public interface IProgramExecutionStrategy
    {
        void execute(ref int[] imageBytes, int offsetStart, int offsetStop, int imageWidth, int imageHeight, double RangeXStart, double RangeXStop, double RangeYStart, double RangeYStop, double CRe, double CIm);
    }
}
