using System;
using SQLite;

namespace Atividade3_Xamarin.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
