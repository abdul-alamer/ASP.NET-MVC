using Dapper;
using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataLibrary.BusinessLogic
{
    public static class PatientProccess
    {
        public static int CreatePatient( string name, 
            int age, int dosage)
        {
            PatientModel data = new PatientModel
            {
              //  PatientId = patientId,
                Name = name,
                Age = age,
                Dosage = dosage
            };

            string sql = @"insert into dbo.Patient (Name, Age, Dosage)
                           values (@Name, @Age, @Dosage);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<PatientModel> LoadPatient()
        {
           
            string sql = @"select * from dbo.Patient;";

            return SqlDataAccess.LoadData<PatientModel>(sql);
        }

        public static PatientModel SearchPatientByname(string name)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                string sql = @"Select * From dbo.Patient " +
                                    "WHERE CONVERT(VARCHAR, Name) =@name";// + name;
                return cnn.Query<PatientModel>(sql, new { @name=name }).SingleOrDefault();
            }



        }

        public static string GetConnectionString(string connectionName = "MVCDemoDB")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }
    }
}
