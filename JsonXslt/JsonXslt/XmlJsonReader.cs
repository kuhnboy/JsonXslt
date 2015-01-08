using System;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace JsonXslt
{
	public class XmlJsonReader : JsonReader
	{
		private XElement currentElement;
		private bool inProperty;
		private bool endObject;

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlJsonReader"/> class.
		/// </summary>
		/// <param name="doc">The document.</param>
		public XmlJsonReader(XDocument doc)
		{
			currentElement = doc.Root;
		}

		/// <summary>
		/// Gets or sets a value indicating whether values should be inspected to determine proper CLR type. This
		/// may cause performance degradation for documents with a large amount of values.
		/// </summary>
		/// <value>
		///   <c>true</c> if type determination should be disabled; otherwise, <c>false</c>.
		/// </value>
		public bool DisableValueTypeDetermination { get; set; }

		public override bool Read()
		{
			if (currentElement == null)
			{
				return false;
			}

			if (endObject)
			{
				endObject = false;

				SetToken(IsArray() ? JsonToken.EndArray : JsonToken.EndObject);

				return GetNextOrParent();
			}

			if (!inProperty && currentElement.Parent != null)
			{
				// How else can we determine if we are in an array and not to add a property?
				if (currentElement.Parent.Name.LocalName != currentElement.Name.LocalName)
				{
					inProperty = true;
					SetToken(JsonToken.PropertyName, currentElement.Name.LocalName);
					return true;
				}
			}
			else
			{
				inProperty = false;
			}

			if (currentElement.HasElements)
			{
				SetToken(IsArray() ? JsonToken.StartArray : JsonToken.StartObject);

				// Move to first child.
				currentElement = currentElement.Elements().First();
			}
			else
			{
				if (currentElement.IsEmpty)
				{
					SetToken(JsonToken.Null);
					return GetNextOrParent();
				}

				string strValue = currentElement.Value;

				if (DisableValueTypeDetermination)
				{
					SetToken(JsonToken.String, strValue);
					return GetNextOrParent();
				}

				bool boolValue;
				if (bool.TryParse(strValue, out boolValue))
				{
					SetToken(JsonToken.Boolean, boolValue);
				}
				else
				{
					int intValue;
					if (int.TryParse(strValue, out intValue))
					{
						SetToken(JsonToken.Integer, intValue);
					}
					else
					{
						double dubValue;
						if (double.TryParse(strValue, out dubValue))
						{
							SetToken(JsonToken.Float, dubValue);
						}
						else
						{
							DateTime dateValue;
							if (DateTime.TryParse(strValue, out dateValue))
							{
								SetToken(JsonToken.Date, dateValue);
							}
							else
							{
								SetToken(JsonToken.String, strValue);
							}
						}
					}
				}

				return GetNextOrParent();
			}

			return currentElement != null;
		}

		private bool GetNextOrParent()
		{
			var next = currentElement.ElementsAfterSelf().FirstOrDefault();
			if (next == null)
			{
				endObject = true;
				currentElement = currentElement.Parent;

				return true;
			}
			
			currentElement = next;
			return currentElement != null;
		}

		private bool IsArray()
		{
			XElement first = currentElement.Elements().First();

			return first.Name == currentElement.Name;
		}

		public override byte[] ReadAsBytes()
		{
			throw new NotImplementedException();
		}

		public override DateTime? ReadAsDateTime()
		{
			throw new NotImplementedException();
		}

		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			throw new NotImplementedException();
		}

		public override decimal? ReadAsDecimal()
		{
			throw new NotImplementedException();
		}

		public override int? ReadAsInt32()
		{
			throw new NotImplementedException();
		}

		public override string ReadAsString()
		{
			throw new NotImplementedException();
		}
	}
}
