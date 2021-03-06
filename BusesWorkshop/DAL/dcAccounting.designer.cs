﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5420
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusesWorkshop.DAL.Accounting
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="RealEstate")]
	public partial class dcAccountingDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public dcAccountingDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["RealEstateConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public dcAccountingDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public dcAccountingDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public dcAccountingDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public dcAccountingDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[Function(Name="dbo.usp_Ast_Location_Delete")]
		public int usp_Ast_Location_Delete([Parameter(Name="ID", DbType="Int")] System.Nullable<int> iD)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iD);
			return ((int)(result.ReturnValue));
		}
		
		[Function(Name="dbo.usp_Ast_Location_Insert")]
		public int usp_Ast_Location_Insert([Parameter(Name="LocationName", DbType="NVarChar(50)")] string locationName, [Parameter(Name="ParentID", DbType="Int")] System.Nullable<int> parentID, [Parameter(Name="IsDefault", DbType="Bit")] System.Nullable<bool> isDefault, [Parameter(Name="IsShown", DbType="Bit")] System.Nullable<bool> isShown, [Parameter(Name="IsMain", DbType="Bit")] System.Nullable<bool> isMain, [Parameter(Name="ComID", DbType="Int")] System.Nullable<int> comID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), locationName, parentID, isDefault, isShown, isMain, comID);
			return ((int)(result.ReturnValue));
		}
		
		[Function(Name="dbo.usp_Ast_Location_Update")]
		public int usp_Ast_Location_Update([Parameter(Name="ID", DbType="Int")] System.Nullable<int> iD, [Parameter(Name="LocationName", DbType="NVarChar(50)")] string locationName, [Parameter(Name="ParentID", DbType="Int")] System.Nullable<int> parentID, [Parameter(Name="IsDefault", DbType="Bit")] System.Nullable<bool> isDefault, [Parameter(Name="IsShown", DbType="Bit")] System.Nullable<bool> isShown, [Parameter(Name="IsMain", DbType="Bit")] System.Nullable<bool> isMain, [Parameter(Name="ComID", DbType="Int")] System.Nullable<int> comID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iD, locationName, parentID, isDefault, isShown, isMain, comID);
			return ((int)(result.ReturnValue));
		}
		
		[Function(Name="dbo.usp_AST_Select")]
		public ISingleResult<usp_AST_SelectResult> usp_AST_Select()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<usp_AST_SelectResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.usp_AstLocations_Select_ByUserID")]
		public ISingleResult<usp_AstLocations_Select_ByUserIDResult> usp_AstLocations_Select_ByUserID([Parameter(Name="UserID", DbType="Int")] System.Nullable<int> userID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID);
			return ((ISingleResult<usp_AstLocations_Select_ByUserIDResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.usp_COM_Insert")]
		public int usp_COM_Insert([Parameter(Name="COMName", DbType="NVarChar(250)")] string cOMName, [Parameter(Name="COMDescription", DbType="NVarChar(500)")] string cOMDescription, [Parameter(Name="COMAddress", DbType="NVarChar(500)")] string cOMAddress, [Parameter(Name="COMUrl", DbType="NVarChar(250)")] string cOMUrl, [Parameter(Name="COMLogo", DbType="Image")] System.Data.Linq.Binary cOMLogo, [Parameter(Name="COMPhone", DbType="VarChar(50)")] string cOMPhone, [Parameter(Name="COMFax", DbType="VarChar(50)")] string cOMFax, [Parameter(Name="COMStatus", DbType="Bit")] System.Nullable<bool> cOMStatus, [Parameter(Name="COM_ID", DbType="Int")] System.Nullable<int> cOM_ID, [Parameter(Name="DocFooter", DbType="NVarChar(500)")] string docFooter, [Parameter(Name="Accountant_Name", DbType="NVarChar(250)")] string accountant_Name, [Parameter(Name="FM_Name", DbType="NVarChar(250)")] string fM_Name, [Parameter(Name="GM_Name", DbType="NVarChar(250)")] string gM_Name)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cOMName, cOMDescription, cOMAddress, cOMUrl, cOMLogo, cOMPhone, cOMFax, cOMStatus, cOM_ID, docFooter, accountant_Name, fM_Name, gM_Name);
			return ((int)(result.ReturnValue));
		}
		
		[Function(Name="dbo.usp_BCD_Update")]
		public int usp_BCD_Update([Parameter(Name="BCDID", DbType="Int")] System.Nullable<int> bCDID, [Parameter(Name="BCDValue", DbType="NVarChar(50)")] string bCDValue)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), bCDID, bCDValue);
			return ((int)(result.ReturnValue));
		}
		
		[Function(Name="dbo.usp_COM_Select")]
		public ISingleResult<usp_COM_SelectResult> usp_COM_Select([Parameter(Name="COMID", DbType="Int")] System.Nullable<int> cOMID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cOMID);
			return ((ISingleResult<usp_COM_SelectResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.usp_COM_Update")]
		public int usp_COM_Update([Parameter(Name="COMID", DbType="Int")] System.Nullable<int> cOMID, [Parameter(Name="COMName", DbType="NVarChar(250)")] string cOMName, [Parameter(Name="COMDescription", DbType="NVarChar(500)")] string cOMDescription, [Parameter(Name="COMAddress", DbType="NVarChar(500)")] string cOMAddress, [Parameter(Name="COMUrl", DbType="NVarChar(250)")] string cOMUrl, [Parameter(Name="COMLogo", DbType="Image")] System.Data.Linq.Binary cOMLogo, [Parameter(Name="COMPhone", DbType="VarChar(50)")] string cOMPhone, [Parameter(Name="COMFax", DbType="VarChar(50)")] string cOMFax, [Parameter(Name="DocFooter", DbType="NVarChar(500)")] string docFooter, [Parameter(Name="Accountant_Name", DbType="NVarChar(100)")] string accountant_Name, [Parameter(Name="FM_Name", DbType="NVarChar(100)")] string fM_Name, [Parameter(Name="GM_Name", DbType="NVarChar(100)")] string gM_Name)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cOMID, cOMName, cOMDescription, cOMAddress, cOMUrl, cOMLogo, cOMPhone, cOMFax, docFooter, accountant_Name, fM_Name, gM_Name);
			return ((int)(result.ReturnValue));
		}
		
		[Function(Name="dbo.usp_FSY_Select")]
		public ISingleResult<usp_FSY_SelectResult> usp_FSY_Select([Parameter(Name="FSYID", DbType="Int")] System.Nullable<int> fSYID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), fSYID);
			return ((ISingleResult<usp_FSY_SelectResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.Usp_Company_Select_ByUserID")]
		public ISingleResult<Usp_Company_Select_ByUserIDResult> Usp_Company_Select_ByUserID([Parameter(DbType="Int")] System.Nullable<int> userID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID);
			return ((ISingleResult<Usp_Company_Select_ByUserIDResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.Fn_DefaultCompany", IsComposable=true)]
		public string Fn_DefaultCompany([Parameter(Name="UserID", DbType="Int")] System.Nullable<int> userID)
		{
			return ((string)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID).ReturnValue));
		}
		
		[Function(Name="dbo.usp_CON_USR_Update")]
		public int usp_CON_USR_Update([Parameter(Name="CON_ID", DbType="Int")] System.Nullable<int> cON_ID, [Parameter(Name="CONUserName", DbType="NVarChar(50)")] string cONUserName, [Parameter(Name="CONPassword", DbType="NVarChar(50)")] string cONPassword, [Parameter(Name="CONConfirmPassword", DbType="NVarChar(50)")] string cONConfirmPassword, [Parameter(Name="CONPasswordHint", DbType="NVarChar(50)")] string cONPasswordHint, [Parameter(Name="ComID", DbType="Int")] System.Nullable<int> comID, [Parameter(Name="Con_USR_ID", DbType="Int")] System.Nullable<int> con_USR_ID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cON_ID, cONUserName, cONPassword, cONConfirmPassword, cONPasswordHint, comID, con_USR_ID);
			return ((int)(result.ReturnValue));
		}
		
		[Function(Name="dbo.usp_FSY_Insert")]
		public int usp_FSY_Insert([Parameter(Name="FSYStartDate", DbType="Date")] System.Nullable<System.DateTime> fSYStartDate, [Parameter(Name="FSYEndDate", DbType="Date")] System.Nullable<System.DateTime> fSYEndDate, [Parameter(Name="FSYStatus", DbType="Bit")] System.Nullable<bool> fSYStatus, [Parameter(Name="COM_ID", DbType="Int")] System.Nullable<int> cOM_ID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), fSYStartDate, fSYEndDate, fSYStatus, cOM_ID);
			return ((int)(result.ReturnValue));
		}
		
		[Function(Name="dbo.usp_CON_COM_Update")]
		public int usp_CON_COM_Update([Parameter(Name="CONID", DbType="Int")] System.Nullable<int> cONID, [Parameter(Name="CMP_ID", DbType="Int")] System.Nullable<int> cMP_ID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cONID, cMP_ID);
			return ((int)(result.ReturnValue));
		}
	}
	
	public partial class usp_AST_SelectResult
	{
		
		private int _ID;
		
		private string _Name;
		
		private int _CAT_ID;
		
		private string _CatName;
		
		private int _COA_ID;
		
		private string _COADescription;
		
		private int _DEP_ID;
		
		private int _Age;
		
		private decimal _Percent;
		
		private decimal _ScrapValue;
		
		private decimal _DEPAnnuallValue;
		
		private string _Notes;
		
		private string _Details;
		
		private bool _IsActive;
		
		private string _PurchaseDate;
		
		private System.Nullable<decimal> _PurchaseValue;
		
		private System.Nullable<int> _CON_ID;
		
		private System.Nullable<int> _Location;
		
		private string _DEPStartDate;
		
		private System.Nullable<int> _Status;
		
		private System.Nullable<int> _OppositeAccount_ID;
		
		private System.Nullable<int> _OpenDep;
		
		private System.Nullable<bool> _IsDep;
		
		private System.Nullable<decimal> _PremDep;
		
		private string _AST_Description;
		
		private System.Nullable<int> _JOB_ID;
		
		private System.Nullable<int> _AST_Dep_COA_ID;
		
		private System.Nullable<int> _AST_Dep_Total_COA_ID;
		
		private string _DOC_NUM;
		
		private System.Nullable<int> _VEN_ID;
		
		private System.Nullable<decimal> _AST_PeriodUseValue;
		
		private string _AST_DEP_LastDate;
		
		private System.Nullable<int> _QTY;
		
		private System.Nullable<int> _DepYears;
		
		private System.Nullable<decimal> _Disposals_Value;
		
		private string _Disposals_Date;
		
		private string _PlateNumber;
		
		private string _Model;
		
		private string _Color;
		
		private string _CountryOfManufacture;
		
		private string _YearOfManufacture;
		
		private string _Code;
		
		private string _LicenseEndDate;
		
		private System.Nullable<int> _ComID;
		
		private System.Nullable<long> _GJHID;
		
		public usp_AST_SelectResult()
		{
		}
		
		[Column(Storage="_ID", DbType="Int NOT NULL")]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this._ID = value;
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this._Name = value;
				}
			}
		}
		
		[Column(Storage="_CAT_ID", DbType="Int NOT NULL")]
		public int CAT_ID
		{
			get
			{
				return this._CAT_ID;
			}
			set
			{
				if ((this._CAT_ID != value))
				{
					this._CAT_ID = value;
				}
			}
		}
		
		[Column(Storage="_CatName", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string CatName
		{
			get
			{
				return this._CatName;
			}
			set
			{
				if ((this._CatName != value))
				{
					this._CatName = value;
				}
			}
		}
		
		[Column(Storage="_COA_ID", DbType="Int NOT NULL")]
		public int COA_ID
		{
			get
			{
				return this._COA_ID;
			}
			set
			{
				if ((this._COA_ID != value))
				{
					this._COA_ID = value;
				}
			}
		}
		
		[Column(Storage="_COADescription", DbType="NVarChar(250)")]
		public string COADescription
		{
			get
			{
				return this._COADescription;
			}
			set
			{
				if ((this._COADescription != value))
				{
					this._COADescription = value;
				}
			}
		}
		
		[Column(Storage="_DEP_ID", DbType="Int NOT NULL")]
		public int DEP_ID
		{
			get
			{
				return this._DEP_ID;
			}
			set
			{
				if ((this._DEP_ID != value))
				{
					this._DEP_ID = value;
				}
			}
		}
		
		[Column(Storage="_Age", DbType="Int NOT NULL")]
		public int Age
		{
			get
			{
				return this._Age;
			}
			set
			{
				if ((this._Age != value))
				{
					this._Age = value;
				}
			}
		}
		
		[Column(Name="[Percent]", Storage="_Percent", DbType="Decimal(0,0) NOT NULL")]
		public decimal Percent
		{
			get
			{
				return this._Percent;
			}
			set
			{
				if ((this._Percent != value))
				{
					this._Percent = value;
				}
			}
		}
		
		[Column(Storage="_ScrapValue", DbType="Money NOT NULL")]
		public decimal ScrapValue
		{
			get
			{
				return this._ScrapValue;
			}
			set
			{
				if ((this._ScrapValue != value))
				{
					this._ScrapValue = value;
				}
			}
		}
		
		[Column(Storage="_DEPAnnuallValue", DbType="Money NOT NULL")]
		public decimal DEPAnnuallValue
		{
			get
			{
				return this._DEPAnnuallValue;
			}
			set
			{
				if ((this._DEPAnnuallValue != value))
				{
					this._DEPAnnuallValue = value;
				}
			}
		}
		
		[Column(Storage="_Notes", DbType="NVarChar(500)")]
		public string Notes
		{
			get
			{
				return this._Notes;
			}
			set
			{
				if ((this._Notes != value))
				{
					this._Notes = value;
				}
			}
		}
		
		[Column(Storage="_Details", DbType="NVarChar(500)")]
		public string Details
		{
			get
			{
				return this._Details;
			}
			set
			{
				if ((this._Details != value))
				{
					this._Details = value;
				}
			}
		}
		
		[Column(Storage="_IsActive", DbType="Bit NOT NULL")]
		public bool IsActive
		{
			get
			{
				return this._IsActive;
			}
			set
			{
				if ((this._IsActive != value))
				{
					this._IsActive = value;
				}
			}
		}
		
		[Column(Storage="_PurchaseDate", DbType="VarChar(10)")]
		public string PurchaseDate
		{
			get
			{
				return this._PurchaseDate;
			}
			set
			{
				if ((this._PurchaseDate != value))
				{
					this._PurchaseDate = value;
				}
			}
		}
		
		[Column(Storage="_PurchaseValue", DbType="Money")]
		public System.Nullable<decimal> PurchaseValue
		{
			get
			{
				return this._PurchaseValue;
			}
			set
			{
				if ((this._PurchaseValue != value))
				{
					this._PurchaseValue = value;
				}
			}
		}
		
		[Column(Storage="_CON_ID", DbType="Int")]
		public System.Nullable<int> CON_ID
		{
			get
			{
				return this._CON_ID;
			}
			set
			{
				if ((this._CON_ID != value))
				{
					this._CON_ID = value;
				}
			}
		}
		
		[Column(Storage="_Location", DbType="Int")]
		public System.Nullable<int> Location
		{
			get
			{
				return this._Location;
			}
			set
			{
				if ((this._Location != value))
				{
					this._Location = value;
				}
			}
		}
		
		[Column(Storage="_DEPStartDate", DbType="VarChar(10)")]
		public string DEPStartDate
		{
			get
			{
				return this._DEPStartDate;
			}
			set
			{
				if ((this._DEPStartDate != value))
				{
					this._DEPStartDate = value;
				}
			}
		}
		
		[Column(Storage="_Status", DbType="Int")]
		public System.Nullable<int> Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this._Status = value;
				}
			}
		}
		
		[Column(Storage="_OppositeAccount_ID", DbType="Int")]
		public System.Nullable<int> OppositeAccount_ID
		{
			get
			{
				return this._OppositeAccount_ID;
			}
			set
			{
				if ((this._OppositeAccount_ID != value))
				{
					this._OppositeAccount_ID = value;
				}
			}
		}
		
		[Column(Storage="_OpenDep", DbType="Int")]
		public System.Nullable<int> OpenDep
		{
			get
			{
				return this._OpenDep;
			}
			set
			{
				if ((this._OpenDep != value))
				{
					this._OpenDep = value;
				}
			}
		}
		
		[Column(Storage="_IsDep", DbType="Bit")]
		public System.Nullable<bool> IsDep
		{
			get
			{
				return this._IsDep;
			}
			set
			{
				if ((this._IsDep != value))
				{
					this._IsDep = value;
				}
			}
		}
		
		[Column(Storage="_PremDep", DbType="Money")]
		public System.Nullable<decimal> PremDep
		{
			get
			{
				return this._PremDep;
			}
			set
			{
				if ((this._PremDep != value))
				{
					this._PremDep = value;
				}
			}
		}
		
		[Column(Storage="_AST_Description", DbType="NVarChar(500)")]
		public string AST_Description
		{
			get
			{
				return this._AST_Description;
			}
			set
			{
				if ((this._AST_Description != value))
				{
					this._AST_Description = value;
				}
			}
		}
		
		[Column(Storage="_JOB_ID", DbType="Int")]
		public System.Nullable<int> JOB_ID
		{
			get
			{
				return this._JOB_ID;
			}
			set
			{
				if ((this._JOB_ID != value))
				{
					this._JOB_ID = value;
				}
			}
		}
		
		[Column(Storage="_AST_Dep_COA_ID", DbType="Int")]
		public System.Nullable<int> AST_Dep_COA_ID
		{
			get
			{
				return this._AST_Dep_COA_ID;
			}
			set
			{
				if ((this._AST_Dep_COA_ID != value))
				{
					this._AST_Dep_COA_ID = value;
				}
			}
		}
		
		[Column(Storage="_AST_Dep_Total_COA_ID", DbType="Int")]
		public System.Nullable<int> AST_Dep_Total_COA_ID
		{
			get
			{
				return this._AST_Dep_Total_COA_ID;
			}
			set
			{
				if ((this._AST_Dep_Total_COA_ID != value))
				{
					this._AST_Dep_Total_COA_ID = value;
				}
			}
		}
		
		[Column(Storage="_DOC_NUM", DbType="NVarChar(50)")]
		public string DOC_NUM
		{
			get
			{
				return this._DOC_NUM;
			}
			set
			{
				if ((this._DOC_NUM != value))
				{
					this._DOC_NUM = value;
				}
			}
		}
		
		[Column(Storage="_VEN_ID", DbType="Int")]
		public System.Nullable<int> VEN_ID
		{
			get
			{
				return this._VEN_ID;
			}
			set
			{
				if ((this._VEN_ID != value))
				{
					this._VEN_ID = value;
				}
			}
		}
		
		[Column(Storage="_AST_PeriodUseValue", DbType="Money")]
		public System.Nullable<decimal> AST_PeriodUseValue
		{
			get
			{
				return this._AST_PeriodUseValue;
			}
			set
			{
				if ((this._AST_PeriodUseValue != value))
				{
					this._AST_PeriodUseValue = value;
				}
			}
		}
		
		[Column(Storage="_AST_DEP_LastDate", DbType="VarChar(10)")]
		public string AST_DEP_LastDate
		{
			get
			{
				return this._AST_DEP_LastDate;
			}
			set
			{
				if ((this._AST_DEP_LastDate != value))
				{
					this._AST_DEP_LastDate = value;
				}
			}
		}
		
		[Column(Storage="_QTY", DbType="Int")]
		public System.Nullable<int> QTY
		{
			get
			{
				return this._QTY;
			}
			set
			{
				if ((this._QTY != value))
				{
					this._QTY = value;
				}
			}
		}
		
		[Column(Storage="_DepYears", DbType="Int")]
		public System.Nullable<int> DepYears
		{
			get
			{
				return this._DepYears;
			}
			set
			{
				if ((this._DepYears != value))
				{
					this._DepYears = value;
				}
			}
		}
		
		[Column(Storage="_Disposals_Value", DbType="Decimal(0,0)")]
		public System.Nullable<decimal> Disposals_Value
		{
			get
			{
				return this._Disposals_Value;
			}
			set
			{
				if ((this._Disposals_Value != value))
				{
					this._Disposals_Value = value;
				}
			}
		}
		
		[Column(Storage="_Disposals_Date", DbType="VarChar(10)")]
		public string Disposals_Date
		{
			get
			{
				return this._Disposals_Date;
			}
			set
			{
				if ((this._Disposals_Date != value))
				{
					this._Disposals_Date = value;
				}
			}
		}
		
		[Column(Storage="_PlateNumber", DbType="NVarChar(500)")]
		public string PlateNumber
		{
			get
			{
				return this._PlateNumber;
			}
			set
			{
				if ((this._PlateNumber != value))
				{
					this._PlateNumber = value;
				}
			}
		}
		
		[Column(Storage="_Model", DbType="NVarChar(500)")]
		public string Model
		{
			get
			{
				return this._Model;
			}
			set
			{
				if ((this._Model != value))
				{
					this._Model = value;
				}
			}
		}
		
		[Column(Storage="_Color", DbType="NVarChar(500)")]
		public string Color
		{
			get
			{
				return this._Color;
			}
			set
			{
				if ((this._Color != value))
				{
					this._Color = value;
				}
			}
		}
		
		[Column(Storage="_CountryOfManufacture", DbType="NVarChar(500)")]
		public string CountryOfManufacture
		{
			get
			{
				return this._CountryOfManufacture;
			}
			set
			{
				if ((this._CountryOfManufacture != value))
				{
					this._CountryOfManufacture = value;
				}
			}
		}
		
		[Column(Storage="_YearOfManufacture", DbType="VarChar(10)")]
		public string YearOfManufacture
		{
			get
			{
				return this._YearOfManufacture;
			}
			set
			{
				if ((this._YearOfManufacture != value))
				{
					this._YearOfManufacture = value;
				}
			}
		}
		
		[Column(Storage="_Code", DbType="NVarChar(500)")]
		public string Code
		{
			get
			{
				return this._Code;
			}
			set
			{
				if ((this._Code != value))
				{
					this._Code = value;
				}
			}
		}
		
		[Column(Storage="_LicenseEndDate", DbType="VarChar(10)")]
		public string LicenseEndDate
		{
			get
			{
				return this._LicenseEndDate;
			}
			set
			{
				if ((this._LicenseEndDate != value))
				{
					this._LicenseEndDate = value;
				}
			}
		}
		
		[Column(Storage="_ComID", DbType="Int")]
		public System.Nullable<int> ComID
		{
			get
			{
				return this._ComID;
			}
			set
			{
				if ((this._ComID != value))
				{
					this._ComID = value;
				}
			}
		}
		
		[Column(Storage="_GJHID", DbType="BigInt")]
		public System.Nullable<long> GJHID
		{
			get
			{
				return this._GJHID;
			}
			set
			{
				if ((this._GJHID != value))
				{
					this._GJHID = value;
				}
			}
		}
	}
	
	public partial class usp_AstLocations_Select_ByUserIDResult
	{
		
		private int _ID;
		
		private string _LocationName;
		
		private System.Nullable<int> _ParentID;
		
		private string _ParentName;
		
		private System.Nullable<bool> _IsDefault;
		
		private System.Nullable<bool> _IsShown;
		
		private System.Nullable<bool> _IsMain;
		
		private System.Nullable<int> _Com_ID;
		
		private string _ComName;
		
		public usp_AstLocations_Select_ByUserIDResult()
		{
		}
		
		[Column(Storage="_ID", DbType="Int NOT NULL")]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this._ID = value;
				}
			}
		}
		
		[Column(Storage="_LocationName", DbType="NVarChar(50)")]
		public string LocationName
		{
			get
			{
				return this._LocationName;
			}
			set
			{
				if ((this._LocationName != value))
				{
					this._LocationName = value;
				}
			}
		}
		
		[Column(Storage="_ParentID", DbType="Int")]
		public System.Nullable<int> ParentID
		{
			get
			{
				return this._ParentID;
			}
			set
			{
				if ((this._ParentID != value))
				{
					this._ParentID = value;
				}
			}
		}
		
		[Column(Storage="_ParentName", DbType="NVarChar(50)")]
		public string ParentName
		{
			get
			{
				return this._ParentName;
			}
			set
			{
				if ((this._ParentName != value))
				{
					this._ParentName = value;
				}
			}
		}
		
		[Column(Storage="_IsDefault", DbType="Bit")]
		public System.Nullable<bool> IsDefault
		{
			get
			{
				return this._IsDefault;
			}
			set
			{
				if ((this._IsDefault != value))
				{
					this._IsDefault = value;
				}
			}
		}
		
		[Column(Storage="_IsShown", DbType="Bit")]
		public System.Nullable<bool> IsShown
		{
			get
			{
				return this._IsShown;
			}
			set
			{
				if ((this._IsShown != value))
				{
					this._IsShown = value;
				}
			}
		}
		
		[Column(Storage="_IsMain", DbType="Bit")]
		public System.Nullable<bool> IsMain
		{
			get
			{
				return this._IsMain;
			}
			set
			{
				if ((this._IsMain != value))
				{
					this._IsMain = value;
				}
			}
		}
		
		[Column(Storage="_Com_ID", DbType="Int")]
		public System.Nullable<int> Com_ID
		{
			get
			{
				return this._Com_ID;
			}
			set
			{
				if ((this._Com_ID != value))
				{
					this._Com_ID = value;
				}
			}
		}
		
		[Column(Storage="_ComName", DbType="NVarChar(250) NOT NULL", CanBeNull=false)]
		public string ComName
		{
			get
			{
				return this._ComName;
			}
			set
			{
				if ((this._ComName != value))
				{
					this._ComName = value;
				}
			}
		}
	}
	
	public partial class usp_COM_SelectResult
	{
		
		private int _COMID;
		
		private System.Nullable<int> _ParentID;
		
		private string _COMName;
		
		private string _COMDescription;
		
		private string _COMAddress;
		
		private string _COMUrl;
		
		private System.Data.Linq.Binary _COMLogo;
		
		private string _COMPhone;
		
		private string _COMFax;
		
		private bool _COMStatus;
		
		private int _COM_ID;
		
		private string _DocFooter;
		
		private string _Accountant_Name;
		
		private string _FM_Name;
		
		private string _GM_Name;
		
		public usp_COM_SelectResult()
		{
		}
		
		[Column(Storage="_COMID", DbType="Int NOT NULL")]
		public int COMID
		{
			get
			{
				return this._COMID;
			}
			set
			{
				if ((this._COMID != value))
				{
					this._COMID = value;
				}
			}
		}
		
		[Column(Storage="_ParentID", DbType="Int")]
		public System.Nullable<int> ParentID
		{
			get
			{
				return this._ParentID;
			}
			set
			{
				if ((this._ParentID != value))
				{
					this._ParentID = value;
				}
			}
		}
		
		[Column(Storage="_COMName", DbType="NVarChar(250) NOT NULL", CanBeNull=false)]
		public string COMName
		{
			get
			{
				return this._COMName;
			}
			set
			{
				if ((this._COMName != value))
				{
					this._COMName = value;
				}
			}
		}
		
		[Column(Storage="_COMDescription", DbType="NVarChar(500) NOT NULL", CanBeNull=false)]
		public string COMDescription
		{
			get
			{
				return this._COMDescription;
			}
			set
			{
				if ((this._COMDescription != value))
				{
					this._COMDescription = value;
				}
			}
		}
		
		[Column(Storage="_COMAddress", DbType="NVarChar(500) NOT NULL", CanBeNull=false)]
		public string COMAddress
		{
			get
			{
				return this._COMAddress;
			}
			set
			{
				if ((this._COMAddress != value))
				{
					this._COMAddress = value;
				}
			}
		}
		
		[Column(Storage="_COMUrl", DbType="NVarChar(250) NOT NULL", CanBeNull=false)]
		public string COMUrl
		{
			get
			{
				return this._COMUrl;
			}
			set
			{
				if ((this._COMUrl != value))
				{
					this._COMUrl = value;
				}
			}
		}
		
		[Column(Storage="_COMLogo", DbType="Image")]
		public System.Data.Linq.Binary COMLogo
		{
			get
			{
				return this._COMLogo;
			}
			set
			{
				if ((this._COMLogo != value))
				{
					this._COMLogo = value;
				}
			}
		}
		
		[Column(Storage="_COMPhone", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string COMPhone
		{
			get
			{
				return this._COMPhone;
			}
			set
			{
				if ((this._COMPhone != value))
				{
					this._COMPhone = value;
				}
			}
		}
		
		[Column(Storage="_COMFax", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string COMFax
		{
			get
			{
				return this._COMFax;
			}
			set
			{
				if ((this._COMFax != value))
				{
					this._COMFax = value;
				}
			}
		}
		
		[Column(Storage="_COMStatus", DbType="Bit NOT NULL")]
		public bool COMStatus
		{
			get
			{
				return this._COMStatus;
			}
			set
			{
				if ((this._COMStatus != value))
				{
					this._COMStatus = value;
				}
			}
		}
		
		[Column(Storage="_COM_ID", DbType="Int NOT NULL")]
		public int COM_ID
		{
			get
			{
				return this._COM_ID;
			}
			set
			{
				if ((this._COM_ID != value))
				{
					this._COM_ID = value;
				}
			}
		}
		
		[Column(Storage="_DocFooter", DbType="NVarChar(500)")]
		public string DocFooter
		{
			get
			{
				return this._DocFooter;
			}
			set
			{
				if ((this._DocFooter != value))
				{
					this._DocFooter = value;
				}
			}
		}
		
		[Column(Storage="_Accountant_Name", DbType="NVarChar(250)")]
		public string Accountant_Name
		{
			get
			{
				return this._Accountant_Name;
			}
			set
			{
				if ((this._Accountant_Name != value))
				{
					this._Accountant_Name = value;
				}
			}
		}
		
		[Column(Storage="_FM_Name", DbType="NVarChar(250)")]
		public string FM_Name
		{
			get
			{
				return this._FM_Name;
			}
			set
			{
				if ((this._FM_Name != value))
				{
					this._FM_Name = value;
				}
			}
		}
		
		[Column(Storage="_GM_Name", DbType="NVarChar(250)")]
		public string GM_Name
		{
			get
			{
				return this._GM_Name;
			}
			set
			{
				if ((this._GM_Name != value))
				{
					this._GM_Name = value;
				}
			}
		}
	}
	
	public partial class usp_FSY_SelectResult
	{
		
		private int _FSYID;
		
		private System.DateTime _FSYStartDate;
		
		private System.DateTime _FSYEndDate;
		
		private bool _FSYStatus;
		
		private int _COM_ID;
		
		private System.Nullable<bool> _Is_Open;
		
		private string _Fsy_Name;
		
		private string _FsyPrefix;
		
		public usp_FSY_SelectResult()
		{
		}
		
		[Column(Storage="_FSYID", DbType="Int NOT NULL")]
		public int FSYID
		{
			get
			{
				return this._FSYID;
			}
			set
			{
				if ((this._FSYID != value))
				{
					this._FSYID = value;
				}
			}
		}
		
		[Column(Storage="_FSYStartDate", DbType="Date NOT NULL")]
		public System.DateTime FSYStartDate
		{
			get
			{
				return this._FSYStartDate;
			}
			set
			{
				if ((this._FSYStartDate != value))
				{
					this._FSYStartDate = value;
				}
			}
		}
		
		[Column(Storage="_FSYEndDate", DbType="Date NOT NULL")]
		public System.DateTime FSYEndDate
		{
			get
			{
				return this._FSYEndDate;
			}
			set
			{
				if ((this._FSYEndDate != value))
				{
					this._FSYEndDate = value;
				}
			}
		}
		
		[Column(Storage="_FSYStatus", DbType="Bit NOT NULL")]
		public bool FSYStatus
		{
			get
			{
				return this._FSYStatus;
			}
			set
			{
				if ((this._FSYStatus != value))
				{
					this._FSYStatus = value;
				}
			}
		}
		
		[Column(Storage="_COM_ID", DbType="Int NOT NULL")]
		public int COM_ID
		{
			get
			{
				return this._COM_ID;
			}
			set
			{
				if ((this._COM_ID != value))
				{
					this._COM_ID = value;
				}
			}
		}
		
		[Column(Storage="_Is_Open", DbType="Bit")]
		public System.Nullable<bool> Is_Open
		{
			get
			{
				return this._Is_Open;
			}
			set
			{
				if ((this._Is_Open != value))
				{
					this._Is_Open = value;
				}
			}
		}
		
		[Column(Storage="_Fsy_Name", DbType="NVarChar(50)")]
		public string Fsy_Name
		{
			get
			{
				return this._Fsy_Name;
			}
			set
			{
				if ((this._Fsy_Name != value))
				{
					this._Fsy_Name = value;
				}
			}
		}
		
		[Column(Storage="_FsyPrefix", DbType="NVarChar(10)")]
		public string FsyPrefix
		{
			get
			{
				return this._FsyPrefix;
			}
			set
			{
				if ((this._FsyPrefix != value))
				{
					this._FsyPrefix = value;
				}
			}
		}
	}
	
	public partial class Usp_Company_Select_ByUserIDResult
	{
		
		private int _COMID;
		
		private string _COMName;
		
		private string _Accountant_Name;
		
		private string _FM_Name;
		
		private string _GM_Name;
		
		public Usp_Company_Select_ByUserIDResult()
		{
		}
		
		[Column(Storage="_COMID", DbType="Int NOT NULL")]
		public int COMID
		{
			get
			{
				return this._COMID;
			}
			set
			{
				if ((this._COMID != value))
				{
					this._COMID = value;
				}
			}
		}
		
		[Column(Storage="_COMName", DbType="NVarChar(250) NOT NULL", CanBeNull=false)]
		public string COMName
		{
			get
			{
				return this._COMName;
			}
			set
			{
				if ((this._COMName != value))
				{
					this._COMName = value;
				}
			}
		}
		
		[Column(Storage="_Accountant_Name", DbType="NVarChar(250)")]
		public string Accountant_Name
		{
			get
			{
				return this._Accountant_Name;
			}
			set
			{
				if ((this._Accountant_Name != value))
				{
					this._Accountant_Name = value;
				}
			}
		}
		
		[Column(Storage="_FM_Name", DbType="NVarChar(250)")]
		public string FM_Name
		{
			get
			{
				return this._FM_Name;
			}
			set
			{
				if ((this._FM_Name != value))
				{
					this._FM_Name = value;
				}
			}
		}
		
		[Column(Storage="_GM_Name", DbType="NVarChar(250)")]
		public string GM_Name
		{
			get
			{
				return this._GM_Name;
			}
			set
			{
				if ((this._GM_Name != value))
				{
					this._GM_Name = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
