using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPorohWPF
{
    public class Func
    {
        static public string GetDecimalSeparator()
        {
            bool fl = false;
            string a = "12.3";
            string b = "12,3";
            double f;
            fl = double.TryParse(a,out f);
            if (fl) return ".";
            fl = double.TryParse(b, out f);
            if (fl) return ",";
            return ";";
        }
        static public string ConvertToFloat(string f)
        {
            string ndc;
            string fl;
            string dc = GetDecimalSeparator();
            bool test;
            double flt;
            int poz;

            test = double.TryParse(f,out flt);
            if (test) return f;

            if (dc == ".") ndc = ",";
            else ndc = ".";

            poz = f.IndexOf(ndc);

            if (poz == 0) throw (0);
            fl = f.Substring(1, poz - 1) + dc + f.Substring(poz + 1, f.Length - poz);
            return fl;

        }
        static public double ConvertToFloatF(string f)
        {
            return Convert.ToDouble(f);
        }
        static public string RoundS(double f, int x)
        {
            double g;
            double h = Math.Pow(10, x);
            g = (Convert.ToInt32(f * h + 0.5)) / h;
            return g.ToString();
        }
        static public double RoundF(double f, int x)
        {
            double g;
            double h = Math.Pow(10, x);
            g = ((int)(f * h + 0.5)) / h;
            return g;
        }
    }
}
