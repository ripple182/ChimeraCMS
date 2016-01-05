<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%PartialColumnModuleModel PCMM = Model ?? new PartialColumnModuleModel(); %>
<%string Id = PCMM.UniqueIdentifier; %>
<%string Title = "Accordion_" + Id + "_Title"; %>
<%string Date_Location_Line = "Accordion_" + Id + "_Date_Location_Line"; %>
<%string Date = "Accordion_" + Id + "_Date"; %>
<%string Location = "Accordion_" + Id + "_Location"; %>
<%string Description = "Accordion_" + Id + "_Description"; %>
<div class="accordion-group">
    <div class="accordion-heading">
        <a class="accordion-toggle" data-toggle="collapse">
            <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("h5", Title, EFT.SmallText) %>
        </a>
    </div>
    <div class="accordion-body collapse <%=Id.Equals("1") ? "in" : "" %>">
        <div class="accordion-inner">
            <span <%=PCMM.ColumnModuleModel.ShowBasedOnActiveKey(Date_Location_Line) %>><i class="icon-calendar"></i>Date : <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("spann", Date, EFT.ExtraSmallText) %> - <i class="icon-home"></i>Location : <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("spann", Location, EFT.ExtraSmallText) %></span>
            <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("p", Description, EFT.LargeWYSIWYG) %>
        </div>
    </div>
</div>
