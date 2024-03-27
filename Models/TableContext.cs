using MySql.Data.MySqlClient;

namespace MySqlTest.Models
{
    public class TableContext
    {
        public string ConnectionString { get; set; }

        public TableContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Table> GetAllInfo()
        {
            List<Table> list = new List<Table>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Test", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Table()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString() ?? ""
                        });
                    }
                }
            }
            return list;
        }
    }
}
