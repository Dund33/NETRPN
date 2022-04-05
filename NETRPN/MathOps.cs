using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETRPN
{
    internal class MathOps
    {
        public double Eval(double arg1, double? arg2, string function)
        {
            if (function == "SIN")
            {
                return Math.Sin(arg1);
            }

            if (function == "COS")
            {
                return Math.Cos(arg1);
            }

            if (function == "TAN")
            {
                return Math.Cos(arg1);
            }

            if (function == "SQRT")
            {
                return Math.Sqrt(arg1);
            }

            if(arg2 is null)
                throw new ArgumentException();

            if (function == "*")
            {
                return arg1 * arg2.Value;
            }

            if (function == "/")
            {
                return arg2.Value / arg1;
            }

            if (function == "+")
            {
                return arg1 + arg2.Value;
            }

            if (function == "-")
            {
                return arg2.Value - arg1;
            }

            throw new InvalidOperationException("Invalid operation requested");
        }
    }
}
