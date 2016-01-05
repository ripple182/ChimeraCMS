<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%PartialColumnModuleModel PCMM = Model ?? new PartialColumnModuleModel(); %>
<%string Id = PCMM.UniqueIdentifier; %>
<%string Image = "Slide_" + Id + "_Image"; %>
<%string CaptionBox = "Slide_" + Id + "_CaptionBox"; %>
<%string Title = "Slide_" + Id + "_Title"; %>
<%string Description = "Slide_" + Id + "_Description"; %>
<li>
    <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("img", Image, EFT.ImageSelect) %>
    <div class="flex-caption" <%=PCMM.ColumnModuleModel.ShowBasedOnActiveKey(CaptionBox) %>>
        <h3>
            <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("span", Title, EFT.SmallText) %>
        </h3>
        <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("p", Description, EFT.LargeText) %>
    </div>
</li>
