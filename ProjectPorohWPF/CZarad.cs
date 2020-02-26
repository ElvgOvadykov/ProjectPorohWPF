using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPorohWPF
{
    class CZarad : IEquatable<CZarad>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Dnar { get; set; }        //диаметр наружный  ,мм
        public double Dvnutr { get; set; }      //диаметр внутренний ,мм
        public double L { get; set; }           //длина заряда    ,мм
        public CPoroh Poroh { get; set; }
        public static ObservableCollection<CPoroh> porohs {
            get {
                return new ObservableCollection<CPoroh>(DataBaseController.GetPorohs());
            }
            set { }
        }
        //public bool IsActive;     //активен заряд или типа удалили его уже
        public void SetParametr(string Name, double Dnar, double Dvnutr, double L)
        {
            this.Name = Name;
            this.Dnar = Dnar;
            this.Dvnutr = Dvnutr;
            this.L = L;
        }

        public void SetPoroh(CPoroh Poroh)
        {
            this.Poroh = Poroh;
        }

        public CZarad()
        {
            //IsActive = false;
            Poroh = null;
        }

        public CZarad(int id, string name, CPoroh poroh, double dnar, double dvnutr, double length)
        {
            ID = id;
            Name = name;
            Poroh = poroh;
            Dnar = dnar;
            Dvnutr = dvnutr;
            L = length;
            //IsActive = false;
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(CZarad other)
        {
            bool equalityPoroh = true;

            if (other is null)
                return false;

            if (this.Poroh == null)
                return false;

            return this.Name == other.Name
                && this.Dnar == other.Dnar
                && this.Dvnutr == other.Dvnutr
                && this.L == other.L;
                //&& this.Poroh.Equals(other.Poroh);
        }
    }
}
