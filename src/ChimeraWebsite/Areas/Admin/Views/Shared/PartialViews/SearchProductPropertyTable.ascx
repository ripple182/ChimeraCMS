<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%List<CEP.Property> PropertyList = Model; %>
<%if(PropertyList != null && PropertyList.Count > 0){ %>
<table class="table table-condensed chimera-search-product-property-table">
    <tbody>
        <%foreach(var MyProperty in PropertyList)
          {%>
        <tr>
            <td><%=MyProperty.Name %>:</td>
            <td>
                <%if(MyProperty.Values != null && MyProperty.Values.Count > 0)
                  {%>
                    <%foreach(var PropVal in MyProperty.Values)
                         {%>
                    <button type="button" class="btn btn-info btn-xs"><%=PropVal.Value%></button>
                    <%}%>
                <%}
                  else{%>
                N/A
                <%}%>
            </td>
        </tr>
        <%}%>
    </tbody>
</table>
<%}%>