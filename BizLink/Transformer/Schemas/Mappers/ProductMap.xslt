<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <!-- Match the root element and transform it -->
  <xsl:template match="/Product">
    <ProductInformation>
      <xsl:copy-of select="ProductID"/>
      <xsl:element name="Name">
        <xsl:value-of select="ProductName"/>
      </xsl:element>
      <xsl:element name="Cost">
        <xsl:value-of select="Price"/>
      </xsl:element>
      <xsl:element name="IsAvailable">
        <xsl:value-of select="InStock"/>
      </xsl:element>
    </ProductInformation>
  </xsl:template>

  <!-- Identity transform for other elements -->
  <xsl:template match="@*|node()">
    <xsl:copy/>
  </xsl:template>
</xsl:stylesheet>