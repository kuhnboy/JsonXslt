# JsonXslt
Provides ability to convert Json &lt;-> Xml and the ability to transform via Xslt via Newtonsoft's Json.Net. This allows you to do Json <-> Json (in a different format) Json -> HTML, Json -> XML, XML -> Json (when certain rules are met).

# Examples

Json Representation:
```json
{
  "member1": "True",
  "member2": {
    "child1": 1.245
  },
  "member3": [
    "One",
    "Two",
    {
      "child2": 2.4596
    },
    {
      "child3": {
        "subchild1": 2.4596,
        "subchild2": 2.4596
      }
    }
  ]
}
```

Xml Representation:
```xml
<Document>
  <member1>True</member1>
  <member2>
    <child1>1.245</child1>
  </member2>
  <member3>
    <member3>One</member3>
    <member3>Two</member3>
    <member3>
      <child2>2.4596</child2>
    </member3>
    <member3>
      <child3>
        <subchild1>2.4596</subchild1>
        <subchild2>2.4596</subchild2>
      </child3>
    </member3>
  </member3>
</Document>
```

# Tips

- When Json is represented as XML, all properties are elements.
- When XML is converted to Json, a tree of nodes is determined to be an array if an element's first child is the same name as the element. (I'm open to other suggestions on how to determine this).
