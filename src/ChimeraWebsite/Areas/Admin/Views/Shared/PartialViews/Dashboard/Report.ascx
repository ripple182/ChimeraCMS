<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%PageReportCache PageReportCache = ViewBag.PageReportSummary ?? new PageReportSummary();
  PageReportSummary PageReportSummary = PageReportCache.ReportSummary;
  List<string> PageTypes = ViewBag.UniquePageTypes;
  string selectedPageType = ViewBag.selectedPageType;
  DateTime dateFrom = ViewBag.dateFrom;
  DateTime dateTo = ViewBag.dateTo;
  PageReportType pageReportType = ViewBag.pageReportType; %>
<br />
<form class="chimer-dashboard-report-form form-inline" method="post" action="<%=Url.Content("~/Admin/Dashboard/Index") %>">
    <div class="form-group">
        <label for="pageTypes" class="control-label">Page</label>
        <select name="selectedPageType" id="pageTypes" class="form-control input-sm">
            <option value="" <%if (string.IsNullOrWhiteSpace(selectedPageType))
                               {%>selected="selected"
                <%}%>>All Pages</option>
            <%if (PageTypes != null && PageTypes.Count > 0)
              {%>
            <%foreach (var PageType in PageTypes)
              {%>
            <option <%if (!string.IsNullOrWhiteSpace(selectedPageType) && PageType.Equals(selectedPageType))
                      {%>selected="selected"
                <%}%> value="<%=PageType %>"><%=PageType %></option>
            <%}%>
            <%}%>
        </select>
    </div>
    <div class="form-group">
        <label class="control-label">Date From</label>
        <input type="text" class="form-control date-picker input-sm" name="dateFrom" value="<%=dateFrom.ToShortDateString() %>">
    </div>
    <div class="form-group">
        <label class="control-label">Date To</label>
        <input type="text" class="form-control date-picker input-sm" name="dateTo" value="<%=dateTo.ToShortDateString() %>">
    </div>
    <div class="btn-group " data-toggle="buttons">
        <label class="btn btn-sm btn-default <%if (pageReportType.Equals(PageReportType.HOURLY))
                                               {%>active<%}%>">
            <input type="radio" name="pageReportType" value="<%=PageReportType.HOURLY %>" <%if (pageReportType.Equals(PageReportType.HOURLY))
                                                                                            {%>checked="checked"
                <%}%>>
            Hour
        </label>
        <label class="btn btn-sm btn-default <%if (pageReportType.Equals(PageReportType.DAY))
                                               {%>active<%}%>">
            <input type="radio" name="pageReportType" value="<%=PageReportType.DAY %>" <%if (pageReportType.Equals(PageReportType.DAY))
                                                                                         {%>checked="checked"
                <%}%>>
            Day
        </label>
        <label class="btn btn-sm btn-default <%if (pageReportType.Equals(PageReportType.MONTH))
                                               {%>active<%}%>">
            <input type="radio" name="pageReportType" value="<%=PageReportType.MONTH %>" <%if (pageReportType.Equals(PageReportType.MONTH))
                                                                                           {%>checked="checked"
                <%}%>>
            Month
        </label>
    </div>
    <button type="submit" class="btn btn-sm btn-primary">Submit</button>
</form>
<div class="row">
    <div class="col-md-12">
        <div>This report has been cached and will not be refresh for another <%=PageReportCache.LastAccessedUtc.Subtract(DateTime.UtcNow).Minutes %> minutes.  <a href="<%=Url.Content("~/Admin/Dashboard/RefreshReport?cacheKey=" + PageReportCache.CacheKey) %>">refresh now</a></div>
    </div>
