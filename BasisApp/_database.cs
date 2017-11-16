using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Json;
using Newtonsoft.Json;
using System.Collections;
using Mono.Data.Sqlite;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BasisApp
{
    public class _database
    {
        string url = "http://webservices.educom.nu/services/second/";

        // context definieren
        private Context context;

        public void DownloadData()
        {
            Console.WriteLine("DownloadData gestart");
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            Rootobject result;
            try
            {
                byte[] myDataBuffer = webClient.DownloadData(this.url);
                string myString = Encoding.ASCII.GetString(myDataBuffer);

                // deserialize the json string
                result = JsonConvert.DeserializeObject<Rootobject>(myString);
                Console.WriteLine("result :" + result);
                foreach (var value in result.een)
                {
                    Console.WriteLine("++++++++++++++begin+++++++++++++++++++");
                    Console.WriteLine( myString);
                    Console.WriteLine(value);
                    Console.WriteLine("++++++++++++++eind+++++++++++++++++++");
                }
            }

            catch (WebException)
            {
                //even niks
            }
        }

        // database maken
        public void CreateDatabase()
        {
            Resources res = this.context.Resources;
            string app_name
                = res.GetString(Resource.String.app_name);
            string app_version
                = res.GetString(Resource.String.app_version);
            string createTableData
                = res.GetString(Resource.String.createTableData);

            Console.WriteLine("begin create database");
            Console.WriteLine(createTableData);

            string dbname = "_db_" + app_name + "_" + app_version + ".sqlite";
            Console.WriteLine(dbname);
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string pathToDatabase = Path.Combine(documentsPath, dbname);
            Console.WriteLine(pathToDatabase);

            if (!File.Exists(pathToDatabase))
            {
                Mono.Data.Sqlite.SqliteConnection.CreateFile(pathToDatabase);
                var connectionString = String.Format("Data Source={0}; Version = 3;", pathToDatabase);
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        //table data
                        cmd.CommandText = createTableData;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            Console.WriteLine("einde create database");
        }

        // constructor
        public _database(Context context)
        {
            this.context = context;
            CreateDatabase();
            DownloadData();
        }

        public class Rootobject
        {
            [JsonProperty("een")]
            public Een[] een { get; set; }

            [JsonProperty("twee")]
            public Twee[] twee { get; set; }
        }

        public class Een
        {
            [JsonProperty("code")]
            public string code { get; set; }
            [JsonProperty("description")]
            public string description { get; set; }
        }

        public class Twee
        {
            [JsonProperty("code")]
            public string code { get; set; }
            [JsonProperty("description")]
            public string description { get; set; }
        }
    }
}