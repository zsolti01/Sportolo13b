using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Sportolo13b.Models;
using System.Security.Cryptography;

namespace Sportolo13b.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EredmenyController : ControllerBase
    {
        private readonly string connStr = "server=localhost;database=sportolo13b;uid=root;password=";

        [HttpGet]
        public List<Eredmeny> ReadEredmeny()
        {
            List<Eredmeny> eredmenyek = new();

            var conn = new MySqlConnection(connStr);

            conn.Open();

            string sql = $"SELECT * FROM eredmeny";

            var cmd = new MySqlCommand(sql, conn);

            var dataReader = new cmd.ExecuteReader();

            while (dataReader.Read())
            {
                var eredmeny = new Eredmeny
                {
                    Id = dataReader.GetInt32(0),
                    Competition = dataReader.GetString(1),
                    Description = dataReader.GetString(2),
                    ResultTime = dataReader.GetDateTime(3),
                    UpdateTime = dataReader.GetDateTime(4),
                    SportoloId = dataReader.GetInt32(5),
                };
            }

            conn.Close();

            return eredmenyek;
        }
    }
}
