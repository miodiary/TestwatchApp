using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TestwatchApp
{
    public partial class Form1 : Form
    {
        private Series series;
        public Form1()
        {
            InitializeComponent();
            chart1.ChartAreas.Clear();
            chart1.Series.Clear();
            // 「chartArea」という名前のエリアを生成します
            ChartArea chartArea = new ChartArea("chartArea");
            chart1.ChartAreas.Add(chartArea);

            // Series(系列)を生成します
            series = new Series();
            series.ChartType = SeriesChartType.Line;
            series.LegendText = "模試";
            series.MarkerSize = 10;                    //マーカーのサイズ
            series.MarkerColor = Color.Blue;          //マーカーの背景色
            series.MarkerStyle = MarkerStyle.Circle;  //マーカーの形状 
        }

        string path = "Test.csv";
        //string[] id = new string[25];
        //int[] jap = new int[25];
        //int[] math = new int[25];
        //int[] eng = new int[25];
        int[] ave = new int[100];
        int n = 0;

        int x1 = 0;
        int y1 = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series.Add(series);

            if(System.IO.File.Exists(path) == false)
            {
                MessageBox.Show("ファイルが見つかりません");
                return;
            }
            using(var sr = new StreamReader(path))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] str = line.Split(',');

                    ave[n] = int.Parse(str[1]);

                    x1 = n;
                    y1 = ave[n];

                    series.Points.AddXY(x1, y1);

                    label5.Text = textBox1.Text + "：" + textBox2.Text + "点";

                    n++;
                }
                sr.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            x1 = n;
            y1 = int.Parse(textBox2.Text);

            series.Points.AddXY(x1, y1);          

            label5.Text = textBox1.Text + "：" + textBox2.Text + "点";

            using(var sr = new System.IO.StreamWriter(path,true))
            {
                sr.WriteLine(textBox1.Text + "," + y1);
            }
            MessageBox.Show("ファイルに追記しました");

            n++;
        }
    }
}
