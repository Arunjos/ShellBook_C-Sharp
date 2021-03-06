﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace aron
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        public string inputFile, dbConnection;
        public int maxId,rowsUpdated;
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                inputFile = "database.s3db";
                dbConnection = String.Format("Data Source={0};Version=3;Password=KraQlin;", inputFile);
 using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
               {
                   string sql = "SELECT count(*) FROM exphddetails where emainhd ='" + textBox2.Text + "';";
                   SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                   cnn.Open();
                    rowsUpdated = Convert.ToInt32(sqlCmd.ExecuteScalar());
               }
 if (rowsUpdated > 0)
 { MessageBox.Show("THE ENTERD EXPENSE MAINHEAD ALREADY EXIST "); }
 else
 {
     using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
     {
         string sql = "insert into exphddetails (slno,emainhd,esubhd) values ('" + maxId + "','" + textBox2.Text + "','" + null + "');";
         SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
         cnn.Open();
         int rowsUpdated1 = sqlCmd.ExecuteNonQuery();
         MessageBox.Show("Saved successfully");
         slnoload();
         textBox2.Text = "";
     }
 }
            }
            else
            { MessageBox.Show("Completely Fill data"); }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form8_Load(object sender, EventArgs e)
        {

            slnoload();
        }
        public void slnoload()
        {
            inputFile = "database.s3db";
            dbConnection = String.Format("Data Source={0};Version=3;Password=KraQlin;", inputFile);

            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "Select Max(slno) From exphddetails where esubhd = ''";
                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open(); object val = sqlCmd.ExecuteScalar();
                try
                {
                    maxId = int.Parse(val.ToString()) + 1;
                    textBox1.Text = maxId.ToString();
                }
                catch (Exception ex)
                {
                    maxId = 1;
                    textBox1.Text = maxId.ToString();
                }
            }


        }
    }
}
