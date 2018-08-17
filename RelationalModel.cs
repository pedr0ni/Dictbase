using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace ConsoleApp1
{
    /// <summary>
    /// ORM Class for MySQL
    /// Author: M. Pedroni [pedr0ni@icloud.com]
    /// License: https://www.apache.org/licenses/LICENSE-2.0
    /// Version: 1.0.0
    /// </summary>
    public class RelationalModel
    {

        private MySqlConnection con;
        public string table;

        public Boolean timestamps;
        public List<string> fields;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="con">MySQLConnection</param>
        public RelationalModel(MySqlConnection con)
        {
            this.con = con;
            this.timestamps = true;
            this.table = this.GetType().Name.ToLower() + "s";
            this.fields = new List<string>();
            this.loadFields();
        }

        /// <summary>
        /// Load fields from MySQL Table
        /// </summary>
        private void loadFields()
        {
            MySqlCommand cmd = new MySqlCommand("SHOW COLUMNS FROM " + this.table, this.con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                this.fields.Add((string)reader["Field"]);
            }
            reader.Close();
        }

        /// <summary>
        /// Select MySQL Data from table where condition 
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns>A list of string, object Dictionary</returns>
        public List<Dictionary<string, object>> where(Dictionary<string, string> conditions)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            string query = "SELECT * FROM " + this.table + " WHERE ";
            foreach (var condition in conditions)
            {
                query += condition.Key + " = '" + condition.Value + "',";
            }
            query = query.Substring(0, query.Length - 1); // Remove last ',' char
            MySqlCommand cmd = new MySqlCommand(query, this.con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                foreach (string field in this.fields)
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    row.Add(field, reader[field]);
                    result.Add(row);
                }
            }
            reader.Close();
            return result;
        }

        /// <summary>
        /// Find ONE row in MySQL table
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A string, object Dictionary</returns>
        public Dictionary<string, object> find(int id)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM " + this.table + " WHERE id = " + id, this.con);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                foreach (string field in this.fields)
                {
                    result.Add(field, reader[field]);
                }
            }
            reader.Close();
            return result;
        }

        /// <summary>
        /// Return only fields on table
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="conditions"></param>
        /// <returns>A List of string, object Dictionary</returns>
        public List<Dictionary<string, object>> only(string[] fields, Dictionary<string, string> conditions = null)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            string query = "SELECT ";
            foreach (string field in fields)
            {
                query += field + ",";
            }
            query = query.Substring(0, query.Length - 1) + " FROM " + this.table; // Remove last ',' char
            if (conditions != null)
            {
                query += " WHERE ";
                foreach (var condition in conditions)
                {
                    query += condition.Key + " = '" + condition.Value + "',";
                }
                query = query.Substring(0, query.Length - 1); // Remove last ',' char
            }

            MySqlCommand cmd = new MySqlCommand(query, this.con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                foreach (string field in fields)
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    row.Add(field, reader[field]);
                    result.Add(row);
                }
            }
            reader.Close();
            return result;
        }

        /// <summary>
        /// Insert value in table
        /// </summary>
        /// <param name="data"></param>
        public void insert(Dictionary<string, object> values)
        {
            string query = "INSERT INTO " + this.table + " (";
            foreach (var value in values)
            {
                query += value.Key + ",";
            }
            query = query.Substring(0, query.Length - 1) + ") VALUES ("; // Remove last ',' char
            foreach (var value in values)
            {
                query += "'" + value.Value + "',";
            }
            query = query.Substring(0, query.Length - 1) + ")"; // Remove last ',' char
            new MySqlCommand(query, this.con).ExecuteNonQuery();
        }

    }
}
