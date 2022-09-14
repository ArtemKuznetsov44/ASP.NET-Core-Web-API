using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;
using Dapper;

namespace MetricsAgent.Services.Implimintation
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        #region SQL Lite connection string from options (appsettings.json file):

        private readonly IOptions<DataBaseOptions> _dataBaseOptions;

        #endregion 

        public HddMetricsRepository(IOptions<DataBaseOptions> dataBaseOptions) => _dataBaseOptions = dataBaseOptions;

        public void Create(HddMetric item)
        {
            using var connection = new SQLiteConnection(_dataBaseOptions.Value.ConnectionString);
            connection.Open();

            connection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time);", new
            {
                value = item.Value, 
                time = item.Time
            });

            #region Without Dapper:

            //// Make object for creating SQL commands:
            //using var cmd = new SQLiteCommand(connection);

            //cmd.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(@value, @time);";

            //// Add some parameters to our request:
            //cmd.Parameters.AddWithValue("@value", item.Value);
            //// Time should be in seconds, that is why we use a property to convert it:
            //cmd.Parameters.AddWithValue("@time", item.Time);

            //// Prepare commands for executing:
            //cmd.Prepare();
            //cmd.ExecuteNonQuery();

            #endregion
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_dataBaseOptions.Value.ConnectionString);
            connection.Open();

            connection.Execute("DELETE FROM hddmetrics WHERE id=@id;", new
            {
                id = id
            });

            #region Without Dapper:

            //using var cmd = new SQLiteCommand(connection);
            //// Writing command for deleting metric which id is the same from DataBase:
            //cmd.CommandText = "DELETE FROM hddmetrics WHERE id=@id;";
            //cmd.Parameters.AddWithValue("@id", id);
            //cmd.Prepare();
            //cmd.ExecuteNonQuery();

            #endregion
        }

        public IList<HddMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_dataBaseOptions.Value.ConnectionString);
            connection.Open();

            return connection.Query<HddMetric>("SELECT * FROM hddmetrics;").ToList();

            #region Without Dapper:

            //using var cmd = new SQLiteCommand(connection);
            //// Writing a SELECT command to our DataBase for getting information:
            //cmd.CommandText = "SELECT * FROM hddmetrics;";
            //// Creting a list for saving result:
            //var returnedList = new List<HddMetric>();

            //using (SQLiteDataReader reader = cmd.ExecuteReader())
            //{
            //    // While DataBase has information for reading (while reader can read something):
            //    while (reader.Read())
            //    {
            //        // Add object into the result list:
            //        returnedList.Add(new HddMetric
            //        {
            //            // Make new CpuMetric object with data from reader
            //            Id = reader.GetInt32(0),
            //            Value = reader.GetInt32(1),
            //            Time = reader.GetInt32(2)
            //        });
            //    }
            //}
            //return returnedList;

            #endregion
        }

        public HddMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_dataBaseOptions.Value.ConnectionString);
            connection.Open();

            HddMetric metric = connection.QuerySingle<HddMetric>("SELECT * FROM hddmetrics WHERE id=@id;", new
            {
                id = id
            }); 
            return metric;

            #region Without Dapper:

            //using var cmd = new SQLiteCommand(connection);
            //cmd.CommandText = "SELECT * FROM hddmetrics WHERE id=@id;";
            //using (SQLiteDataReader reader = cmd.ExecuteReader())
            //{
            //    // If reader can reader something successful:
            //    if (reader.Read())
            //    {
            //        return new HddMetric
            //        {
            //            Id = reader.GetInt32(0),
            //            Value = reader.GetInt32(1),
            //            Time = reader.GetInt32(2)

            //        };
            //    }
            //    // If reader could not find any record whith this id: 
            //    else return null;
            //}

            #endregion
        }

        public IList<HddMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using var connection = new SQLiteConnection(_dataBaseOptions.Value.ConnectionString);
            connection.Open();

            List<HddMetric> resList = connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE time >= @fromTime and time <= @toTime;", new
            {
                fromTime = fromTime.TotalSeconds, 
                toTime = toTime.TotalSeconds
            }).ToList(); 
            return resList;

            #region Without Dapper:

            //using var cmd = new SQLiteCommand(connection);
            //// Writing a SELECT command to our DataBase for getting information:
            //cmd.CommandText = "SELECT * FROM hddmetrics WHERE time >= @fromTime and time <= @toTime;";
            //cmd.Parameters.AddWithValue(@"fromTime", fromTime.TotalSeconds);
            //cmd.Parameters.AddWithValue(@"toTime", toTime.TotalSeconds);
            //// Creting a list for saving result:
            //var returnedList = new List<HddMetric>();

            //using (SQLiteDataReader reader = cmd.ExecuteReader())
            //{
            //    // While DataBase has information for reading (while reader can read something):
            //    while (reader.Read())
            //    {
            //        // Add object into the result list:
            //        returnedList.Add(new HddMetric
            //        {
            //            // Make new CpuMetric object with data from reader
            //            Id = reader.GetInt32(0),
            //            Value = reader.GetInt32(1),
            //            Time = reader.GetInt32(2)
            //        });
            //    }
            //}
            //return returnedList;

            #endregion
        }

        public void Update(HddMetric item)
        {
            using var connection = new SQLiteConnection(_dataBaseOptions.Value.ConnectionString);
            connection.Open();

            connection.Execute("UPDATE hddmetrics SET value=@value, time = @time WHERE id=@id;", new
            {
                value = item.Value, 
                time = item.Time, 
                id = item.Id
            });

            #region Without Dapper:

            //using var cmd = new SQLiteCommand(connection);
            //// Writing a command for updating data in our DataBase:
            //cmd.CommandText = "UPDATE hddmetrics SET value=@value, time = @time WHERE id=@id;";
            //cmd.Parameters.AddWithValue("@id", item.Id);
            //cmd.Parameters.AddWithValue("@value", item.Value);
            //cmd.Parameters.AddWithValue("time", item.Time);
            //cmd.Prepare();
            //cmd.ExecuteNonQuery();

            #endregion
        }
    }
}
