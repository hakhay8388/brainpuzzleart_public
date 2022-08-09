using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.BatchJobService.nBatchJobManager;
using Core.BatchJobService.nBatchJobManager.nJobs.nMailSenderJob;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDoCheckLoginRequestAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultItemAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nCheckLoginCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetLoginCheckCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetUserCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nLoginCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nLogoutCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nRegisterCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSellerRegisterCheckCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSellerRegisterCommand;
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDataService;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Integration.Managers.nManagers;
using Integration.MicroServiceGraph.nMicroService;
using System;
using System.Collections.Generic;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nListenerGraph.nLogInOutListener
{
    public class cLogInOutListener : cBaseListener
		, ILoginReceiver
		, ILogoutReceiver
		, ICheckLoginReceiver
		, IGetUserReceiver
		, IGetLoginCheckReceiver
		, IRegisterReceiver
		, ISellerRegisterCheckReceiver
		, ISellerRegisterReceiver

	{
		public cSessionDataManager SessionDataManager { get; set; }
		public cUserDataManager UserDataManager { get; set; }
		public cBatchJobManager BatchJobManager { get; set; }
		public IManagers Managers { get; set; }
		public cPageDataManager PageDataManager { get; set; }
		public cUserTempDataManager UserTempDataManager { get; set; }
		public cLogInOutListener(cApp _App, IMicroService _MicroService, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager
			, cSessionDataManager _SessionDataManager
			, cUserDataManager _UserDataManager
			, cBatchJobManager _BatchJobManager
			, IManagers _Managers
			, cPageDataManager _PageDataManager
			, cUserTempDataManager _UserTempDataManager
			)
		   : base(_App, _MicroService, _WebGraph, _DataServiceManager)
		{
			WebGraph = _WebGraph;
			SessionDataManager = _SessionDataManager;
			UserDataManager = _UserDataManager;
			BatchJobManager = _BatchJobManager;
			Managers = _Managers;
			PageDataManager = _PageDataManager;
			UserTempDataManager = _UserTempDataManager;
		}

		public void ReceiveLogoutData(cListenerEvent _ListenerEvent, IController _Controller, cLogoutCommandData _ReceivedData)
		{
			if (_Controller.ClientSession.IsLogined)
			{
				cMessageProps __MessageProps = new cMessageProps();
				__MessageProps.Header = _Controller.GetWordValue("Exit");
				__MessageProps.Message = _Controller.GetWordValue("DoYouWantToExit");
				__MessageProps.ColorType = EColorTypes.None;
				__MessageProps.MessageButtons = EMessageButtons.YesNo;
				__MessageProps.DefaultMessageButton = EMessageButton.No;

				__MessageProps.FirstButtonColorType = EColorTypes.Error;
				__MessageProps.FirstButtonEnabled = true;

				__MessageProps.SecondButtonColorType = EColorTypes.Success;
				__MessageProps.SecondButtonEnabled = true;
				__MessageProps.Action = delegate (cBaseCommand __BaseCommand, IController __InnerController, EMessageButton __MessageButton, object __RequestObject)
				{
					if (__MessageButton.ID == EMessageButton.Yes.ID)
					{

						string __Session_ID = __InnerController.ClientSession.SessionID;
						long _UserID = __InnerController.ClientSession.User.ID;

						//List<cSession> __MySessions = WebGraph.SessionManager.GetSessionByUserID(_UserID);


						cGenericWebScaffoldDataService __DataService = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();

						__DataService.Perform(() =>
						{
							SessionDataManager.DeleteSession(__Session_ID);

						});

						__InnerController.Logout();

						//WebGraph.ActionGraph.LogInOutAction.Action(__InnerController, new List<cSession>() { __InnerController.ClientSession }, true);

						WebGraph.ActionGraph.DoCheckLoginRequestAction.Action(__InnerController, new cDoCheckLoginRequestProps() { IsLogined = false }, new List<cSession>() { __InnerController.ClientSession }, true);
						WebGraph.ActionGraph.DoCheckLoginRequestAction.Action(__InnerController, new cDoCheckLoginRequestProps() { IsLogined = false });
					}
				};

				WebGraph.ActionGraph.ShowMessageAction.Action(_Controller, __MessageProps);

				/*WebGraph.ActionGraph.ShowMessageAction.Action(_Controller, "Çıkış", "Oturumu kapatmak istediğinizden emin misiniz?", EColorTypes.None, EMessageButtons.YesNo, EMessageButton.No, true, "Evet", EColorTypes.Success, true, "Hayır", EColorTypes.Error, false, "", EColorTypes.Primary, true,
                delegate (cBaseCommand __BaseCommand, IController __InnerController, EMessageButton __MessageButton) 
                {
                    if (__MessageButton.ID == EMessageButton.Yes.ID)
                    {
                        __InnerController.Logout();
                    }
                    WebGraph.ActionGraph.LogInOutAction.Action(__InnerController);
                });                */

			}
			else
			{
				WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
			}
		}

		public void ReceiveLoginData(cListenerEvent _ListenerEvent, IController _Controller, cLoginCommandData _ReceivedData)
		{
			if (!_Controller.ClientSession.IsLogined)
			{

				if (!String.IsNullOrEmpty(_ReceivedData.UserName) && !String.IsNullOrEmpty(_ReceivedData.Password))
				{

					cGenericWebScaffoldDataService __DataService = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();





					cUserEntity __UserEntity = UserDataManager.GetUserByEmailAndPassword(_ReceivedData.UserName, _ReceivedData.Password);
					cUserTempEntity __UserTempEntity =
						UserTempDataManager.GetRegistrationByMail(_ReceivedData.UserName);

					if (__UserEntity != null)
					{
						EUserState __UserState = EUserState.GetByID(__UserEntity.State, EUserState.Canceled);
						if (EUserState.GetByID(__UserEntity.State, EUserState.Canceled).ID == EUserState.Confirmed.ID)
						{

							if (_ReceivedData.StaySession)
							{
								__DataService.Perform(() =>
								{
									SessionDataManager.AddUserSession(__UserEntity, _Controller.ClientSession.SessionID, _Controller.ClientSession.IpAddress);

								});
							}
							else
							{
								__DataService.Perform(() =>
								{
									SessionDataManager.AddUserSessionTemp(__UserEntity, _Controller.ClientSession.SessionID, _Controller.ClientSession.IpAddress);

								});

							}
							
							_Controller.ClientSession.SetUser(__UserEntity);
							//ActionGraph.ShowMessageAndRunCommandAction.Action_ClassicNone(_Session, "Merhaba", "Hoşgeldin " + _Session.User.Name);
							//ActionGraph.HotSpotMessageAndRunCommandAction.Action(_Controller, "Merhaba", "Hoşgeldin " + _Controller.ClientSession.User.Name, ColorTypes.Success);

							//cLanguageItem __LanguageItem = App.Handlers.LanguageHandler.GetLanguageByCode(__UserEntity.Language);
							//WebGraph.ActionGraph.LanguageAction.Action(_Controller, new cLanguageProps() { Language = __LanguageItem.LanguageObject, LanguageCode = __UserEntity.Language });
							WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
							WebGraph.ActionGraph.HotSpotMessageAction.Action(_Controller, new cHotSpotProps() { Header = _Controller.GetWordValue("Hi"), Message = _Controller.GetWordValue("Welcome", _Controller.ClientSession.User.Name), ColorType = EColorTypes.Success, DurationMS = 2500 });
							WebGraph.ActionGraph.SuccessResultAction.Action(_Controller);
						}
						else
						{
							WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
							WebGraph.ActionGraph.ShowMessageAction.WarningAction(_Controller, new cMessageProps() { Header = _Controller.GetWordValue("Warning"), Message = _Controller.GetWordValue("AccountStateMessage", __UserEntity.Name, __UserState.Name) });
						}
					}
					else
					{
						if (__UserTempEntity != null)
						{
							WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller, new cMessageProps() { Header = _Controller.GetWordValue("Error"), Message = _Controller.GetWordValue("NotConfirmedError") });
						}
						else
						{
							//ActionGraph.HotSpotMessage.Action(_Controller, "Hata", __GbsUserKontrolEtResponse.GbsUserKontrolEtResult.aciklama, ColorTypes.Danger);
							WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller, new cMessageProps() { Header = _Controller.GetWordValue("Error"), Message = _Controller.GetWordValue("LoginError") });

						}
					}
				}
				else
				{
					WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
					WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller, new cMessageProps() { Header = _Controller.GetWordValue("Error"), Message = _Controller.GetWordValue("LoginError2") });
					//ActionGraph.HotSpotMessage.Action(_Controller, "Uyarı", "Lütfen Kullanıcı adı ve şifrenizi girdikten sonra giriş yapınız.", ColorTypes.Warning);
				}
				//ActionGraph.SetStateAction.Action(_Session, "UserName", "Deneme");
				//ActionGraph.SetVariableAction.Action(_Session, "TextStyle", new { color = "#FF0000", backgroundColor = "#00FF00" }, true);
			}
			else
			{
				WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
			}
		}

		public void ReceiveCheckLoginData(cListenerEvent _ListenerEvent, IController _Controller, cCheckLoginCommandData _ReceivedData)
		{
			cGenericWebScaffoldDataService __DataService = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();
			if (__DataService != null)
			{
				cUserEntity __UserEntity = SessionDataManager.GetUserBySessionID(_Controller.ClientSession.SessionID);
				if (__UserEntity != null)
				{
					_Controller.ClientSession.SetUser(__UserEntity);
				}

				//WebGraph.ActionGraph.SetServerDateTimeAction.Action(_Controller, new cSetServerDateTimeProps() { ServerDate = DateTime.Now });
				//WebGraph.ActionGraph.SetGlobalParamListAction.Action(_Controller, new cSetGlobalParamListProps() { ParamList = __DataService.GlobalParamList });
				WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
			}
			else
			{
				WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller, new cMessageProps() { Header = _Controller.GetWordValue("Error"), Message = _Controller.GetWordValue("DomainError") });
			}
		}

		public void ReceiveRegisterData(cListenerEvent _ListenerEvent, IController _Controller,
			cRegisterCommandData _ReceivedData)
		{
			IDataService __DataService = DataServiceManager.GetDataService();
			if (App.Handlers.ValidationHandler.IsValidEmail(_ReceivedData.Email))
			{
				if (!String.IsNullOrEmpty(_ReceivedData.Name.Trim()))
				{
					if (_ReceivedData.Name.Length <= ((cGenericWebScaffoldDataService)__DataService).NameCharacterLimit)
					{
						if (_ReceivedData.Surname.Length <= ((cGenericWebScaffoldDataService)__DataService).SurnameCharacterLimit)
						{
							if (!String.IsNullOrEmpty(_ReceivedData.Surname.Trim()))
							{
								if (!String.IsNullOrEmpty(_ReceivedData.Password.Trim()))
								{
									if (_ReceivedData.Password.Length >= ((cGenericWebScaffoldDataService)__DataService).PasswordLimit)
									{
										if (_ReceivedData.Password.Trim() == _ReceivedData.PasswordConfirm.Trim())
										{
											if (!String.IsNullOrEmpty(_ReceivedData.Telephone.Trim()))
											{
												if (UserDataManager.IsValidBirthDate(_ReceivedData.DateOfBirth, false))
												{
													cUserEntity __UserEntity =
														UserDataManager.GetUserByEmail(_ReceivedData.Email);
													if (__UserEntity == null)
													{
														if ((_ReceivedData.IsSeller &&
															 _ReceivedData.University.HasValue) ||
															!_ReceivedData.IsSeller)
														{
															if ((_ReceivedData.IsSeller &&
																 _ReceivedData.UniversitySection.HasValue) ||
																!_ReceivedData.IsSeller)
															{
																if ((_ReceivedData.IsSeller &&
																	 _ReceivedData.EducationLevel.HasValue) ||
																	!_ReceivedData.IsSeller)
																{
																	cUserTempEntity __UserTemp =
																		__DataService
																			.Perform<cRegisterCommandData,
																				cUserTempEntity>(
																				(_ReceivedDataInner) =>
																				{
																					cUserTempEntity __InnerUser =
																						UserTempDataManager.AddTempUser(App.Handlers.StringHandler.ChangeFormatNameAndSurname(_ReceivedData.Name.Trim()), App.Handlers.StringHandler.ChangeFormatNameAndSurname(_ReceivedData.Surname.Trim()), _ReceivedData.Email.Trim(), _ReceivedData.Telephone.Trim(), _ReceivedData.Password.Trim(), _ReceivedData.EducationLevel, _ReceivedData.DateOfBirth, false,  _ReceivedData.UniversitySection, "", "");

																					return __InnerUser;
																				}, _ReceivedData);

																	if (__UserTemp.IsValid)
																	{

																		WebGraph.ActionGraph.SuccessResultAction.Action(
																			_Controller);
																		WebGraph.ActionGraph
																			.ShowMessageAndRunCommandAction
																			.SuccessAction(_Controller,
																				new cMessageProps()
																				{
																					Header = _Controller.GetWordValue(
																						"Congratulations"),
																					Message = _Controller.GetWordValue(
																						"RegisterCompleteConfirmEmail",
																						((
																								cGenericWebScaffoldDataService)
																							__DataService)
																						.ActivationReminderDeadline)
																				});
																	}
																	else
																	{
																		WebGraph.ErrorMessageManager.ErrorAction(_Controller, _Controller.GetWordValue("Error"), _Controller.GetWordValue("UnknownError"));
																	}
																}
																else
																{
																	WebGraph.ActionGraph.ShowMessageAction.ErrorAction(
																		_Controller,
																		new cMessageProps()
																		{
																			Header =
																				_Controller.GetWordValue("Warning"),
																			Message = _Controller.GetWordValue(
																				"PleaseSelectEducationLevel")
																		});
																}
															}
															else
															{
																WebGraph.ActionGraph.ShowMessageAction.ErrorAction(
																	_Controller,
																	new cMessageProps()
																	{
																		Header = _Controller.GetWordValue("Warning"),
																		Message = _Controller.GetWordValue(
																			"PleaseSelectSection")
																	});
															}
														}
														else
														{
															WebGraph.ActionGraph.ShowMessageAction.ErrorAction(
																_Controller,
																new cMessageProps()
																{
																	Header = _Controller.GetWordValue("Warning"),
																	Message = _Controller.GetWordValue(
																		"PleaseSelectUniversity")
																});
														}
													}
													else
													{
														WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller,
															new cMessageProps()
															{
																Header = _Controller.GetWordValue("Warning"),
																Message = _Controller.GetWordValue(
																	"RegisterEmailAlreadyUsedError")
															});
													}
												}
												else
												{
													WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller,

	   new cMessageProps()
	   {
		   Header = _Controller.GetWordValue("Warning"),
		   Message = _Controller.GetWordValue("RegisterBirthDateError",
			   ((cGenericWebScaffoldDataService)__DataService).CustomerAgeLimit)
	   });
												}
											}
											else
											{
												WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller,
													new cMessageProps()
													{
														Header = _Controller.GetWordValue("Warning"),
														Message = _Controller.GetWordValue("RegisterTelError")
													});
											}
										}
										else
										{
											WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller,
												new cMessageProps()
												{
													Header = _Controller.GetWordValue("Warning"),
													Message = _Controller.GetWordValue("RegisterPasswordError2")
												});
										}
									}
									else
									{
										WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller,
											new cMessageProps()
											{
												Header = _Controller.GetWordValue("Warning"),
												Message = _Controller.GetWordValue("RegisterPasswordMinCharacter",
													((cGenericWebScaffoldDataService)__DataService).PasswordLimit)
											});
									}
								}
								else
								{
									WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller,
										new cMessageProps()
										{
											Header = _Controller.GetWordValue("Warning"),
											Message = _Controller.GetWordValue("RegisterPasswordError1")
										});
								}
							}
							else
							{
								WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller,
									new cMessageProps()
									{
										Header = _Controller.GetWordValue("Warning"),
										Message = _Controller.GetWordValue("RegisterSurnameError")
									});
							}
						}
						else
						{
							WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller,
								new cMessageProps()
								{
									Header = _Controller.GetWordValue("Warning"),
									Message = _Controller.GetWordValue("SurnameDefinitionCharacterSizeError",
									((cGenericWebScaffoldDataService)__DataService).SurnameCharacterLimit)
								});
						}
					}

					else
					{
						WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller,
							new cMessageProps()
							{
								Header = _Controller.GetWordValue("Warning"),
								Message = _Controller.GetWordValue("NameDefinitionCharacterSizeError",
									((cGenericWebScaffoldDataService)__DataService).NameCharacterLimit)
							});
					}
				}
				else
				{
					WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller,
						new cMessageProps()
						{
							Header = _Controller.GetWordValue("Warning"),
							Message = _Controller.GetWordValue("RegisterNameError")
						});
				}
			}
			else
			{
				WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller,
					new cMessageProps()
					{
						Header = _Controller.GetWordValue("Warning"),
						Message = _Controller.GetWordValue("RegisterEmailError")
					});
			}
		}

		public void ReceiveSellerRegisterCheckData(cListenerEvent _ListenerEvent, IController _Controller, cSellerRegisterCheckCommandData _ReceivedData)
		{
			WebGraph.ActionGraph.SuccessResultAction.Action(_Controller);
		}

		public void ReceiveSellerRegisterData(cListenerEvent _ListenerEvent, IController _Controller,
			cSellerRegisterCommandData _ReceivedData)
		{
			IDataService __DataService = DataServiceManager.GetDataService();
			cUserTempEntity __UserTemp = __DataService.Perform<cSellerRegisterCommandData, cUserTempEntity>((_ReceivedDataInner) =>
			{
				cUserTempEntity __InnerUser = UserTempDataManager.AddTempUser
						(
							App.Handlers.StringHandler.ChangeFormatNameAndSurname(_ReceivedData.Name.Trim()),
							App.Handlers.StringHandler.ChangeFormatNameAndSurname(_ReceivedData.Surname.Trim()),
											_ReceivedData.Email.Trim(),
											_ReceivedData.Telephone.Trim(),
											_ReceivedData.Password.Trim(),
											_ReceivedData.EducationLevel,
											_ReceivedData.DateOfBirth,
											_ReceivedData.IsSeller,
											_ReceivedData.UniversitySection,
											_ReceivedData.OtherUniversity,
											_ReceivedData.OtherSection

						);
				return __InnerUser;
			}, _ReceivedData);

			if (__UserTemp.IsValid)
			{

				WebGraph.ActionGraph.SuccessResultAction.Action(_Controller);
				WebGraph.ActionGraph.ShowMessageAndRunCommandAction
					.SuccessAction(_Controller,
						new cMessageProps()
						{
							Header = _Controller.GetWordValue(
								"Congratulations"),
							Message = _Controller.GetWordValue(
								"RegisterCompleteConfirmEmail", ((cGenericWebScaffoldDataService)__DataService).ActivationReminderDeadline)
						});
			}
			else
			{
				WebGraph.ErrorMessageManager.ErrorAction(_Controller, _Controller.GetWordValue("Error"), _Controller.GetWordValue("UnknownError"));
			}
		}

		public void ReceiveGetUserData(cListenerEvent _ListenerEvent, IController _Controller, cGetUserCommandData _ReceivedData)
		{
			if (_Controller.ClientSession.IsLogined)
			{
				WebGraph.ActionGraph.SetUserOnClientAction.Action(_Controller);
			}
			else
			{
				WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
			}
		}

		public void ReceiveGetLoginCheckData(cListenerEvent _ListenerEvent, IController _Controller, cGetLoginCheckCommandData _ReceivedData)
		{
			if (_Controller.ClientSession.IsLogined)
			{
				bool __IsCustomer = false;
				bool __IsSeller = false;
				cActorEntity __Actor = _Controller.ClientSession.User.Actor.GetValue();
				if (__Actor.Roles.GetValue().Code == RoleIDs.Customer.Code)
				{
					__IsCustomer = true;
				}
				if (__Actor.Roles.GetValue().Code == RoleIDs.Seller.Code)
				{
					__IsSeller = true;
				}

				WebGraph.ActionGraph.ResultItemAction.Action(_Controller, new cResultItemProps() { Item = new { UserOnline = true, isCustomer = __IsCustomer, isSeller = __IsSeller } });
			}
			else
			{
				WebGraph.ActionGraph.ResultItemAction.Action(_Controller, new cResultItemProps() { Item = new { UserOnline = false } });
			}
		}

	}
}