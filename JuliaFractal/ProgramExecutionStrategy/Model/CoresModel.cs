using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhoenixJuliaFractal.DataSource;

namespace PhoenixJuliaFractal.Model
{
    class CoresModel
    {
        public int getCoresCount()
        {
            return Environment.ProcessorCount;
        }

        public List<CoresDataSource> getCoresExecutionModeDataSource()
        {
            List<CoresDataSource> result = new List<CoresDataSource>();

            int cores = getCoresCount();

            double coresLog = Math.Log(cores, 2);

            if(Math.Floor(coresLog) != coresLog)
            {
                cores = (int) Math.Pow(2, coresLog);
            }

            CoresDataSource item;
            do
            {
                item = new CoresDataSource();
                item.Value = cores;
                item.Label = cores + ((cores > 1) ? " cores" : " core");

                result.Add(item);

                cores /= 2;
            }
            while (cores >= 1);

            return result;
        }
    }
}
