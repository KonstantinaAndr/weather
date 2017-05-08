using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace weather
{
    public partial class Form1 : Form
           
    {       
            public Form1()
        {
            InitializeComponent();
        }
        private const string API_KEY = "e1053d1b85007fa1f764226b8401b627";

        private const string CurrentUrl =
           "http://api.openweathermap.org/data/2.5/weather?" +
           "q=@LOC@&mode=xml&units=imperial&APPID=" + API_KEY;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string url = CurrentUrl.Replace("@LOC@", textBox1.Text);
                richTextBox1.Text = GetFormattedXml(url);
                XmlDocument xml_doc = new XmlDocument();
                string weburl = "http://api.openweathermap.org/data/2.5/weather?q=" + textBox1.Text + "&mode=xml";
                var xml = new WebClient().DownloadStringTaskAsync(new Uri(weburl));
                string szTemp = xml_doc.DocumentElement.SelectSingleNode("temperature").Attributes["value"].Value;
                double temp = double.Parse(szTemp) - 273.16;
                richTextBox1.Text = temp.ToString("N2") + " Celcius";
            }
            catch(Exception)
            { }  
        }
        private string GetFormattedXml(string sztemp)
       {
           using (WebClient client = new WebClient())
           {
               string xml = client.DownloadString(CurrentUrl);
               XmlDocument xml_doc = new XmlDocument();
               xml_doc.LoadXml(xml);
              XmlNodeList xmlnd = xml_doc.SelectNodes("//temperature");
                                
               using (StringWriter stringwrt = new StringWriter())
              {
                 XmlTextWriter xmltextwrit = new XmlTextWriter(stringwrt);
                  xmltextwrit.Formatting = Formatting.Indented;
                  xml_doc.WriteTo(xmltextwrit);
               return stringwrt.ToString();
              }
            }
        }
       
    
  
