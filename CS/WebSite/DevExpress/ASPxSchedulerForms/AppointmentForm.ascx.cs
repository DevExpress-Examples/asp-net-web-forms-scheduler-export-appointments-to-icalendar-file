/*
{************************************************************************************}
{                                                                                    }
{   DO NOT MODIFY THIS FILE!                                                         }
{                                                                                    }
{   It will be overwritten without prompting when a new version becomes              }
{   available. All your changes will be lost.                                        }
{                                                                                    }
{   This file contains the default template and is required for the form             }
{   rendering. Improper modifications may result in incorrect behavior of            }
{   the appointment form.                                                            }
{                                                                                    }
{   In order to create and use your own custom template, perform the following       }
{   steps:                                                                           }
{       1. Save a copy of this file with a different name in another location.       }
{       2. Specify the file location as the 'OptionsForms.AppointmentFormTemplateUrl'}
{          property of the ASPxScheduler control.                                    }
{       3. If you need custom fields to be displayed and processed, you should       }
{          accomplish steps 4-9; otherwise, go to step 10.                           }
{       4. Create a class, derived from the AppointmentFormTemplateContainer,        }
{          containing custom properties. This class definition can be located        }
{          within a class file in the App_Code folder.                               }
{       5. Replace AppointmentFormTemplateContainer references in the template       }
{          page with the name of the class you've created in step 4.                 }
{       6. Handle the AppointmentFormShowing event to create an instance of the      }
{          template container class, defined in step 4, and specify it as the        }
{          destination container instead of the default one.                         }
{       7. Define a class, which inherits from the                                   }
{          DevExpress.Web.ASPxScheduler.Internal.AppointmentFormController.          }
{          This class provides data exchange between the form and the appointment.   }
{          You should override ApplyCustomFieldsValues() method of the base class.   }
{       8. Define a class, which inherits from the                                   }
{          DevExpress.Web.ASPxScheduler.Internal.AppointmentFormSaveCallbackCommand. }
{          This class creates an instance of the AppointmentFormController inheritor }
{          (defined in step 7) via the CreateAppointmentFormController method and    }
{          overrides the AssignControllerValues method  of the base class to collect }
{          user data from the form's editors.                                        }
{       9. Handle the BeforeExecuteCallbackCommand event. The event handler code     }
{          should create an instance of the class defined in step 8, and specify it  }
{          as the destination command instead of the default one.                    }
{      10. Modify the overall appearance of the page and its layout.                 }
{                                                                                    }
{************************************************************************************}
*/
using System;
using System.Web.UI;
using DevExpress.XtraScheduler;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;

public partial class AppointmentForm : UserControl {
	public bool CanShowReminders {
		get {
			return ((AppointmentFormTemplateContainer)Parent).Control.Storage.EnableReminders;
		}
	}

    protected void Page_Load(object sender, EventArgs e) {
		PrepareChildControls();
    } 
    public override void DataBind() {
        base.DataBind();
        AppointmentFormTemplateContainer container = (AppointmentFormTemplateContainer)Parent;
        Appointment apt = container.Appointment;
        edtLabel.SelectedIndex = apt.LabelId;
        edtStatus.SelectedIndex = apt.StatusId;
		if(!Object.Equals(apt.ResourceId, Resource.Empty.Id))
			edtResource.Value = apt.ResourceId.ToString();
		else
			edtResource.Value = SchedulerIdHelper.EmptyResourceId;
				
		AppointmentRecurrenceForm1.Visible = container.ShouldShowRecurrence;

		if(container.Appointment.HasReminder) {
			cbReminder.Value = container.Appointment.Reminder.TimeBeforeStart.ToString();
			chkReminder.Checked = true;
		}
		else {
			cbReminder.ClientEnabled = false;
		}

		btnOk.ClientSideEvents.Click = container.SaveHandler;
		btnCancel.ClientSideEvents.Click = container.CancelHandler;
		btnDelete.ClientSideEvents.Click = container.DeleteHandler;
		//btnDelete.Enabled = !container.IsNewAppointment;
    }

	void PrepareChildControls() {		
		AppointmentFormTemplateContainer container = (AppointmentFormTemplateContainer)Parent;
		ASPxScheduler control = container.Control;

		AppointmentRecurrenceForm1.EditorsInfo = new EditorsInfo(control, control.Styles.FormEditors, control.Images.FormEditors);

		ASPxEditBase[] edits = new ASPxEditBase[] {
			lblSubject, tbSubject, 
			lblLocation, tbLocation,
			lblLabel, edtLabel,
			lblStartDate, edtStartDate,
			lblEndDate, edtEndDate,
			lblStatus, edtStatus,
			lblAllDay, chkAllDay,
			lblResource, edtResource,
			tbDescription			
		};
		foreach(ASPxEditBase edit in edits) {
			edit.ParentSkinOwner = control;
			edit.ParentStyles = control.Styles.FormEditors;
			edit.ParentImages = control.Images.FormEditors;
		}

		ASPxButton[] buttons = new ASPxButton[] {
			btnOk, btnCancel, btnDelete
		};
		foreach(ASPxButton button in buttons) {
			button.ParentSkinOwner = control;
			button.ControlStyle.CopyFrom(control.Styles.FormButton);
		}
	}
}
