using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPorohWPF
{
    class CPoroh : IEquatable<CPoroh>
    {
        public int ID { get; set; }
        public string Name { get; set; }   //Название смеси
        public double Power { get; set; }        //сила пороха
        public double Temper { get; set; }      //температура горения в K
        public double UdGaz { get; set; }        //удельная газопроизводительность
        public double Dens { get; set; }         //плотность  в гк/м3
        //public bool IsActive      //активна смесь или уже типа удалили ее

        public CPoroh()
        {
            //IsActive = false;
        }

        public CPoroh(int id, string Name, double Power, double Temper, double UdGaz, double Dens)
        {
            ID = id;
            this.Name = Name;
            this.Power = Power;
            this.Temper = Temper;
            this.UdGaz = UdGaz;
            this.Dens = Dens;
            //IsActive = false;
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(CPoroh other)
        {
            if(other is null)
                return false;

            return this.Name == other.Name
                && this.Power == other.Power
                && this.Temper == other.Temper
                && this.UdGaz == other.UdGaz
                && this.Dens == this.Dens;
        }
    }
}
