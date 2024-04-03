using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using MySql.Data.MySqlClient;
using MySqlTest.Models.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MySqlTest.Models
{
    public class TableContext : ITableContext
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

        public bool InsertRow(Table table)
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {

                    string sql = $"INSERT INTO Test (Name) VALUES ('{table.Name}')";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Table SearchTable(int id)
        {
            Table srch = null;
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Test WHERE Id = {id}", conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            srch = new Table()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString() ?? ""
                            };
                        }
                    }
                }
            }
            catch (Exception ex) { }

            return srch;
        }
        public bool EditRow(Table table)
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {

                    string sql = $"UPDATE Test SET Name = '{table.Name}' WHERE Id = {table.Id}";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