</div>
<br />
<br />
<div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
<br />
<br />
<%if (string.IsNullOrWhiteSpace(selectedPageType) || "all".Equals(selectedPageType))
  {%>
<div class="row">
    <div class="col-md-2 col-sm-6">
        <div class="well">
            <h4 class="color">Page Views</h4>
            <h3><%=PageReportSummary.TotalPageVisits %></h3>
        </div>
    </div>
    <div class="col-md-2 col-sm-6">
        <div class="well">
            <h4 class="color">Visits</h4>
            <h3><%=PageReportSummary.TotalVisits %></h3>
        </div>
    </div>
    <div class="col-md-2 col-sm-6">
        <div class="well">
            <h4 class="color">Unique Visits</h4>
            <h3><%=PageReportSummary.TotalUniqueVistors %></h3>
        </div>
    </div>
    <div class="col-md-3 col-sm-6">
        <div class="well">
            <h4 class="color">Pages Per Visit</h4>
            <h3><%=PageReportSummary.PageViewsPerVisit.ToString("0.00") %></h3>
        </div>
    </div>
    <div class="col-md-3 col-sm-6">
        <div class="well">
            <h4 class="color">Avg Visit Duration</h4>
            <h3><%=PageReportSummary.AverageVisitDurationFormatted() %></h3>
        </div>
    </div>
</div>
<%}
  else
  {%>
<div class="row">
    <div class="col-md-2 col-sm-6">
        <div class="well">
            <h4 class="color">Page Views</h4>
            <h3><%=PageReportSummary.TotalPageVisits %></h3>
        </div>
    </div>
    <div class="col-md-4 col-sm-6">
        <div class="well">
            <h4 class="color">Unique Page Views</h4>
            <h3><%=PageReportSummary.TotalUniqueVistors %></h3>
        </div>
    </div>
    <div class="col-md-3 col-sm-6">
        <div class="well">
            <h4 class="color">Avg Visit Duration</h4>
            <h3><%=PageReportSummary.AverageVisitDurationFormatted() %></h3>
        </div>
    </div>
</div>
<%}%>
<div class="row">
    <div class="col-md-12">
        <div class="row">
            <div class="col-md-6">
                <div class="well">
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th>Browser ( Visits )</th>
                                <th class="width-10">#</th>
                                <th class="width-10">%</th>
                                <th class="width-25"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <%decimal TotalNumBrowserVisits = PageReportSummary.BrowserSummary.Sum(e => e.Value);%>
                            <%if (PageReportSummary.BrowserSummary != null && PageReportSummary.BrowserSummary.Count > 0)
                              {%>
                            <%foreach (var EntityName in PageReportSummary.BrowserSummary.Keys)
                              {%>
                            <%decimal Width = (PageReportSummary.BrowserSummary[EntityName] / TotalNumBrowserVisits) * 100; %>
                            <tr>
                                <td><%=EntityName %></td>
                                <td><%=PageReportSummary.BrowserSummary[EntityName]%></td>
                                <td><%=Math.Floor(Width).ToString() %>%</td>
                                <td>
                                    <div class="progress">
                                        <div class="progress-bar" role="progressbar" aria-valuenow="<%=Width %>" aria-valuemin="0" aria-valuemax="100" style="width: <%=Width%>%;"></div>
                                    </div>
                                </td>
                            </tr>
                            <%}%>
                            <%}%>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="col-md-6">
                <div class="well">
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th>Operating System ( Visitors )</th>
                                <th class="width-10">#</th>
                                <th class="width-10">%</th>
                                <th class="width-25"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <%decimal TotalNumOSVisits = PageReportSummary.OperatingSystemSummary.Sum(e => e.Value);%>
                            <%if (PageReportSummary.BrowserSummary != null && PageReportSummary.OperatingSystemSummary.Count > 0)
                              {%>
                            <%foreach (var EntityName in PageReportSummary.OperatingSystemSummary.Keys)
                              {%>
                            <%decimal Width = (PageReportSummary.OperatingSystemSummary[EntityName] / TotalNumOSVisits) * 100; %>
                            <tr>
                                <td><%=EntityName %></td>
                                <td><%=PageReportSummary.OperatingSystemSummary[EntityName]%></td>
                                <td><%=Math.Floor(Width).ToString() %>%</td>
                                <td>
                                    <div class="progress">
                                        <div class="progress-bar" role="progressbar" aria-valuenow="<%=Width%>" aria-valuemin="0" aria-valuemax="100" style="width: <%=Width%>%;"></div>
                                    </div>
                                </td>
                            </tr>
                            <%}%>
                            <%}%>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">

    $(function ()
    {
        Chimera_LoadDashboardPage(<%=JsonConvert.SerializeObject(PageReportSummary.MasterCategoryList)%>, <%=JsonConvert.SerializeObject(PageReportSummary.PageReportSeriesList)%>);
    });
</script>
