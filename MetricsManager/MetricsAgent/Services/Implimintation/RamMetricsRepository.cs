using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Services.Implimintation
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        #region SQL Lite connection string:

        private const string connectionString = 
            "Data Source=metrics.db;" +
            "Version=3;" +
            "Pooling=true;" +
            "Max Pool Size=100;";

        #endregion 

        public void Create(RamMetric item)
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            // Make object for creating SQL commands:
            using var cmd = new SQLiteCommand(connection);

            cmd.CommandText = "INSERT INTO rammetrics(value, time) VALUES(@value, @time);";

            // Add some parameters to our request:
            cmd.Parameters.AddWithValue("@value", item.Value);
            // Time should be in seconds, that is why we use a property to convert it:
            cmd.Parameters.AddWithValue("@time", item.Time);

            // Prepare commands for executing:
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);
            // Writing command for deleting metric which id is the same from DataBase:
            cmd.CommandText = "DELETE FROM rammetrics WHERE id=@id;";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<RamMetric> GetAll()
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);
            // Writing a SELECT command to our DataBase for getting information:
            cmd.CommandText = "SELECT * FROM rammetrics;";
            // Creting a list for saving result:
            var returnedList = new List<RamMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // While DataBase has information for reading (while reader can read something):
                while (reader.Read())
                {
                    // Add object into the result list:
                    returnedList.Add(new RamMetric
                    {
                        // Make new CpuMetric object with data from reader
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt32(2)
                    });
                }
            }
            return returnedList;
        }

        public RamMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM rammetrics WHERE id=@id;";
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // If reader can reader something successful:
                if (reader.Read())
                {
                    return new RamMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt32(2)

                    };
                }
                // If reader could not find any record whith this id: 
                else return null;
            }
        }

        public IList<RamMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);
            // Writing a SELECT command to our DataBase for getting information:
            cmd.CommandText = "SELECT * FROM rammetrics WHERE time >= @fromTime and time <= @toTime;";
            cmd.Parameters.AddWithValue(@"fromTime", fromTime.TotalSeconds);
            cmd.Parameters.AddWithValue(@"toTime", toTime.TotalSeconds);
            // Creting a list for saving result:
            var returnedList = new List<RamMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // While DataBase has information for reading (while reader can read something):
                while (reader.Read())
                {
                    // Add object into the result list:
                    returnedList.Add(new RamMetric
                    {
                        // Make new CpuMetric object with data from reader
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt32(2)
                    });
                }
            }
            return returnedList;
        }

        public void Update(RamMetric item)
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);
            // Writing a command for updating data in our DataBase:
            cmd.CommandText = "UPDATE rammetrics SET value=@value, time = @time WHERE id=@id;";
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("time", item.Time);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}
