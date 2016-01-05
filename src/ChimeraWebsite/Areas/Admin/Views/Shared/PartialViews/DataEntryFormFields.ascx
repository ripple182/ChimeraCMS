<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%DataEntryFormField MyModel = Model; %>
<%List<AdminUser> AdminUserList = AdminUserDAO.LoadAll(); %>
<%if (MyModel.Setting.EntryType == DataEntryType.SmallText)
  {%>
<input type="text" class="form-control" name="setting_<%=MyModel.Setting.Key %>" value="<%=MyModel.Setting.Value %>" />
<%}
  else if (MyModel.Setting.EntryType == DataEntryType.DropdownSelect)
  {%>
<select class="form-control" name="setting_<%=MyModel.Setting.Key %>">
    <%foreach (var PropertyVal in MyModel.StaticProperty.PropertyNameValues)
      {%>
    <option value="<%=PropertyVal %>" <%if (MyModel.Setting.Value.Equals(PropertyVal))
                                        {%>
        selected <%}%>><%=PropertyVal %></option>
    <%}%>
</select>
<%}
  else if (MyModel.Setting.EntryType == DataEntryType.MultipleCheckboxes)
  {%>
<%string[] CheckboxValues = MyModel.Setting.Value.Split(','); %>
<%foreach (var PropertyVal in MyModel.StaticProperty.PropertyNameValues)
  {%>
<div class="checkbox">
    <label>
        <input type="checkbox" name="setting_<%=MyModel.Setting.Key %>" value="<%=PropertyVal%>" <%if (CheckboxValues.Contains(PropertyVal))
                                                                                                   { %>checked<%}%>>
        <%=PropertyVal%>
    </label>
</div>
<%}%>
<%}
  else if (MyModel.Setting.EntryType == DataEntryType.ImageUpload)
  {%>
<input type="hidden" value="<%=MyModel.Setting.Value %>" id="setting_<%=MyModel.Setting.Key %>" name="setting_<%=MyModel.Setting.Key %>" />
<img class="chimera-data-entry-field-img img-thumbnail" src="<%=MyModel.Setting.Value.Equals(string.Empty) ? ConfigurationManager.AppSettings["[CHIMERA_VALUE_DEFAULT_IMAGE]"] : MyModel.Setting.Value %>" />
<button role="button" class="btn btn-sm btn-success" data-bind="click: function(data, event) { openSettingImageDialog('setting_<%=MyModel.Setting.Key %>    '); }">Upload New Image</button>
<%}
  else if (MyModel.Setting.EntryType == DataEntryType.MoneyInput)
  {%>
<div class="input-group">
    <span class="input-group-addon">$</span>
    <input type="text" class="form-control" name="setting_<%=MyModel.Setting.Key %>" value="<%=MyModel.Setting.Value %>" />
</div>
<%}
  else if (MyModel.Setting.EntryType == DataEntryType.PercentageDecimalInput)
  {%>
<div class="input-group">
    <span class="input-group-addon">%</span>
    <input type="text" class="form-control" name="setting_<%=MyModel.Setting.Key %>" value="<%=MyModel.Setting.Value %>" />
</div>
<%}
  else if (MyModel.Setting.EntryType == DataEntryType.SelectMultipleAdminUser)
  {%>
<%string[] CheckboxValues = MyModel.Setting.Value.Split(','); %>
<%foreach (var AdminUser in AdminUserList)
  {%>
<div class="checkbox">
    <label>
        <input type="checkbox" name="setting_<%=MyModel.Setting.Key %>" value="<%=AdminUser.Id.ToString()%>" <%if (CheckboxValues.Contains(AdminUser.Id.ToString()))
                                                                                                               { %>checked<%}%>>
        <%=AdminUser.Username%>
    </label>
</div>
<%}%>
<%}%>