<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128546782/15.2.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E80006)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Scheduler for ASP.NET Web Forms- How to export selected appointments to iCalendar file

This example demonstrates how to use a custom popup menu command to export selected appointments to the file in `iCalendar` format. See the following article for more information: [iCalendar support](https://docs.devexpress.com/AspNet/4864/components/scheduler/concepts/icalendar-support).

## Overview

* Implement the [iCalendarExporter Constructor](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraScheduler.iCalendar.iCalendarExporter.-ctor.overloads) that takes a collection of appointments for export.
* Select several appointments by clicking them while holding the Ctrl key.
* Hover over one of them for the smart tag to appear.
* Click the smart tag and select the Export command in the context menu.

## Files to Review

* [CustomEvents.cs](./CS/App_Code/CustomEvents.cs) (VB: [CustomEvents.vb](./VB/App_Code/CustomEvents.vb))
* [CustomResources.cs](./CS/App_Code/CustomResources.cs) (VB: [CustomResources.vb](./VB/App_Code/CustomResources.vb))
* [DataHelper.cs](./CS/App_Code/DataHelper.cs) (VB: [DataHelper.vb](./VB/App_Code/DataHelper.vb))
* [Default.aspx](./CS/Default.aspx) (VB: [Default.aspx](./VB/Default.aspx))
* [Default.aspx.cs](./CS/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/Default.aspx.vb))
* [DefaultObjectDataSources.ascx](./CS/DefaultObjectDataSources.ascx) (VB: [DefaultObjectDataSources.ascx](./VB/DefaultObjectDataSources.ascx))
* [DefaultObjectDataSources.ascx.cs](./CS/DefaultObjectDataSources.ascx.cs) (VB: [DefaultObjectDataSources.ascx.vb](./VB/DefaultObjectDataSources.ascx.vb))
