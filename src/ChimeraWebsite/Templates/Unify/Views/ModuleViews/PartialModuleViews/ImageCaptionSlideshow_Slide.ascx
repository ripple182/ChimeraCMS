<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%PartialColumnModuleModel PCMM = Model ?? new PartialColumnModuleModel(); %>
<%string Id = PCMM.UniqueIdentifier; %>
<%string Image = "Slide_" + Id + "_Image"; %>
<%string Description = "Slide_" + Id + "_Description"; %>
<div class="item <%=Id.Equals("1") ? "active" : "" %> ">
    <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("img", Image, EFT.ImageSelect) %>
    <div class="carousel-caption">
        <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("p", Description, EFT.SmallText) %>
    </div>
</div>
