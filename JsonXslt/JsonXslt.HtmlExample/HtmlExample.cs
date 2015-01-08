using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using JsonXslt.HtmlExample.Properties;
using Newtonsoft.Json.Linq;

namespace JsonXslt.HtmlExample
{
	public partial class HtmlExample : Form
	{
		private readonly JObject json = JObject.Parse("{ \"Sport\" : \"Football\", \"Scores\" : [ { \"Name\" : \"Eagles\", \"Score\" : 21 }, { \"Name\" : \"Hawks\", \"Score\" : 14 } ]}");
		private readonly XslCompiledTransform transform = new XslCompiledTransform(true);

		public HtmlExample()
		{
			InitializeComponent();

			using (StringReader sr = new StringReader(Resources.HtmlExample))
			{
				using (XmlTextReader xr = new XmlTextReader(sr))
				{
					transform.Load(xr, new XsltSettings(true, false), null);
				}
			}
		}

		private void ViewJsonButton_Click(object sender, EventArgs e)
		{
			WebBrowserControl.DocumentText = FormatAsWeb(json.ToString());
		}

		private void ViewXsltButton_Click(object sender, EventArgs e)
		{
			WebBrowserControl.DocumentText = FormatAsWeb(Resources.HtmlExample);
		}

		private void ViewResultButton_Click(object sender, EventArgs e)
		{
			using (StringWriter sw = new StringWriter())
			{
				using (XmlTextWriter tw = new XmlTextWriter(sw))
				{
					transform.Transform(new JsonXPathNavigator(json), tw);
				}

				WebBrowserControl.DocumentText = sw.ToString();
			}
		}

		private void ViewAsXmlButton_Click(object sender, EventArgs e)
		{
			WebBrowserControl.DocumentText = FormatAsWeb(XDocument.Load(new JsonXPathNavigator(json).ReadSubtree()).ToString());
		}

		private string FormatAsWeb(string str)
		{
			return HttpUtility.HtmlEncode(str).Replace("\r\n", "<br/>").Replace(" ", "&nbsp;").Replace("\t", "    ");
		}
	}
}