﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace endo.io
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Endo.IO")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertBasalRate(BasalRate instance);
    partial void UpdateBasalRate(BasalRate instance);
    partial void DeleteBasalRate(BasalRate instance);
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::Endo.IO.Properties.Settings.Default.Endo_IOConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<BasalRate> BasalRates
		{
			get
			{
				return this.GetTable<BasalRate>();
			}
		}
		
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.BasalRates")]
	public partial class BasalRate : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Hour;
		
		private System.Nullable<decimal> _Rate;
		
		private string _UserName;
		
		private EntityRef<User> _User;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnHourChanging(int value);
    partial void OnHourChanged();
    partial void OnRateChanging(System.Nullable<decimal> value);
    partial void OnRateChanged();
    partial void OnUserNameChanging(string value);
    partial void OnUserNameChanged();
    #endregion
		
		public BasalRate()
		{
			this._User = default(EntityRef<User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Hour", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Hour
		{
			get
			{
				return this._Hour;
			}
			set
			{
				if ((this._Hour != value))
				{
					this.OnHourChanging(value);
					this.SendPropertyChanging();
					this._Hour = value;
					this.SendPropertyChanged("Hour");
					this.OnHourChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Rate", DbType="Decimal(3,2)")]
		public System.Nullable<decimal> Rate
		{
			get
			{
				return this._Rate;
			}
			set
			{
				if ((this._Rate != value))
				{
					this.OnRateChanging(value);
					this.SendPropertyChanging();
					this._Rate = value;
					this.SendPropertyChanged("Rate");
					this.OnRateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserName", DbType="VarChar(15) NOT NULL", CanBeNull=false)]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if ((this._UserName != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserNameChanging(value);
					this.SendPropertyChanging();
					this._UserName = value;
					this.SendPropertyChanged("UserName");
					this.OnUserNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_BasalRate", Storage="_User", ThisKey="UserName", OtherKey="UserName", IsForeignKey=true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.BasalRates.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.BasalRates.Add(this);
						this._UserName = value.UserName;
					}
					else
					{
						this._UserName = default(string);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Users")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _UserName;
		
		private string _Password;
		
		private System.DateTime _CreateDate;
		
		private System.Nullable<decimal> _MaxBasalRate;
		
		private System.Nullable<int> _TargetBg;
		
		private System.Nullable<int> _HighBg;
		
		private System.Nullable<int> _LowBg;
		
		private string _FirstName;
		
		private EntitySet<BasalRate> _BasalRates;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserNameChanging(string value);
    partial void OnUserNameChanged();
    partial void OnPasswordChanging(string value);
    partial void OnPasswordChanged();
    partial void OnCreateDateChanging(System.DateTime value);
    partial void OnCreateDateChanged();
    partial void OnMaxBasalRateChanging(System.Nullable<decimal> value);
    partial void OnMaxBasalRateChanged();
    partial void OnTargetBgChanging(System.Nullable<int> value);
    partial void OnTargetBgChanged();
    partial void OnHighBgChanging(System.Nullable<int> value);
    partial void OnHighBgChanged();
    partial void OnLowBgChanging(System.Nullable<int> value);
    partial void OnLowBgChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    #endregion
		
		public User()
		{
			this._BasalRates = new EntitySet<BasalRate>(new Action<BasalRate>(this.attach_BasalRates), new Action<BasalRate>(this.detach_BasalRates));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserName", DbType="VarChar(15) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if ((this._UserName != value))
				{
					this.OnUserNameChanging(value);
					this.SendPropertyChanging();
					this._UserName = value;
					this.SendPropertyChanged("UserName");
					this.OnUserNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="VarChar(25) NOT NULL", CanBeNull=false)]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._Password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateDate", DbType="DateTime NOT NULL")]
		public System.DateTime CreateDate
		{
			get
			{
				return this._CreateDate;
			}
			set
			{
				if ((this._CreateDate != value))
				{
					this.OnCreateDateChanging(value);
					this.SendPropertyChanging();
					this._CreateDate = value;
					this.SendPropertyChanged("CreateDate");
					this.OnCreateDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MaxBasalRate", DbType="Decimal(3,2)")]
		public System.Nullable<decimal> MaxBasalRate
		{
			get
			{
				return this._MaxBasalRate;
			}
			set
			{
				if ((this._MaxBasalRate != value))
				{
					this.OnMaxBasalRateChanging(value);
					this.SendPropertyChanging();
					this._MaxBasalRate = value;
					this.SendPropertyChanged("MaxBasalRate");
					this.OnMaxBasalRateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TargetBg", DbType="Int")]
		public System.Nullable<int> TargetBg
		{
			get
			{
				return this._TargetBg;
			}
			set
			{
				if ((this._TargetBg != value))
				{
					this.OnTargetBgChanging(value);
					this.SendPropertyChanging();
					this._TargetBg = value;
					this.SendPropertyChanged("TargetBg");
					this.OnTargetBgChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HighBg", DbType="Int")]
		public System.Nullable<int> HighBg
		{
			get
			{
				return this._HighBg;
			}
			set
			{
				if ((this._HighBg != value))
				{
					this.OnHighBgChanging(value);
					this.SendPropertyChanging();
					this._HighBg = value;
					this.SendPropertyChanged("HighBg");
					this.OnHighBgChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LowBg", DbType="Int")]
		public System.Nullable<int> LowBg
		{
			get
			{
				return this._LowBg;
			}
			set
			{
				if ((this._LowBg != value))
				{
					this.OnLowBgChanging(value);
					this.SendPropertyChanging();
					this._LowBg = value;
					this.SendPropertyChanged("LowBg");
					this.OnLowBgChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="VarChar(25) NOT NULL", CanBeNull=false)]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_BasalRate", Storage="_BasalRates", ThisKey="UserName", OtherKey="UserName")]
		public EntitySet<BasalRate> BasalRates
		{
			get
			{
				return this._BasalRates;
			}
			set
			{
				this._BasalRates.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_BasalRates(BasalRate entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_BasalRates(BasalRate entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
	}
}
#pragma warning restore 1591