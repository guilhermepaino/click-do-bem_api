using System;

namespace SantaHelena.ClickDoBem.Infra.CrossCutting.Common.EnvironmentConfigs
{

    public static class Database
    {
        public static string MysqlServer => Environment.GetEnvironmentVariable("MYSQL_SERVER");
        public static string MysqlDatabase => Environment.GetEnvironmentVariable("MYSQL_DATABASE");
        public static string MysqlUser => Environment.GetEnvironmentVariable("MYSQL_USER");
        public static string MysqlPwd => Environment.GetEnvironmentVariable("MYSQL_PASSWORD");

        public static string MySqlConnectionString => $"Server={MysqlServer};database={MysqlDatabase};uid={MysqlUser};pwd={MysqlPwd};";
    }

}
