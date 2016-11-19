using consumeTracker.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
//using System.Data;

namespace consumeTracker.Services
{
    public class PostGreSqlService
    {
        //private NpgsqlConnection postGresConnection;
        //private NpgsqlConnectionStringBuilder connectionSettings;

        public event ConnectionStateStatus ConnectionStatus;
        public int status = SQLStates.DISCONNECTED;        
        public delegate void ConnectionStateStatus(int status);

        public void Connect(string username, string password)
        {
            //using (var conn = new NpgsqlConnection("Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase"))
            //{
            //    //conn.Open();
            //    //using (var cmd = new NpgsqlCommand())
            //    //{
            //    //    cmd.Connection = conn;

            //    //    // Insert some data
            //    //    cmd.CommandText = "INSERT INTO data (some_field) VALUES ('Hello world')";
            //    //    cmd.ExecuteNonQuery();

            //    //    // Retrieve all rows
            //    //    cmd.CommandText = "SELECT some_field FROM data";
            //    //    using (var reader = cmd.ExecuteReader())
            //    //    {
            //    //        while (reader.Read())
            //    //        {
            //    //            Console.WriteLine(reader.GetString(0));
            //    //        }
            //    //    }
            //    //}
            //}









            //connectionSettings = new NpgsqlConnectionStringBuilder();
            //connectionSettings.Host = URL;
            //connectionSettings.Username = username;
            //connectionSettings.Password = password;
            //connectionSettings.Database = DB_NAME;
            //postGresConnection = new NpgsqlConnection("Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase");
            //postGresConnection.StateChange += OnConnectionStateChanged;
            //postGresConnection.Open();


        }

        //private void OnConnectionStateChanged(object sender, StateChangeEventArgs e)
        //{         
        //    if (postGresConnection.FullState == ConnectionState.Closed)
        //    {
        //        ConnectionStatus(SQLStates.DISCONNECTED);
        //    }
        //    if (postGresConnection.FullState == ConnectionState.Connecting)
        //    {
        //        ConnectionStatus(SQLStates.CONNECTING);
        //    }
        //    if (postGresConnection.FullState == ConnectionState.Open)
        //    {
        //        ConnectionStatus(SQLStates.CONNECTED);
        //    }
        //    if (postGresConnection.FullState == ConnectionState.Executing)
        //    {
        //        ConnectionStatus(SQLStates.EXECUTING);
        //    }
        //    if (postGresConnection.FullState == ConnectionState.Fetching)
        //    {
        //        ConnectionStatus(SQLStates.FETCHING);
        //    }
        //    if (postGresConnection.FullState == ConnectionState.Broken)
        //    {
        //        ConnectionStatus(SQLStates.BROKEN);
        //    }
        //}

        public void Disconnect()
        {
            //if (postGresConnection != null)
            //{
            //    postGresConnection.Close();
            //}
        }

        public bool GetConnectionStatus()
        {
            //if (postGresConnection == null)
            //{
            //    return false;
            //}
            //else
            //{                
            //    if (postGresConnection.FullState == System.Data.ConnectionState.Open)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}

            return true;

        }

        public void CreateData(ConsumeItem item)
        {
            //using (var cmd = postGresConnection.CreateCommand())
            //{
            //    cmd.Parameters.Add(new NpgsqlParameter("id", item.Id));
            //    cmd.Parameters.Add(new NpgsqlParameter("date", item.Date));
            //    cmd.Parameters.Add(new NpgsqlParameter("store", item.Store));
            //    cmd.Parameters.Add(new NpgsqlParameter("amount", item.Amount));
            //    cmd.Parameters.Add(new NpgsqlParameter("linenumber", item.LineNumber));

            //    cmd.CommandText = "INSERT INTO consume (id, date, store, amount, linenumber ) values(:id, :date, :store, :amount, :linenumber);";
            //    cmd.ExecuteNonQuery();
            //}
        }

        public List<ConsumeItem> GetData()
        {
            List<ConsumeItem> consumeList = new List<ConsumeItem>();

            //using (var cmd = postGresConnection.CreateCommand())
            //{
            //    cmd.CommandText = "SELECT * FROM consume;";
            //    using (var rdr = cmd.ExecuteReader())
            //    {
            //        while (rdr.Read())
            //        {
            //            ConsumeItem consumeItem = new ConsumeItem();
            //            consumeItem.Id = (decimal)rdr["id"];
            //            consumeItem.Date = (DateTime)rdr["date"];
            //            consumeItem.Amount = (decimal)rdr["amount"];
            //            consumeItem.Store = rdr["store"].ToString();
            //            consumeItem.LineNumber = (decimal)rdr["linenumber"];
            //            consumeList.Add(consumeItem);

            //            //Console.WriteLine("Field Name;Pg Type Name;DbType;NpgsqlDbType;Type;Value");

            //            //var values = rdr.GetValues(null);
            //            //for (var field = 0; field < values.; field++)
            //            //{
            //            //    Console.WriteLine("{0};{1};{2};{3};{4};{5}", rdr.GetName(field), rdr.GetDataTypeName(field), rdr.GetFieldDbType(field), rdr.GetFieldNpgsqlDbType(field), values[field].GetType(), values[field]);
            //            //}
            //            //Console.WriteLine();
            //        }
            //    }
            //}

            return consumeList;
        }

        public void UpdateData(ConsumeItem item)
        {
            //using (var cmd = postGresConnection.CreateCommand())
            //{
            //    cmd.Parameters.Add(new NpgsqlParameter("id", item.Id));
            //    cmd.Parameters.Add(new NpgsqlParameter("date", item.Date));
            //    cmd.Parameters.Add(new NpgsqlParameter("amount", item.Amount));
            //    cmd.Parameters.Add(new NpgsqlParameter("store", item.Store));
            //    cmd.Parameters.Add(new NpgsqlParameter("linenumber", item.LineNumber));

            //    cmd.CommandText = "UPDATE consume SET date= :date, amount= :amount, store= :store, linenumber= :linenumber WHERE id= :id;";
            //    cmd.ExecuteNonQuery();
            //}
        }

        public void DeleteData(ConsumeItem item)
        {
            //using (var cmd = postGresConnection.CreateCommand())
            //{
            //    cmd.Parameters.Add(new NpgsqlParameter("id", item.Id));
            //    //cmd.Parameters.Add(new NpgsqlParameter("date", item.Date));
            //    cmd.Parameters.Add(new NpgsqlParameter("amount", item.Amount));
            //    //cmd.Parameters.Add(new NpgsqlParameter("store", item.Store));

            //    cmd.CommandText = "DELETE FROM consume WHERE id= :id";
            //    cmd.ExecuteNonQuery();
            //}
        }

    }
}
