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
        private Series series1;
        private Series series2;
        private Series series3;
        private Series series4;
        private Series series5;

        public Form1()
        {
            InitializeComponent();
            chart1.ChartAreas.Clear();
            chart1.Series.Clear();
            // 「chartArea」という名前のエリアを生成します
            ChartArea chartArea = new ChartArea("chartArea");
            chart1.ChartAreas.Add(chartArea);
            chart1.ChartAreas["chartArea"].AxisX.Minimum = 0;
            chart1.ChartAreas["chartArea"].AxisY.Minimum = 0;
            chart1.ChartAreas["chartArea"].AxisY.Maximum = 500;

            // Series(系列)を生成します
            series1 = new Series();
            series1.ChartType = SeriesChartType.Line;
            series1.LegendText = "合計";
            series1.MarkerSize = 10;                    //マーカーのサイズ
            series1.MarkerColor = Color.Blue;          //マーカーの背景色
            series1.MarkerStyle = MarkerStyle.Circle;  //マーカーの形状 

            series2 = new Series();
            series2.ChartType = SeriesChartType.Line;
            series2.LegendText = "国語";
            series2.MarkerSize = 10;                    //マーカーのサイズ
            series2.MarkerColor = Color.Red;          //マーカーの背景色
            series2.MarkerStyle = MarkerStyle.Circle;  //マーカーの形状 

            series3 = new Series();
            series3.ChartType = SeriesChartType.Line;
            series3.LegendText = "数Ⅰ・A";
            series3.MarkerSize = 10;                    //マーカーのサイズ
            series3.MarkerColor = Color.Green;          //マーカーの背景色
            series3.MarkerStyle = MarkerStyle.Circle;  //マーカーの形状 

            series4 = new Series();
            series4.ChartType = SeriesChartType.Line;
            series4.LegendText = "数Ⅱ・B";
            series4.MarkerSize = 10;                    //マーカーのサイズ
            series4.MarkerColor = Color.DarkBlue;          //マーカーの背景色
            series4.MarkerStyle = MarkerStyle.Circle;  //マーカーの形状 

            series5 = new Series();
            series5.ChartType = SeriesChartType.Line;
            series5.LegendText = "英語";
            series5.MarkerSize = 10;                    //マーカーのサイズ
            series5.MarkerColor = Color.Orange;          //マーカーの背景色
            series5.MarkerStyle = MarkerStyle.Circle;  //マーカーの形状 
        }

        string path = "Test.csv";
        int[] jap = new int[100];
        int[] math1 = new int[100];
        int[] math2 = new int[100];
        int[] eng = new int[100];
        int[] sum = new int[100];
        int n = 0;

        int x1 = 0;
        int y1 = 0;
        int y2 = 0;
        int y3 = 0;
        int y4 = 0;
        int y5 = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series.Add(series1);
            chart1.Series.Add(series2);
            chart1.Series.Add(series3);
            chart1.Series.Add(series4);
            chart1.Series.Add(series5);

            if (System.IO.File.Exists(path) == false)
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

                    jap[n] = int.Parse(str[1]);
                    math1[n] = int.Parse(str[2]);
                    math2[n] = int.Parse(str[3]);
                    eng[n] = int.Parse(str[4]);
                    sum[n] = int.Parse(str[5]);

                    x1 = n;
                    y1 = sum[n];
                    y2 = jap[n];
                    y3 = math1[n];
                    y4 = math2[n];
                    y5 = eng[n];

                    series1.Points.AddXY(x1, y1);
                    series2.Points.AddXY(x1, y2);
                    series3.Points.AddXY(x1, y3);
                    series4.Points.AddXY(x1, y4);
                    series5.Points.AddXY(x1, y5);

                    n++;
                }
                sr.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            x1 = n;
            y2 = int.Parse(textBox2.Text);
            y3 = int.Parse(textBox3.Text);
            y4 = int.Parse(textBox4.Text);
            y5 = int.Parse(textBox5.Text);
            y1 = y2 + y3 + y4 + y5;

            series1.Points.AddXY(x1, y1);
            series2.Points.AddXY(x1, y2);
            series3.Points.AddXY(x1, y3);
            series4.Points.AddXY(x1, y4);
            series5.Points.AddXY(x1, y5);

            label5.Text = textBox1.Text + " 合計：" + y1 + "点";

            using(var sr = new System.IO.StreamWriter(path,true))
            {
                sr.WriteLine($"{textBox1.Text},{textBox2.Text},{textBox3.Text}," +
                    $"{textBox4.Text},{textBox5.Text},{y1}");
            }
            MessageBox.Show("ファイルに追記しました");

            n++;
        }
    }
}
