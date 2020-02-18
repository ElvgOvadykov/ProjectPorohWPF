using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
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
                                zarads.Add(new CZarad(id, name, porohs.Where(x=>x.ID==idporoh).First(), ediameter, idiameter, length));
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
            string deleteAllPorohSQL = "DELETE FROM `Porohs`";
            StringBuilder addUpdatedListPorohs = new StringBuilder();
            foreach(var item in porohs)
            {
                string addedporoh = "INSERT INTO Porohs (Name, Power, Temp, UdGaz, Dens) values (" +
                    $"\"{item.Name}\",{item.Power},{item.Temper}, {item.UdGaz}, {item.Dens});";
                addUpdatedListPorohs.Append(addedporoh);
            }
            SQLiteCommand cmd = new SQLiteCommand();
            //using (SQLiteConnection conn = new SQLiteConnection("Data Source = DataBase.db; Version = 3; ", true))
            //{
            //    conn.Open();
            //    cmd.Connection = conn;
            //    cmd.CommandText = deleteAllPorohSQL;
            //    try
            //    {
            //        cmd.ExecuteNonQuery();
            //    }
            //    catch(Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
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
    }
}
