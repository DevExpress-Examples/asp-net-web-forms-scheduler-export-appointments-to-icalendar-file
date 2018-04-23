Imports Microsoft.VisualBasic
Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxScheduler

Partial Public Class DefaultObjectDataSources
	Inherits System.Web.UI.UserControl
	Private initAppointments As Boolean = True
	Private uniqueSessionPrefix_Renamed As String = String.Empty
	Private randomize_Renamed As New Random()

	Public Property UniqueSessionPrefix() As String
		Get
			Return uniqueSessionPrefix_Renamed
		End Get
		Set(ByVal value As String)
			uniqueSessionPrefix_Renamed = value
		End Set
	End Property
	Private ReadOnly Property CustomEventsSessionName() As String
		Get
			Return UniqueSessionPrefix & "CustomEvents"
		End Get
	End Property
	Private ReadOnly Property CustomResourcesSessionName() As String
		Get
			Return UniqueSessionPrefix & "CustomResources"
		End Get
	End Property
	Private ReadOnly Property Randomize() As Random
		Get
			Return randomize_Renamed
		End Get
	End Property

	Public ReadOnly Property AppointmentDataSource() As DataSourceControl
		Get
			Return appointmentDataSourceObject
		End Get
	End Property
	Public ReadOnly Property ResourceDataSource() As DataSourceControl
		Get
			Return resourceDataSourceObject
		End Get
	End Property

	Public Sub AttachTo(ByVal control As ASPxScheduler)
		control.ResourceDataSource = Me.ResourceDataSource
		control.AppointmentDataSource = Me.AppointmentDataSource
		control.DataBind()
	End Sub

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
	End Sub

	#Region "Site Mode implementation"
	Protected Sub temporaryAppointmentDataSource_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
		Dim events As CustomEventList = GetCustomEvents()
		e.ObjectInstance = New CustomEventDataSource(events)
	End Sub
	Protected Sub temporaryResourceDataSource_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
		Dim resources As CustomResourceList = GetCustomResources()
		e.ObjectInstance = New CustomResourceDataSource(resources)
	End Sub
	Protected Function GetCustomEvents() As CustomEventList
		Dim events As CustomEventList = TryCast(Session(CustomEventsSessionName), CustomEventList)
		If events IsNot Nothing Then
			Return events
		End If

		events = New CustomEventList()
		PopulateEventList(events)
		Session(CustomEventsSessionName) = events
		Return events
	End Function
	Protected Function GetCustomResources() As CustomResourceList
		Dim resources As CustomResourceList = TryCast(Session(CustomResourcesSessionName), CustomResourceList)
		If resources IsNot Nothing Then
			Return resources
		End If

		resources = New CustomResourceList()
		PopulateResourceList(resources)
		Session(CustomResourcesSessionName) = resources
		Return resources
	End Function

	Private baseDate As DateTime = DateTime.Now

	Private Sub PopulateEventList(ByVal events As CustomEventList)
		PopulateRentals(events)
		PopulateMaintenance(events)
	End Sub

	Private Sub PopulateRentals(ByVal events As CustomEventList)
		Dim customers() As String = { "Mr.Brown", "Mr.White", "Mrs.Black", "Mr.Green" }
		Dim locations() As String = { "city", "out of town" }
		Dim count As Integer = 20
		For i As Integer = 0 To count - 1
			events.Add(CreateRental(customers, locations))
		Next i
	End Sub
	Private Function CreateRental(ByVal customers() As String, ByVal locations() As String) As CustomEvent
		Dim result As New CustomEvent()
		result.Subject = customers(Randomize.Next(0, customers.Length))
		result.Location = locations(Randomize.Next(0, locations.Length))
		result.Description = "Rent this car"

		Dim rangeInDays As Integer = 7
		Dim rangeInHours As Integer = 18
		Dim offset As TimeSpan = TimeSpan.FromDays(Randomize.Next(0, rangeInDays)) + TimeSpan.FromHours(Randomize.Next(0, rangeInHours))
		result.StartTime = baseDate.Add(offset)
		result.EndTime = result.StartTime + TimeSpan.FromHours(Randomize.Next(1, rangeInHours))
		result.OwnerId = Randomize.Next(1, 6)
		result.Status = 2
		result.Label = Randomize.Next(0, 7)
		result.Id = Guid.NewGuid()
		AddEventAdditionalInfo(result)
		Return result
	End Function
	Private Sub PopulateMaintenance(ByVal events As CustomEventList)
		Dim wash As New CustomEvent()
		wash.Subject = "Wash"
		wash.Description = "Wash this car in the garage"
		wash.Location = "Garage"
		wash.StartTime = baseDate + TimeSpan.FromHours(7)
		wash.EndTime = baseDate + TimeSpan.FromHours(8)
		wash.OwnerId = Randomize.Next(1, 6)
		wash.Label = 2
		wash.EventType = 1
		wash.RecurrenceInfo = "<RecurrenceInfo AllDay=""False"" DayNumber=""1"" DayOfMonth=""0"" WeekDays=""42"" Id=""51c81018-53fa-4d10-925f-2ed7f8408c75"" Month=""12"" OccurenceCount=""19"" Periodicity=""1"" Range=""2"" Start=""7/11/2008 7:00:00"" End=""8/24/2008 1:00:00"" Type=""1"" />"
		wash.Id = Guid.NewGuid()
		AddEventAdditionalInfo(wash)
		events.Add(wash)
	End Sub
	Private Sub AddEventAdditionalInfo(ByVal ev As CustomEvent)
		Dim info() As String = { "Email: info{0}@wash_garage.com", "cellular: +530145961{0}", "Address: WA Seattle {0} - 24th Ave. S.Suite 4B phone: (206) 555-4{0}", "Contact: Address: OR Elgin City Center Plaza {0} Main St.", "Phone: (262) 946-9{0}; w ({0}) 723-2678 x22, cell (253) 713-0{0}, fax (361) 733-2{0}" }
		ev.Price = Randomize.Next(5, 100)

		Dim infoTemplate As String = info(Randomize.Next(0, info.Length))
		Dim ch As Char = Char.Parse(Randomize.Next(1, 9).ToString())
		ev.ContactInfo = String.Format(infoTemplate, New String(ch, 3))
	End Sub

	Private Sub PopulateResourceList(ByVal resources As CustomResourceList)
		resources.Add(CreateCustomResource(1, "SL500 Roadster"))
		resources.Add(CreateCustomResource(2, "CLK55 AMG Cabriolet"))
		resources.Add(CreateCustomResource(3, "C230 Kompressor Sport Coupe"))
		resources.Add(CreateCustomResource(4, "530i"))
		resources.Add(CreateCustomResource(5, "Corniche"))
	End Sub

	Private Function CreateCustomResource(ByVal id As Integer, ByVal caption As String) As CustomResource
		Dim result As New CustomResource()
		result.Id = id
		result.Caption = caption
		Return result
	End Function
	#End Region
End Class
