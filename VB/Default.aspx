<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v15.2, Version=15.2.20.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>

<%@ Register Assembly="DevExpress.Web.v15.2" Namespace="DevExpress.Web" TagPrefix="dxm" %>

<%@ Register Assembly="DevExpress.XtraScheduler.v15.2.Core, Version=15.2.20.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v15.2" Namespace="DevExpress.Web.ASPxScheduler"
    TagPrefix="dxwschs" %>

<%@ Register Src="~/DefaultObjectDataSources.ascx" TagName="DefaultObjectDataSources" TagPrefix="dds" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dds:DefaultObjectDataSources runat="server" ID="DataSource1" />
        <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server"  GroupType="Resource" Start="2008-07-24" ClientInstanceName="scheduler" 
            OnBeforeExecuteCallbackCommand="ASPxScheduler1_BeforeExecuteCallbackCommand"
            OnPopupMenuShowing="ASPxScheduler1_PopupMenuShowing" Width="700px">
            <Views>
<DayView><TimeRulers>
<cc1:TimeRuler></cc1:TimeRuler>
</TimeRulers>
</DayView>

<WorkWeekView><TimeRulers>
<cc1:TimeRuler></cc1:TimeRuler>
</TimeRulers>
</WorkWeekView>
</Views>
        </dxwschs:ASPxScheduler>
    </div>
    </form>
<script type="text/javascript">
function OnMenuClick(s, e) {
    if (e.item.GetItemCount() <= 0) {
        if (e.item.name == "ExportAppointment")
            scheduler.SendPostBack("EXPORTAPT|");
        else
            scheduler.RaiseCallback("MNUAPT|" + e.item.name);
    }
}
</script>
</body>
</html>
