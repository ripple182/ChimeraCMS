<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%PartialColumnModuleModel PCMM = Model ?? new PartialColumnModuleModel(); %>
<%string Id = PCMM.UniqueIdentifier; %>
<%string Image = "Slide_" + Id + "_Image"; %>
<%string Title = "Slide_" + Id + "_Title"; %>
<%string Description = "Slide_" + Id + "_Description"; %>
<%string Button = "Slide_" + Id + "_Button"; %>
<div class="da-slide">
    <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("h2",Title, EFT.SmallText) %>
    <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("p",Description, EFT.LargeText) %>
    <div class="da-link">
        <%= PCMM.ColumnModuleModel.ShowChildAndGetValue(SHE.BootstrapButton, Button, EFT.BootstrapButton) %>
    </div>
    <div class="da-img">
        <%= PCMM.ColumnModuleModel.ShowChildAndGetValue("img",Image, EFT.ImageSelect) %>
    </div>
</div>