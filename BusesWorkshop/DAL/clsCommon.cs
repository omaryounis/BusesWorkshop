using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AjaxControlToolkit;


namespace Accounting.DAL
{
    public class clsCommon
    {
        /// <summary>
        /// Enum to represent contact type.
        /// </summary>
        public enum CotactType
        {
            Customer = 'c',
            Vendor = 'v',
            Employee = 'e'
        }
        public class COA
        {

            public static int CashCoa = 0;
            public static string CashName = "النقدية بالصندوق";
        }
        public enum DocTypes
        {
            CatchCash = 40,
            ExchangeCash = 43,
            TransferBank = 80,
            CatchBank = 78,
            ExchangeBank = 79
        }
        /// <summary>
        /// Enum to represent Group type.
        /// </summary>
        public enum GroupID
        {
            DirectInvoice = 69,
            ActivityExpenses = 70,
            OtherIncome = 71,
            CapitalGains=72,
            GAExpenses=73,
            DepreciationFixedAssets=74,
            UnlikeDepreciationAllowances=75,
            Zakat=76
        }


        /// <summary>
        /// Enum to represent account types 
        /// </summary>
        public enum BCDType
        {
            GeneralJournal=64,
            CatchCash = 40,
            ExchangeCash = 43,
            TransferBank = 80,
            CatchBank = 78,
            ExchangeBank = 79,
            BuyAssets =60
        }
        public class DocPrefx
        {
            public static string Operation = "OP- ";
            public static string CatchCash = "CC- ";
            public static string ExchangeCash = "CX- ";
            public static string TransferBank = "CT- ";
            public static string CatchBank = "BC- ";
            public static string ExchangeBank = "BX- ";
            public static string ContractNumber = "CN- ";
            public static string PurchaseAst = "PAS- ";
        }
        /// <summary>
        /// Enum to represent account types 
        /// </summary>
        public enum AccountType
        {
            CreditCardReceivable = 1,
            Bank = 2,
            Cash = 3,
            CreditCardPayable = 4,
            Equity = 5,
            OtherFixedAsset = 6,
            OtherCurrentAsset = 7,
            CurrentLiability = 8,
            Sales = 9,
            TaxExpense = 10,
            CurrentAsset = 11,
            LongTermLiability = 12,
            GeneralandAdministrativeExpense = 13,
            PrepaidExpense = 14,
            OtherAccruedExpense = 15,
            AccountsPayable = 16,
            PropertyandEquipment = 17,
            AccountsReceivable = 18,
            CostofGoodsSold = 19,
            AccruedTax = 20,
            NetIncome = 21,
            Expense = 22,
            OtherExpense = 23,
            OtherIncome = 24
        }
        /// <summary>
        /// Enum to represent contact type.
        /// </summary>
        public enum Status
        {
            Old = 1,
            New = 2,
            Updated = 3,
            Deleted = 4
        }

        /// <summary>
        /// Enum to Basic Attribute
        /// </summary>
        public enum BasicAttribute
        {
            Customers = 1,
            Vendors = 2,
            Employees = 3,
            Assets = 4,
            Liabilities = 5,
            Revenues = 6,
            Expense = 7,
            Equity = 8,
            IssuedCheck = 21,
            ReceivedCheck = 22,
            BankTransfer = 23
        }

        /// <summary>
        /// Class To Repesent Contact Type.
        /// </summary>
        public class CotactName
        {
            public static string Customer = "العملاء";
            public static string Vendor = "الموردين";
            public static string Employee = "الموظفين";
        }

        /// <summary>
        /// Class To Repesent Cash Title.
        /// </summary>
        public class CashTitle
        {
            public static string CRV = "تحصيلات نقدية";
            public static string CPT = "مدفوعات نقدية";
            public static string CTF = "تحويلات نقدية";
        }

        /// <summary>
        /// Class To Represent Messages.
        /// </summary>
        public class Message
        {
            public static string sucess = "تم الإدخال بنجاح ";
            public static string sucessRelay = "تم الحفظ بنجاح ";
            public static string sucessUpdate = "تم التعديل بنجاح ";
            public static string failed = "حدث خطا اثناء الإدخال ";
            public static string falidEmailFormat = "صيغة البريد الالكترونى غير صحيحه.";
            public static string createdDB = "تم إنشاء قاعدة البيانات .";
            public static string faildCreateDB = "حدث خطأ أثناء إنشا قاعدة البيانات .";
            public static string usedBefore = "الاسم غير مسجل من قبل.";
            public static string wrongPassword = "كلمة المرور غير صحيحه.";
        }

        /// <summary>
        /// Class To Represent Messages.
        /// </summary>
        public class Config
        {
            public static int Config_CustomerSuspenseAccount = 1;
            public static int Config_VendorSuspenseAccount = 2;
            public static int Config_MainAccount = 3;
            public static int Config_ProfitAccount = 4;
            public static int Config_DepAccount = 5;
            public static int Fixedpremium = 6;
            public static int Fixedpercentage = 7;
            public static int Premiumdecreasing = 8;
            public static int Config_CapitalGains = 14;
            public static int Config_CapitalLosses = 15;
            public static int Config_CashCoaRent = 16;
            public static int Config_CashCoa = 17;
            public static int Config_MainBoxAcc = 18;
            public static int Config_MainBankAcc = 19;
            public static int Config_MainAstAcc = 21;
            public static int Config_MainDepAcc = 22;
            public static int Config_MainTotalDepAcc = 23;
            public static int Config_RealEstateExpanse = 27;
            public static int Config_RealEstateRevenue = 28;
            public static int Config_OwnersCoa = 29;
            public static int Config_ClientsCoa = 30;
            public static int Config_RentCoa = 53;

        }

        public class ChekTitle
        {
            public static string MDP = "تحصيل شيك بنكى";
            public static string RCK = "تحصيل شيك";
            public static string BTF = "تحويل بنكي";
            public static string WCK = "اصدار شيك";
        }

        /// <summary>
        /// To CHeck Serial Number
        /// </summary>
        public class DocType
        {
            public static string BankTransfer = "BnkTrans";
            public static string CashPayment = "CashPay";
            public static string CashReceive = "CashRecv";
            public static string CashTransfer = "CashTrans";
            public static string JournalEntry = "Entry";
            public static string CheckIssue = "CheckIss";
            public static string CheckReceive = "CheckRec";
        }

        public static DateTime GetFormatedDate(DateTime date)
        {   
            return new DateTime(date.Year, date.Month, date.Day);
        }

        public static DataTable GetDataSource(DataTable dt)
        {
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                //dr[1] = "--إختر--";
               
                dt.Rows.InsertAt(dr, 0);
                return dt;
            }
            else
            {
                return null;
            }
        }

        /// </summary>
        /// <param name="cmb">cmb</param>
        /// <param name="dt">dt</param>
        /// <param name="displayMember">displayMember</param>
        /// <param name="valueMember">valueMember</param>
        
    }
}
