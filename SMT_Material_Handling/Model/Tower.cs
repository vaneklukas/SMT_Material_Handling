using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMT_Material_Handling.Model
{
    class Tower
    {
        string remoteOrderDestination = ConfigurationManager.AppSettings["TowerRemoteOrder"];
        string remoteAnsDestination = ConfigurationManager.AppSettings["TowerRemoteAns"];
        
        public string sendRequest(string input)
        {
            
            string[] mat = input.Split(' ');
            string timestamp = "";
            string messageForOperator = "Materiál "+mat[1]+" ";
            Dictionary<string, int> outofTower = getDictionary();

            

            if (outofTower.ContainsKey(mat[2]))
            {
                int position;
                outofTower.TryGetValue(mat[2],out position);
                messageForOperator ="Materiál "+mat[2]+ " není ve věži." + Environment.NewLine + "Je uložen v supermarketu na pozici " + position.ToString();

            }
            else
            {


                timestamp = "\\" + DateTime.Now.Year + DateTime.Now.Month.ToString().PadRight(2, '0') +
                           DateTime.Now.Day.ToString().PadRight(2, '0') + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                string eto = ".eto";

                string filename = remoteOrderDestination + timestamp + mat[2] + eto;

                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.WriteLine("[Order]");
                    sw.WriteLine("Action=PROVIDE");
                    sw.WriteLine("Object=ARTICLE");
                    sw.WriteLine();
                    sw.WriteLine("Name=" + mat[2]);
                    sw.WriteLine("Demand=1");
                    sw.WriteLine("Target=Default");
                    sw.WriteLine("ForceTower=TRUE");
                    sw.Flush();
                }
                Thread.Sleep(2000);
                messageForOperator = getAnswer(timestamp, mat[2], filename);

            }

            return messageForOperator;
        }

        private Dictionary<string, int> getDictionary()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            string[] lines = File.ReadAllLines("OutMaterials.ini");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] input= lines[i].Split(',');
                dic.Add(input[0], Convert.ToInt32(input[1]));
            }
            return dic;
        }

        public string getAnswer(string timestamp,string material,string filename)
        {
            string filenameanswer = remoteAnsDestination + timestamp + material + ".ans";
            
            string ans = "Materiál "+material+" ";
            try
            {

            
                string[] lines = File.ReadAllLines(filenameanswer);
                string errorcode = "";
                for (int i = lines.Length-1; i > 0; i--)
                {
                    string line = lines[i];
                    if (line.StartsWith("Error"))
                    {
                         string[] result = line.Split('=');
                        errorcode = result[1];
                        break;
                    }
                }
                    switch (errorcode)
                    {
                        case "0":
                     ans =ans+ "Byl vyskladněn";
                     break;

                        case "-1":
                     ans =ans+ "Nelze rozpoznat";
                     break;

                        case "-3":
                     ans =ans+ "Vyskladnění je již v procesu zpracování";
                     break;

                        case "9":
                     ans =ans+ "Chyba komunikace";
                     break;

                        case "1876":
                     ans =ans+ "Nelze vyskladnit (blokovaný materiál) - kontaktujte pohotovost";
                     break;

                        case "1879":
                     ans =ans+ "Nedostatečná zásoba ve věžích";
                     break;

                        case "1880":
                     ans =ans+ "Byl vyskladněn";
                     break;

                        case "1881":
                     ans =ans+ "Byl vyskladněn";
                     break;

                        case "1928":
                     ans =ans+ "Nedostatečná zásoba ve věžích";
                     break;

                        default:
                     ans = "Chyba - kontaktujte pohotovost";
                     break;
                            
                    }

            }
            catch (Exception)
            {
                ans = "Nepodařilo se získat odpověď z věže." + Environment.NewLine + "V případě nevyskladnění příkaz opakujte";
            }

            File.Delete(filenameanswer);
            File.Delete(filename);

            return ans;
        }

    }
}
