using System;

namespace NETRPN
{
    internal class MathOps
    {
        public enum Operation
        {
            Sin,
            Cos,
            Tan,
            Sqrt,
            Mult,
            Div,
            Add,
            Sub,
            Exp,
            Log,
            Noop
        }

        public double Eval(double arg1, double? arg2, Operation function)
        {
            if (function == Operation.Sin)
            {
                return Math.Sin(arg1);
            }

            if (function == Operation.Cos)
            {
                return Math.Cos(arg1);
            }

            if (function == Operation.Tan)
            {
                return Math.Cos(arg1);
            }

            if (function == Operation.Sqrt)
            {
                return Math.Sqrt(arg1);
            }

            if(function == Operation.Exp)
            {
                return Math.Exp(arg1);
            }

            if(function == Operation.Log)
            {
                return Math.Log(arg1);
            }

            if (arg2 is null)
                throw new ArgumentException();

            if (function == Operation.Mult)
            {
                return arg1 * arg2.Value;
            }

            if (function == Operation.Div)
            {
                return arg2.Value / arg1;
            }

            if (function == Operation.Add)
            {
                return arg1 + arg2.Value;
            }

            if (function == Operation.Sub)
            {
                return arg2.Value - arg1;
            }

            if(function == Operation.Noop)
            {
                return default;
            }

            throw new InvalidOperationException("Invalid operation requested");
        }
    }
}
