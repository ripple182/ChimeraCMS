<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%PartialColumnModuleModel PCMM = Model ?? new PartialColumnModuleModel(); %>
<%string Id = PCMM.UniqueIdentifier; %>
<%string Title = "Accordion_" + Id + "_Title"; %>
<%string Description = "Accordion_" + Id + "_Description"; %>
<div class="accordion-group" <%=PCMM.ColumnModuleModel.ShowBasedOnActiveKey(Title) %>>
    <div class="accordion-heading">
        <a class="accordion-toggle" data-toggle="collapse">
            <h5>
                <i class="icon-plus"></i>
                <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("span", Title, EFT.SmallText) %>
            </h5>
        </a>
    </div>
    <div class="accordion-body collapse <%=Id.Equals("1") ? "in" : "" %>">
        <div class="accordion-inner">
            <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("p", Description, EFT.LargeWYSIWYG) %>
        </div>
    </div>
</div>
