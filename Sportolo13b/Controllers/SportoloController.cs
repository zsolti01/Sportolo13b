using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportolo13b.Models;
using MySqlConnector;

namespace Sportolo13b.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportoloController : ControllerBase
    {
        private readonly string connStr = "server=localhost;database=sportolo13b;uid=root;password=";

        [HttpGet]
        public List<Sportolo> GetSportolok()
        {
            List<Sportolo> sportolok = new();

            var conn = new MySqlConnection(connStr);

            conn.Open();

            var sql = "SELECT * FROM `sportolo`";

            var cmd = new MySqlCommand(sql, conn);

            var dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                var sportolo = new Sportolo
                {
                    id = dataReader.GetInt32(0),
                    name = dataReader.GetString(1),
                    email = dataReader.GetString(2),
                    age = dataReader.GetInt32(3),
                    password = dataReader.GetString(4),
                    registrationTime = dataReader.GetDateTime(5)
                };
                sportolok.Add(sportolo);
            }

            conn.Close();

            return sportolok;
        }
    }
}
