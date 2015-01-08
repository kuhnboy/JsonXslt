using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using Newtonsoft.Json.Linq;

namespace JsonXslt
{
	public class JsonXPathNavigator : XPathNavigator
	{
		private JToken currentObject;
		private List<int> arrayIdxStack = new List<int>();
		private XPathNodeType type = XPathNodeType.Root;

		public JsonXPathNavigator(JToken json)
		{
			currentObject = json;
			arrayIdxStack.Add(-1);
		}

		public override string BaseURI
		{
			get { return string.Empty; }
		}

		public override XPathNavigator Clone()
		{
			return new JsonXPathNavigator(currentObject) { type = type, arrayIdxStack = new List<int>(arrayIdxStack) };
		}

		public override bool IsEmptyElement
		{
			get
			{
				return type != XPathNodeType.Root && !currentObject.HasValues;
			}
		}

		public override bool IsSamePosition(XPathNavigator other)
		{
			JsonXPathNavigator nav = other as JsonXPathNavigator;

			if (nav == null)
			{
				return false;
			}

			return nav.currentObject == currentObject && nav.type == type && nav.arrayIdxStack.Count == arrayIdxStack.Count && nav.arrayIdxStack[nav.arrayIdxStack.Count - 1] == arrayIdxStack[nav.arrayIdxStack.Count - 1];
		}

		private int CurrentIdx()
		{
			return arrayIdxStack[arrayIdxStack.Count - 1];
		}

		private void CurrentIdx(int idx)
		{
			arrayIdxStack[arrayIdxStack.Count - 1] = idx;
		}

		private string GetName()
		{
			string path = currentObject.Path;

			if (string.IsNullOrEmpty(path))
			{
				return "Document";
			}

			int idx = path.LastIndexOf('.');

			if (idx == -1)
			{
				return path;
			}

			return path.Substring(idx + 1, path.Length - idx - 1);
		}

		public override string LocalName
		{
			get
			{
				return GetName();
			}
		}

		public override bool MoveTo(XPathNavigator other)
		{
			JsonXPathNavigator nav = other as JsonXPathNavigator;

			if (nav == null)
			{
				throw new InvalidOperationException();
			}

			currentObject = nav.currentObject;
			type = nav.type;
			arrayIdxStack = new List<int>(nav.arrayIdxStack);

			return true;
		}

		public override bool MoveToFirstAttribute()
		{
			return false;
		}

		public override bool MoveToFirstChild()
		{
			switch (type)
			{
				case XPathNodeType.Root:
					type = XPathNodeType.Element;
					return true;
				case XPathNodeType.Text:
					return false;
			}

			JToken obj;

			var jarray = currentObject as JArray;
			if (jarray != null)
			{
				obj = jarray[CurrentIdx()];
			}
			else
			{
				obj = currentObject;

				if (obj is JProperty)
				{
					obj = ((JProperty)obj).Value;
				}
				else
				{
					obj = obj.First;
				}
			}

			if (obj is JObject)
			{
				obj = ((JObject)obj).First;
			}

			if (obj is JArray)
			{
				currentObject = obj;
				arrayIdxStack.Add(0);

				return true;
			}

			if (obj != null)
			{
				currentObject = obj;
				type = currentObject is JValue ? XPathNodeType.Text : XPathNodeType.Element;
				arrayIdxStack.Add(-1);
			}

			return obj != null;
		}

		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			return false;
		}

		public override bool MoveToId(string id)
		{
			return false;
		}

		public override bool MoveToNext()
		{
			if (type == XPathNodeType.Root || type == XPathNodeType.Text)
			{
				return false;
			}

			if (CurrentIdx() != -1)
			{
				if (CurrentIdx() + 1 < ((JArray)currentObject).Count)
				{
					CurrentIdx(CurrentIdx() + 1);
					return true;
				}

				return false;
			}

			JToken next = currentObject.Next;

			if (next != null)
			{
				currentObject = next;
			}

			return next != null;
		}

		public override bool MoveToNextAttribute()
		{
			return false;
		}

		public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			return false;
		}

		public override bool MoveToParent()
		{
			if (currentObject is JArray)
			{
				if (CurrentIdx() == -1)
				{
					arrayIdxStack.RemoveAt(arrayIdxStack.Count - 1);
					type = XPathNodeType.Element;
					return true;
				}
			}

			JToken parent = currentObject.Parent;

			if (parent != null)
			{
				if (parent is JObject && parent.Parent != null)
				{
					parent = parent.Parent;
				}

				arrayIdxStack.RemoveAt(arrayIdxStack.Count - 1);
				currentObject = parent;
				type = XPathNodeType.Element;
			}
			else
			{
				if (type == XPathNodeType.Root)
				{
					return false;
				}
				
				type = XPathNodeType.Root;
				return true;
			}

			return true;
		}

		public override bool MoveToPrevious()
		{
			if (type == XPathNodeType.Root)
			{
				return false;
			}

			if (CurrentIdx() != -1)
			{
				if (CurrentIdx() - 1 >= 0)
				{
					CurrentIdx(CurrentIdx() - 1);
					return true;
				}

				return false;
			}

			JToken previous = currentObject.Previous;

			if (previous != null)
			{
				currentObject = previous;
			}

			return previous != null;
		}

		public override string Name
		{
			get
			{
				return GetName();
			}
		}

		public override XmlNameTable NameTable
		{
			get { return new NameTable(); }
		}

		public override string NamespaceURI
		{
			get { return string.Empty; }
		}

		public override XPathNodeType NodeType
		{
			get
			{
				return type;
			}
		}

		public override string Prefix
		{
			get
			{
				return string.Empty;
			}
		}

		public override string Value
		{
			get
			{
				if (CurrentIdx() != -1)
				{
					return ((JArray)currentObject)[CurrentIdx()].ToString();
				}

				if (currentObject is JProperty)
				{
					return ((JProperty)currentObject).Value.ToString();
				}
				else
				{
					return currentObject.ToString();
				}
			}
		}
	}
}
