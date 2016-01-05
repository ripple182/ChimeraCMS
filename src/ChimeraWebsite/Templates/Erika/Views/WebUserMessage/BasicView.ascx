<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%WebUserMessage UserMessage = Model; %>
<%if (!string.IsNullOrWhiteSpace(UserMessage.User_Message_Text))
  {%>
    <div class="alert alert-block <%=UserMessage.User_Message_Class %>">
        <button type="button" class="close" data-dismiss="alert">
            <i class="icon-remove"></i>
        </button>
        <%=UserMessage.User_Message_Text%>
    </div>
<%}%>