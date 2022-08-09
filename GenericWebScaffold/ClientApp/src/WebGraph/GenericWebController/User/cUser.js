import { JSTypeOperator, DebugAlert, Class, Interface, Abstract, ObjectTypes, cListForBase } from "../GenericCoreGraph//ClassFramework/Class"

var cUser = Class(cBaseObject,// IMessageBoxReceiver, 
{
	ObjectType: ObjectTypes.Get("cUser")
	, Id : 0
	, Email : ""
	, Name : ""
	, LastName : ""
	, Gender : ""
	, BirtdayTimeStamp : 0
	, Birtday : null
	, Birtday_Day : 0
	, Birtday_Month : 0
	, Birtday_Year : 0
	,
	constructor: function (_Id, _Email, _Name, _LastName, _Gender, _Birtday)
	{
		cUser.BaseObject.constructor.call(this);
		this.Id = _Id;
		this.Email = _Email;
		this.Name = _Name;
		this.LastName = _LastName;
		this.Gender = _Gender;
		this.BirtdayTimeStamp = _Birtday;
		this.Birtday = new Date(_Birtday);
		this.Birtday_Day = this.Birtday.getDate();
		this.Birtday_Month = this.Birtday.getMonth() + 1;
		this.Birtday_Year = this.Birtday.getFullYear();
	}
	,
	TimeStampToDate: function(_TimeStamp)
	{
	  var __Date = new Date(_TimeStamp);
	  var __Month = Language.MilisecondToMonth[__Date.getMonth()];
	  return __Date.getDate() + " " + __Month + " " + __Date.getFullYear() + " "  + __Date.getHours() + ":" + __Date.getMinutes();
	}
	,
	Destroy: function ()
	{
		cBaseObject.prototype.Destroy.call(this);
	}
}, {});







