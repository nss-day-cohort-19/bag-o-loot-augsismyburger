using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace BagOLoot
{
    public class ToyBag
    {
        
        List<(string, string)> _orderReport = new List<(string, string)>();
        private string _connectionString = $"Data Source={Environment.GetEnvironmentVariable("BAGOLOOT_DB")}";
        private SqliteConnection _connection;
        public ToyBag()
        {
            _connection = new SqliteConnection(_connectionString);
        }
        // private string _newToy;

        public bool AddToyToBag(int ChildId, string toyName)
        {
            int _lastId = 0; // Will store the id of the last inserted record
            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Insert the new child
                dbcmd.CommandText = $"insert into ToyBag values (null, '{toyName}', '{ChildId}')";
                Console.WriteLine(dbcmd.CommandText);
                dbcmd.ExecuteNonQuery ();

                // Get the id of the new row
                dbcmd.CommandText = $"select last_insert_rowid()";
                using (SqliteDataReader dr = dbcmd.ExecuteReader()) 
                {
                    if (dr.Read()) {
                        _lastId = dr.GetInt32(0);
                    } else {
                        throw new Exception("Unable to insert value");
                    }
                }

                // clean up
                dbcmd.Dispose ();
                _connection.Close ();
            }

            return _lastId != 0;
        }
        public Dictionary<int, string> GetChildToys(int ChildId)
        {
            Dictionary<int, string> _toys = new Dictionary<int, string>();
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Get toy list by ChildId
                dbcmd.CommandText = $"select t.Name, t.ToyId from ToyBag t, Child c where c.ChildId = t.ChildId and c.ChildId = {ChildId}";
                using(SqliteDataReader dr = dbcmd.ExecuteReader())
                {
                    // read each row in the result set
                    while (dr.Read())
                    {

                        _toys.Add(dr.GetInt32(1), dr[0].ToString()); 
                    }
                }

                // clean up
                dbcmd.Dispose();
                _connection.Close();

            }
            return _toys;
        }
        public bool RevokeToyFromBag(int ToyId)
        {
            bool isDeleted = false;

            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand ();
                try
                {
                    dbcmd.CommandText = $"DELETE from ToyBag where ToyId = {ToyId}";

                    dbcmd.ExecuteNonQuery();
                    // clean up
                    dbcmd.Dispose();
                    isDeleted = true;
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex)
                {
                    Console.WriteLine("Cannot be deleted:" + ex.Message);
                }
                _connection.Close();
            }
            return isDeleted;
        }
        public List<(string, string)> ListChildrenWithToys()
        {
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Get toy list by ChildId
                dbcmd.CommandText = $"select c.Name, t.Name from Child c, ToyBag t where c.ChildId = t.ChildId";
                using(SqliteDataReader dr = dbcmd.ExecuteReader())
                {
                    // read each row in the result set
                    while (dr.Read())
                    {

                        _orderReport.Add((dr[0].ToString(), dr[1].ToString())); 
                    }
                }

                // clean up
                dbcmd.Dispose();
                _connection.Close();

            }
            return _orderReport;
        }
    }
}