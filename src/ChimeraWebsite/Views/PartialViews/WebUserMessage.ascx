<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%List<WebUserMessage> UserMessageList = CompanyCommons.SessionHelpers.UserMessage.LoadWebUserMessagesFromSession(new HttpRequestWrapper(Request));%>
<%if (UserMessageList != null && UserMessageList.Count > 0)
  {%>
    <%foreach (var UserMessage in UserMessageList)
      {%>
        <%Html.RenderPartial(Url.Content(String.Format("~/Templates/{0}/Views/WebUserMessage/BasicView.ascx", ChimeraTemplate.TemplateName)), UserMessage);%>
    <%}%>
<%}%>
<%CompanyCommons.SessionHelpers.UserMessage.RemoveWebUserMessagesFromSession(new HttpRequestWrapper(Request));%>