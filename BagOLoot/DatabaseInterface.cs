using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using System.Collections;

namespace BagOLoot
{
    public class DatabaseInterface
    {
        private string _connectionString = $"Data Source={Environment.GetEnvironmentVariable("BAGOLOOT_DB")}";
        private SqliteConnection _connection;

        public DatabaseInterface()
        {
            _connection = new SqliteConnection(_connectionString);
        }

        public void CheckForChildTable ()
        {
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Query the child table to see if table is created
                dbcmd.CommandText = $"select ChildId from Child";

                try
                {
                    // Try to run the query. If it throws an exception, create the table
                    using (SqliteDataReader reader = dbcmd.ExecuteReader())
                    {
                        
                    }
                    dbcmd.Dispose ();
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.Contains("no such table"))
                    {
                        dbcmd.CommandText = $@"create table Child (
                            `ChildId`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                            `Name`	varchar(80) not null, 
                            `Delivered` integer not null default 0
                        )";
                        dbcmd.ExecuteNonQuery ();
                        dbcmd.Dispose ();
                    }
                }
                _connection.Close ();
            }
        }
        public void CheckForToyTable ()
        {
            using(_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Query Toy Table to See iF it has been created
                dbcmd.CommandText = $"select ToyId from ToyBag";
                try
                {
                    // Try to run the query. If it throws an exception, create the table
                    using (SqliteDataReader reader = dbcmd.ExecuteReader())
                    {
                        
                    }
                    dbcmd.Dispose ();
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.Contains("no such table"))
                    {
                        dbcmd.CommandText = $@"create table ToyBag (
                            `ToyId`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                            `Name`	varchar(80) not null,
                            `ChildId` integer not null,
                            FOREIGN KEY (`ChildId`) REFERENCES `Child`(`ChildId`)
                        )";
                        dbcmd.ExecuteNonQuery ();
                        dbcmd.Dispose ();
                    }
                }
                _connection.Close ();
            }
        }
    }
}