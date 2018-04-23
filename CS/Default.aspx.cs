using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web;
using DevExpress.XtraScheduler;
using DevExpress.Web.ASPxScheduler.Commands;
using DevExpress.Web.ASPxScheduler.Internal;
using System.IO;
using DevExpress.XtraScheduler.iCalendar;


public partial class Default : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		DataHelper.SetupMappings(ASPxScheduler1);
		DataHelper.ProvideRowInsertion(ASPxScheduler1, DataSource1.AppointmentDataSource);
		DataSource1.AttachTo(ASPxScheduler1);

        ASPxScheduler1.DayView.ResourcesPerPage = 3;
        ASPxScheduler1.DayView.DayCount = 2;
        ASPxScheduler1.DayView.Styles.ScrollAreaHeight=450;
        ASPxScheduler1.WorkWeekView.ResourcesPerPage = 1;
        ASPxScheduler1.WorkWeekView.Styles.ScrollAreaHeight = 450;
        ASPxScheduler1.WeekView.ResourcesPerPage = 2;
        ASPxScheduler1.MonthView.ResourcesPerPage = 2;
        ASPxScheduler1.TimelineView.ResourcesPerPage = 3;
        ASPxScheduler1.TimelineView.AppointmentDisplayOptions.AppointmentAutoHeight = true;
        ASPxScheduler1.TimelineView.AppointmentDisplayOptions.TimeDisplayType= AppointmentTimeDisplayType.Text;
        ASPxScheduler1.Start = DateTime.Now;

	}

	protected void ASPxScheduler1_BeforeExecuteCallbackCommand(object sender, SchedulerCallbackCommandEventArgs e) {
		if(e.CommandId == "EXPORTAPT") 
			e.Command = new ExportSelectedAppointmentsCallbackCommand((ASPxScheduler)sender);
	}
    protected void ASPxScheduler1_PopupMenuShowing(object sender, DevExpress.Web.ASPxScheduler.PopupMenuShowingEventArgs e)
    {
		ASPxSchedulerPopupMenu menu = e.Menu;
		if(menu.MenuId.Equals(SchedulerMenuItemId.AppointmentMenu)) {
			DevExpress.Web.MenuItem item = new DevExpress.Web.MenuItem("Export", "ExportAppointment");
			e.Menu.Items.Insert(1, item);
			e.Menu.ClientSideEvents.ItemClick = "function(s, e) { OnMenuClick(s,e); }";
		}
	}

}
#region ExportSelectedAppointmentsCallbackCommand
public class ExportSelectedAppointmentsCallbackCommand : SchedulerCallbackCommand {
	public ExportSelectedAppointmentsCallbackCommand(ASPxScheduler control)
		: base(control) {
	}

	public override string Id { get { return "EXPORTAPT"; } }
	protected override void ParseParameters(string parameters) {
	}
	protected override void ExecuteCore() {
		PostCalendarFile(Control.SelectedAppointments);
	}
	void PostCalendarFile(AppointmentBaseCollection appointments) {
		iCalendarExporter exporter = new iCalendarExporter(Control.Storage, appointments);
		if(appointments.Count == 0)
			return;
		MemoryStream memoryStream = new MemoryStream();

		exporter.Export(memoryStream);
		memoryStream.WriteTo(Control.Page.Response.OutputStream);
		Control.Page.Response.ContentType = "text/calendar";
		Control.Page.Response.AddHeader("Content-Disposition", "attachment; filename=appointment.ics");
		Control.Page.Response.End();
	}

}
#endregion
