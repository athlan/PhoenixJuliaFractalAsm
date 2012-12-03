using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoenixJuliaFractal.ProgramExecutionStrategy.Strategies
{
    class ProgramExecutionStartegyParams
    {
        public int ThradId { get; set; }
        public int Offset { get; set; }
        public IProgramExecutionStrategy Strategy { get; set; }

        public int ExecutionOffsetStart { get; set; }
        public int ExecutionOffsetStop { get; set; }

        public int[] ImageBytes { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public double RangeXStart { get; set; }
        public double RangeXStop { get; set; }
        public double RangeYStart { get; set; }
        public double RangeYStop { get; set; }
        public double ParamCRe { get; set; }
        public double ParamCIm { get; set; }
    }
}
