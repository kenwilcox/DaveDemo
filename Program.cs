using System;
using System.Text;
using Microsoft.Data.Sqlite;

namespace ConsoleApplication
{
    public class Program
    {
        private static string BuildBatchInsert(int numberOfRows) 
        {
            var template = @"insert into Users (Username, Email, Password) values ('{0}', '{1}', '{2}');";
            var sb = new StringBuilder(template.Length + numberOfRows);
            for(var i = 0; i < numberOfRows; i++)
            {
                sb.Append(string.Format(template, "admin" +i.ToString(), 
                    "test" + i.ToString() + "@example.com", "password" + i.ToString()));
            }
            return sb.ToString();
        }

        private static void BuildDatabase()
        {
            var conStr = "FileName=data.db";
            var con = new SqliteConnection(conStr);
            con.Open();

            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = @"
                drop table if exists Users; 
                create table if not exists Users (Id INTEGER PRIMARY KEY, Username, Email, Password);";
                cmd.ExecuteNonQuery();

                var query = BuildBatchInsert(100);

                cmd.CommandText = "BEGIN; -- start a transaction";
                cmd.ExecuteNonQuery();
                
                for (var i = 0; i < 100000; i++)
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }

                cmd.CommandText = "COMMIT; -- commit the transaction";
                cmd.ExecuteNonQuery();

                // select it back out
                //         cmd.CommandText = "select * from Users";
                //         var result = cmd.ExecuteReader();
                //         while (result.Read())
                //         {
                //             Console.WriteLine("username: {0}", result.GetString(1));
                //             Console.WriteLine("Email: {0}", result.GetString(2));
                //         }
                // just dump the count
                
                cmd.CommandText = "SELECT count(*) FROM Users";
                var count = cmd.ExecuteScalar();
                Console.WriteLine("{0:n0}", count);
                con.Close();
            }
        }

        private static void TimeThis(Action action)
        {
            var start = DateTime.Now;
            action();
            var end = DateTime.Now;
            var total = end - start;
            Console.WriteLine(total);
        }

        public static void Main(string[] args)
        {
            // < 5 minutes to create a new table and insert 10,000,000 rows
            TimeThis(BuildDatabase);
        }
    }
}
