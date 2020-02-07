
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPorohWPF
{
    public class CReference
    {
  //      private TIniFile Ini;
  //      private string PathIni;

		//public List<CZarad> DataZarad;          //инфа по зарядам
		//public List<CPoroh> DataPoroh;          //инфа по порохам
		//public List<double> DataTimeInterval;     //инфа по интервалам вывода
		//public int CountZarad;
		//public int CountPoroh;
		//public int CountTime;
		//public bool OptimizeParametr;
		//public CReference(string PathIni)
		//{
		//	this.PathIni += PathIni + "Data.ini";
		//}
		//public int LoadDataFromIni()
		//{
		//	string tempP;
		//	int CountPor = 0;
		//	int CountZar = 0;
		//	int CountTime = 0;
		//	int tempi;
		//	float tempf;
		//	CPoroh poroh;
		//	CZarad zar;

		//	Ini = new TIniFile(this.PathIni);

		//	CountPor = Ini->ReadInteger("Data", "CountPor", 0);
		//	CountZar = Ini->ReadInteger("Data", "CountZar", 0);
		//	CountTime = Ini->ReadInteger("Data", "CountTime", 0);

		//	for (int i = 0; i < CountPor; i++)
		//	{
		//		tempP = "Por" + IntToStr(i);
		//		poroh = new CPoroh();
		//		poroh.IsActive = true;
		//		poroh.Name = Ini->ReadString(tempP, "Name", "Б/н");
		//		poroh.Power = Ini->ReadInteger(tempP, "Pow", 0);
		//		poroh.Temper = Ini->ReadInteger(tempP, "T", 0) + 273.0;
		//		poroh.UdGaz = Ini->ReadInteger(tempP, "Ud", 0);
		//		poroh->Dens = Ini->ReadInteger(tempP, "Ro", 0);
		//		DataPoroh.push_back(poroh);
		//	}

		//	for (int i = 0; i < CountTime; i++)
		//	{
		//		tempP = "TimeInterval_" + IntToStr(i);
		//		tempf = Ini->ReadInteger("Data", tempP, 0) / 1000.0;
		//		DataTimeInterval.push_back(tempf);
		//	}

		//	for (int i = 0; i < CountZar; i++)
		//	{
		//		tempP = "Zar" + IntToStr(i);
		//		zar = new CZarad();
		//		zar->IsActive = true;
		//		zar->Name = Ini->ReadString(tempP, "Name", "Б/н");
		//		zar->Dnar = Ini->ReadInteger(tempP, "Dnar", 0);
		//		zar->Dvnutr = Ini->ReadInteger(tempP, "Dvn", 0);
		//		zar->L = Ini->ReadInteger(tempP, "L", 0);
		//		tempi = Ini->ReadInteger(tempP, "PorType", -99);
		//		if (tempi == -99)
		//		{
		//			zar->Poroh = NULL;
		//		}
		//		else
		//		{
		//			zar->Poroh = DataPoroh[tempi];
		//		}
		//		DataZarad.push_back(zar);
		//	}

		//	this->CountPoroh = CountPor;
		//	this->CountZarad = CountZar;
		//	this->CountTime = CountTime;
		//	Ini->Free();

		//	return 0;
		//}
		//public int SaveDataToIni()
		//{
		//	string tempP, s;
		//	char ch[] = " ";
		//	int tempi;
		//	float tempf;
		//	CPoroh* poroh;
		//	CZarad* zar;

		//	Ini = new TIniFile(this->PathIni);
		//	//удаляем старые данные

		//	TFileStream* file;
		//	file = new TFileStream(PathIni, fmCreate);
		//	file->Write(ch, 1);
		//	file->Free();


		//	//пишем новые данные.
		//	Ini->WriteInteger("Data", "CountPor", this->CountPoroh);
		//	Ini->WriteInteger("Data", "CountZar", this->CountZarad);
		//	Ini->WriteInteger("Data", "CountTime", this->CountTime);

		//	for (int i = 0; i < this->CountTime; i++)
		//	{
		//		tempP = "TimeInterval_" + IntToStr(i);
		//		tempf = DataTimeInterval[i];
		//		Ini->WriteInteger("Data", tempP, tempf * 1000);

		//	}

		//	for (int i = 0; i < CountPoroh; i++)
		//	{
		//		tempP = "Por" + IntToStr(i);
		//		poroh = DataPoroh[i];
		//		Ini->WriteString(tempP, "Name", poroh->Name);
		//		Ini->WriteInteger(tempP, "Ud", poroh->UdGaz);
		//		Ini->WriteInteger(tempP, "T", poroh->Temper - 273.0);
		//		Ini->WriteInteger(tempP, "Ro", poroh->Dens);
		//		Ini->WriteInteger(tempP, "Pow", poroh->Power);
		//	}



		//	int k;
		//	for (int i = 0; i < CountZarad; i++)
		//	{
		//		k = -1;
		//		tempP = "Zar" + IntToStr(i);
		//		zar = DataZarad[i];
		//		Ini->WriteString(tempP, "Name", zar->Name);
		//		//gg=RoundF(zar->Dnar,4)*1000;
		//		Ini->WriteInteger(tempP, "Dnar", zar->Dnar);
		//		//fg=RoundF(,4);
		//		//	gg=fg*1000;
		//		Ini->WriteInteger(tempP, "Dvn", zar->Dvnutr);
		//		//gg=RoundF(,4)*1000;
		//		Ini->WriteInteger(tempP, "L", zar->L);

		//		if (zar->Poroh == NULL)
		//			Ini->WriteInteger(tempP, "PorType", -99);
		//		else
		//		{
		//			for (int j = 0; j < CountPoroh; j++)
		//			{
		//				s = DataPoroh[j]->Name;
		//				if (zar->Poroh->Name == s) { k = j; break; }
		//			}
		//			if (k > -1) Ini->WriteInteger(tempP, "PorType", k);
		//			else Ini->WriteInteger(tempP, "PorType", -99);
		//		}
		//	}


		//}
	}
}
