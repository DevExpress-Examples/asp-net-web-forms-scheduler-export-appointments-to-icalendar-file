Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web
Imports DevExpress.XtraScheduler
Imports DevExpress.Web.ASPxScheduler.Commands
Imports DevExpress.Web.ASPxScheduler.Internal
Imports System.IO
Imports DevExpress.XtraScheduler.iCalendar


Partial Public Class [Default]
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		DataHelper.SetupMappings(ASPxScheduler1)
		DataHelper.ProvideRowInsertion(ASPxScheduler1, DataSource1.AppointmentDataSource)
		DataSource1.AttachTo(ASPxScheduler1)

		ASPxScheduler1.DayView.ResourcesPerPage = 3
		ASPxScheduler1.DayView.DayCount = 2
		ASPxScheduler1.DayView.Styles.ScrollAreaHeight=450
		ASPxScheduler1.WorkWeekView.ResourcesPerPage = 1
		ASPxScheduler1.WorkWeekView.Styles.ScrollAreaHeight = 450
		ASPxScheduler1.WeekView.ResourcesPerPage = 2
		ASPxScheduler1.MonthView.ResourcesPerPage = 2
		ASPxScheduler1.TimelineView.ResourcesPerPage = 3
		ASPxScheduler1.TimelineView.AppointmentDisplayOptions.AppointmentAutoHeight = True
		ASPxScheduler1.TimelineView.AppointmentDisplayOptions.TimeDisplayType= AppointmentTimeDisplayType.Text
		ASPxScheduler1.Start = DateTime.Now

	End Sub

	Protected Sub ASPxScheduler1_BeforeExecuteCallbackCommand(ByVal sender As Object, ByVal e As SchedulerCallbackCommandEventArgs)
		If e.CommandId = "EXPORTAPT" Then
			e.Command = New ExportSelectedAppointmentsCallbackCommand(DirectCast(sender, ASPxScheduler))
		End If
	End Sub
	Protected Sub ASPxScheduler1_PopupMenuShowing(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxScheduler.PopupMenuShowingEventArgs)
		Dim menu As ASPxSchedulerPopupMenu = e.Menu
		If menu.MenuId.Equals(SchedulerMenuItemId.AppointmentMenu) Then
			Dim item As New DevExpress.Web.MenuItem("Export", "ExportAppointment")
			e.Menu.Items.Insert(1, item)
			e.Menu.ClientSideEvents.ItemClick = "function(s, e) { OnMenuClick(s,e); }"
		End If
	End Sub

End Class
#Region "ExportSelectedAppointmentsCallbackCommand"
Public Class ExportSelectedAppointmentsCallbackCommand
	Inherits SchedulerCallbackCommand

	Public Sub New(ByVal control As ASPxScheduler)
		MyBase.New(control)
	End Sub

'INSTANT VB NOTE: The method Id was renamed since Visual Basic does not allow same-signature methods with the same name:
	Public Overrides ReadOnly Property Id_Conflict() As String
		Get
			Return "EXPORTAPT"
		End Get
	End Property
	Protected Overrides Sub ParseParameters(ByVal parameters As String)
	End Sub
	Protected Overrides Sub ExecuteCore()
		PostCalendarFile(Control.SelectedAppointments)
	End Sub
	Private Sub PostCalendarFile(ByVal appointments As AppointmentBaseCollection)
		Dim exporter As New iCalendarExporter(Control.Storage, appointments)
		If appointments.Count = 0 Then
			Return
		End If
		Dim memoryStream As New MemoryStream()

		exporter.Export(memoryStream)
		memoryStream.WriteTo(Control.Page.Response.OutputStream)
		Control.Page.Response.ContentType = "text/calendar"
		Control.Page.Response.AddHeader("Content-Disposition", "attachment; filename=appointment.ics")
		Control.Page.Response.End()
	End Sub

End Class
#End Region
