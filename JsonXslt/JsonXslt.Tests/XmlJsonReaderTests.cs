using System.IO;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonXslt.Tests
{
	[TestClass]
	public class XmlJsonReaderTests
	{
		[TestMethod]
		public void TestJsonConversion()
		{
			XDocument testXml;

			using (StringReader sr = new StringReader("<Document><member1>True</member1><member2><child1>1.245</child1></member2><member3><member3>2014-01-01T20:04:05-07:00</member3><member3>Two</member3><member3><child2>2.4596</child2></member3><member3><child3><subchild1>2.4596</subchild1><subchild2>2.4596</subchild2></child3></member3></member3></Document>"))
			{
				testXml = XDocument.Load(sr);
			}

			XmlJsonReader xjr = new XmlJsonReader(testXml);
			JObject jsonObject = JObject.Load(xjr);

			Assert.AreEqual("{\"member1\":true,\"member2\":{\"child1\":1.245},\"member3\":[\"2014-01-01T20:04:05-07:00\",\"Two\",{\"child2\":2.4596},{\"child3\":{\"subchild1\":2.4596,\"subchild2\":2.4596}}]}", jsonObject.ToString(Formatting.None));

			// Validate types.

			Assert.AreEqual(JTokenType.Boolean, jsonObject["member1"].Type);
			Assert.AreEqual(JTokenType.Float, jsonObject["member2"]["child1"].Type);
			Assert.AreEqual(JTokenType.Date, jsonObject["member3"][0].Type);
		}
	}
}
