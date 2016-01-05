<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<table class="table table-striped chimera-visibility-table">
    <tbody data-bind="foreach: { data: Object.keys($data.ChildrenValueDictionary), as: 'KeyValue' }">
        <tr>
            <td data-bind="text: KeyValue">
            <td>
                <button type="button" class="btn" data-bind="css: { 'btn-success': $parent.ChildrenValueDictionary[KeyValue].Active() }, click: function (data, event) { $parent.ChildrenValueDictionary[KeyValue].Active(true) }">visible</button>
                <button type="button" class="btn" data-bind="css: { 'btn-danger': !$parent.ChildrenValueDictionary[KeyValue].Active() }, click: function (data, event) { $parent.ChildrenValueDictionary[KeyValue].Active(false) }">hidden</button>
            </td>
        </tr>
    </tbody>
</table>
