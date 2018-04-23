'
'{************************************************************************************}
'{                                                                                    }
'{   DO NOT MODIFY THIS FILE!                                                         }
'{                                                                                    }
'{   It will be overwritten without prompting when a new version becomes              }
'{   available. All your changes will be lost.                                        }
'{                                                                                    }
'{   This file contains the default template and is required for the form             }
'{   rendering. Improper modifications may result in incorrect behavior of            }
'{   the appointment form.                                                            }
'{                                                                                    }
'{   In order to create and use your own custom template, perform the following       }
'{   steps:                                                                           }
'{       1. Save a copy of this file with a different name in another location.       }
'{       2. Specify the file location as the 'OptionsForms.GotoDateFormTemplateUrl'   }
'{          property of the ASPxScheduler control.                                    }
'{       3. If you need to display and process additional controls, you               }
'{          should accomplish steps 4-6; otherwise, go to step 7.                     }
'{       4. Create a class, derived from the GotoDateFormTemplateContainer,           }
'{          containing additional properties which correspond to your controls.       }
'{          This class definition can be located within a class file in the App_Code  }
'{          folder.                                                                   }
'{       5. Replace GotoDateFormTemplateContainer references in the template page     }
'{          with the name of the class you've created in step 4.                      }
'{       6. Handle the GotoDateFormShowing event to create an instance of the         }
'{          template container class, defined in step 5, and specify it as the        }
'{          destination container  instead of the  default one.                       }
'{       7. Modify the overall appearance of the page and its layout.                 }
'{                                                                                    }
'{************************************************************************************}
'


Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxScheduler

Partial Public Class GotoDateForm
	Inherits System.Web.UI.UserControl
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		PrepareChildControls()
	End Sub

	Public Overrides Overloads Sub DataBind()
		MyBase.DataBind()
		Dim container As GotoDateFormTemplateContainer = CType(Parent, GotoDateFormTemplateContainer)
		cbView.Value = container.ActiveViewType.ToString()

		btnOk.ClientSideEvents.Click = container.ApplyHandler
		btnCancel.ClientSideEvents.Click = container.CancelHandler
	End Sub
	Private Sub PrepareChildControls()
		Dim container As GotoDateFormTemplateContainer = CType(Parent, GotoDateFormTemplateContainer)
		Dim control As ASPxScheduler = container.Control

		Dim edits() As ASPxEditBase = { lblDate, edtDate, lblView, cbView }

		For Each edit As ASPxEditBase In edits
			edit.ParentSkinOwner = control
			edit.ParentStyles = control.Styles.FormEditors
			edit.ParentImages = control.Images.FormEditors
		Next edit

		btnCancel.ParentSkinOwner = control
		btnOk.ParentSkinOwner = btnCancel.ParentSkinOwner
	End Sub
End Class
