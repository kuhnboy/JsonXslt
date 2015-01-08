using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace JsonXslt.Tests
{
	[TestClass]
	public class JsonXPathNavigatorTests
	{
		[TestMethod]
		public void TestXmlConversion()
		{
			JObject testObject = JObject.Parse("{\"member1\":true,\"member2\":{\"child1\":1.245},\"member3\":[\"One\",\"Two\",{\"child2\":2.4596},{\"child3\":{\"subchild1\":2.4596,\"subchild2\":2.4596}}]}");
			JsonXPathNavigator nav = new JsonXPathNavigator(testObject);
			XDocument doc = XDocument.Load(nav.ReadSubtree());

			string result = doc.ToString(SaveOptions.DisableFormatting);

			Assert.AreEqual("<Document><member1>True</member1><member2><child1>1.245</child1></member2><member3><member3>One</member3><member3>Two</member3><member3><child2>2.4596</child2></member3><member3><child3><subchild1>2.4596</subchild1><subchild2>2.4596</subchild2></child3></member3></member3></Document>", result);
		}
	}
}