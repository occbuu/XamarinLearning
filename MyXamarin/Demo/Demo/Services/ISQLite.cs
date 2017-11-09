using SQLite;

namespace Demo.Services
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}