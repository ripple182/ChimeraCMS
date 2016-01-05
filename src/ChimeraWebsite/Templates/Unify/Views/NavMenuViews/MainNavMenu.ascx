<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%NavigationMenuLink NavMenuLink = Model; %>
<%string BaseWebsiteURL = System.Configuration.ConfigurationManager.AppSettings["BaseWebsiteURL"]; %>
<%if (NavMenuLink.ChildNavLinks != null && NavMenuLink.ChildNavLinks.Count > 0)
  {%>
<li class="dropdown">
    <a href="#" class="dropdown-toggle" data-toggle="dropdown"><%=NavMenuLink.Text %> <b class="caret"></b></a>
    <ul class="dropdown-menu">
        <%foreach (var NavMenuLinkChild in NavMenuLink.ChildNavLinks)
          {
              Html.RenderPartial(Url.Content("~/Templates/Erika/Views/NavMenuViews/MainNavMenu.ascx"), NavMenuLinkChild);
          }%>
    </ul>
</li>
<%}
  else
  {%>
<li><a href="<%= !string.IsNullOrWhiteSpace(NavMenuLink.ChimeraPageUrl) ? BaseWebsiteURL + NavMenuLink.ChimeraPageUrl : NavMenuLink.RealUrl %>" target="<%=NavMenuLink.LinkAction %>"><%=NavMenuLink.Text %></a></li>
<%}%>

