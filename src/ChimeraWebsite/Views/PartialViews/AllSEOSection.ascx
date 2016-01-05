<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%ProductModel ProdModel = ViewBag.ProductModel; %>
<%SettingGroup FavoriteIconSettings = SettingGroupDAO.LoadSettingGroupByName(SettingGroupKeys.FAVORITE_ICON_SETTINGS);%>
<%if (FavoriteIconSettings != null && FavoriteIconSettings.SettingsList != null && FavoriteIconSettings.SettingsList.Count > 0)
  {
      foreach (var FavSetting in FavoriteIconSettings.SettingsList)
      {
              if (FavSetting.SettingAttributeList != null && FavSetting.SettingAttributeList.Count > 0)
              {
                  string SettingAttributeValueKey = string.Empty;
                  string RestOfMetaTag = string.Empty;

                  foreach (var SettingAttr in FavSetting.SettingAttributeList)
                  {
                      RestOfMetaTag += String.Format(" {0}=\"{1}\"", SettingAttr.Key, SettingAttr.Value);
                  }
                        
                  %><meta <%=RestOfMetaTag %> href="<%=FavSetting.Value %>" /><%
               }
     }
  }%>
<%List<string> SEOSettingGroupNames = new List<string>();
  SEOSettingGroupNames.Add(SettingGroupKeys.SEO_COMMON_SETTINGS);
  SEOSettingGroupNames.Add(SettingGroupKeys.SEO_GOOGLEPLUS_SETTINGS);
  SEOSettingGroupNames.Add(SettingGroupKeys.SEO_FACEBOOK_SETTINGS);
  SEOSettingGroupNames.Add(SettingGroupKeys.SEO_TWITTER_SETTINGS);

  List<SettingGroup> SEOSettingGroupList = SettingGroupDAO.LoadByMultipleGroupNames(SEOSettingGroupNames);

  if (SEOSettingGroupList != null && SEOSettingGroupList.Count > 0)
  {
      foreach (var SEOSettingGroup in SEOSettingGroupList)
      {
          if (SEOSettingGroup.SettingsList != null && SEOSettingGroup.SettingsList.Count > 0)
          {
              foreach (var SEOSetting in SEOSettingGroup.SettingsList)
              {
                  //necessary to avoid duplicate metatags for the metatags we need to override for great product SEO
                  if (ProdModel == null || !ProductOverrideSEO.OverrideGlobalSEOList.Contains(SEOSettingGroup.GroupKey + ";" + SEOSetting.Key))
                  {
                      if (SEOSetting.SettingAttributeList != null && SEOSetting.SettingAttributeList.Count > 0)
                      {
                          string SettingAttributeValueKey = string.Empty;
                          string RestOfMetaTag = string.Empty;

                          foreach (var SEOSettingAttr in SEOSetting.SettingAttributeList)
                          {
                              if (SEOSettingAttr.Key.Equals(SettingAttributeKeys.SETTING_ATTRIBUTE_VALUE_KEY))
                              {
                                  SettingAttributeValueKey = SEOSettingAttr.Value;
                              }
                              else
                              {
                                  RestOfMetaTag += String.Format(" {0}=\"{1}\"", SEOSettingAttr.Key, SEOSettingAttr.Value);
                              }
                          }
                        
                          %><meta<%=RestOfMetaTag %> <%=SettingAttributeValueKey %>="<%=SEOSetting.Value %>" /><%
                      }
                  }
              }
          }
      }
  }%>

<%if (ProdModel != null)
  {%>
<title><%=ProdModel.Product.Name %></title>
<!-- Place this data between the <head> tags of your website -->
<meta name="description" content="<%=ProdModel.Product.Description %>" />
<%--Schema.org markup for Google+--%>
<meta itemprop="name" content="<%=ProdModel.Product.Name %>">
<meta itemprop="description" content="<%=ProdModel.Product.Description %>">
<meta itemprop="image" content="<%=ProdModel.Product.MainImage.ImagePath %>">
<%--Twitter Card data --%>
<meta name="twitter:card" content="product">
<meta name="twitter:title" content="<%=ProdModel.Product.Name %>">
<meta name="twitter:description" content="<%=ProdModel.Product.Description %>">
<meta name="twitter:image" content="<%=ProdModel.Product.MainImage.ImagePath %>">
<meta name="twitter:label1" content="Price">
<meta name="twitter:data1" content="<%=ProdModel.Product.PurchaseSettings.PurchasePrice.ToString("C") %>">
<%--Open Graph data --%>
<meta property="og:title" content="<%=ProdModel.Product.Name %>" />
<meta property="og:url" content="<%=ConfigurationManager.AppSettings["BaseWebsiteURL"] + "ViewProduct/Details?id=" + ProdModel.Product.Id%>" />
<meta property="og:image" content="<%=ProdModel.Product.MainImage.ImagePath %>" />
<meta property="og:description" content="<%=ProdModel.Product.Description %>" />
<meta property="og:price:amount" content="<%=ProdModel.Product.PurchaseSettings.PurchasePrice.ToString("0.00") %>" />
<meta property="og:price:currency" content="USD" />
<meta property="article:modified_time" content="<%=ProdModel.Product.CreatedDateUtc.ToString("o") %>" />
<%}
  else
  {%>
<%SettingGroup TCS = SettingGroupDAO.LoadSettingGroupByName(SettingGroupKeys.TEMPLATE_CUSTOM_SETTINGS); %>
<title><%=TCS.GetSettingVal("WebsiteTitle") %></title>
<%}%>