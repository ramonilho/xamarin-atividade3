using System;
using System.IO;
using Atividade3_Xamarin.Data;
using SQLite;
using Xamarin.Forms;

[assembly:Dependency(typeof(Atividade3_Xamarin.Droid.SQLite_Android))]
namespace Atividade3_Xamarin.Droid
{
    public class SQLite_Android : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var databaseFile = "fiapdb.db3";
            string directory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            var path = Path.Combine(directory, databaseFile);
            var connection = new SQLite.SQLiteConnection(path);

            return connection;
        }
    }
}
