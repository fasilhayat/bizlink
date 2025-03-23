<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <!-- Match the root element and transform it -->
  <xsl:template match="/Order">
    <OrderDetails>
      <xsl:copy-of select="OrderID"/>
      <xsl:element name="OrderDate">
        <xsl:value-of select="OrderDate"/>
      </xsl:element>
      <xsl:copy-of select="CustomerID"/>
      <xsl:copy-of select="TotalAmount"/>
      <ItemsList>
        <xsl:for-each select="Items/Item">
          <Item>
            <xsl:copy-of select="ProductID"/>
            <xsl:copy-of select="Quantity"/>
            <xsl:copy-of select="Price"/>
          </Item>
        </xsl:for-each>
      </ItemsList>
    </OrderDetails>
  </xsl:template>

  <!-- Identity transform for other elements -->
  <xsl:template match="@*|node()">
    <xsl:copy/>
  </xsl:template>
</xsl:stylesheet>