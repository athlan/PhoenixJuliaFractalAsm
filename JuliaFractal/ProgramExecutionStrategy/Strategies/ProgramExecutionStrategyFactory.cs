using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoenixJuliaFractal.ProgramExecutionStrategy
{
    public enum ProgramExecutionStrategyType { CSHARP, ASEMBLER }
    
    class ProgramExecutionStrategyFactory
    {
        public static IProgramExecutionStrategy getStrategy(ProgramExecutionStrategyType strategy)
        {
            if(strategy.Equals(ProgramExecutionStrategyType.CSHARP))
                return new CSharpExecutionStrategy();

            else if(strategy.Equals(ProgramExecutionStrategyType.ASEMBLER))
                return new AsemblerExecutionStrategy();

            throw new InvalidOperationException();
        }
    }
}
