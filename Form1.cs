using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        
        public static SqlConnection con = new SqlConnection("SERVER=DESKTOP-285JK12; uid = yubin; pwd =1412; DATABASE=MS;");
        public Form1()
        {
            InitializeComponent();
        }

        private void From1_Load(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {
            string all = "select AVG(DATEDIFF(mi, check_in, check_out)) al from attend";
            SqlCommand all_time = new SqlCommand(all, con);
            con.Open();
            all_time.ExecuteNonQuery();
            SqlDataReader dr_allT ;

            dr_allT = all_time.ExecuteReader();
            while (dr_allT.Read())
            {
                avgtime.Text = (string)dr_allT["al"].ToString();

            }
            con.Close();
            int count;
            string what = "select count(customer_id) ct from attend group by customer_id";
            SqlCommand all_cmd = new SqlCommand(what, con);
            con.Open();
            all_cmd.ExecuteNonQuery();
            SqlDataReader dr_allD;
            int sum = 0;
            count = 0;
            int avg;
            dr_allD = all_cmd.ExecuteReader();
            while (dr_allD.Read())
            {
                sum += (int)dr_allD["ct"];
                count++;
            }
            avg = sum / count;

            metroTextBox4.Text = avg.ToString();

            con.Close();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string cus_id;
            cus_id = "Select customer_id from customer where name = '" + metroTextBox1.Text + "'";
            SqlCommand cus_cmd = new SqlCommand(cus_id, con);
            con.Open();
            cus_cmd.ExecuteNonQuery();
            SqlDataReader dr_id;

            dr_id = cus_cmd.ExecuteReader();
            while (dr_id.Read())
            {
                cus_id = (string)dr_id["customer_id"].ToString();

            }
            con.Close();
            string comtxt = "select name from trainer where trainer_id = (select trainer_id from customer where name = '" + metroTextBox1.Text + "')";
            SqlCommand cmd = new SqlCommand(comtxt, con);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader dr;
           
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                metroTextBox2.Text = (string)dr["name"].ToString();

            }
            con.Close();
            string body = "select * from body_record where customer_id = " + cus_id;
            SqlCommand bodycmd = new SqlCommand(body, con);
            con.Open();
            bodycmd.ExecuteNonQuery();
            SqlDataReader dr_b;
            dr_b = bodycmd.ExecuteReader();
            chart1.Series["muscle"].Points.Clear();
            chart1.Series["weight"].Points.Clear();
            chart1.Series["fat_rate"].Points.Clear();
            while (dr_b.Read())
            {
                int muscle = (int)dr_b["muscle"];
                chart1.Series["muscle"].Points.Add(muscle);
                int weight = (int)dr_b["weight"];
                chart1.Series["weight"].Points.Add(weight);
                int fat_rate = (int)dr_b["fat_rate"];
                chart1.Series["fat_rate"].Points.Add(fat_rate);
            }
            con.Close();

          //평균 시간 구하기
            string times = "select customer_id,AVG(DATEDIFF(mi,check_in, check_out)) time from attend group by customer_id  having (customer_id="+cus_id+")";
            SqlCommand trTmd = new SqlCommand(times, con);
            con.Open();
            trTmd.ExecuteNonQuery();
            SqlDataReader dr_Time;
            dr_Time = trTmd.ExecuteReader();
            while (dr_Time.Read())
            {
                trainTime.Text = (string)dr_Time["time"].ToString();
            }
            con.Close();

            string day = "select count(check_in) day from attend group by customer_id having (customer_id=" + cus_id + ")";
            SqlCommand daycmd = new SqlCommand(day, con);
            con.Open();
            daycmd.ExecuteNonQuery();

            SqlDataReader dr_day;
            dr_day = daycmd.ExecuteReader();
            while (dr_day.Read())
            {
                metroTextBox3.Text = (string)dr_day["day"].ToString();
            }
            con.Close();
            //지금까지 출석 일

        }
   
        private void metroTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void metroLabel3_Click(object sender, EventArgs e)
        {

        }

        private void avgday_Click(object sender, EventArgs e)
        {

        }

        private void metroProgressBar1_Click(object sender, EventArgs e)
        {

        }
    }
    
}
            