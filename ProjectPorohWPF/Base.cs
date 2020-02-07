using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPorohWPF
{
    struct CGLOBAL
    {
        public double[] X, WS, RS, US;
        public double[] PS, ES, RG, UG;
        public double[] PG, EG, AMS;
        public double[] TY, PY, X1Y, X2Y, X3Y, X4Y, X5Y;
        public double[] Y, USX, PSX, XS;
        public double[] FT, FL, FU, FV, FW;

        public double DT, HS, B, AKK, R0, PGS, BB, HH, HH0, E0, EP, BGT;                        //COMMON/DSKV/
        public double FP, FPGOS, DNG, T0, GG, DG, DS;
        public double H0, E1, ZK, ALFA, DENS, A, ALL, PSG, DN;
        public double ANP, HM, NPF, YT, H1, HX1, TA, K1, TAY, STEP, TK;

        public double X1, X2, X3, V1, V2, TE0, AM0, VPSI, AKV;                          //COMMON/FD/
        public double DTP, VT, LT, HT, PSI, DVTP, AE1, CF1, CF2, CF3, HZAR;
        public double PKT, PT, HPI, SD, DGT, AMF, GGS, GG1, D02;
        public double AK, AK1, AK2, AK3, AK4, AK5, AK6, AK7;
        public double AKK1, P0, G0, PA, C02, C0;
        public double AIS, DVJ;                                                     //COMMON/AR/
        public double Z1, Z2, F1, F2, AEGOS, E1G, DSG, DGG, DDG, HGOS, DNGOS, AMGOS;    //COMMON/GGOS/ параметры горения основных зарядов
        public double HV1, HV2;
        public double TG, DTG, PGZ, PGG, PPL, PT0, VT1, CPL, CBG, PUAZ, IP;         //COMMON/FR/
        public int N0, N1, N2, N3, NT, NT1, NT2, NT3, NXG, NGR, NP, NPT1;                     //COMMON
        public double DX, ITP, HSP, TPX;
        public double PSS, TE, PST;
    }

    struct CBASEINTERNAL
    {
        public double ANGR1, ANGR2, ANP0, AGOS, TPL, NGR1, CF4;
        public double SDN, AND2, SDN2, PH, PVG, PH0, Z0, PSI0, VT0, XG;
        public int I;
    }

    struct CGGOSINTERNAL
    {
        public double F1P, CSA, F2P, DGS, DSS, EZ1, EZ2, DZ1, DZ2, DE1, DE2;
        public double AA, U1, ADMT, AMT, DMT, DVM, U2, D, D1, SA, TA;
    }

    struct CFRACINTERNAL
    {
        public double DP, Q1, Q, L0, P0, T0, W0, Q0;
        public double LL, HH, GG, QQ, VV, PPT, WW;
        public int NPT;
    }

    struct CSMESHINTERNAL
    {
        public double US1, XS1, AM1, AM2, US2, XS2, AM3, AS1, AS2, AS3, US3, PATM, XS3, TAY2, AMTP;
    }

    struct CRASPADINTERNAL
    {
        public double[] F;
        public double[] BG1, BG2, BG3;
        public double TT, X1P, X2P;
        public int I;
        public double A1, A2, A3, FS2, FS3, AS1, AS2;
        public double THX, FS1, AS3, HX;
        public double AA2, RC2, PR2, PP, PS2, AA1, R1, RS1, US1, UK;
        public double R2, RS2, US2, PK, PS1, AA12, A122, C11, CS1;
        public double V3, ES1, ES2, CS2, RC1, RC, RP1, RP2, CS, C31, C32, VTW, DV;
        public int N31;
        public double ARAB, A12, C21, C22, C12;
        public int N11;
        public double Z, QT, VZ, XW, IT, C2, S0, SNH, VTJ, VTJ1, IS, LT0, LTX, AMS1, AMS2, AMS3;
        public int N21, NPT2;
    }

    struct CFPGInternal
    {
        public double Z, APW, QT, DPSIZ, DENG, AMG, AMGOS, DMG, DMGOS, AM;
        public double FPM, FPGM, HX, HPSI, WG, SQ, VQ, AA0, AA, TGOS;
        public int NPT;
        public double NIND, DIND, TIND, DZ1, DZ, DPSI, DM, DQT1;
        public double DQT, DW, DWG, DA1, DA, DTGR, TGR, AMG0;
    }

    struct CLOADPARAMS
    {
        public string NameWell;      //имя скважины
        public string NameMestor;  //имя месторождения
        public double Zaboy;                //забой
        public double Dvn;                      //внутренний диаметр колонны
        public double GlubVoda;             //уровень жидкости в стволе
        public double DensVoda;             //плотность жидкости
        public double HPerf;                //мощность интервала перфор
        public double PodIntPerf;     //подошва интервала перфорации
        public int DensPerf;                   //плотность перфорации
        public int CountOsnZarad;      //количество основынх зарядов
        public int CountVospZarad;     //количество восплам зарядов
        public double GlubGen;              //глубина установки генератора
        public double Pplast;                   //пластовое давление
        public double Tplast;                   //пластовая температура
        public double ModUnga;              //модуль Юнга
        public double KPuass;                   //коэфф Пуассона
        public double DlitProc;             //длительность процесса
        public double TPvdolWell;           //время вывода вдоль скважины давления
        public double dHFromGenToMan;   //расстояние от генератора до регистрирующего манометра
        public string NameOsnZarad;    //имя основного заряда
        public string NameVospZarad;	//имя восплам активного заряда
    }

    class CBase
    {
        public double Hidrost;
        public double gorn;
        public int PlusK;

        private CLOADPARAMS Params; //Основные параметры на расчет по скважине и пластам
        private CZarad ZaradOsn; //основные заряды
        private CZarad ZaradVosp;  //активные(восплам) заряды

        int CountPoint;    //сколько точек в расчете будет

        CGLOBAL Glob;
        CBASEINTERNAL InBase;
        CRASPADINTERNAL InRaspad;
        CFPGInternal InFPG;
        CGGOSINTERNAL InGos;

        CFRACINTERNAL InFrac;
        CSMESHINTERNAL InSmesh;

        public CBase() { PlusK = 0; }

        int InitBaseConst()      //инициализировать базовые параметры перед расчетом
        {
            Glob.ALFA = 0.0f;
            Glob.GG = 1.17f;
            Glob.PSG = 1.0f;
            Glob.DNG = ZaradOsn.Poroh.Dens;//   1610.0;    //осн
            Glob.FP = ZaradVosp.Poroh.Power;//  842800.0;
            Glob.FPGOS = ZaradOsn.Poroh.Power;//  842800.0;
                                              //Glob.A=0.083;
            Glob.A = 0.068f;

            Glob.DENS = ZaradVosp.Poroh.Dens;  //воспл
            Glob.T0 = ZaradVosp.Poroh.Temper; // 2500;
            Glob.DN = 0.01f;
            Glob.ANP = 24.0f;
            Glob.HM = 2.0f;
            Glob.E0 = 49050000000.0f;
            Glob.HH0 = 0.2f;
            Glob.HH = 0.000001f;
            Glob.E1 = 30.0f;
            Glob.ALL = 0.484f;
            //Glob.ALL=10.184;
            Glob.DS = 0.032f;
            Glob.DG = 0.092f;
            this.Glob.R0 = 1000.0f;
            Glob.BB = 0.0783f;
            Glob.PGS = 18000000.0f;
            Glob.HS = 1800.0f;
            Glob.B = 304500000.0f;
            Glob.AKK = 7.15f;
            //Glob.AKK=5.87;
            Glob.H1 = 20.0f;
            Glob.HX1 = 1.0f;
            Glob.TA = 0.02f;
            Glob.TAY = 0.0005f;
            Glob.STEP = 0.05f;
            Glob.TK = 2.0f;
            Glob.K1 = 10;
            Glob.H0 = 2.0f;
            //	Glob.DT=0.126;

            Glob.NP = this.CountPoint;
            //геом осн

            Glob.DSG = this.ZaradOsn.Dvnutr / 10;  //1.5;
            Glob.DGG = this.ZaradOsn.Dnar / 10;    //4.2;

            Glob.DG = this.ZaradVosp.Dnar / 1000.0f;
            Glob.DS = this.ZaradVosp.Dvnutr / 1000.0f;
            return 0;
        }
        int ClearDataStruct()      //очистить структуры
        {
            Glob = new CGLOBAL();
            InBase = new CBASEINTERNAL();
            InRaspad = new CRASPADINTERNAL();
            InFPG = new CFPGInternal();
            InGos = new CGGOSINTERNAL();
            InFrac = new CFRACINTERNAL();
            InSmesh = new CSMESHINTERNAL();
            return 0;
            //memset(&Glob, 0, sizeof(CGlobal));
            //memset(&InBase, 0, sizeof(CBaseInternal));
            //memset(&InRaspad, 0, sizeof(RaspadInternal));
            //memset(&InFPG, 0, sizeof(FPGInternal));
            //memset(&InGos, 0, sizeof(GGosInternal));
            //memset(&InFrac, 0, sizeof(CFracInternal));
            //memset(&InSmesh, 0, sizeof(CSmeshInternal));
            //return 0;
        }
        int ReloadBaseParams()     //перезагрузить базовые параметры из структуры
        {
            LoadBaseParams(ref this.Params);
            return 0;
        }
        int CalcBase()
        {
            //Glob.NP=600;
            //Glob.NP=1000;
            Glob.DN = 5.0f;

            this.Glob.R0 = 1000.0f * Glob.R0;
            Glob.DT = Glob.DT / 1000.0f;
            Glob.HS = Glob.HSP - Glob.HS;
            Glob.PUAZ = 1.5f;
            Glob.PPL = Glob.PPL * 1000000.0f;

            InBase.ANGR1 = 2.0f;
            InBase.ANGR2 = 2.0f;
            Glob.DTP = 0.0f;
            Glob.VT = 0.0f;
            Glob.LT = 0.0f;
            Glob.HT = 0.0f;

            Glob.DN = Glob.DN / 1000.0f;
            Glob.E0 = Glob.E0 * 1000000.0f;
            Glob.PUAZ = 0.001f * Glob.PUAZ;
            InBase.ANP0 = 1.0f / Glob.DN;

            Glob.BB = 0.04f;
            Glob.DX = 6.0f;

            if (Glob.TK <= 4.0) Glob.DX = 2.0f;//   сравнивают время процесса воздейсвия и выбирают шаг
            if (Glob.TK > 4.0 && Glob.TK <= 8.0) Glob.DX = 3.0f;
            if (Glob.TK > 8.0 && Glob.TK <= 10.0) Glob.DX = 4.0f;
            if (Glob.TK > 10.0 && Glob.TK <= 12.0) Glob.DX = 6.0f;

            Glob.K1 = Glob.H1 / Glob.DX;
            if (Glob.K1 < Glob.DX) Glob.K1 = Glob.DX;
            Glob.K1 += PlusK;  //оптимизатор, чтобы не вылетал расчет
                               //if (Glob.K1<4) Glob.K1=4;

            Glob.N0 = 2;
            Glob.PA = 98100.0f;
            Glob.P0 = Glob.B;
            Glob.C0 = (double)(Math.Sqrt(Glob.AKK * (Glob.P0 + Glob.PA) / Glob.R0));
            //Glob.TAY=0.8* Glob.DX/ Glob.C0;
            Glob.TAY = 0.8f * Glob.DX / Glob.C0;
            Glob.STEP = 0.1f;

            //Glob.DSG=1.5;
            //Glob.DGG=4.2;
            Glob.NGR = 2;
            Glob.DDG = Glob.DGG * Glob.DGG - Glob.DSG * Glob.DSG;
            Glob.E1G = (Glob.DGG - Glob.DSG) / (2.0f * Glob.NGR);
            Glob.Z1 = 0.0f;
            Glob.Z2 = 0.0f;
            Glob.F1 = 0.0f;
            Glob.F2 = 0.0f;
            Glob.HV1 = 0.0f;
            Glob.HV2 = 0.0f;
            Glob.HGOS = 100.0f * Glob.HGOS;                       //перевели длина основных зарядов из метров в сантиметры
            Glob.DNGOS = Glob.DNG / 1000.0f;                      //плотность заряда перевели из килограмм на куб метр в граммах на кубич см
            Glob.AMGOS = Glob.HGOS * (3.14f * Glob.DDG / 4.0f) * Glob.DNGOS;       //масса заряда в граммах
            InBase.AGOS = 0.072f + 0.011f * (InBase.TPL - 30.0f) / 30.0f;      //пока тяжело  сказать, но что то связано с пересчетом по пластоовой температуре. Какой то температурный параметр снаряда
            Glob.AEGOS = InBase.AGOS / (10.0f * Glob.E1G);                 //10* Glob.E1G- похоже на   перевод из сантиметров в миллиметры, расчет какой то доли возмжно температуры на один миллиметр заряда

            //Glob.DG=0.042;
            //Glob.DS=0.015;
            Glob.A = 0.072f + 0.011f * (InBase.TPL - 30.0f) / 30.0f;
            InBase.NGR1 = InBase.ANGR1;
            Glob.E1 = 1000.0f * (Glob.DG - Glob.DS) / 8.0f;
            Glob.ALL = 0.0f;



            Glob.TE0 = InBase.TPL + 273.0f;
            Glob.GG1 = Glob.GG - 1.0f;
            Glob.AE1 = Glob.A / Glob.E1;
            Glob.D02 = Glob.DG * Glob.DG;
            Glob.HPI = 3.14f * Glob.H0 / 4.0f;
            Glob.AM0 = Glob.HPI * (Glob.DG * Glob.DG - Glob.DS * Glob.DS);
            Glob.AM0 = Glob.AM0 * Glob.DENS;
            Glob.SD = 3.14f * Glob.DT * Glob.DT / 4.0f;
            Glob.VPSI = Glob.HPI * Glob.DS * Glob.DS;
            InBase.SDN = 0.786f * Glob.DN * Glob.DN;
            Glob.G0 = 9.81f;
            Glob.C02 = Glob.C0 * Glob.C0;
            Glob.AK7 = 1.0f / Glob.AKK;
            Glob.AK = Glob.AKK - 1.0f;
            Glob.AK1 = 0.5f * Glob.AK;
            Glob.AKK1 = 0.5f * (Glob.AKK + 1);
            Glob.AK4 = 2.0f * Glob.AKK / (Glob.AKK - 1.0f);
            Glob.AK5 = 2.0f / (Glob.AKK - 1.0f);
            Glob.AK6 = 1.0f / Glob.AK4;
            Glob.P0 = Glob.B;

            //начальная сетка
            Glob.X[1] = 0.0f;
            Glob.N1 = Convert.ToInt32(Glob.K1 + 1);
            Glob.HZAR = Glob.AM0 / (Glob.DENS * Glob.SD) + (Glob.AMGOS / 1000.0f) / (Glob.DNG * Glob.SD);
            Glob.N2 = Convert.ToInt32(Glob.N1 + 1 + (Glob.HZAR + Glob.VPSI / Glob.SD) / Glob.DX);
            InBase.I = 1;
            while (true)      //забили массив  Glob.X вдоль скважины значениями по глубине
            {
                InBase.I++;
                Glob.X[InBase.I] = Glob.X[InBase.I - 1] + Glob.DX;
                if (Math.Abs(Glob.X[InBase.I] - Glob.HS) <= Glob.DX / 2.0) Glob.NT3 = InBase.I;
                if (Glob.X[InBase.I] >= Glob.HSP) break;
            };
            Glob.N3 = InBase.I;

            Glob.NXG = Convert.ToInt32(Glob.N2 + InBase.XG / Glob.DX);
            Glob.HSP = Glob.X[Glob.N3];
            Glob.HS = Glob.X[Glob.NT3];

            InBase.PH = 9.81f * Glob.R0 * (Glob.HS - Glob.H1) + Glob.PA;
            this.Hidrost = InBase.PH;
            double A = 5.87f * (double)Math.Pow(this.Hidrost, 0.415);


            Glob.PGS = InBase.PH;
            InBase.PVG = 9.81f * 2.5f * Glob.R0 * Glob.HSP;
            //InBase.PVG=9.81*2.5* (Glob.HSP);
            this.gorn = 9.81f * 2.5f * (Glob.HSP - 10.0f) * 1000.0f;
            Glob.PGG = InBase.PVG * Glob.HH0 / (1.0f - Glob.HH0);

            Glob.PKT = 2.0f * InBase.PH;
            Glob.NT = Glob.N3;

            for (InBase.I = 1; InBase.I <= Glob.N3; InBase.I++)
            {
                InBase.PH0 = 9.81f * Glob.R0 * Glob.HS;
                Glob.PS[InBase.I] = InBase.PH0 - Glob.R0 * 9.81f * Glob.X[InBase.I];
                if (InBase.I >= Glob.NT3) Glob.PS[InBase.I] = Glob.PA;
                Glob.RS[InBase.I] = Glob.R0 * (double)Math.Pow((Glob.PS[InBase.I] + Glob.P0) / Glob.P0, Glob.AK7);
                Glob.US[InBase.I] = 0.0f;
                Glob.ES[InBase.I] = (Glob.PS[InBase.I] - Glob.C02 * (Glob.RS[InBase.I] - Glob.R0)) / (Glob.AK * Glob.RS[InBase.I]);
                Glob.PG[InBase.I] = Glob.PS[InBase.I];
                Glob.UG[InBase.I] = 0.0f;
                Glob.RG[InBase.I] = Glob.RS[InBase.I];
                Glob.EG[InBase.I] = Glob.ES[InBase.I];
                Glob.WS[InBase.I] = 0.0f;
                Glob.AMS[InBase.I] = Glob.RS[InBase.I] * (Glob.X[InBase.I + 1] - Glob.X[InBase.I]);
                Glob.PSX[InBase.I] = Glob.PS[InBase.I];
                Glob.USX[InBase.I] = 0.0f;
            }

            Glob.PS[1] = Glob.PS[2];
            Glob.PG[1] = Glob.PS[2];


            InBase.PSI0 = Glob.PGS * Glob.VPSI / (Glob.AM0 * (Glob.FP - Glob.PGS / Glob.DENS));

            if (Math.Abs(Glob.ALL) < 0.001)
            {
                InBase.Z0 = InBase.PSI0;
            }
            else
            {
                InBase.Z0 = (Glob.ALL - 1.0f + (double)Math.Sqrt(4.0f * Glob.ALL * InBase.PSI0 + (double)Math.Pow(1.0f - Glob.ALL, 2f))) / (2.0f * Glob.ALL);
            }

            Glob.Y[1] = InBase.Z0;
            Glob.Y[2] = 0.0f;
            Glob.Y[3] = 0.0f;
            InBase.VT0 = InBase.SDN * Glob.ANP * Glob.HM * 0.1f;



            Glob.Y[4] = InBase.VT0;
            Glob.X1 = Glob.X[Glob.N1];
            Glob.X2 = Glob.X1 + Glob.HZAR + Glob.VPSI / Glob.SD;
            Glob.X3 = Glob.X[Glob.NT3];
            Glob.NT1 = Glob.N1 - 1;

            Glob.NT2 = Glob.N2;
            Glob.NT3 = Glob.NT3 - 1;

            //RASPAD();
            //	RASPAD();
            CalcRaspad();
            return 0;

        }
        int CalcRaspad()
        {
            Glob.IP = 0;
            Glob.PGZ = Glob.PGS;
            Glob.N0 = 2;
            InRaspad.IT = 1;

            Glob.DTG = Glob.TK / Glob.NP;
            if (Glob.DTG < Glob.TAY) Glob.TAY = Glob.DTG * 0.9f;

            Glob.TG = Glob.DTG;
            Glob.NPT1 = 1;
            InRaspad.NPT2 = 1;

            Glob.PT = Glob.PPL;

            InRaspad.C2 = 2.318407f;

            InRaspad.S0 = 3.14f * Glob.DN * Glob.DN / 4.0f;
            Glob.VT = 2.0f * Glob.DN * Glob.HM * 0.01f;

            Glob.VT1 = Glob.VT;
            //*******************************

            InRaspad.SNH = Glob.ANP * Glob.HM * InRaspad.S0;
            //******************************
            InRaspad.VTJ = 0.0f;
            InRaspad.VTJ1 = 0.0f;
            InRaspad.IS = 0;
            InRaspad.LT0 = 0.1f;
            Glob.LT = InRaspad.LT0;
            InRaspad.LTX = 0.95f * InRaspad.LT0;
            Glob.DVTP = 0.0f;
            Glob.PST = Glob.PGZ;
            InRaspad.AMS1 = Glob.AMS[Glob.NT1];
            InRaspad.AMS2 = Glob.AMS[Glob.NT2 - 1] * (Glob.X[Glob.NT2] - Glob.X2) / (Glob.X[Glob.NT2] - Glob.X[Glob.NT2 - 1]);
            InRaspad.AMS3 = Glob.AMS[Glob.NT3];

            InRaspad.TT = 0.0f;
            for (InRaspad.I = 1; InRaspad.I < 5; InRaspad.I++)
            {
                InRaspad.F[InRaspad.I] = 0.0f;
            }

            //		RASPAD_GOTO10();
            while (true)
            {

                InRaspad.TT = InRaspad.TT + Glob.TAY;
                // Counter++;

                CalcFPG(ref InRaspad.TT, Glob.Y, InRaspad.F);     // вызов процедуры FPG

                InRaspad.Z = Glob.Y[1];
                InRaspad.QT = Glob.Y[3];

                InRaspad.VZ = Glob.V1;

                CalcFRAC(ref InRaspad.TT, ref Glob.TAY, ref InRaspad.LT0, ref Glob.LT, ref Glob.DTP, ref Glob.VT, ref InRaspad.VTW,
                        ref Glob.PT, ref Glob.E0, ref Glob.HH0, ref Glob.R0, ref Glob.HM, ref InRaspad.SNH, ref InRaspad.DV); // вызов FRAC

                InRaspad.XW = 0.05f;

                if (Glob.NT1 < 2) throw (999);
                InRaspad.N11 = Glob.NT1 - 1;
                InRaspad.N21 = Glob.NT2 + 1;

                Glob.PG[Glob.NT1] = Glob.PGZ;
                Glob.PS[Glob.NT1] = Glob.PGZ;

                InRaspad.ARAB = (double)Math.Sqrt(Glob.RS[InRaspad.N11]);
                InRaspad.A12 = InRaspad.ARAB * (double)Math.Sqrt((Glob.PGZ + Glob.P0) * Glob.AKK1 + (Glob.PS[InRaspad.N11] + Glob.P0) * Glob.AK1);

                //Form2->Caption=Form2->Caption+IntToStr(InRaspad.N11);

                if (Glob.PGZ < Glob.PS[InRaspad.N11])
                {
                    InRaspad.CS = (double)Math.Sqrt(Glob.AKK * (Glob.PS[InRaspad.N11] + Glob.P0) / Glob.RS[InRaspad.N11]);
                    Glob.UG[Glob.NT1] = Glob.US[InRaspad.N11] + Glob.AK5 * InRaspad.CS * (1.0f - (double)Math.Pow(((Glob.PGZ + Glob.P0) / (Glob.PS[InRaspad.N11] + Glob.P0)), Glob.AK6));
                    InRaspad.C11 = InRaspad.CS + Glob.AK1 * Glob.US[InRaspad.N11] - Glob.AK1 * Glob.UG[Glob.NT1];
                    InRaspad.C12 = InRaspad.C11 * InRaspad.C11;
                    Glob.RG[Glob.NT1] = Glob.AKK * (Glob.PG[Glob.NT1] + Glob.P0) / InRaspad.C12;
                }
                else
                {
                    Glob.UG[Glob.NT1] = Glob.US[InRaspad.N11] + Glob.PS[InRaspad.N11] / InRaspad.A12 - Glob.PGZ / InRaspad.A12;
                    Glob.RG[Glob.NT1] = Glob.RS[InRaspad.N11] * (InRaspad.A12 / (InRaspad.A12 + Glob.RS[InRaspad.N11] * Glob.UG[Glob.NT1] - Glob.RS[InRaspad.N11] * Glob.US[InRaspad.N11]));
                }

                Glob.EG[Glob.NT1] = (Glob.PG[Glob.NT1] - Glob.C02 * (Glob.RG[Glob.NT1] - Glob.R0)) / (Glob.AK * Glob.RG[Glob.NT1]);
                Glob.PG[Glob.NT2] = Glob.PGZ;
                Glob.PS[Glob.NT2 - 1] = Glob.PGZ;
                InRaspad.ARAB = Math.Sqrt(Glob.RS[Glob.NT2]);
                InRaspad.A12 = InRaspad.ARAB * Math.Sqrt((Glob.PGZ + Glob.P0) * Glob.AKK1 + (Glob.PS[Glob.NT2] + Glob.P0) * Glob.AK1);

                if (Glob.PGZ < Glob.PS[Glob.NT2])
                {
                    InRaspad.CS = Math.Sqrt(Glob.AKK * (Glob.PS[Glob.NT2] + Glob.P0) / Glob.RS[Glob.NT2]);
                    Glob.UG[Glob.NT2] = Glob.US[Glob.NT2] - Glob.AK5 * InRaspad.CS * (1.0 - Math.Pow(((Glob.PGZ + Glob.P0) / (Glob.PS[Glob.NT2] + Glob.P0)), Glob.AK6));
                    InRaspad.C21 = InRaspad.CS + Glob.AK1 * Glob.UG[Glob.NT2] - Glob.AK1 * Glob.US[Glob.NT2];
                    InRaspad.C22 = InRaspad.C21 * InRaspad.C21;
                    Glob.RG[Glob.NT2] = Glob.AKK * (Glob.PG[Glob.NT2] + Glob.P0) / InRaspad.C22;
                }
                else
                {
                    Glob.UG[Glob.NT2] = Glob.US[Glob.NT2] + Glob.PGZ / InRaspad.A12 - Glob.PS[Glob.NT2] / InRaspad.A12;
                    Glob.RG[Glob.NT2] = Glob.RS[Glob.NT2] * (InRaspad.A12 / (InRaspad.A12 + Glob.RS[Glob.NT2] * Glob.US[Glob.NT2] - Glob.RS[Glob.NT2] * Glob.UG[Glob.NT2]));
                };

                Glob.EG[Glob.NT2] = (Glob.PG[Glob.NT2] - Glob.C02 * (Glob.RG[Glob.NT2] - Glob.R0)) / (Glob.AK * Glob.RG[Glob.NT2]);

                InRaspad.N31 = Glob.NT3 - 1;
                Glob.PG[Glob.NT3] = Glob.PA;

                InRaspad.ARAB = Math.Sqrt(Glob.RS[InRaspad.N31]);
                InRaspad.A12 = InRaspad.ARAB * Math.Sqrt((Glob.PA + Glob.P0) * Glob.AKK1 + (Glob.PS[InRaspad.N31] + Glob.P0) * Glob.AK1);

                if (Glob.PA < Glob.PS[InRaspad.N31])
                {
                    InRaspad.CS = Math.Sqrt(Glob.AKK * (Glob.PS[InRaspad.N31] + Glob.P0) / Glob.RS[InRaspad.N31]);
                    Glob.UG[Glob.NT3] = Glob.US[InRaspad.N31] + Glob.AK5 * InRaspad.CS * (1.0 - Math.Pow(((Glob.PA + Glob.P0) / (Glob.PS[InRaspad.N31] + Glob.P0)), Glob.AK6));

                    InRaspad.C31 = InRaspad.CS + Glob.AK1 * Glob.US[InRaspad.N31] - Glob.AK1 * Glob.UG[Glob.NT3];
                    InRaspad.C32 = InRaspad.C31 * InRaspad.C31;
                    Glob.RG[Glob.NT3] = Glob.AKK * (Glob.PG[Glob.NT3] + Glob.P0) / InRaspad.C32;
                }
                else
                {
                    Glob.UG[Glob.NT3] = Glob.US[InRaspad.N31] + Glob.PS[InRaspad.N31] / InRaspad.A12 - Glob.PA / InRaspad.A12;
                    Glob.RG[Glob.NT3] = Glob.RS[InRaspad.N31] * (InRaspad.A12 / (InRaspad.A12 + Glob.RS[InRaspad.N31] * Glob.UG[Glob.NT3] - Glob.RS[InRaspad.N31] * Glob.US[InRaspad.N31]));
                }

                Glob.EG[Glob.NT3] = (Glob.PG[Glob.NT3] - Glob.C02 * (Glob.RG[Glob.NT3] - Glob.R0)) / (Glob.AK * Glob.RG[Glob.NT3]);
                InRaspad.V3 = Glob.UG[Glob.NT3];
                //**************************************************

                //**************************************************
                for (InRaspad.I = 2; InRaspad.I <= Glob.NT3 - 1; InRaspad.I++)
                {
                    if ((InRaspad.I >= Glob.NT1) && (InRaspad.I <= Glob.NT2)) continue;

                    InRaspad.PS1 = Glob.PS[InRaspad.I - 1];
                    InRaspad.PS2 = Glob.PS[InRaspad.I];
                    InRaspad.US1 = Glob.US[InRaspad.I - 1];
                    InRaspad.US2 = Glob.US[InRaspad.I];
                    InRaspad.RS1 = Glob.RS[InRaspad.I - 1];
                    InRaspad.RS2 = Glob.RS[InRaspad.I];
                    InRaspad.ES1 = Glob.ES[InRaspad.I - 1];
                    InRaspad.ES2 = Glob.ES[InRaspad.I];
                    InRaspad.CS1 = Math.Sqrt(Glob.AKK * (InRaspad.PS1 + Glob.P0) / InRaspad.RS1);
                    InRaspad.CS2 = Math.Sqrt(Glob.AKK * (InRaspad.PS2 + Glob.P0) / InRaspad.RS2);
                    InRaspad.RC1 = InRaspad.RS1 * InRaspad.CS1;
                    InRaspad.RC2 = InRaspad.RS2 * InRaspad.CS2;
                    InRaspad.RC = InRaspad.RC1 + InRaspad.RC2;
                    InRaspad.RP1 = InRaspad.RC1 / InRaspad.RC;
                    InRaspad.RP2 = InRaspad.RC2 / InRaspad.RC;
                    InRaspad.AA1 = InRaspad.RC1;
                    InRaspad.AA2 = InRaspad.RC2;
                    InRaspad.AA12 = InRaspad.AA1 + InRaspad.AA2;
                    InRaspad.A122 = InRaspad.AA1 * (InRaspad.AA2 / InRaspad.AA12);
                    InRaspad.PK = InRaspad.PS1 * (InRaspad.AA2 / InRaspad.AA12) + InRaspad.PS2 * (InRaspad.AA1 / InRaspad.AA12) + InRaspad.A122 * InRaspad.US1 - InRaspad.A122 * InRaspad.US2;
                    InRaspad.UK = (InRaspad.AA1 * InRaspad.US1 + InRaspad.AA2 * InRaspad.US2 + InRaspad.PS1 - InRaspad.PS2) / InRaspad.AA12;
                    Glob.UG[InRaspad.I] = InRaspad.UK;
                    Glob.PG[InRaspad.I] = InRaspad.PK;

                    if (InRaspad.PK > InRaspad.PS1)
                    {
                        InRaspad.R1 = InRaspad.RS1 * (InRaspad.AA1 / (InRaspad.AA1 - InRaspad.RS1 * (InRaspad.US1 - InRaspad.UK)));
                        Glob.RG[InRaspad.I] = InRaspad.R1;
                    }
                    else
                    {
                        InRaspad.C11 = InRaspad.CS1 + Glob.AK1 * (InRaspad.US1 - InRaspad.UK);
                        InRaspad.R1 = Glob.AKK * (InRaspad.PK + Glob.P0) / (InRaspad.C11 * InRaspad.C11);
                        Glob.RG[InRaspad.I] = InRaspad.R1;
                    }
                    Glob.EG[InRaspad.I] = (Glob.PG[InRaspad.I] - Glob.C02 * (Glob.RG[InRaspad.I] - Glob.R0)) / (Glob.AK * Glob.RG[InRaspad.I]);
                };

                for (InRaspad.I = Glob.N0; InRaspad.I <= Glob.NT3; InRaspad.I++)
                {
                    if ((InRaspad.I > Glob.NT1) && (InRaspad.I < Glob.NT2)) continue;
                    //else{
                    InRaspad.BG1[InRaspad.I] = Glob.RG[InRaspad.I] * Glob.UG[InRaspad.I];
                    InRaspad.BG2[InRaspad.I] = Glob.PG[InRaspad.I] + Glob.UG[InRaspad.I] * InRaspad.BG1[InRaspad.I];
                    InRaspad.BG3[InRaspad.I] = InRaspad.BG1[InRaspad.I] * (Glob.EG[InRaspad.I] + Glob.UG[InRaspad.I] * Glob.UG[InRaspad.I] / 2.0) + Glob.PG[InRaspad.I] * Glob.UG[InRaspad.I];
                    //};
                };

                for (InRaspad.I = Glob.N0; InRaspad.I <= Glob.NT3 - 1; InRaspad.I++)
                {
                    if ((InRaspad.I >= Glob.NT1) && (InRaspad.I < Glob.NT2)) //goto 69;//RASPAD_GOTO69();
                    {
                        Glob.PS[InRaspad.I] = Glob.PGZ;
                        Glob.US[InRaspad.I] = Glob.US[Glob.NT1] + (Glob.US[Glob.NT2] - Glob.US[Glob.NT1]) * (InRaspad.I - Glob.NT1) / (Glob.NT2 - Glob.NT1);
                    }
                    else
                    {
                        InRaspad.HX = Glob.X[InRaspad.I + 1] - Glob.X[InRaspad.I];
                        InRaspad.THX = Glob.TAY / InRaspad.HX;
                        InRaspad.A1 = Glob.RS[InRaspad.I];
                        InRaspad.A2 = InRaspad.A1 * Glob.US[InRaspad.I];
                        InRaspad.A3 = InRaspad.A1 * Glob.ES[InRaspad.I] + InRaspad.A2 * Glob.US[InRaspad.I] / 2.0;
                        InRaspad.FS1 = 0.0;
                        InRaspad.FS2 = -InRaspad.A1 * Glob.G0 - Glob.BB * Math.Abs(InRaspad.A2) * Glob.US[InRaspad.I];
                        InRaspad.FS3 = -InRaspad.A2 * Glob.G0 - Glob.BB * InRaspad.A1 * Glob.US[InRaspad.I] * Glob.US[InRaspad.I] * Math.Abs(Glob.US[InRaspad.I]);
                        InRaspad.AS1 = InRaspad.A1 + InRaspad.BG1[InRaspad.I] * InRaspad.THX - InRaspad.BG1[InRaspad.I + 1] * InRaspad.THX + InRaspad.FS1 * Glob.TAY;
                        Glob.AMS[InRaspad.I] = Glob.AMS[InRaspad.I] + Glob.TAY * InRaspad.BG1[InRaspad.I] - Glob.TAY * InRaspad.BG1[InRaspad.I + 1];
                        if (InRaspad.AS1 < Glob.R0) InRaspad.AS1 = Glob.R0;
                        InRaspad.AS2 = InRaspad.A2 + InRaspad.BG2[InRaspad.I] * InRaspad.THX - InRaspad.BG2[InRaspad.I + 1] * InRaspad.THX + InRaspad.FS2 * Glob.TAY;
                        InRaspad.AS3 = InRaspad.A3 + InRaspad.BG3[InRaspad.I] * InRaspad.THX - InRaspad.BG3[InRaspad.I + 1] * InRaspad.THX + InRaspad.FS3 * Glob.TAY;
                        Glob.RS[InRaspad.I] = InRaspad.AS1;
                        Glob.US[InRaspad.I] = InRaspad.AS2 / InRaspad.AS1;
                        Glob.ES[InRaspad.I] = InRaspad.AS3 / InRaspad.AS1 - Glob.US[InRaspad.I] * Glob.US[InRaspad.I] / 2.0;
                        Glob.PS[InRaspad.I] = Glob.ES[InRaspad.I] * InRaspad.AS1 * Glob.AK + Glob.C02 * (InRaspad.AS1 - Glob.R0);
                    }

                };

                Glob.RS[1] = Glob.RS[2];
                Glob.PS[1] = Glob.PS[2];
                Glob.US[1] = -Glob.US[2];
                Glob.US[Glob.NT3] = Glob.UG[Glob.NT3];
                Glob.PS[Glob.NT3] = Glob.PA;
                Glob.RS[Glob.NT3] = Glob.R0;
                InRaspad.X1P = Glob.X1;
                InRaspad.X2P = Glob.X2;

                CalcSMESH(ref Glob.R0, ref Glob.AK, ref Glob.C02, ref Glob.X1, ref Glob.X2, ref Glob.X3, ref InRaspad.AMS1, ref InRaspad.AMS2, ref InRaspad.AMS3, ref Glob.TAY, ref Glob.PGZ, ref Glob.PA, ref InRaspad.BG1, ref InRaspad.DV, ref Glob.SD);  // вызов SMESH

                Glob.V1 = (Glob.X1 - InRaspad.X1P) / Glob.TAY;
                Glob.V2 = (Glob.X2 - InRaspad.X2P) / Glob.TAY;
                Glob.PSS = Glob.PS[Glob.NXG];
                Glob.PST = Glob.PGZ;

                //	 double gh=fabs(InRaspad.TT-Glob.TPX);
                if (Math.Abs(InRaspad.TT - Glob.TPX) <= Glob.TAY)
                {

                    for (InRaspad.I = 1; InRaspad.I <= Glob.N3; InRaspad.I++)
                    {
                        Glob.PSX[InRaspad.I] = Glob.PS[InRaspad.I];
                        Glob.USX[InRaspad.I] = Glob.US[InRaspad.I];
                        Glob.XS[InRaspad.I] = Glob.X[InRaspad.I];
                    };

                };

                if (InRaspad.TT > Glob.TK) break;  //условие выхода из цикла
            }
            return 0;
        }

        int CalcFPG(ref double T, double[] Y, double[] F)
        {
            double AMS;

            InFPG.Z = Y[1];
            InFPG.APW = Y[2];
            InFPG.QT = Y[3];

            Glob.HT = Glob.VT / Glob.SD;
            Glob.PSI = InFPG.Z * (1.0f + Glob.ALL * (InFPG.Z - 1.0f));
            InFPG.DPSIZ = 1.0f - Glob.ALL + 2.0f * Glob.ALL * InFPG.Z;
            if (T <= Glob.TAY) InFPG.DENG = Glob.R0;

            CalcGOS(ref T, ref Glob.PGZ, ref Glob.TAY, ref InFPG.DENG, ref Glob.R0, ref InFPG.AMGOS, ref InFPG.DMGOS,
            ref InFPG.AMG0,ref  Glob.DNGOS, ref  Glob.AMGOS);

            InFPG.AMG = InFPG.AMGOS / 1000.0;
            InFPG.DMG = InFPG.DMGOS / 1000.0;
            InFPG.AM = Glob.PSI * Glob.AM0;
            AMS = InFPG.AM + InFPG.AMG;
            InFPG.FPM = Glob.FP * InFPG.AM;
            InFPG.FPGM = Glob.FPGOS * InFPG.AMG;
            InFPG.HX = Glob.X2 - Glob.X1;
            InFPG.HPSI = Glob.AM0 * (1.0 - Glob.PSI) / (Glob.DENS * Glob.SD) + ((InFPG.AMG0 - InFPG.AMGOS) / 1000.0) / (Glob.DNG * Glob.SD);
            InFPG.WG = Glob.SD * (Glob.X2 - Glob.X1 - InFPG.HPSI);
            InFPG.SQ = 3.14 * Glob.DT * (Glob.X2 - Glob.X1 - InFPG.HPSI);

            if (T > Glob.TAY)
            {
                //InFPG.DENG=( InFPG.AM+ InFPG.AMG)/ InFPG.WG;
                InFPG.DENG = AMS / InFPG.WG;
                InFPG.VQ = Math.Abs(Glob.V2);
                if (InFPG.VQ < Math.Abs(Glob.V1)) InFPG.VQ = Math.Abs(Glob.V1);
                InFPG.AA0 = 0.0;
                InFPG.AA = InFPG.AA0 + 7.36 * Math.Pow(InFPG.DENG * InFPG.VQ, 0.8) / Math.Pow(Glob.DT, 0.2);
                Glob.PGZ = (InFPG.FPM + InFPG.FPGM - Glob.GG1 * (InFPG.APW + InFPG.QT)) / InFPG.WG;
                InFPG.TGOS = Glob.T0;                      //  InFPG.TGOS заменим на  Glob.T0. Поко не понятно, что это, но по аналогии с мкав4000 заменим
                Glob.TE = Glob.PGZ * InFPG.WG / (InFPG.FPM / Glob.T0 + InFPG.FPGM / InFPG.TGOS);
                if (Glob.TE < Glob.TE0) Glob.TE = Glob.TE0;
            }
            else
            {
                Glob.TE = Glob.T0;
                InFPG.TGOS = Glob.T0;
                Glob.PGZ = Glob.PGS;
                InFPG.NPT = 0;
                InFPG.NIND = 0;
                InFPG.DIND = Glob.TK / 24.0;
                InFPG.TIND = InFPG.DIND;
            }


            InFPG.DZ1 = F[1];
            InFPG.DZ = Glob.AE1 * Math.Pow(Glob.PGZ / 98100.0, Glob.PSG);
            if (InFPG.Z >= 1.0) InFPG.DZ = 0.0;
            InFPG.DPSI = InFPG.DPSIZ * InFPG.DZ;
            InFPG.DM = InFPG.DPSI * Glob.AM0;
            InFPG.DQT1 = F[3];

            InFPG.DQT = InFPG.AA * InFPG.SQ * (Glob.TE - Glob.TE0);

            InFPG.DW = InFPG.DM / Glob.DENS + InFPG.DMG / Glob.DNG;
            InFPG.DWG = Glob.SD * (Glob.V2 - Glob.V1) + InFPG.DW;
            InFPG.DA1 = F[2];
            InFPG.DA = Glob.PGZ * InFPG.DWG;

            F[1] = InFPG.DZ;
            if (InFPG.Z >= 1.0) F[1] = 0.0;
            F[2] = InFPG.DA;
            F[3] = InFPG.DQT;

            Y[1] = Y[1] + 0.5 * Glob.TAY * (InFPG.DZ1 + InFPG.DZ);
            if (Y[1] > 1.0) Y[1] = 1.0;
            Y[2] = Y[2] + 0.5 * Glob.TAY * (InFPG.DA1 + InFPG.DA);
            Y[3] = Y[3] + 0.5 * Glob.TAY * (InFPG.DQT1 + InFPG.DQT);

            //double  InFPG.DTGR, InFPG.TGR;

            InFPG.Z = Y[1];
            InFPG.APW = Y[2];
            InFPG.QT = Y[3];
            InFPG.DTGR = Glob.TK / Glob.NP;
            if (T <= Glob.TAY) InFPG.TGR = InFPG.DTGR;
            Glob.TY[1] = 0.0;
            Glob.PY[1] = Glob.PGS / 100000.0;
            Glob.X1Y[1] = 0.0;
            Glob.X2Y[1] = Glob.H1;
            Glob.X3Y[1] = Glob.H1 + Glob.HZAR + Glob.VPSI / Glob.SD;
            Glob.X4Y[1] = Glob.T0 - 273.0;

            if (T > Glob.TAY)
            {
                if (Math.Abs(T - InFPG.TGR) <= Glob.TAY)
                {
                    InFPG.TGR = InFPG.TGR + InFPG.DTGR;
                    InFPG.NPT = InFPG.NPT + 1;
                    if (InFPG.NPT <= Glob.NP)
                    {
                        Glob.TY[InFPG.NPT] = T;
                        // (InFPG.NPT=100)
                        Glob.PY[InFPG.NPT] = Glob.PSS / 100000.0;
                        Glob.X1Y[InFPG.NPT] = Glob.TY[InFPG.NPT];
                        Glob.X2Y[InFPG.NPT] = Glob.X1;
                        Glob.X3Y[InFPG.NPT] = Glob.X2;
                        Glob.X4Y[InFPG.NPT] = Glob.TE - 273.0;
                        Glob.X5Y[InFPG.NPT] = Glob.X3;
                    }
                }
            }

            //100
            if (Math.Abs(T - InFPG.TIND) < Glob.TAY)
            {
                InFPG.TIND = InFPG.TIND + InFPG.DIND;
                InFPG.NIND = InFPG.NIND + 1;
            }
            return 0;
        }
        int CalcGOS(ref double T, ref double PGZ,ref  double TAY, ref double DENG, ref double DEN, ref double AMGOS,
                    ref double DMGOS, ref double AMG, ref double DENS, ref double AMG0)
        {
            InGos.F1P = Glob.F1;
            if (T <= TAY) InGos.CSA = 1.0;
            Glob.F1 = Glob.AEGOS * (PGZ / 98100.0) / InGos.CSA;
            if (Glob.Z1 >= 1.0) Glob.F1 = 0.0;

            if (Glob.Z2 >= 1.0) Glob.F2 = 0.0;
            Glob.Z1 = Glob.Z1 + 0.5 * TAY * (InGos.F1P + Glob.F1);
            if (Glob.Z1 >= 1.0) Glob.Z1 = 1.0;
            Glob.Z2 = Glob.Z2 + 0.5 * TAY * (InGos.F2P + Glob.F2);
            if (Glob.Z2 >= 1.0) Glob.Z2 = 1.0;
            InGos.DGS = Glob.DGG + Glob.DSG;
            InGos.DSS = Glob.DGG - Glob.DSG;
            InGos.EZ1 = Glob.E1G * Glob.Z1;
            InGos.EZ2 = Glob.E1G * Glob.Z2;
            InGos.DZ1 = Glob.E1G * Glob.F1;
            InGos.DZ2 = Glob.E1G * Glob.F2;
            InGos.DE1 = Glob.DSG + 2.0 * InGos.EZ1;
            InGos.DE2 = Glob.DSG + 2.0 * InGos.EZ2;

            InGos.AA = 981.0 * (DEN / DENG - 1.0);
            InGos.U1 = InGos.U1 + InGos.AA * TAY;
            Glob.HV1 = Glob.HV1 + TAY * InGos.U1;
            if (Glob.HV1 > Glob.HGOS) Glob.HV1 = Glob.HGOS;



            if (Glob.HV1 >= Glob.HGOS) gos_goto50(ref T, ref PGZ, ref TAY, ref DENG, ref DEN, ref AMGOS, ref DMGOS, ref AMG, ref DENS, ref AMG0);
            else
            {
                InGos.F2P = Glob.F2;
                Glob.F2 = 0.0;
                if (Glob.Z1 >= 1.0) gos_goto49(ref T, ref PGZ, ref TAY, ref DENG, ref DEN, ref AMGOS, ref DMGOS, ref AMG, ref DENS, ref AMG0);
                else
                {
                    InGos.CSA = 1.0;
                    InGos.D = InGos.DGS * (InGos.DSS - 2.0 * InGos.EZ1);
                    InGos.D1 = -2.0 * InGos.DGS * InGos.DZ1;
                    InGos.AMT = AMG0 * Glob.HV1 * (1.0 - InGos.D / Glob.DDG) / Glob.HGOS;
                    InGos.DMT = AMG0 * InGos.U1 * (1.0 - InGos.D / Glob.DDG) / Glob.HGOS - AMG0 * Glob.HV1 * InGos.D1 / (Glob.DDG * Glob.HGOS);

                    if (Glob.NGR == 2) gos_goto53(ref T, ref PGZ, ref TAY, ref DENG, ref DEN, ref AMGOS, ref DMGOS, ref AMG, ref DENS, ref AMG0);
                    else
                    {
                        InGos.ADMT = Glob.HV1 * InGos.DZ1 * (Glob.DSG / 2.0 + 0.67 * InGos.EZ1) + InGos.U1 * InGos.EZ1 * (0.5 * Glob.DSG + 0.33 * InGos.EZ1);
                        InGos.AMT = 3.14 * DENS * Glob.HV1 * InGos.EZ1 * (0.5 * Glob.DSG + 0.33 * InGos.EZ1);
                        InGos.DMT = 3.14 * DENS * InGos.ADMT;
                        gos_goto53(ref T, ref PGZ, ref TAY, ref DENG, ref DEN, ref AMGOS, ref DMGOS, ref AMG, ref DENS, ref AMG0);
                    }
                }
            }
            return 0;
        }
        void CalcFRAC(ref double T,ref double TAY, ref double LT0, ref double LT, ref double WT, ref double VT, ref double VTW, ref double PT, ref double E0,
                     ref double HH0, ref double R0, ref double HM, ref double SNH, ref double DV)
        {
            //double Glob.FT[1001],Glob.FL[1001],Glob.FU[1001],Glob.FV[1001],Glob.FW[1001];
            //double InFrac.L0,LT0,LT,InFrac.LL;

            if (SNH <= 0)
            {                             //
                FRAC_GOTO20(ref T,ref TAY,ref LT,ref VT,ref WT);
            }
            else
            {
                if (Glob.IP == 2)
                {

                    FRAC_GOTO200(ref T,ref PT,ref SNH,ref R0,ref VT,ref TAY,ref DV,ref LT0,ref E0,ref HH0,ref LT,ref HM,ref WT,ref VTW);

                }
                else
                {
                    if (Glob.PGZ > (Glob.PGG + Glob.PPL))
                    {
                        Glob.IP = 2;

                        FRAC_GOTO200(ref T, ref PT, ref SNH, ref R0, ref VT, ref TAY, ref DV, ref LT0, ref E0, ref HH0, ref LT, ref HM, ref WT, ref VTW);

                    }
                    else
                    {
                        FRAC_GOTO20(ref T,ref TAY,ref LT,ref VT,ref WT);
                    };
                };
            };

        }


        void CalcSMESH(ref double R0, ref double AK, ref double C02, ref double X1, ref double X2, ref double X3, ref double AMS1, ref
                        double AMS2, ref double AMS3, ref double TAY, ref double PGZ, ref double PA, ref double[] BG1, ref
                        double DVTP, ref double SD)
        {
            Glob.DVJ = DVTP;
            InSmesh.PATM = PA;
            InSmesh.AM1 = AMS1;
            Glob.RS[Glob.NT1] = Glob.RG[Glob.NT1];

            InSmesh.AMTP = Glob.RS[Glob.NT1] * DVTP * TAY / SD;
            AMS1 = AMS1 + TAY * BG1[Glob.NT1] - InSmesh.AMTP;

            if (AMS1 > 0.0)
            {
                InSmesh.US1 = Glob.UG[Glob.NT1];
                InSmesh.XS1 = X1;

                X1 = Glob.X[Glob.NT1] + AMS1 / Glob.RS[Glob.NT1];
                if (X1 > Glob.X[Glob.NT1 + 1])
                {
                    Glob.RS[Glob.NT1] = Glob.RG[Glob.NT1];
                    InSmesh.AM1 = Glob.RS[Glob.NT1] * Glob.DX;
                    Glob.AMS[Glob.NT1] = InSmesh.AM1;
                    Glob.ES[Glob.NT1] = (Glob.PS[Glob.NT1] - C02 * (Glob.RS[Glob.NT1] - R0)) / (AK * Glob.RS[Glob.NT1]);
                    Glob.PS[Glob.NT1] = Glob.PG[Glob.NT1];
                    Glob.US[Glob.NT1] = InSmesh.US1;
                    AMS1 = AMS1 - InSmesh.AM1;
                    Glob.NT1 = Glob.NT1 + 1;
                    Glob.PS[Glob.NT1] = PGZ;
                }
            }
            else
            {
                InSmesh.AS1 = Glob.AMS[Glob.NT1 - 1];
                AMS1 = InSmesh.AM1 + InSmesh.AS1 + TAY * BG1[Glob.NT1 - 1] - InSmesh.AMTP;
                InSmesh.US1 = Glob.UG[Glob.NT1 - 1];
                X1 = Glob.X[Glob.NT1 - 1] + AMS1 / Glob.RS[Glob.NT1];
                Glob.NT1 = Glob.NT1 - 1;
                Glob.PS[Glob.NT1] = PGZ;
            };

            InSmesh.AM2 = AMS2;
            Glob.RS[Glob.NT2 - 1] = Glob.RG[Glob.NT2];
            AMS2 = AMS2 - TAY * BG1[Glob.NT2];

            if (AMS2 > 0.0)
            {
                InSmesh.US2 = Glob.UG[Glob.NT2];
                InSmesh.XS2 = X2;
                X2 = Glob.X[Glob.NT2] - AMS2 / Glob.RS[Glob.NT2];
                if (X2 < Glob.X[Glob.NT2 - 1])
                {
                    InSmesh.AM2 = Glob.RS[Glob.NT2] * Glob.DX;
                    Glob.AMS[Glob.NT2 - 1] = InSmesh.AM2;

                    Glob.ES[Glob.NT2 - 1] = Glob.ES[Glob.NT2];
                    Glob.PS[Glob.NT2 - 1] = Glob.PS[Glob.NT2];
                    Glob.US[Glob.NT2 - 1] = Glob.US[Glob.NT2];
                    Glob.RS[Glob.NT2 - 1] = Glob.RS[Glob.NT2];

                    AMS2 = AMS2 - InSmesh.AM2;

                    Glob.NT2 = Glob.NT2 - 1;
                    Glob.PS[Glob.NT2 - 1] = PGZ;
                };
            }
            else
            {
                InSmesh.TAY2 = InSmesh.AM2 / BG1[Glob.NT2];
                InSmesh.AS2 = Glob.AMS[Glob.NT2];
                AMS2 = InSmesh.AS2 + InSmesh.TAY2 * BG1[Glob.NT2] - TAY * BG1[Glob.NT2 + 1];
                InSmesh.US2 = Glob.UG[Glob.NT2 + 1];
                X2 = Glob.X[Glob.NT2 + 1] - AMS2 / Glob.RS[Glob.NT2];
                Glob.NT2 = Glob.NT2 + 1;
                Glob.PS[Glob.NT2 - 1] = PGZ;
            };

            InSmesh.AM3 = AMS3;
            Glob.RS[Glob.NT3] = Glob.RG[Glob.NT3];
            AMS3 = AMS3 + TAY * BG1[Glob.NT3];
            if (AMS3 > 0.0)
            {
                InSmesh.US3 = Glob.UG[Glob.NT3];
                InSmesh.XS3 = X3;

                X3 = Glob.X[Glob.NT3] + AMS3 / Glob.RS[Glob.NT3];
                //***********************************
                if (Glob.NT3 < Glob.N3)
                {
                    Glob.US[Glob.NT3] = Glob.UG[Glob.NT3];

                    if (X3 > Glob.X[Glob.NT3 + 1])
                    {
                        InSmesh.AM3 = Glob.RS[Glob.NT3] * Glob.DX;

                        Glob.AMS[Glob.NT3] = InSmesh.AM3;

                        Glob.ES[Glob.NT3] = (InSmesh.PATM - C02 * (Glob.RS[Glob.NT3] - R0)) / (AK * Glob.RS[Glob.NT3]);
                        Glob.PS[Glob.NT3] = InSmesh.PATM;
                        Glob.US[Glob.NT3] = InSmesh.US3;

                        AMS3 = AMS3 - InSmesh.AM3;
                        Glob.NT3 = Glob.NT3 + 1;

                        Glob.PS[Glob.NT3] = InSmesh.PATM;
                        Glob.US[Glob.NT3] = Glob.US[Glob.NT3 - 1];
                        Glob.RS[Glob.NT3] = AMS3 / (X3 - Glob.X[Glob.NT3]);
                    };
                    Glob.ES[Glob.NT3] = (InSmesh.PATM - C02 * (Glob.RS[Glob.NT3] - R0)) / (AK * Glob.RS[Glob.NT3]);

                }
                else
                {
                    AMS3 = 0.0;
                    Glob.PS[Glob.NT3] = InSmesh.PATM;
                    Glob.US[Glob.NT3] = Glob.UG[Glob.NT3];
                };

            }
            else
            {
                InSmesh.AS3 = Glob.AMS[Glob.NT3 - 1];

                AMS3 = InSmesh.AM3 + InSmesh.AS3 + TAY * BG1[Glob.NT3 - 1];
                InSmesh.US3 = Glob.UG[Glob.NT3 - 1];

                X3 = Glob.X[Glob.NT3 - 1] + AMS3 / Glob.RS[Glob.NT3];
                Glob.NT3 = Glob.NT3 - 1;
                Glob.PS[Glob.NT3] = InSmesh.PATM;
                Glob.US[Glob.NT3] = Glob.US[Glob.NT3 + 1];
                Glob.RS[Glob.NT3] = AMS3 / (X3 - Glob.X[Glob.NT3]);

                Glob.ES[Glob.NT3] = (InSmesh.PATM - C02 * (Glob.RS[Glob.NT3] - R0)) / (AK * Glob.RS[Glob.NT3]);

            };

        }

        //-----промежуточные функции основных процедур-------------
        void gos_goto53(ref double T, ref double PGZ, ref double TAY, ref double DENG, ref double DEN, ref double AMGOS, ref
                    double DMGOS, ref double AMG, ref double DENS, ref double AMG0)
        {
            AMGOS = InGos.AMT;
            DMGOS = InGos.DMT;
            AMG = AMG0;
        }
        void gos_goto52(ref double T, ref double PGZ, ref double TAY, ref double DENG, ref double DEN, ref double AMGOS, ref
                    double DMGOS, ref double AMG, ref double DENS, ref double AMG0)
        {
            Glob.HV2 = Glob.HGOS;
            InGos.AMT = AMG0;
            InGos.DMT = 0.0;
            gos_goto53(ref T, ref PGZ, ref TAY, ref DENG, ref DEN, ref AMGOS, ref DMGOS, ref AMG, ref DENS, ref AMG0);
        }
        void gos_goto51(ref double T, ref double PGZ, ref double TAY, ref double DENG, ref double DEN, ref double AMGOS, ref
                    double DMGOS, ref double AMG, ref double DENS, ref double AMG0)
        {

            if (Glob.Z2 >= 1.0) gos_goto52(ref T, ref PGZ, ref TAY, ref DENG, ref DEN, ref AMGOS, ref DMGOS, ref AMG, ref DENS, ref AMG0);
            else
            {
                Glob.HV2 = Glob.HV2 + TAY * InGos.U2;
                if (Glob.HV2 >= Glob.HGOS) gos_goto52(ref T, ref PGZ, ref TAY, ref DENG, ref DEN, ref AMGOS, ref DMGOS, ref AMG, ref DENS, ref AMG0);
                else
                {
                    InGos.TA = (Glob.E1G - InGos.EZ2) / (Glob.HGOS - Glob.HV2);
                    InGos.SA = InGos.TA / Math.Sqrt(1.0 + InGos.TA * InGos.TA);
                    InGos.CSA = InGos.SA / InGos.TA;
                    InGos.U2 = InGos.DZ1 / InGos.SA;
                    InGos.D = (InGos.DSS - 4.0 * InGos.EZ2) * (InGos.DGS + Glob.DGG - 2.0 * Glob.E1G);
                    InGos.D = InGos.D / 3.0;
                    InGos.D1 = -4.0 * InGos.DZ2 * (InGos.DGS + Glob.DGG - 2.0 * Glob.E1G);
                    InGos.D1 = InGos.D1 / 3.0;
                    InGos.AMT = AMG0 * (1.0 - (Glob.HGOS - Glob.HV2) * InGos.D / (Glob.HGOS * Glob.DDG));
                    InGos.DMT = AMG0 * (InGos.U2 * InGos.D / Glob.HGOS + Glob.HV2 * InGos.D1 / Glob.HGOS - InGos.D1);
                    InGos.DMT = InGos.DMT / Glob.DDG;
                    if (Glob.NGR == 2) gos_goto53(ref T, ref PGZ, ref TAY, ref DENG, ref DEN, ref AMGOS, ref DMGOS, ref AMG, ref DENS, ref AMG0);
                    else
                    {
                        InGos.ADMT = 2.0 * Glob.DGG * Glob.DGG - InGos.DE2 * InGos.DE2 - Glob.DGG * InGos.DE2;
                        InGos.AMT = 0.785 * DENS * (Glob.DDG * Glob.HGOS - 0.33 * (Glob.HGOS - Glob.HV2) * InGos.ADMT);
                        InGos.DMT = 0.2618 * DENS * (InGos.U2 * InGos.ADMT + (Glob.HGOS - Glob.HV2) * 2.0 * InGos.DZ2 * (2.0 * InGos.DE2 + Glob.DGG));
                        gos_goto53(ref T, ref PGZ, ref TAY, ref DENG, ref DEN, ref AMGOS, ref DMGOS, ref AMG, ref DENS, ref AMG0);
                    }
                }
            }
        }
        void gos_goto50(ref double T, ref double PGZ, ref double TAY, ref double DENG, ref double DEN, ref double AMGOS, ref
                    double DMGOS, ref double AMG, ref double DENS, ref double AMG0)
        {
            InGos.F2P = Glob.F2;
            Glob.F2 = Glob.AEGOS * (PGZ / 98100.0) / InGos.CSA;
            if (Glob.Z2 >= 1.0) Glob.F2 = 0.0;

            if (Glob.Z1 >= 1.0) gos_goto51(ref T, ref PGZ, ref TAY, ref DENG, ref DEN, ref AMGOS, ref DMGOS, ref AMG, ref DENS, ref AMG0);
            else
            {
                InGos.D = 3.0 * InGos.DGS * (InGos.DSS - 2.0 * (InGos.EZ1 + InGos.EZ2));
                InGos.D = InGos.D / 3.0;
                InGos.D1 = 2.0 * InGos.DGS * (InGos.DZ1 + InGos.DZ2);
                InGos.AMT = AMG0 * (1.0 - InGos.D / Glob.DDG);
                InGos.DMT = AMG0 * InGos.D1 / Glob.DDG;
                if (Glob.NGR == 2) gos_goto53(ref T, ref PGZ, ref TAY, ref DENG, ref DEN, ref AMGOS, ref DMGOS, ref AMG, ref DENS, ref AMG0);
                else
                {
                    InGos.AMT = 0.785 * DENS * Glob.HGOS * (0.33 * (InGos.DE1 * InGos.DE1 + InGos.DE2 * InGos.DE2 + InGos.DE1 * InGos.DE2) - Glob.DSG * Glob.DSG);
                    InGos.DMT = 0.5233 * DENS * Glob.HGOS * (InGos.DE1 * (2.0 * InGos.DZ1 + InGos.DZ2) + InGos.DE2 * (2.0 * InGos.DZ2 + InGos.DZ1));

                    gos_goto53(ref T, ref PGZ, ref TAY, ref DENG, ref DEN, ref AMGOS, ref DMGOS, ref AMG, ref DENS, ref AMG0);
                }
            }

        }
        void gos_goto49(ref double T, ref double PGZ, ref double TAY, ref double DENG, ref double DEN, ref double AMGOS, ref
                    double DMGOS, ref double AMG, ref double DENS, ref double AMG0)
        {
            Glob.HV2 = Glob.HV2 + TAY * InGos.U2;
            if (Glob.HV2 > Glob.HGOS) Glob.HV2 = Glob.HGOS;
            InGos.TA = Glob.E1G / (Glob.HV1 - Glob.HV2);
            InGos.SA = InGos.TA / Math.Sqrt(1.0 + InGos.TA * InGos.TA);
            InGos.CSA = InGos.SA / InGos.TA;
            InGos.U2 = InGos.DZ1 / InGos.SA;
            InGos.D = InGos.DGS * (InGos.DSS - 2.0 * Glob.E1G);
            InGos.AMT = AMG0 * (Glob.HV1 / Glob.HGOS - InGos.D * (Glob.HV1 - Glob.HV2) / (Glob.HGOS * Glob.DDG));
            InGos.DMT = AMG0 * (InGos.U1 - (InGos.U1 - InGos.U2) * InGos.D / Glob.DDG) / Glob.HGOS;
            if (Glob.NGR == 2) gos_goto53(ref T, ref PGZ, ref TAY, ref DENG, ref DEN, ref AMGOS, ref DMGOS, ref AMG, ref DENS, ref AMG0);
            else
            {
                InGos.DVM = Math.Pow((Glob.DSG + 2.0 * Glob.E1G), 2.0) + Glob.DSG * (Glob.DSG + 2.0 * Glob.E1G);
                InGos.AMT = 0.785 * DENS * (Glob.DDG * Glob.HV2 + 0.33 * (Glob.HV1 - Glob.HV2) * InGos.DVM);
                InGos.DMT = 0.785 * DENS * (Glob.DDG * InGos.U2 + 0.33 * (InGos.U1 - InGos.U2) * InGos.DVM);

                gos_goto53(ref T, ref PGZ, ref TAY, ref DENG, ref DEN, ref AMGOS, ref DMGOS, ref AMG, ref DENS, ref AMG0);
            }
        }
        void gos_goto48(ref double T, ref double PGZ, ref double TAY, ref double DENG, ref double DEN, ref double AMGOS, ref
                    double DMGOS, ref double AMG, ref double DENS, ref double AMG0)
        {

        }

        void FRAC_GOTO200(ref double T,ref double PT,ref double SNH,ref double R0,ref double VT,ref double TAY,ref double DV,
            ref double LT0,ref double E0,ref double HH0,ref double LT,ref double HM,ref double WT,ref double VTW)
        {
            InFrac.DP = Glob.PGZ - Glob.PPL;


            //Form2->Memo1->Lines->Append(IntToStr(Counter)+" "+ FloatToStr(Glob.PGZ)+" " + FloatToStr(Glob.PGG)+" "+ FloatToStr(Glob.PPL));
            //Form2->Memo1->Lines->SaveToFile("D:\\123.txt");


            if (InFrac.DP < 0.0) InFrac.DP = 0.0;
            if (Glob.PGZ < PT) InFrac.DP = 0.0;
            if (Glob.PGZ < (Glob.PGG + Glob.PPL)) InFrac.DP = 0.0;
            InFrac.Q1 = InFrac.Q;
            InFrac.Q = 0.6 * SNH * Math.Sqrt(2.0 * InFrac.DP / R0);
            VT = VT + 0.5 * TAY * (InFrac.Q1 + InFrac.Q);
            DV = 0.5 * (InFrac.Q1 + InFrac.Q);
            Glob.VT1 = VT;

            InFrac.L0 = LT0;
            InFrac.P0 = E0 / (1.0 - HH0 * HH0);
            InFrac.T0 = Glob.PUAZ * (1.0 - HH0 * HH0) / E0;
            InFrac.W0 = LT0 * LT0 * LT0;
            InFrac.Q0 = InFrac.W0 / InFrac.T0;

            InFrac.LL = LT / InFrac.L0;
            InFrac.HH = HM / InFrac.L0;
            InFrac.GG = Glob.PGG / InFrac.P0;
            InFrac.QQ = InFrac.Q / InFrac.Q0;
            InFrac.VV = VT / InFrac.W0;

            InFrac.PPT = InFrac.GG + Math.Pow((3.8571 * InFrac.QQ / InFrac.VV), 0.3333);

            if (InFrac.DP <= 0.0)
            {
                FRAC_GOTO20(ref T,ref TAY,ref LT,ref VT,ref WT);
            }
            else
            {
                InFrac.LL = Math.Pow((1.2284 * InFrac.QQ / (InFrac.HH * Math.Pow((InFrac.PPT - InFrac.GG), 4.0))), 0.5);

                InFrac.WW = 1.3333 * Math.Pow(InFrac.LL, 0.5) * Math.Pow((1.2284 * InFrac.QQ / InFrac.HH), 0.25);
                InFrac.WW = 2.0 * InFrac.WW;

                PT = InFrac.PPT * InFrac.P0;
                Glob.PT0 = PT;
                LT = InFrac.LL * InFrac.L0;
                WT = InFrac.WW * InFrac.L0;

                VTW = 1.5 * HM * LT * WT;
                FRAC_GOTO20(ref T,ref TAY,ref LT,ref VT,ref WT);
            };
        }
        void FRAC_GOTO20(ref double T,ref double TAY,ref double LT,ref double VT,ref double WT)
        {
            double vt, wt;
            if (Math.Abs(T - Glob.TG) <= (TAY / 2.0))
            {
                Glob.TG = Glob.TG + Glob.DTG;
                InFrac.NPT = InFrac.NPT + 1;
                if (InFrac.NPT > Glob.NP)
                {
                    //GO TO 100
                }
                else
                {
                    vt = VT * 1000.0;
                    wt = WT * 1000.0;

                    Glob.FT[InFrac.NPT] = T;
                    Glob.FL[InFrac.NPT] = LT;
                    Glob.FV[InFrac.NPT] = vt;
                    Glob.FW[InFrac.NPT] = wt;

                    /*		if (InFrac.NPT>2)
                            {
                                 if (LT<Glob.FL[InFrac.NPT])
                                     Glob.FL[InFrac.NPT]=Glob.FL[InFrac.NPT-1];
                                 if (vt<Glob.FV[InFrac.NPT])
                                     Glob.FV[InFrac.NPT]=Glob.FV[InFrac.NPT-1];
                                 if (wt<Glob.FW[InFrac.NPT])
                                     Glob.FW[InFrac.NPT]=Glob.FW[InFrac.NPT-1];
                            } */


                };
            }
            else
            {
                //go to 100
            };
        }

        void SetCountCalcPoint(int CountPoint)                 //присвоить количество точек для вычисления
        {
            this.CountPoint = CountPoint;
        }
        void SetZarad(ref CZarad Osn,ref CZarad Vosp)           //выбрать заряды на расчет
        {
            ZaradOsn = Osn;
            ZaradVosp = Vosp;
        }
        void LoadBaseParams(ref CLOADPARAMS Params)                   // базовые параметры для расчета
        {
            if (!object.ReferenceEquals(this.Params, Params)) 
            //if (this.Params != Params)
                this.Params = Params;


            Glob.HSP = Params.PodIntPerf + 10.0; //добавили 10 метров зумпфу
            Glob.HS = Params.GlubVoda;
            Glob.DT = Params.Dvn;
            Glob.R0 = Params.DensVoda;
            Glob.HM = Params.HPerf;
            Glob.ANP = Params.DensPerf;
            Glob.H0 = Params.CountVospZarad * ZaradVosp.L / 1000.0;
            Glob.HGOS = Params.CountOsnZarad * ZaradOsn.L / 1000.0;
            Glob.H1 = Glob.HSP - Params.GlubGen;
            InBase.TPL = Params.Tplast;
            Glob.E0 = Params.ModUnga;
            Glob.HH0 = Params.KPuass;
            Glob.PPL = Params.Pplast;
            Glob.TK = Params.DlitProc;
            Glob.TPX = Params.TPvdolWell;
            InBase.XG = Params.dHFromGenToMan;
        }
        void SetCalcInterval(double T)                  //присвоить интервал для расчета,c
        {
            Params.DlitProc = T;
            Glob.TK = T;
        }
        void SetdHFromGenToMan(double dH)                               //выбрать расстояние от манометра до генератора
        {
            Params.dHFromGenToMan = dH;
            InBase.XG = dH;
        }
        void SetTPvdolWell(double dT)                                       //выбрать время вывода вдоль скважины
        {
            Params.TPvdolWell = dT;
            Glob.TPX = dT;
        }
        void Calc()                                                                        //чего нить посчитать
        {

        }
        void GetCalcTimes(ref List<double> T)           //вернуть интервалы времени
        {
            for (int i = 1; i < CountPoint; i++)
            {
                T.Add(Glob.TY[i]);
            }
        }
        void GetCalcP(ref List<double> P)          //вернуть давление
        {
            for (int i = 1; i < CountPoint; i++)
            {
                P.Add(Glob.PY[i]);
            }
        }
        void GetCalcTemper(ref List<double> Temp)   //вернуть температуру{
        {
            for (int i = 1; i < CountPoint; i++)
            {
                Temp.Add(Glob.X4Y[i]);
            }
        }
        void GetDlinTrech(ref List<double> L)           //вернуть длину трещины от времени
        {
            double oldL = -1;
            for (int i = 1; i < CountPoint; i++)
            {
                if (oldL > Glob.FL[i]) Glob.FL[i] = Glob.FL[i - 1];
                oldL = Glob.FL[i];
                L.Add(Glob.FL[i]);
            }
        }
        void GetShirTrech(ref List<double> Shir)        //вернуть ширину трещины от времени
        {
            double oldS = -1;
            for (int i = 1; i < CountPoint; i++)
            {
                if (oldS > Glob.FW[i]) Glob.FW[i] = Glob.FW[i - 1];
                oldS = Glob.FW[i];
                Shir.Add(Glob.FW[i]);
            }
        }
        void Get1CoorGaz(ref List<double> Coord1)  //первая координата границы газовой области{
        {
            for (int i = 1; i < CountPoint; i++)
            {
                Coord1.Add(Glob.HSP - Glob.X2Y[i]);            //от низа  перф отв
            }
        }
        void Get2CoorGaz(ref List<double> Coord2)  //вторая координата границы газовой области 
        {
            for (int i = 1; i < CountPoint; i++)
            {
                Coord2.Add(Glob.HSP - Glob.X3Y[i]);
            }
        }
        void GetDavlOfWell(ref List<double> Davl)   //распределение давление в стволе скважины
        {
            int j;
            for (int i = 1; i < Glob.N3; i++)
            {
                j = Glob.N3 - i + 1;
                Davl.Add(Glob.PSX[j] / 100000.0);
            }
        }
        void GetWellCoord(ref List<double> Well)      //положение на стволе
        {
            for (int i = 1; i < Glob.N3; i++)
            {
                Well.Add(Glob.XS[i]);
            }
        }
        void GetCoordVoda(ref List<double> Voda)      //положение жидкости в скважине
        {
            for (int i = 1; i < CountPoint; i++)
            {
                Voda.Add(Glob.HSP - Glob.X5Y[i]);
            }
        }
        void OptimizeCalc() //пробуем с прбавлением PlusK считать
        {
            PlusK++;
        }
    }

    class CPoroh
    {
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

        public CPoroh(string Name, double Power, double Temper, double UdGaz, double Dens)
        {
            this.Name = Name;
            this.Power = Power;
            this.Temper = Temper;
            this.UdGaz = UdGaz;
            this.Dens = Dens;
            IsActive = false;
        }
    }

    class CZarad
    {
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
    }
}
