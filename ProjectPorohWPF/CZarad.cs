﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPorohWPF
{
    class CZarad
    {
        public int ID;
        public string Name;
        public CPoroh Poroh;
        public double Dnar;        //диаметр наружный  ,мм
        public double Dvnutr;      //диаметр внутренний ,мм
        public double L;           //длина заряда    ,мм
        public bool IsActive;     //активен заряд или типа удалили его уже
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
            IsActive = false;
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
            IsActive = false;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
