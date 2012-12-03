using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace PhoenixJuliaFractal.ProgramExecutionStrategy
{
    public class Complex
    {
        public double Re;
        public double Im;

        public Complex()
        {
            this.Re = 0;
            this.Im = 0;
        }

        public Complex(double Re, double Im)
        {
            this.Re = Re;
            this.Im = Im;
        }

        public double modPow()
        {
            return (this.Re * this.Re) + (this.Im * this.Im);
        }
    }

    public class CSharpExecutionStrategy : IProgramExecutionStrategy
    {
        public void execute(ref int[] imageBytes, int offsetStart, int offsetStop, int imageWidth, int imageHeight, double RangeXStart, double RangeXStop, double RangeYStart, double RangeYStop, double CRe, double CIm)
        {
            Complex p = new Complex();
            Complex c = new Complex(CRe, CIm);

            int i, j;
            int segment;

            double ratioX = (RangeXStop - RangeXStart) / imageWidth;
            double ratioY = (RangeYStop - RangeYStart) / imageHeight;

            //for (i = 0; i < imageHeight; i++)
            //for (int e = 0; e < 10; ++e)
            //{
                for (i = offsetStart; i < offsetStop; i++)
                {
                    //for (int e = 0; e < 1000000; ++e) { double test = Math.Sin(e); }
                    //System.Console.WriteLine("tid: " + offsetStart + " it: " + i);
                    segment = i * imageHeight;

                    p.Im = i * ratioY + RangeYStart;

                    for (j = 0; j < imageWidth; j++)
                    {
                        p.Re = j * ratioX + RangeXStart;
                        imageBytes[segment + j] = levelSet(p, c);
                        //graph.DrawRectangle(levelSet(p, c), j, i, 1, 1);
                    }
                }
            //}
            //Thread.Sleep(500);
        }

        //function z[0]=p
        private Complex f(Complex p)
        {
            return p;
        }

        //function z[n+1] = z[n]^2 + Re(c) + Im(c)*Z[n-1]
        private Complex g(Complex zp, Complex z, Complex c)
        {
            Complex result = new Complex();
            result.Re = z.Re * z.Re - z.Im * z.Im + c.Re + c.Im * zp.Re;
            result.Im = 2 * z.Re * z.Im + c.Im * zp.Im;
            return result;
        }

        private int levelSet(Complex p, Complex c)
        {
            Complex z = new Complex();
            Complex z_next = new Complex();
            Complex z_prev = new Complex();

            int iteration;

            iteration = 0;
            z_prev.Re = 0;
            z_prev.Im = 0;
            z.Re = 0;
            z.Im = 0;

            //function z[0]=p
            z_next.Re = p.Re;
            z_next.Im = p.Im;

            double z_next_mod = 0;

            do
            {
                //function z[n+1] = z[n]^2 + Re(c) + Im(c)*Z[n-1]

                z_prev.Re = z.Re;
                z_prev.Im = z.Im;

                z.Re = z_next.Re;
                z.Im = z_next.Im;

                z_next = g(z_prev, z, c);
                //z_next.Re = z.Re * z.Re - z.Im * z.Im + c.Re + c.Im * z_prev.Re;
                //z_next.Im = 2 * z.Re * z.Im + c.Im * z_prev.Im;

                z_next_mod = z.modPow();
                //z_next_mod = (z.Re * z.Re) + (z.Im * z.Im);
                
                iteration++;
            } while (z_next_mod < 4 && iteration < 120);

            int color = 0;
            color += (int)(255.0 * iteration / 120);
            color = color << 8;

            color += (int)(255.0 * iteration / 120);
            color = color << 8;

            color += (int)(255.0 * Math.Log(iteration) / Math.Log(120));

            return color;
        }
    }
}
