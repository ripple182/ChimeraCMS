<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%List<WebUserMessage> UserMessageList = CompanyCommons.SessionHelpers.UserMessage.LoadWebUserMessagesFromSession(new HttpRequestWrapper(Request));%>
<%if (UserMessageList != null && UserMessageList.Count > 0)
      {%>
    <%foreach (var UserMessage in UserMessageList)
          {%>
            <%if(!string.IsNullOrWhiteSpace(UserMessage.User_Message_Text))
              {%>
                <div class="alert alert-block <%=UserMessage.User_Message_Class %>">
                    <button type="button" class="close" data-dismiss="alert">
                        <i class="icon-remove"></i>
                    </button>
                    <%=UserMessage.User_Message_Text%>
                </div>
            <%}%>
        <%}%>
<%}%>
<%CompanyCommons.SessionHelpers.UserMessage.RemoveWebUserMessagesFromSession(new HttpRequestWrapper(Request));%>