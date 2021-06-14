
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;


namespace App4
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Browser.Source = "file:///android_asset/sample.html";
        }

        public object JsonConvert { get; private set; }
        public object JSON { get; private set; }
        // User can get the data from selected items.
        public void getData(object sender, EventArgs e)
        {
            // Array of data pass to the javascript.
            string json = "[{'X':0,'Y':1.52},{'X':1,'Y':1.56},{'X':2,'Y':1.42},{'X':3,'Y':1.85},{'X':4,'Y':1.62}]";

            // Check selected item from the list.
            if (chart.SelectedIndex == -1)
            {
                Browser.Source = "Nil Value" + chart.SelectedIndex;
            }
            else
            {
                var selectchart = chart.Items[chart.SelectedIndex];
                if (selectchart == "Line chart")
                {
                    // Using Evaluate function sent json data to javascript for line series.
                    Browser.EvaluateJavaScriptAsync(("javascript:ClearAll()"));
                    Browser.EvaluateJavaScriptAsync(("javascript:showlinechart(" + json + ")"));
                }
                else
                {
                     // Using Evaluate function sent json data to javascript for 3D chart.
                    // create series by passing number of series , number of points in each series
                   // to generator function  
                    var point = GeneratePointSeries(3,50);
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(point);
                    Browser.EvaluateJavaScriptAsync(("javascript:ClearAll()"));
                    Browser.EvaluateJavaScriptAsync(("javascript:show3Dchart("+ output + ")"));  
                }
            }
           
        }

       

        private List<Point3D[]> GeneratePointSeries(int seriesCount, int pointsPerSeries)
        {
            Random r = new Random();
            List<Point3D[]> pointSeries = new List<Point3D[]>();

            for (int i = 0; i < seriesCount; i++)
            {
              
                Point3D[] points = new Point3D[pointsPerSeries];
                for (int j = 0; j < pointsPerSeries; j++)
                {
                    
                     points[j] = new Point3D(j, r.Next(0, 100), r.Next(0, 100));
                   
                }
                pointSeries.Add(points);
            }
           
            return pointSeries;
        }

     

    }


    public struct Point3D
    {

        public int X { get; set; }


        public int Y { get; set; }


        public int Z { get; set; }


        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

}
