using MetricsManager.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;
using Dapper;
using AutoMapper;
using MetricsManager.Models.DTO;

namespace MetricsManager.Services.Implimintation
{
    public class AgentsRepository : IAgentsRepository
    {
        #region Services:

        private readonly IOptions<DataBaseOptions> _dataBaseOptions;
        private readonly IMapper _mapper;

        #endregion 

        public AgentsRepository(
            IOptions<DataBaseOptions> dataBaseOptions, 
            IMapper mapper)
        {
            _dataBaseOptions = dataBaseOptions;
            _mapper = mapper;
        }

        public void Add(AgentInfoDto item)
        {
            using var connection = new SQLiteConnection(_dataBaseOptions.Value.ConnectionString);
            connection.Open();
            connection.Execute("INSERT INTO agents(agentUri, enable) VALUES(@agentUri, @enable);", new
            {
                agentUri = item.AgentUri, 
                enable = item.Enable
            }); 
        }

        public void Disable(int id)
        {
            using var connection = new SQLiteConnection(_dataBaseOptions.Value.ConnectionString);
            connection.Open();
            connection.Execute("UPDATE agents SET enable=@enable WHERE agentId=@id;", new
            {
                enable = false
            });
        }

        public void Enable(int id)
        {
            using var connection = new SQLiteConnection(_dataBaseOptions.Value.ConnectionString);
            connection.Open();
            connection.Execute("UPDATE agents SET enable=@enable WHERE agentId=@id;", new
            {
                enable = true
            }); 
        }

        public AgentInfoDto[] GetAll()
        {
            using var connection = new SQLiteConnection(_dataBaseOptions.Value.ConnectionString);
            connection.Open(); 

            return connection.Query<AgentInfoDto>("SELECT * FROM agents").ToArray();
        }

        public AgentInfoDto GetById(int id)
        {
            using var connection = new SQLiteConnection(_dataBaseOptions.Value.ConnectionString);
            connection.Open();

            AgentInfoDto agentInfo = connection.QuerySingle<AgentInfoDto>("SELECT * FROM agents WHERE agentId = @agentId;", new
            {
                agentId = id
            });
            return agentInfo;

        }

        public void Remove(int id)
        {
            using var connection = new SQLiteConnection(_dataBaseOptions.Value.ConnectionString);
            connection.Open();
            connection.Execute("DELETE FROM agents WHERE agentId=@id;", new
            {
                agentId = id
            }); 
        }

        public void Update(AgentInfoDto item)
        {
            using var connection = new SQLiteConnection(_dataBaseOptions.Value.ConnectionString);
            connection.Open();
            connection.Execute("UPDATE agents SET agentUri=@agentUri, enable=@enable WHERE agentId=@agentId;", new
            {
                agentUri = item.AgentUri, 
                enable = item.Enable, 
                agentId = item.AgentId
            });
        }
    }
}
