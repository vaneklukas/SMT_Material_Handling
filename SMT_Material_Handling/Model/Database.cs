using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT_Material_Handling.Model
{
    class Database
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;


        public List<ActualMaterialInLine> GetActualMaterialInLines(string machine,string module)
        {
            List<ActualMaterialInLine> inLines = new List<ActualMaterialInLine>();
            DataTable dt = new DataTable();

            using (OracleConnection con =new OracleConnection(connectionstring))
            {
                con.Open();

                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = "select LOCSLT,LOCBAR from T_LOC WHERE LOCMID="+machine+ " and LOCSTG=" + module + " and LOCSPASTT BETWEEN 0 AND 1 ";
                OracleDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ActualMaterialInLine actualMaterial = new ActualMaterialInLine();
                actualMaterial.Stage = Convert.ToInt32(dt.Rows[i]["LOCSLT"]);
                actualMaterial.Material = dt.Rows[i]["LOCBAR"].ToString();
                inLines.Add(actualMaterial);
            }

            return inLines;

        }
    }

    
}
