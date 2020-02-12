using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPorohWPF
{
    class CPoroh
    {
        public int ID;
        public string Name;    //Название смеси
        public double Power;        //сила пороха
        public double Temper;       //температура горения в K
        public double UdGaz;        //удельная газопроизводительность
        public double Dens;         //плотность  в гк/м3
        public bool IsActive;      //активна смесь или уже типа удалили ее

        public CPoroh()
        {
            IsActive = false;
        }

        public CPoroh(int id, string Name, double Power, double Temper, double UdGaz, double Dens)
        {
            ID = id;
            this.Name = Name;
            this.Power = Power;
            this.Temper = Temper;
            this.UdGaz = UdGaz;
            this.Dens = Dens;
            IsActive = false;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
