<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <!-- Match the root element and transform it -->
  <xsl:template match="/Customer">
    <CustomerDetails>
      <xsl:copy-of select="CustomerID"/>
      <xsl:element name="FullName">
        <xsl:value-of select="Name"/>
      </xsl:element>
      <xsl:element name="ContactEmail">
        <xsl:value-of select="Email"/>
      </xsl:element>
      <xsl:element name="BirthDate">
        <xsl:value-of select="DateOfBirth"/>
      </xsl:element>
    </CustomerDetails>
  </xsl:template>

  <!-- Identity transform for other elements -->
  <xsl:template match="@*|node()">
    <xsl:copy/>
  </xsl:template>
</xsl:stylesheet>