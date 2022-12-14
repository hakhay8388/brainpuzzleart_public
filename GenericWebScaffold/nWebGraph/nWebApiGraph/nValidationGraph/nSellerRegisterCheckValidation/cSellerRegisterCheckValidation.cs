using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nValidationResultAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSellerRegisterCheckCommand;
using Data.GenericWebScaffold.nDataService;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using System;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nValidationGraph.nSellerRegisterCheckValidation
{
    public class cSellerRegisterCheckValidation : cBaseValidation, ISellerRegisterCheckReceiver
	{
		public cUserTempDataManager UserTempDataManager { get; set; }

		public cUserDataManager UserDataManager { get; set; }

		public cPageDataManager PageDataManager { get; set; }

		public cSellerRegisterCheckValidation(cApp _App, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager, cUserDataManager _UserDataManager
			, cUserTempDataManager _UserTempDataManager
			, cPageDataManager _PageDataManager)
			: base(_App, _WebGraph, _DataServiceManager)
		{
			WebGraph = _WebGraph;
			DataServiceManager = _DataServiceManager;
			UserDataManager = _UserDataManager;
			UserTempDataManager = _UserTempDataManager;
			PageDataManager = _PageDataManager;
		}


		public void ReceiveSellerRegisterCheckData(cListenerEvent _ListenerEvent, IController _Controller, cSellerRegisterCheckCommandData _ReceivedData)
		{
			cValidationResultProps __ValidationResultProps = new cValidationResultProps();

			IDataService __DataService =
				DataServiceManager.GetDataService();
			if (!App.Handlers.ValidationHandler.IsValidEmail(_ReceivedData.Email))
			{
				__ValidationResultProps.ValidationItems.Add(new cValidationItem()
				{
					FieldName = App.Handlers.LambdaHandler.GetObjectPropName(() => _ReceivedData.Email),
					Success = false,
					Message = _Controller.GetWordValue("RegisterEmailError")
				});
			}


			if (String.IsNullOrEmpty(_ReceivedData.Name.Trim()))
			{

				__ValidationResultProps.ValidationItems.Add(new cValidationItem()
				{
					FieldName = App.Handlers.LambdaHandler.GetObjectPropName(() => _ReceivedData.Name),
					Success = false,
					Message = _Controller.GetWordValue("RegisterNameError")
				});
			
			}
			
			if (_ReceivedData.Name.Length > 15)
			{
				__ValidationResultProps.ValidationItems.Add(new cValidationItem()
				{
					FieldName = App.Handlers.LambdaHandler.GetObjectPropName(() => _ReceivedData.Name),
					Success = false,
					Message = _Controller.GetWordValue("NameDefinitionCharacterSizeError")
				});

			}

			if (String.IsNullOrEmpty(_ReceivedData.Surname.Trim()))
			{
				__ValidationResultProps.ValidationItems.Add(new cValidationItem()
				{
					FieldName = App.Handlers.LambdaHandler.GetObjectPropName(() => _ReceivedData.Surname),
					Success = false,
					Message = _Controller.GetWordValue("RegisterSurnameError")
				});

			}
			if (_ReceivedData.Surname.Length > 20)
			{
				__ValidationResultProps.ValidationItems.Add(new cValidationItem()
				{
					FieldName = App.Handlers.LambdaHandler.GetObjectPropName(() => _ReceivedData.Surname),
					Success = false,
					Message = _Controller.GetWordValue("SurnameDefinitionCharacterSizeError")
				});

			}
			if (String.IsNullOrEmpty(_ReceivedData.Password.Trim()))
			{
				__ValidationResultProps.ValidationItems.Add(new cValidationItem()
				{
					FieldName = App.Handlers.LambdaHandler.GetObjectPropName(() => _ReceivedData.Password),
					Success = false,
					Message = _Controller.GetWordValue("RegisterPasswordError1")
				});

			}

			if (_ReceivedData.Password.Length < ((cGenericWebScaffoldDataService)__DataService).PasswordLimit)
			{
				__ValidationResultProps.ValidationItems.Add(new cValidationItem()
				{
					FieldName = App.Handlers.LambdaHandler.GetObjectPropName(() => _ReceivedData.Password),
					Success = false,
					Message = _Controller.GetWordValue("RegisterPasswordMinCharacter")
				});

			}
			if (_ReceivedData.Password.Trim() != _ReceivedData.PasswordConfirm.Trim())
			{
				__ValidationResultProps.ValidationItems.Add(new cValidationItem()
				{
					FieldName = App.Handlers.LambdaHandler.GetObjectPropName(() => _ReceivedData.PasswordConfirm),
					Success = false,
					Message = _Controller.GetWordValue("RegisterPasswordError2")
				});

			}

			if (String.IsNullOrEmpty(_ReceivedData.Telephone.Trim()) ||
				!App.Handlers.ValidationHandler.IsValidPhoneNumber(_ReceivedData
					.Telephone
					.Trim()))
			{
				__ValidationResultProps.ValidationItems.Add(new cValidationItem()
				{
					FieldName = App.Handlers.LambdaHandler.GetObjectPropName(() => _ReceivedData.Telephone),
					Success = false,
					Message = _Controller.GetWordValue("RegisterTelError")
				});

			}

			if (UserDataManager.IsThereEmail(_ReceivedData.Email))
			{

				__ValidationResultProps.ValidationItems.Add(new cValidationItem()
				{
					FieldName = App.Handlers.LambdaHandler.GetObjectPropName(() => _ReceivedData.Email),
					Success = false,
					Message = _Controller.GetWordValue("RegisterEmailAlreadyUsedError")
				});

			}
			if (UserDataManager.IsThereNumber(_ReceivedData.Telephone))
			{
				__ValidationResultProps.ValidationItems.Add(new cValidationItem()
				{
					FieldName = App.Handlers.LambdaHandler.GetObjectPropName(() => _ReceivedData.Telephone),
					Success = false,
					Message = _Controller.GetWordValue("RegisterTelephoneAlreadyUsedError")
				});

			}
			
			if (!UserDataManager.IsValidBirthDate(_ReceivedData.DateOfBirth, _ReceivedData.IsSeller))
			{
				__ValidationResultProps.ValidationItems.Add(new cValidationItem()
				{
					FieldName = App.Handlers.LambdaHandler.GetObjectPropName(() => _ReceivedData.DateOfBirth),
					Success = false,
					Message = _Controller.GetWordValue("RegisterBirthDateError", ((cGenericWebScaffoldDataService)__DataService).SellerAgeLimit)
				});

			}

			if (__ValidationResultProps.ValidationItems.Count > 0)
			{
				_ListenerEvent.StopPropogation();
			}

			WebGraph.ActionGraph.ValidationResultAction.Action(_Controller, __ValidationResultProps);
		}
	}
}