<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%PartialColumnModuleModel PCMM = Model ?? new PartialColumnModuleModel(); %>
<%string Id = PCMM.UniqueIdentifier; %>
<%string Title = "Accordion_" + Id + "_Title"; %>
<%string Description = "Accordion_" + Id + "_Description"; %>
<div class="panel panel-default">
    <div class="panel-heading">
        <h4 class="panel-title">
            <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("a", Title, EFT.SmallText, "data-toggle='collapse'", "accordion-toggle") %>
        </h4>
    </div>
    <div class="panel-collapse collapse <%=Id.Equals("1") ? "in" : "" %>">
        <div class="panel-body">
            <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("p", Description, EFT.LargeWYSIWYG) %>
        </div>
    </div>
</div>
