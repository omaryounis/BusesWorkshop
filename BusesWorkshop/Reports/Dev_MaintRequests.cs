using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using BusesWorkshop.DAL.Bus;

namespace BusesWorkshop.Reports
{
    public partial class Dev_MaintRequests : DevExpress.XtraReports.UI.XtraReport
    {
     private static   bool? IsClosed = null;
     private static bool? IsNotClosed = null;
     private static int? CompanyId = null;
     private static int? RequestedEmpId = null;
     private static int? DelayDays = null;
     private static bool? PriorUrgent = null;
     private static bool? PriorHigh = null;
     private static bool? PriorLow = null;

        public Dev_MaintRequests()
        {
            InitializeComponent();
        }
        public Dev_MaintRequests(bool? prmIsClosed, bool? prmIsNotClosed, int? prmCompanyId, int? prmRequestedEmpId, int? prmDelayDays, bool? prmPriorUrgent, bool? prmPriorHigh, bool? prmPriorLow)
        {
            InitializeComponent();
            IsClosed = prmIsClosed;
            IsNotClosed = prmIsNotClosed;
            CompanyId = prmCompanyId;
            RequestedEmpId = prmRequestedEmpId;
            DelayDays = prmDelayDays;
            PriorUrgent = prmPriorUrgent;
            PriorHigh = prmPriorHigh;
            PriorLow = prmPriorLow;

        }

    }
}
