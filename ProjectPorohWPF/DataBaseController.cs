using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectPorohWPF
{
    class DataBaseController
    {
        public static BindingList<CPoroh> GetPorohs()
        {
            string sql = "SELECT * FROM `Porohs`";
            BindingList<CPoroh> porohs = new BindingList<CPoroh>();
            // Создать объект Command.
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = DataBase.db; Version = 3; ", true))
            {
                conn.Open();
                // Сочетать Command с Connection.
                cmd.Connection = conn;
                cmd.CommandText = sql;
                try
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt16(0);
                                string name = reader.GetString(1);
                                double power = reader.GetDouble(2);
                                double temp = reader.GetDouble(3);
                                double udgaz = reader.GetDouble(4);
                                double dens = reader.GetDouble(5);
                                porohs.Add(new CPoroh(id, name,power,temp,udgaz,dens));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                return porohs;
            }
        }

        public static BindingList<CZarad> GetZarads()
        {
            string sql = "SELECT * FROM `Zarads`";
            BindingList<CZarad> zarads = new BindingList<CZarad>();
            // Создать объект Command.
            SQLiteCommand cmd = new SQLiteCommand();
            BindingList<CPoroh> porohs = GetPorohs();
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = DataBase.db; Version = 3; ", true))
            {
                conn.Open();
                // Сочетать Command с Connection.
                cmd.Connection = conn;
                cmd.CommandText = sql;
                try
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt16(0);
                                string name = reader.GetString(1);
                                double ediameter = reader.GetDouble(2);
                                double idiameter = reader.GetDouble(3);
                                double length = reader.GetDouble(4);
                                int idporoh = reader.GetInt32(5);
                                zarads.Add(new CZarad(id, name, porohs.Where(x=>x.ID==idporoh).FirstOrDefault(), ediameter, idiameter, length));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                return zarads;
            }
        }

        public static List<double> GetDataTimeIntervals() //инфа по интервалам вывода
        {
            string sql = "SELECT * FROM `DataTimeInterval`";
            List<double> result = new List<double>();
            // Создать объект Command.
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = DataBase.db; Version = 3; ", true))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                try
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                double value = reader.GetDouble(1);
                                result.Add(value);
                            }
                        }
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return result;
        }

        public static void UpdateAllPorohs(List<CPoroh> porohs)
        {
            string addedporoh = "";
            StringBuilder addUpdatedListPorohs = new StringBuilder();
            foreach(var item in porohs)
            {
                if(item.ID == 0)
                {
                    addedporoh = "INSERT INTO Porohs (Name, Power, Temp, UdGaz, Dens) values (" +
                        $"\"{item.Name}\",{item.Power.ToString("G", CultureInfo.InvariantCulture)},{item.Temper.ToString("G", CultureInfo.InvariantCulture)}, {item.UdGaz.ToString("G", CultureInfo.InvariantCulture)}, {item.Dens.ToString("G", CultureInfo.InvariantCulture)});";
                }
                else
                {
                    addedporoh = $"UPDATE Porohs SET Name = \"{item.Name}\", Power = {item.Power.ToString("G", CultureInfo.InvariantCulture)}, Temp = {item.Temper.ToString("G", CultureInfo.InvariantCulture)}, UdGaz = {item.UdGaz.ToString("G", CultureInfo.InvariantCulture)}, Dens = {item.Dens.ToString("G", CultureInfo.InvariantCulture)} WHERE ID = {item.ID};";
                }
                addUpdatedListPorohs.Append(addedporoh);
            }
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = DataBase.db; Version = 3; ", true))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = addUpdatedListPorohs.ToString();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static void DeletePoroh(CPoroh poroh)
        {
            if(poroh != null)
            {
                string deletesql = $"delete from Porohs where ID = {poroh.ID}";
                SQLiteCommand cmd = new SQLiteCommand();
                using (SQLiteConnection conn = new SQLiteConnection("Data Source = DataBase.db; Version = 3; ", true))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = deletesql;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void UpdateAllCharges(List<CZarad> zarads)
        {
            string addedCharges = "";
            StringBuilder addUpdatedListCharges = new StringBuilder();
            foreach (var item in zarads)
            {
                if (item.ID == 0)
                {
                    addedCharges = "insert into Zarads (Name, EDiameter, IDiameter, \"Length\", IDPowder) values (" +
                        $"\"{item.Name}\",{item.Dnar.ToString("G", CultureInfo.InvariantCulture)},{item.Dvnutr.ToString("G", CultureInfo.InvariantCulture)}, {item.L.ToString("G", CultureInfo.InvariantCulture)}, {item.Poroh.ID});";
                }
                else
                {
                    if (item.Poroh == null)
                        item.Poroh = new CPoroh();
                    addedCharges = $"UPDATE Zarads SET Name = \"{item.Name}\", EDiameter = {item.Dnar.ToString("G", CultureInfo.InvariantCulture)}, IDiameter = {item.Dvnutr.ToString("G", CultureInfo.InvariantCulture)}, \"Length\" = {item.L.ToString("G", CultureInfo.InvariantCulture)}, IDPowder = {item.Poroh.ID} WHERE ID = {item.ID};";
                }
                addUpdatedListCharges.Append(addedCharges);
            }
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = DataBase.db; Version = 3; ", true))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = addUpdatedListCharges.ToString();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static void DeleteCharge(CZarad zarad)
        {
            if(zarad != null)
            {
                string deletesql = $"delete from Zarads where ID = {zarad.ID}";
                SQLiteCommand cmd = new SQLiteCommand();
                using (SQLiteConnection conn = new SQLiteConnection("Data Source = DataBase.db; Version = 3; ", true))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = deletesql;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void SaveCalculationToArchive(CLOADPARAMS BaseCalcParam)
        {
            string sql = "insert into Archive (Name, Date, CompanyName, Executer, MadeFor, FieldName, BushNumber, WellNumber, " +
                "SlaughterCurrent, PunchIntervalPower, SolePerforationInterval, GenerationDepth, PerforationDensity, " +
                "CasingDiameter, CasingThickness, ReservoirPressure, ReservoirTemperature, YoungModulus, " +
                "PoissonRatio, TypeFluid, FluidLevel, FluidDensity, SimulationDuration, IDOsnZarad, CountOsnZarad, " +
                " IDOsnPoroh, IDVospZarad, CountVospZarad, IDVospPoroh, Distance, TimeAfterExposure, OsnNameZarad, " +
                "OsnEDiameter, OsnIDiameter, OsnLength, OsnNamePoroh, OsnPower, OsnTemp, OsnUdgaz, OsnDens," +
                "VospNameZarad, VospEDiameter, VospIDiameter, VospLength, VospNamePoroh, VospPower, VospTemp," +
                "VospUdgaz, VospDens) values ";
            string sqlvalues = $"(\"{BaseCalcParam.CalculationName}\", " +
                $"\"{BaseCalcParam.DateWithOutTime}\", " +
                $"\"{BaseCalcParam.CompanyName}\"," +
                $"\"{BaseCalcParam.CalculationExecutor}\"," +
                $"\"{BaseCalcParam.MadeFor}\"," +
                $"\"{BaseCalcParam.NameMestor}\"," +
                $"\"{BaseCalcParam.BushNumber}\", " +
                $"\"{BaseCalcParam.NameWell}\", " +
                $"{BaseCalcParam.Zaboy.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.HPerf.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.PodIntPerf.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.GlubGen.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.DensPerf.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.CasingDiameter.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.CasingThickness.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.Pplast.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.Tplast.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.ModUnga.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.KPuass.ToString("G", CultureInfo.InvariantCulture)}," +
                $"\"{BaseCalcParam.TypeFluid}\"," +
                $"{BaseCalcParam.GlubVoda.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.DensVoda.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.TimeInterval.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.osnZar.ID}," +
                $"{BaseCalcParam.CountOsnZarad.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.osnZar.Poroh.ID}," +
                $"{BaseCalcParam.vospZar.ID}," +
                $"{BaseCalcParam.CountVospZarad}," +
                $"{BaseCalcParam.vospZar.Poroh.ID}," +
                $"{BaseCalcParam.dHFromGenToMan.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.TPvdolWell.ToString("G", CultureInfo.InvariantCulture)}," +
                $"\"{BaseCalcParam.osnZar.Name}\"," +
                $"{BaseCalcParam.osnZar.Dnar.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.osnZar.Dvnutr.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.osnZar.L.ToString("G", CultureInfo.InvariantCulture)}," +
                $"\"{BaseCalcParam.osnZar.Poroh.Name}\"," +
                $"{BaseCalcParam.osnZar.Poroh.Power.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.osnZar.Poroh.Temper.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.osnZar.Poroh.UdGaz.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.osnZar.Poroh.Dens.ToString("G", CultureInfo.InvariantCulture)}," +
                $"\"{BaseCalcParam.vospZar.Name}\"," +
                $"{BaseCalcParam.vospZar.Dnar.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.vospZar.Dvnutr.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.vospZar.L.ToString("G", CultureInfo.InvariantCulture)}," +
                $"\"{BaseCalcParam.vospZar.Poroh.Name}\"," +
                $"{BaseCalcParam.vospZar.Poroh.Power.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.vospZar.Poroh.Temper.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.vospZar.Poroh.UdGaz.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{BaseCalcParam.vospZar.Poroh.Dens.ToString("G", CultureInfo.InvariantCulture)});";
            sql += sqlvalues;
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = DataBase.db; Version = 3; ", true))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sql.ToString();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static BindingList<CLOADPARAMS> GetArchive()
        {
            List<CZarad> zarads = new List<CZarad>(GetZarads());
            List<CPoroh> porohs = new List<CPoroh>(GetPorohs());
            string sql = "SELECT * FROM `Archive`";
            BindingList<CLOADPARAMS> result = new BindingList<CLOADPARAMS>();
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = DataBase.db; Version = 3; ", true))
            {
                conn.Open();
                // Сочетать Command с Connection.
                cmd.Connection = conn;
                cmd.CommandText = sql;
                try
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt16(0);
                                DateTime date = DateTime.Parse(reader.GetString(1));
                                string name = reader.GetString(2);
                                string companyname = reader.GetString(3);
                                string executer = reader.GetString(4);
                                string madefor = reader.GetString(5);
                                string fieldname = reader.GetString(6);
                                string bushnumber = reader.GetString(7);
                                string wellnumber = reader.GetString(8);
                                double zaboy = reader.GetDouble(9);
                                double hperf = reader.GetDouble(10);
                                double podintperf = reader.GetDouble(11);
                                double glubgen = reader.GetDouble(12);
                                double densperf = reader.GetDouble(13);
                                double casingdiameter = reader.GetDouble(14);
                                double casingthickness = reader.GetDouble(15);
                                double pplast = reader.GetDouble(16);
                                double tplast = reader.GetDouble(17);
                                double modunga = reader.GetDouble(18);
                                double kpuass = reader.GetDouble(19);
                                string typefluid = reader.GetString(20);
                                double glubvoda = reader.GetDouble(21);
                                double densvoda = reader.GetDouble(22);
                                double timeinterval = reader.GetDouble(23);
                                int idosnzar = reader.GetInt32(24);
                                int countosnzar = reader.GetInt32(25);
                                int idosnporoh = reader.GetInt32(26);
                                int idvospzar = reader.GetInt32(27);
                                int countvospzar = reader.GetInt32(28);
                                int idvospporoh = reader.GetInt32(29);
                                double distance = reader.GetDouble(30);
                                double timeafterexposure = reader.GetDouble(31);

                                    string osnnameporoh = reader.GetString(36);
                                    double osnpower = reader.GetDouble(37);
                                    double osntemp = reader.GetDouble(38);
                                    double osnudgaz = reader.GetDouble(39);
                                    double osndens = reader.GetDouble(40);
                                CPoroh osnporoh = new CPoroh()
                                    {
                                        Name = osnnameporoh,
                                        Power = osnpower,
                                        Temper = osntemp,
                                        UdGaz = osnudgaz,
                                        Dens = osndens
                                    };

                                    string osnnamezarad = reader.GetString(32);
                                    double osnediameter = reader.GetDouble(33);
                                    double osnidiameter = reader.GetDouble(34);
                                    double osnlength = reader.GetDouble(35);
                                CZarad osnzarad = new CZarad()
                                    {
                                        Name = osnnamezarad,
                                        Dnar = osnediameter,
                                        Dvnutr = osnidiameter,
                                        L = osnlength,
                                        Poroh = osnporoh
                                    };
                                


                                    string vospnameporoh = reader.GetString(45);
                                    double vosppower = reader.GetDouble(46);
                                    double vosptemp = reader.GetDouble(47);
                                    double vospudgaz = reader.GetDouble(48);
                                    double vospdens = reader.GetDouble(49);
                                    CPoroh vospporoh = new CPoroh()
                                    {
                                        Name = vospnameporoh,
                                        Power = vosppower,
                                        Temper = vosptemp,
                                        UdGaz = vospudgaz,
                                        Dens = vospdens
                                    };

                                    string vospnamezarad = reader.GetString(41);
                                    double vospediameter = reader.GetDouble(42);
                                    double vospidiameter = reader.GetDouble(43);
                                    double vosplength = reader.GetDouble(44);
                                CZarad vospzarad = new CZarad()
                                    {
                                        Name = vospnamezarad,
                                        Dnar = vospediameter,
                                        Dvnutr = vospidiameter,
                                        L = vosplength,
                                        Poroh = vospporoh
                                    };
                                

                                result.Add(new CLOADPARAMS()
                                {
                                    ID = id,
                                    CalculationName = name,
                                    BushNumber = bushnumber,
                                    CalculationExecutor = executer,
                                    CasingDiameter = casingdiameter,
                                    CasingThickness = casingthickness,
                                    CompanyName = companyname,
                                    CountOsnZarad = countosnzar,
                                    CountVospZarad = countvospzar,
                                    Date = date,
                                    DensPerf = densperf,
                                    DensVoda = densvoda,
                                    TimeInterval = timeinterval,
                                    GlubGen = glubgen,
                                    GlubVoda = glubvoda,
                                    HPerf = hperf,
                                    KPuass = kpuass,
                                    MadeFor = madefor,
                                    ModUnga = modunga,
                                    NameMestor = fieldname,
                                    NameWell = wellnumber,
                                    osnZar = osnzarad,
                                    vospZar = vospzarad,
                                    PodIntPerf = podintperf,
                                    Pplast = pplast,
                                    Tplast = tplast,
                                    TypeFluid = typefluid,
                                    Zaboy = zaboy,
                                    dHFromGenToMan = distance,
                                    TPvdolWell = timeafterexposure,
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return result;
        }

        public static void DeleteArchive(CLOADPARAMS deleted)
        {
            if (deleted != null)
            {
                string sql = $"delete from Archive where ID = {deleted.ID.ToString()};";
                SQLiteCommand cmd = new SQLiteCommand();
                using (SQLiteConnection conn = new SQLiteConnection("Data Source = DataBase.db; Version = 3; ", true))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static CZarad AddCharge(CZarad zarad)
        {
            string sqlInsert = "insert into Zarads (Name, EDiameter, IDiameter, Length, IDPowder) values " +
                $"(\"{zarad.Name}\",{zarad.Dnar.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{zarad.Dvnutr.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{zarad.L.ToString("G", CultureInfo.InvariantCulture)}," +
                $"{zarad.Poroh.ID});";
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = DataBase.db; Version = 3; ", true))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlInsert;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            string sqlreturnid = "select ID from Zarads;";
            int lastid = 0;
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = DataBase.db; Version = 3; ", true))
            {
                conn.Open();
                // Сочетать Command с Connection.
                cmd.Connection = conn;
                cmd.CommandText = sqlreturnid;
                try
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lastid = reader.GetInt16(0);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            zarad.ID = lastid;

            return zarad;
        }

        public static CPoroh AddPoroh(CPoroh poroh)
        {
            string sqlInsert = "insert into Porohs (Name, Power, Temp, UdGaz, Dens) values " +
                   $"(\"{poroh.Name}\",{poroh.Power.ToString("G", CultureInfo.InvariantCulture)}," +
                   $"{poroh.Temper.ToString("G", CultureInfo.InvariantCulture)}," +
                   $"{poroh.UdGaz.ToString("G", CultureInfo.InvariantCulture)}," +
                   $"{poroh.Dens.ToString("G", CultureInfo.InvariantCulture)});";
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = DataBase.db; Version = 3; ", true))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlInsert;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            string sqlreturnid = "select ID from Porohs;";
            int lastid = 0;
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = DataBase.db; Version = 3; ", true))
            {
                conn.Open();
                // Сочетать Command с Connection.
                cmd.Connection = conn;
                cmd.CommandText = sqlreturnid;
                try
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lastid = reader.GetInt16(0);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            poroh.ID = lastid;
            return poroh;
        }
    }
}
