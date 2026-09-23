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

        [HttpGet("byid")]
        public object GetSportolokById(int id)
        {
            var conn = new MySqlConnection(connStr);

            conn.Open();

            var sql = $"SELECT `name`, `email` FROM `sportolo` WHERE `id` = @id";

            var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", id);

            var dataReader = cmd.ExecuteReader();

            dataReader.Read();

            var sportolo = new
            {
                Name = dataReader.GetString(0),
                Email = dataReader.GetString(1)
            };

            conn.Close();

            return sportolo;
        }
    }
}
