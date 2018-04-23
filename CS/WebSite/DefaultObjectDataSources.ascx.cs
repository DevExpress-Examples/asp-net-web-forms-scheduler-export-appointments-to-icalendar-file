using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxScheduler;

public partial class DefaultObjectDataSources : System.Web.UI.UserControl {
	bool initAppointments = true;
	string uniqueSessionPrefix = String.Empty;
	Random randomize = new Random();

	public string UniqueSessionPrefix { get { return uniqueSessionPrefix; } set { uniqueSessionPrefix = value; } }
	string CustomEventsSessionName { get { return UniqueSessionPrefix + "CustomEvents"; } }
	string CustomResourcesSessionName { get { return UniqueSessionPrefix + "CustomResources"; } }
	Random Randomize { get { return randomize; } }

	public DataSourceControl AppointmentDataSource { get { return appointmentDataSourceObject; } }
	public DataSourceControl ResourceDataSource { get { return resourceDataSourceObject; } }

	public void AttachTo(ASPxScheduler control) {
		control.ResourceDataSource = this.ResourceDataSource;
		control.AppointmentDataSource = this.AppointmentDataSource;
		control.DataBind();
	}

	protected void Page_Load(object sender, EventArgs e) {
	}

	#region Site Mode implementation
	protected void temporaryAppointmentDataSource_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
		CustomEventList events = GetCustomEvents();
		e.ObjectInstance = new CustomEventDataSource(events);
	}
	protected void temporaryResourceDataSource_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
		CustomResourceList resources = GetCustomResources();
		e.ObjectInstance = new CustomResourceDataSource(resources);
	}
	protected CustomEventList GetCustomEvents() {
		CustomEventList events = Session[CustomEventsSessionName] as CustomEventList;
		if(events != null)
			return events;

		events = new CustomEventList();
		PopulateEventList(events);
		Session[CustomEventsSessionName] = events;
		return events;
	}
	protected CustomResourceList GetCustomResources() {
		CustomResourceList resources = Session[CustomResourcesSessionName] as CustomResourceList;
		if(resources != null)
			return resources;

		resources = new CustomResourceList();
		PopulateResourceList(resources);
		Session[CustomResourcesSessionName] = resources;
		return resources;
	}

	DateTime baseDate = DateTime.Now;

	void PopulateEventList(CustomEventList events) {
		PopulateRentals(events);
		PopulateMaintenance(events);
	}

	void PopulateRentals(CustomEventList events) {
		string[] customers = new string[] { "Mr.Brown", "Mr.White", "Mrs.Black", "Mr.Green" };
		string[] locations = new string[] { "city", "out of town" };
		int count = 20;
		for(int i = 0; i < count; i++)
			events.Add(CreateRental(customers, locations));
	}
	CustomEvent CreateRental(string[] customers, string[] locations) {
		CustomEvent result = new CustomEvent();
		result.Subject = customers[Randomize.Next(0, customers.Length)];
		result.Location = locations[Randomize.Next(0, locations.Length)];
		result.Description = "Rent this car";

		int rangeInDays = 7;
		int rangeInHours = 18;
		TimeSpan offset = TimeSpan.FromDays(Randomize.Next(0, rangeInDays)) + TimeSpan.FromHours(Randomize.Next(0, rangeInHours));
		result.StartTime = baseDate + offset;
		result.EndTime = result.StartTime + TimeSpan.FromHours(Randomize.Next(1, rangeInHours));
		result.OwnerId = Randomize.Next(1, 6);
		result.Status = 2;
		result.Label = Randomize.Next(0, 7);
		result.Id = Guid.NewGuid();
		AddEventAdditionalInfo(result);
		return result;
	}
	void PopulateMaintenance(CustomEventList events) {
		CustomEvent wash = new CustomEvent();
		wash.Subject = "Wash";
		wash.Description = "Wash this car in the garage";
		wash.Location = "Garage";
		wash.StartTime = baseDate + TimeSpan.FromHours(7);
		wash.EndTime = baseDate + TimeSpan.FromHours(8);
		wash.OwnerId = Randomize.Next(1, 6);
		wash.Label = 2;
		wash.EventType = 1;
		wash.RecurrenceInfo = @"<RecurrenceInfo AllDay=""False"" DayNumber=""1"" DayOfMonth=""0"" WeekDays=""42"" Id=""51c81018-53fa-4d10-925f-2ed7f8408c75"" Month=""12"" OccurenceCount=""19"" Periodicity=""1"" Range=""2"" Start=""7/11/2008 7:00:00"" End=""8/24/2008 1:00:00"" Type=""1"" />";
		wash.Id = Guid.NewGuid();
		AddEventAdditionalInfo(wash);
		events.Add(wash);
	}
	void AddEventAdditionalInfo(CustomEvent ev) {
		string[] info = new string[] {
				"Email: info{0}@wash_garage.com",
				"cellular: +530145961{0}",
				"Address: WA Seattle {0} - 24th Ave. S.Suite 4B phone: (206) 555-4{0}",
				"Contact: Address: OR Elgin City Center Plaza {0} Main St.",
				"Phone: (262) 946-9{0}; w ({0}) 723-2678 x22, cell (253) 713-0{0}, fax (361) 733-2{0}" 
		};
		ev.Price = Randomize.Next(5, 100);

		string infoTemplate = info[Randomize.Next(0, info.Length)];
		Char ch = Char.Parse(Randomize.Next(1, 9).ToString());
		ev.ContactInfo = string.Format(infoTemplate, new String(ch, 3));
	}

	void PopulateResourceList(CustomResourceList resources) {
		resources.Add(CreateCustomResource(1, "SL500 Roadster"));
		resources.Add(CreateCustomResource(2, "CLK55 AMG Cabriolet"));
		resources.Add(CreateCustomResource(3, "C230 Kompressor Sport Coupe"));
		resources.Add(CreateCustomResource(4, "530i"));
		resources.Add(CreateCustomResource(5, "Corniche"));
	}

	CustomResource CreateCustomResource(int id, string caption) {
		CustomResource result = new CustomResource();
		result.Id = id;
		result.Caption = caption;
		return result;
	}
	#endregion
}
