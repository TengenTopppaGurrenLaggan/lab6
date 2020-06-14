using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Remoting.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;



namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=DESKTOP-2QF0L78\LENOVO;Initial Catalog=lab6_hritsaienko;Integrated Security=True";
            string sqlExpression;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu");
                Console.WriteLine("1. Display tables.");// SqlDataReader, SqlDataAdapter.DataSet
                Console.WriteLine("2. Add element to the Depart table.");
                Console.WriteLine("3. Delete added item from the Depart table.");
                Console.WriteLine("4. Change the position Admin to Admin+ (Update Emplo table).");
                Console.WriteLine("5. Count the number of projects and display the minimum project price");
                Console.WriteLine("6. Display the project with the maximum cost");
                Console.WriteLine("7. Display the numbers of worker for each Depart");
                Console.WriteLine("8. Display the license on product for some customer");
                Console.WriteLine("9. Display project emloyers");
                Console.WriteLine("10. Display all worker with pos=run_boy sorted by name");

                string ch = Console.ReadLine();
                switch (ch)
                {
                    case "1":
                        Console.WriteLine("--------------Customers-----------------");
                        sqlExpression = "SELECT * FROM Cust";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {


                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlExpression, connection);
                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.HasRows) // если есть данные
                            {
                                // выводим названия столбцов
                                Console.WriteLine("{0}\t{1}\t\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));

                                while (reader.Read()) // построчно считываем данные
                                {
                                    object id = reader.GetValue(0);
                                    object name = reader.GetValue(1);
                                    object froms = reader.GetValue(2);

                                    Console.WriteLine("{0} \t{1} \t\t{2}", id, name, froms);
                                }
                            }

                            reader.Close();
                          
                        Console.WriteLine("\nDepartment");
                        sqlExpression = "SELECT * FROM Depart";
                            command = new SqlCommand(sqlExpression, connection);
                            reader = command.ExecuteReader();

                            if (reader.HasRows) // если есть данные
                            {
                                // выводим названия столбцов
                                Console.WriteLine("{0}\t\t{1}\t", reader.GetName(0), reader.GetName(1));

                                while (reader.Read()) // построчно считываем данные
                                {
                                    object id = reader.GetValue(0);
                                    object name = reader.GetValue(1);


                                    Console.WriteLine("{0} \t\t{1}", id, name);
                                }
                            }
                            
                            reader.Close();
                          
                        Console.WriteLine("\n--------------Employees-----------------");
                        sqlExpression = "SELECT * FROM Emplo";
                       
                           // connection.Open();
                            command = new SqlCommand(sqlExpression, connection);
                            reader = command.ExecuteReader();

                            if (reader.HasRows) // если есть данные
                            {
                                // выводим названия столбцов
                                Console.WriteLine("{0}\t{1}\t{2}\t\t{3}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3));
                               
                                while (reader.Read()) // построчно считываем данные
                                {
                                    object id = reader.GetValue(0);
                                    object DepartmentId = reader.GetValue(1);
                                    object name = reader.GetValue(2);
                                    object pos = reader.GetValue(3);

                                    
                                    Console.WriteLine("{0}\t{1}\t\t{2}\t\t{3}", id, DepartmentId, name, pos);
                                    
                                }
                                
                            }

                            reader.Close();
                           
                            Console.WriteLine("\n--------------Softwares-----------------");
                            sqlExpression = "SELECT * FROM Soft";
                          
                            // Создаем объект DataAdapter
                            SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, connection);
                            // Создаем объект Dataset
                            DataSet ds = new DataSet();
                            // Заполняем Dataset
                            adapter.Fill(ds);
                            // Отображаем данные

                            //Console.WriteLine(DataSource);
                            
                            foreach (DataRow pRow in ds.Tables[0].Rows)
                            {
                                
                                Console.WriteLine("{0}  {1}\t{2}\t{3}\t{4}\t{5}", pRow["id"], pRow["name"], pRow["Price"], pRow["doc"], pRow["Distribution"],pRow["userule"]);
                                
                            }
                            Console.WriteLine("\n--------------Personal-----------------");
                            sqlExpression = "SELECT * From Personal";
                            // using (SqlConnection connection = new SqlConnection(connectionString))
                            // {
                            //connection.Open();
                            // Создаем объект DataAdapter
                            adapter = new SqlDataAdapter(sqlExpression, connection);
                            // Создаем объект Dataset
                            ds = new DataSet();
                            // Заполняем Dataset
                            adapter.Fill(ds);
                            // Отображаем данные
                            
                            //Console.WriteLine(DataSource);
                            foreach (DataRow pRow in ds.Tables[0].Rows)
                            {
                                Console.WriteLine("{0}\t{1}\t{2}",pRow["id"], pRow["id_Empl"], pRow["id_Soft"]);

                            }
                          
                            Console.WriteLine("\n--------------Licenses-----------------");
                            sqlExpression = "SELECT * FROM Lic";

                            // Создаем объект DataAdapter
                            adapter = new SqlDataAdapter(sqlExpression, connection);
                            // Создаем объект Dataset
                            ds = new DataSet();
                            // Заполняем Dataset
                            adapter.Fill(ds);
                            // Отображаем данные

                            
                            
                            foreach (DataRow pRow in ds.Tables[0].Rows)
                            {
                               
                                    Console.WriteLine("{0}  {1}\t{2}\t{3}\t{4}\t{5}", pRow["Id"], pRow["idSoft"], pRow["id_cust"], Convert.ToDateTime(pRow["sale_date"]).ToShortDateString(), pRow["Price"], Convert.ToDateTime(pRow["Upg_date"]));
                             
                            }
                          
                            connection.Close();

                        }
                        Console.Read();
                        Console.Clear();
                        break;
                    case "2":
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            DataTable schema = connection.GetSchema("Tables");
                            List<string> DataNames = new List<string>();
                            int i = 1;
                            Console.WriteLine("SQL tales");
                            foreach (DataRow row in schema.Rows)
                            {
                                DataNames.Add(row[2].ToString());
                                Console.WriteLine("{0}.{1}", i, DataNames[i-1]);
                                i++;
                            }
                            Console.WriteLine("Enter name of table");
                            connection.Close();
                        }
                        int z = 0;
                        string cname = Console.ReadLine();
                        List<string> TableNames = new List<string>();
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            sqlExpression = "SELECT * FROM "+cname;
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlExpression, connection);
                            SqlDataReader reader = command.ExecuteReader();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                TableNames.Add(reader.GetName(i).ToString());
                                //Console.WriteLine("{0}: {1}", i, reader.GetName(i));
                                z = i+1;
                            }

                            reader.Close();
                            connection.Close();
                        }
                            sqlExpression = "INSERT"+cname+"VALUES ('"+ "')";
                        Console.WriteLine("Choose the metod of adding");
                        Console.WriteLine("1.SqlCommand");
                        Console.WriteLine("2.SqlCommandBuilder,DataSet");
                        string ch2 = Console.ReadLine();
                        

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            if (ch2 == "1")
                            {
                                sqlExpression = "INSERT [" + cname + "] VALUES (" ;
                                Console.WriteLine("write: {0}", TableNames[1]);
                                cname = Console.ReadLine();
                                sqlExpression = sqlExpression + "'" + cname + "'";
                                for (int t = 2; t < z; t++)
                                {
                                    Console.WriteLine("write: {0}", TableNames[t]);
                                    cname = Console.ReadLine();
                                    sqlExpression = sqlExpression +",'" +cname + "'";
                                }
                                sqlExpression = sqlExpression + ")";
                                SqlCommand command = new SqlCommand(sqlExpression, connection);
                                int number = command.ExecuteNonQuery();
                                Console.WriteLine("Added object: {0}", number);
                            }
                            else if (ch2 =="2")
                            {
                                sqlExpression = "SELECT * FROM [" + cname+"]";
                                SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, connection);
                                DataSet ds = new DataSet();
                                adapter.Fill(ds);

                                DataTable dt = ds.Tables[0];
                                // добавим новую строку
                                DataRow newRow = dt.NewRow();
                                for (int t = 1; t< z; t++) {
                                    Console.WriteLine("write: {0}", TableNames[t]);
                                    cname = Console.ReadLine();
                                    newRow[TableNames[t]] = cname;
                                }
                                dt.Rows.Add(newRow);

                                // создаем объект SqlCommandBuilder
                                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                                //adapter.Update(ds);
                                // альтернативный способ - обновление только одной таблицы
                                //adapter.Update(dt);
                                // заново получаем данные из бд
                                // очищаем полностью DataSet
                                ds.Clear();
                                // перезагружаем данные
                                adapter.Fill(ds);
                                foreach (DataColumn column in dt.Columns)
                                    Console.Write("\t{0}", column.ColumnName);
                                Console.WriteLine();
                                // перебор всех строк таблицы
                                foreach (DataRow row in dt.Rows)
                                {
                                    // получаем все ячейки строки
                                    var cells = row.ItemArray;
                                    foreach (object cell in cells)
                                        Console.Write("\t{0}", cell);
                                    Console.WriteLine();
                                }
                                connection.Close();
                            }
                            connection.Close();
                        }
                        Console.Read();
                        Console.Clear();
                        break;
                    case "3":
                        TableNames = new List<string>();
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            DataTable schema = connection.GetSchema("Tables");
                            
                            int i = 1;
                            Console.WriteLine("SQL tales");
                            foreach (DataRow row in schema.Rows)
                            {
                                TableNames.Add(row[2].ToString());
                                Console.WriteLine("{0}.{1}", i, TableNames[i - 1]);
                                i++;
                            }
                            Console.WriteLine("Enter name of table");
                            connection.Close();
                        }
                        Console.WriteLine("enter table from which you wanna delete data");
                        string caz=Console.ReadLine();
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            sqlExpression = "SELECT * FROM " + caz;
                            //sqlExpression = "SELECT * FROM Emplo";
                            SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, connection);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            DataTable dt = ds.Tables[0];
                            foreach (DataColumn column in dt.Columns)
                                Console.Write("\t{0}", column.ColumnName);
                            Console.WriteLine();
                            // перебор всех строк таблицы
                            foreach (DataRow row in dt.Rows)
                            {
                                // получаем все ячейки строки
                                var cells = row.ItemArray;
                                foreach (object cell in cells)
                                    Console.Write("\t{0}", cell);
                                Console.WriteLine();
                            }
                            Console.WriteLine("enter id");
                            string b = Console.ReadLine();
                            sqlExpression = "DELETE  FROM [" + caz + "] where ([id]='" + b + "')";
                            //Delete from depart where id = 9
                            SqlCommand command = new SqlCommand(sqlExpression, connection);
                            try
                            {
                                //int number = 
                                command.ExecuteNonQuery();
                                Console.WriteLine("Deleted object:");
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine("Message ={0}", ex.Message);
                                Console.WriteLine("Deleted chained to allow delett");
                            }

                            sqlExpression = "SELECT * FROM "+caz;
                            //sqlExpression = "SELECT * FROM Emplo";
                            adapter = new SqlDataAdapter(sqlExpression, connection);
                            ds = new DataSet();
                            adapter.Fill(ds);
                            dt = ds.Tables[0];
                            foreach (DataColumn column in dt.Columns)
                                Console.Write("\t{0}", column.ColumnName);
                            Console.WriteLine();
                            // перебор всех строк таблицы
                            foreach (DataRow row in dt.Rows)
                            {
                                // получаем все ячейки строки
                                var cells = row.ItemArray;
                                foreach (object cell in cells)
                                    Console.Write("\t{0}", cell);
                                Console.WriteLine();
                            }
                            connection.Close();
                            Console.Read();
                            Console.Clear();
                        }
                        break;
                    case "4":
                        TableNames = new List<string>();
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            DataTable schema = connection.GetSchema("Tables");
                            int i = 1;
                            Console.WriteLine("SQL tables");
                            foreach (DataRow row in schema.Rows)
                            {
                                TableNames.Add(row[2].ToString());
                                Console.WriteLine("{0}.{1}", i, TableNames[i - 1]);
                                i++;
                            }
                            Console.WriteLine("Enter name of table to update");
                            connection.Close();
                        }
                        Console.WriteLine("enter table from which you update");
                        caz = Console.ReadLine();
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            sqlExpression = "SELECT * FROM " + caz;
                            //sqlExpression = "SELECT * FROM Emplo";
                            SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, connection);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            DataTable dt = ds.Tables[0];
                            foreach (DataColumn column in dt.Columns)
                                Console.Write("{0}\n", column.ColumnName);
                            Console.WriteLine();
                        
                        }
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            sqlExpression = "SELECT * FROM " + caz;
                            //sqlExpression = "SELECT * FROM Emplo";
                            SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, connection);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            DataTable dt = ds.Tables[0];
                            foreach (DataColumn column in dt.Columns)
                                Console.Write("\t{0}", column.ColumnName);
                            Console.WriteLine();
                            // перебор всех строк таблицы
                            foreach (DataRow row in dt.Rows)
                            {
                                // получаем все ячейки строки
                                var cells = row.ItemArray;
                                foreach (object cell in cells)
                                    Console.Write("\t{0}", cell);
                                Console.WriteLine();
                            }
                        }
                        Console.Write("what colum u wanna update\n");
                        cname= Console.ReadLine();
                        sqlExpression = "UPDATE ["+caz+"] SET ["+cname+"]=";
                        
                            Console.Write("enter id of record\n");
                        cname = Console.ReadLine();
                        Console.Write("enter what will be setted\n");
                        caz = Console.ReadLine();
                        sqlExpression = sqlExpression+ "'"+caz+"' WHERE id = '"+cname+"'";
                        
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlExpression, connection);
                            int number = command.ExecuteNonQuery();
                            Console.WriteLine("Updated object: {0}", number);
                            connection.Close();
                        }
                        Console.Read();
                        Console.Clear();
                        break;
                    case "5":
                        sqlExpression = "SELECT COUNT(*) FROM Soft";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlExpression, connection);
                            object count = command.ExecuteScalar();

                            command.CommandText = "SELECT MIN(Price) FROM Soft";
                            object minAge = command.ExecuteScalar();

                            Console.WriteLine("There are {0} projects", count);
                            Console.WriteLine("Min price: {0}", minAge);
                            connection.Close();
                        }
                        Console.Read();
                        Console.Clear();
                        break;
                    case "6":
                        sqlExpression = "SELECT * FROM Soft WHERE price = (SELECT MAX(Price) FROM Soft)";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {

                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlExpression, connection);
                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.HasRows) // если есть данные
                            {
                                // выводим названия столбцов
                                Console.WriteLine("{0}  {1}\t{2}\t{3}\t{4}\t{5}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3), reader.GetName(4), reader.GetName(5));

                                while (reader.Read()) // построчно считываем данные
                                {
                                    object id = reader.GetValue(0);
                                    object name = reader.GetValue(1);
                                    object price = reader.GetValue(2);
                                    object documentation = reader.GetValue(3);
                                    object distribution= reader.GetValue(4);
                                    object rules = reader.GetValue(5);

                                    Console.WriteLine("{0}  {1}\t\t{2}\t{3}\t{4}\t\t{5}", id, name, price,documentation,distribution,rules);
                                }
                            }
                         reader.Close();
                         connection.Close();
                        }
                        Console.Read();
                        Console.Clear();
                        break;
                    case "7":
                        sqlExpression = "SELECT Depart.Name, COUNT(*) AS Number FROM Emplo JOIN Depart ON Emplo.idDepart = Depart.id GROUP BY  Depart.name";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {

                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlExpression, connection);
                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.HasRows) // если есть данные
                            {
                                // выводим названия столбцов
                                Console.WriteLine("{0}\t\t\t\t\t{1}", reader.GetName(0), reader.GetName(1));

                                while (reader.Read()) // построчно считываем данные
                                {
                                    object name = reader.GetValue(0);
                                    object number = reader.GetValue(1);


                                    Console.WriteLine("{0}\t\t{1}", name, number);
                                }
                            }
                            reader.Close();
                            connection.Close();
                        }

                        Console.Read();
                        Console.Clear();
                        break;
                    case "8":
                        sqlExpression = "SELECT id, name FROM Cust";
                        object ID = null;
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {

                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlExpression, connection);
                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.HasRows) // если есть данные
                            {
                                // выводим названия столбцов
                                Console.WriteLine("{0}  {1}", reader.GetName(0), reader.GetName(1));

                                while (reader.Read()) // построчно считываем данные
                                {
                                    ID = reader.GetValue(0);
                                    object name = reader.GetValue(1);


                                    Console.WriteLine("{0}  {1}", ID, name);
                                }
                            }
                            reader.Close();
                            connection.Close();
                        }
                        Console.WriteLine("Enter id of customer in which you want to see liecence");
                        string C = Console.ReadLine();
                        if (Convert.ToInt32(C) <= Convert.ToInt32(ID))
                        {
                            sqlExpression = "SELECT soft.name,sale_date, lic.Price, Upg_date FROM Lic join Soft on Lic.idSoft = Soft.id WHERE id_cust=" + C;
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {

                                connection.Open();
                                SqlCommand command = new SqlCommand(sqlExpression, connection);
                                SqlDataReader reader = command.ExecuteReader();

                                if (reader.HasRows) // если есть данные
                                {

                                    // выводим названия столбцов
                                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3));

                                    while (reader.Read()) // построчно считываем данные
                                    {
                                        object name= reader.GetValue(0);
                                        object sale_date = reader.GetValue(1);
                                        object Price = reader.GetValue(2);
                                        object Upg_date = reader.GetValue(3);
                                        Console.WriteLine("{0}\t{1}\t{2}\t{3}",name, Convert.ToDateTime(sale_date).ToShortDateString(), Price,Convert.ToDateTime( Upg_date).ToShortDateString());

                                    }
                                }
                                reader.Close();
                                connection.Close();
                            }
                        }
                        Console.Read();
                        Console.Clear();
                        break;
                    case "9":
                        sqlExpression = "SELECT Id,Name FROM Soft";
                        object Id = null;
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {

                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlExpression, connection);
                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.HasRows) // если есть данные
                            {
                                // выводим названия столбцов
                                Console.WriteLine("{0}  {1}", reader.GetName(0), reader.GetName(1));

                                while (reader.Read()) // построчно считываем данные
                                {
                                    Id = reader.GetValue(0);
                                    object name = reader.GetValue(1);


                                    Console.WriteLine("{0}  {1}", Id, name);
                                }
                            }
                            reader.Close();
                            connection.Close();
                        }
                        Console.WriteLine("Enter id of project in which you want to see partisipants");
                        string n = Console.ReadLine();
                        if (Convert.ToInt32(n) <= Convert.ToInt32(Id))
                        {
                            sqlExpression = "SELECT Emplo.Name, Emplo.pos" +
                                " FROM Personal JOIN " +
                                " Soft ON Personal.id_Soft = Soft.id JOIN " +
                                " Emplo ON Personal.id_Empl = Emplo.Id" +
                                " WHERE Soft.id=" + n;
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {

                                connection.Open();
                                SqlCommand command = new SqlCommand(sqlExpression, connection);
                                SqlDataReader reader = command.ExecuteReader();

                                if (reader.HasRows) // если есть данные
                                {

                                    // выводим названия столбцов
                                    Console.WriteLine("{0}\t\t{1}", reader.GetName(0), reader.GetName(1));

                                    while (reader.Read()) // построчно считываем данные
                                    {
                                        object name = reader.GetValue(0);
                                        object position = reader.GetValue(1);


                                        Console.WriteLine("{0}\t\t{1}", name, position);

                                    }
                                }
                                reader.Close();
                                connection.Close();
                            }
                        }


                        Console.Read();
                        Console.Clear();
                        break;
                    case "10":
                        sqlExpression = "SELECT * FROM Emplo WHERE pos='run_boy' ORDER BY Name";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {

                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlExpression, connection);
                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.HasRows) // если есть данные
                            {
                                // выводим названия столбцов
                                Console.WriteLine("{0}\t{1}\t{2}\t{3}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3));
                                
                                while (reader.Read()) // построчно считываем данные
                                {
                                    object id = reader.GetValue(0);
                                    object idDepart = reader.GetValue(1);
                                    object name = reader.GetValue(2);
                                    object pos = reader.GetValue(3);

                                    
                                       Console.WriteLine("{0}\t{1}\t\t{2}\t{3}", id, idDepart, name, pos);
                                   
                                }
                            }
                            reader.Close();
                            connection.Close();
                        }
                        Console.Read();
                        Console.Clear();
                        break;
                        
                }
               
            }
           
        }
        
    }
   
}
