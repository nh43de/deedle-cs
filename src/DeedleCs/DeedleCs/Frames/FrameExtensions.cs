using Deedle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeedleCs.Frames
{
    public static class FrameExtensions
    {
        public static Frame<TRowKey, TColumnKey> Acos<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Acos<TRowKey>((Func<double, double>)new Frame.Acos()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Asin<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Asin<TRowKey>((Func<double, double>)new Frame.Asin()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Atan<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Atan<TRowKey>((Func<double, double>)new Frame.Atan()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Sin<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Sin<TRowKey>((Func<double, double>)new Frame.Sin()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Sinh<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Sinh<TRowKey>((Func<double, double>)new Frame.Sinh()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Cos<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Cos<TRowKey>((Func<double, double>)new Frame.Cos()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Cosh<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Cosh<TRowKey>((Func<double, double>)new Frame.Cosh()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Tan<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Tan<TRowKey>((Func<double, double>)new Frame.Tan()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Tanh<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Tanh<TRowKey>((Func<double, double>)new Frame.Tanh()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Abs<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Abs<TRowKey>((Func<double, double>)new Frame.Abs()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Ceiling<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Ceiling<TRowKey>((Func<double, double>)new Frame.Ceiling()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Exp<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Exp<TRowKey>((Func<double, double>)new Frame.Exp()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Floor<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Floor<TRowKey>((Func<double, double>)new Frame.Floor()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Truncate<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Truncate<TRowKey>((Func<double, double>)new Frame.Truncate()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Log<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Log<TRowKey>((Func<double, double>)new Frame.Log()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Log10<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Log10<TRowKey>((Func<double, double>)new Frame.Log10()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Round<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Round<TRowKey>((Func<double, double>)new Frame.Round()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Sign<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Sign<TRowKey>((Func<double, int>)new Frame.Sign()).Invoke));
        }

        public static Frame<TRowKey, TColumnKey> Sqrt<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
        {
            return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Sqrt<TRowKey>((Func<double, double>)new Frame.Sqrt()).Invoke));
        }

    }
}
