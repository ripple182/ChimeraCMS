<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%string confirmationNumber = Model; %>
<div class="row">
    <div class="col-md-12">
        <div class="error">
            <div class="error-page">
                <p class="error-med">Thank you for your order!  Your order confirmation number is: <b><%=confirmationNumber %></b></p>
            </div>
        </div>
    </div>
</div>
