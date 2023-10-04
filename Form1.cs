//Написать программу которая будет адаптирована 2 базы данных одна SSMS вторая на ваше усмотрение (кроме SQLite)

using System;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using Npgsql;
using System.Data.SqlClient;
using WindowsFormsApp20;

namespace WindowsFormsApp20
{
    public partial class Form1 : Form
    {
        private IDbProviderFactoryWrapper CurrentDbFactory; 

        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.Add("SQL Server");
            comboBox1.Items.Add("PostgreSQL");
        }

        private void LoadData()
        {
            try
            {
                using (var connection = CurrentDbFactory.CreateConnection())
                {
                    connection.Open();

                    var query = "SELECT * FROM Foods";

                    var command = connection.CreateCommand();
                    command.CommandText = query;

                    var dataTable = new DataTable();
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "SQL Server":
                    CurrentDbFactory = new SqlServerFactoryWrapper
                    {
                        ConnectionString = @"Data Source = DESKTOP-NBVARLP; Initial Catalog = FoodBase_bd; Trusted_Connection=True; TrustServerCertificate=True"
                    };
                    if(CurrentDbFactory==null)
                    {
                        MessageBox.Show("no connection");
                    }
                    else
                    {
                        MessageBox.Show("connection");
                        LoadData();
                    }
                    break;
                case "PostgreSQL":
                    CurrentDbFactory = new PostgreSQLFactoryWrapper
                    {
                        ConnectionString = @"Server = localhost; User Id = postgres; Password = 123; Port = 5433; Database = FoodBase_bd;"
                    };
                    if (CurrentDbFactory == null)
                    {
                        MessageBox.Show("no connection");
                    }
                    else
                    {
                        MessageBox.Show("connection");
                        LoadData();
                    }
                    break;
                default:
                    break;
            }
        }
        internal interface IDbProviderFactoryWrapper
        {
            
            IDbConnection CreateConnection();
            
        }

        internal class SqlServerFactoryWrapper : IDbProviderFactoryWrapper
        {
            public string ConnectionString { get; set; }

            public IDbConnection CreateConnection()
            {
                return new SqlConnection(ConnectionString);
            }
         
        }

        internal class PostgreSQLFactoryWrapper : IDbProviderFactoryWrapper
        {
            public string ConnectionString { get; set; }

            public IDbConnection CreateConnection()
            {
                return new NpgsqlConnection(ConnectionString);
            }

        }
   
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    LoadData();
        //}
    }
}
