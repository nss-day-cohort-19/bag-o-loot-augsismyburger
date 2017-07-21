using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace BagOLoot
{
   
    public class Child
    {
            public int iter;
            public int childId;
            public string toyName;
        public Child(int i, int c, string t )
        {
            iter = i;
            childId = c;
            toyName = t;
        }
    }
    public class ChildRegister
    {
        public Dictionary<int, string> nameList = new Dictionary<int, string>();
        private Dictionary<int, string> child = new Dictionary<int, string>();
        private string _connectionString = $"Data Source={Environment.GetEnvironmentVariable("BAGOLOOT_DB")}";
        private SqliteConnection _connection;
        public ChildRegister()
        {
            _connection = new SqliteConnection(_connectionString);
        }
        public bool AddChild (string child) 
        {
            int _lastId = 0; // Will store the id of the last inserted record
            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Insert the new child
                dbcmd.CommandText = $"insert into Child values (null, '{child}', 0)";
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
        public Dictionary<int, string> GetChildren ()
        {
            Dictionary<int, string> _children = new Dictionary<int, string>();
            using(_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();
                // Select the id and name of every child
                dbcmd.CommandText = "select ChildId, Name from Child";

                using(SqliteDataReader dr = dbcmd.ExecuteReader())
                {
                    // read each row in the result set
                    while (dr.Read())
                    {
                        _children.Add(dr.GetInt32(0), dr[1].ToString()); // Add child name to list
                    }
                }
                // clean up
                dbcmd.Dispose();
                _connection.Close();
            }
            return _children;
        }
        public Dictionary<int, string> GetChild (string name)
        {
            // var child = _children.SingleOrDefault(c => c == name);
            using(_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();
                // Select the id and name of every child
                dbcmd.CommandText = $"select ChildId, Name from Child where Name = {name}";

                using(SqliteDataReader dr = dbcmd.ExecuteReader())
                {
                    // read each row in the result set
                    while (dr.Read())
                    {
                        child.Add(dr.GetInt32(0), dr[1].ToString()); 
                    }
                }
                // clean up
                dbcmd.Dispose();
                _connection.Close();
            }
            // Inevitably, two children will have the same name. Then what?
            return child;
        }
        public string DeliverChildToys(int ChildId)
        {
            using(_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                dbcmd.CommandText = $"UPDATE Child SET Delivered=1 WHERE ChildId = {ChildId}";
                dbcmd.ExecuteNonQuery();
                // clean up
                dbcmd.Dispose();
                _connection.Close();
            }

            return $"Toys have been delivered";
        }
        public Dictionary<int, bool> GetDeleveryStatus()
        {
            Dictionary<int, bool> deliveredList = new Dictionary<int, bool>();
            bool isDelivered;
            using(_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                dbcmd.CommandText = $"SELECT ChildId, Delivered FROM Child";
                using(SqliteDataReader dr = dbcmd.ExecuteReader())
                {
                    // read each row in the result set
                    while (dr.Read())
                    {
                        var isDeliveredHolder = dr.GetInt32(1);
                        if(isDeliveredHolder == 0)
                        {
                           isDelivered = false;
                        }else
                        {
                            isDelivered = true;
                        }
                        deliveredList.Add(dr.GetInt32(0), isDelivered); 
                    }
                }
                // clean up
                dbcmd.Dispose();
                _connection.Close();
            }
            return deliveredList;
        }
    }
}