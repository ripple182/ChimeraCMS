<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%PartialColumnModuleModel PCMM = Model ?? new PartialColumnModuleModel(); %>
<%string Id = PCMM.UniqueIdentifier; %>
<%string Image = "Slide_" + Id + "_Image"; %>
<%string Description = "Slide_" + Id + "_Description"; %>
<%string ClientName = "Slide_" + Id + "_ClientName"; %>
<%string ClientSubtitle = "Slide_" + Id + "_ClientSubtitle"; %>
<div class="item <%=Id.Equals("1") ? "active" : "" %> ">
    <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("p", Description, EFT.SmallText) %>
    <div class="testimonial-info">
        <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("img", Image, EFT.ImageSelect) %>
        <span class="testimonial-author">
            <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("span", ClientName, EFT.ExtraSmallText) %>
            <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("em", ClientSubtitle, EFT.ExtraSmallText) %>
        </span>
    </div>
</div>
