using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpLogging;
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

            string sql = $"SELECT * FROM `eredmeny`";

            var cmd = new MySqlCommand(sql, conn);

            var dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                var eredmeny = new Eredmeny
                {
                    Id = dataReader.GetInt32(0),
                    Competition = dataReader.GetString(1),
                    Description = dataReader.GetString(2),
                    ResultTime = dataReader.GetDateTime(3),
                    UpdateTime = dataReader.GetDateTime(4)
                };
                eredmenyek.Add(eredmeny);
            }

            conn.Close();

            return eredmenyek;
        }

        [HttpGet("byId")]
        public object ReadEredmenyById(int id)
        {
            var conn = new MySqlConnection(connStr);

            conn.Open();

            string sql = $"SELECT `Competition`, `Description` FROM `eredmeny` WHERE `Id` = @id";

            var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", id);

            var dataReader = cmd.ExecuteReader();

            dataReader.Read();

            var eredmeny = new
            {
                Competition = dataReader.GetString(0),
                Description = dataReader.GetString(1)
            };

            conn.Close();

            return eredmeny;
        }

        [HttpGet("bySportolo")]
        public object GetEredmenyekBySportolo(int sportoloId)
        {
            var conn = new MySqlConnection(connStr);

            conn.Open();

            string sql = @"
        SELECT 
            sportolo.name,
            eredmeny.Competition,
            eredmeny.Description
        FROM sportolo
        INNER JOIN eredmeny ON sportolo.id = eredmeny.SportoloId
        WHERE sportolo.id = @sportoloId";

            var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@sportoloId", sportoloId);

            var eredmenyek = new List<object>();

            var dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                eredmenyek.Add(new
                {
                    Name = dataReader.GetString(0),
                    Competition = dataReader.GetString(1),
                    Description = dataReader.GetString(2)
                });
            }

            conn.Close();

            return eredmenyek;
        }

        [HttpGet("count")]
        public object GetEredmenyekCount()
        {
            var conn = new MySqlConnection(connStr);

            conn.Open();

            string sql = "SELECT COUNT(*) FROM eredmeny";

            var cmd = new MySqlCommand(sql, conn);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            conn.Close();

            return new
            {
                EredmenyekSzama = count
            };
        }

        [HttpGet("countBySportolo")]
        public object GetEredmenyekCountBySportolo(int sportoloId)
        {
            var conn = new MySqlConnection(connStr);

            conn.Open();

            string sql = @"
        SELECT COUNT(*)
        FROM eredmeny
        WHERE SportoloId = @sportoloId";

            var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@sportoloId", sportoloId);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            conn.Close();

            return new
            {
                SportoloId = sportoloId,
                EredmenyekSzama = count
            };
        }

        [HttpPost]
        public Eredmeny PostEredmeny(EredmenyDTO eredmeny)
        {
            var conn = new MySqlConnection(connStr);

            conn.Open();

            var score = new Eredmeny
            {
                Competition = eredmeny.Competition,
                Description = eredmeny.Description,
                ResultTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                SportoloId = eredmeny.SportoloId
            };

            var sql = $"INSERT INTO `eredmeny`(`Competition`, `Description`, `ResultTime`, `UpdateTime`, `SportoloId`) VALUES (@competition,@description,@resultTime,@updateTime,@sportoloId)";

            var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@competition", score.Competition);
            cmd.Parameters.AddWithValue("@description", score.Description);
            cmd.Parameters.AddWithValue("@resultTime", score.ResultTime);
            cmd.Parameters.AddWithValue("@updateTime", score.UpdateTime);
            cmd.Parameters.AddWithValue("@sportoloId", score.SportoloId);
            
            cmd.ExecuteNonQuery();

            conn.Close();

            return score;
        }

        [HttpPut]
        public object UpdateEredmeny(int id, EredmenyDTO eredmeny)
        {
            var conn = new MySqlConnection(connStr);

            conn.Open();

            var sql = $"UPDATE `eredmeny` SET `Competition`=@competition,`Description`=@description WHERE `Id` = @id";

            var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@competition", eredmeny.Competition);
            cmd.Parameters.AddWithValue("@description", eredmeny.Description);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            conn.Close();

            return new { message = "Eredmeny sikeresen frissítve." };
        }

        [HttpDelete]
        public object DeleteEredmeny (int id)
        {
            var conn = new MySqlConnection(connStr);

            conn.Open();

            var sql = $"DELETE FROM `eredmeny` WHERE `Id` = @id";

            var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue(@"id", id);

            cmd.ExecuteNonQuery();

            conn.Close();

            return new { message = "Eredmeny sikeresen törölve." };
        }
    }
}
