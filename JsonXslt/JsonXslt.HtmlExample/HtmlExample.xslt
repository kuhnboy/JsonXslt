<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="html" indent="yes" />
  <xsl:template match="Document">
    <html xmlns="http://www.w3.org/1999/xhtml">
      <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>
          <xsl:value-of select="Sport"/>
        </title>
      </head>
      <body style="background-color: gray">
        <h1>
          <xsl:value-of select="Sport"/>
        </h1>
        <table style="border: 1px solid black">
          <tr>
            <td>Team</td>
            <td>Score</td>
          </tr>
          <xsl:for-each select="Scores/Scores">
            <tr>
              <td><xsl:value-of select="Name"/></td>
              <td><xsl:value-of select="Score"/></td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
