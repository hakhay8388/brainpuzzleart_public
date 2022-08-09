using System;
using System.Collections.Generic;
using System.Reflection;
using Base.Core.nApplication.nConfiguration.nStartParameter;
using Base.Core.nExceptions;
using System.Globalization;
using System.Linq.Expressions;
using Base.Core.nCore;
using System.IO;
using Base.Boundary.nCore.nBootType;

namespace Base.Core.nApplication.nConfiguration
{
    public class cConfiguration : cCoreObject
    {
        private cStartParameterController StartParameterController = null;
        public List<string> DomainNames { get; private set; }
        public string UICultureName { get; private set; }
        public string ScriptPath { get; private set; }
        public string ScriptCachePath { get; private set; }
        public string ScriptDebugSourcePath { get; private set; }
        public string BinPath { get; private set; }
		public string LanguagePath { get; set; }
        public string ProfileImagePath { get; private set; }
        public string UserFilePath { get; private set; }
        public string TicketFilePath { get; private set; }
        public string DefaultDataPath { get; set; }
        public string ConfigurationDataPath { get; set; }

        public EBootType BootType { get; set; }

        public CultureInfo UICulture
        {
            get
            {
                try
                {
                    return string.IsNullOrEmpty(UICultureName) ? CultureInfo.InvariantCulture : new CultureInfo(UICultureName);
                }
                catch(Exception _Ex)
                {
					App.Loggers.CoreLogger.LogError(_Ex);
					return CultureInfo.InvariantCulture;
                }
            }
        }
        public string HomePath{ get; private set; }

		public string LogPath { get; private set; }
		public string GeneralLogPath { get; private set; }
		public string ExecutedSqlLogPath { get; private set; }
		public string LifecycleLoggerPath { get; private set; }
		public string WebHooksLoggerPath { get; private set; }
		public string IntegrationLoggerPath { get; private set; }	




		public string RequestPerformanceLogPath { get; private set; }
		public string BatchJobLogPath { get; private set; }
		public bool LogToFile { get; private set; }
		public bool LogToConsole { get; private set; }

	
		public bool LogDebugEnabled { get; private set; }
		public bool LogInfoEnabled { get; private set; }
		public bool LogExceptionEnabled { get; private set; }


		public bool LogExecutedSqlEnabled { get; set; }
		public bool LogGeneralEnabled { get; set; }
		public bool LogBatchJobEnabled { get; set; }
		public bool LogPerformanceEnabled { get; set; }
		public bool LogLifecycleEnabled { get; set; }
		public bool LogWebHooksEnabled { get; set; }

		public bool LogIntegrationsEnabled { get; set; }
		







		public cConfiguration(EBootType _BootType)
            :base(null)
        {
            BootType = _BootType;
            StartParameterController = new cStartParameterController(this);
        }

        public override void Init()
        {
            base.Init();
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.ScriptCachePath, true);
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.ScriptDebugSourcePath, true);
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.GeneralLogPath, true);
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.ExecutedSqlLogPath, true);
			App.Handlers.FileHandler.MakeDirectory(App.Configuration.LifecycleLoggerPath, true);
			App.Handlers.FileHandler.MakeDirectory(App.Configuration.WebHooksLoggerPath, true);
			App.Handlers.FileHandler.MakeDirectory(App.Configuration.IntegrationLoggerPath, true);


			App.Handlers.FileHandler.MakeDirectory(App.Configuration.RequestPerformanceLogPath, true);
			App.Handlers.FileHandler.MakeDirectory(App.Configuration.LanguagePath, true);
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.ProfileImagePath, true);
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.DefaultDataPath, true);
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.ConfigurationDataPath, true);
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.BatchJobLogPath, true);
			App.Handlers.FileHandler.MakeDirectory(App.Configuration.TicketFilePath, true);
			App.Handlers.FileHandler.MakeDirectory(App.Configuration.UserFilePath, true);			
		}

        public void InnerInit(cApp _App)
        {
            App = _App;
            App.Factories.ObjectFactory.RegisterInstance(GetType(), this);
            DomainNames = new List<string>() { "Base", "Data", "Integration", "Core", "App" };
            UICultureName = "tr-TR";
            HomePath = AppDomain.CurrentDomain.BaseDirectory;
            BinPath = AppDomain.CurrentDomain.BaseDirectory;

			LogPath = GetVariableName(() => LogPath);
			LogPath = Path.Combine(HomePath, LogPath);

#if !DEBUG
             ///////// Log Nereye basılacak Ayarı //////
			LogToFile = true;
            LogToConsole = false;
			///////////////////////////////////////////

			///////// Hangi Tip loglar basılacak ayarı //////
			LogDebugEnabled = true;
			LogInfoEnabled = true;
			LogExceptionEnabled = true;
			/////////////////////////////////////////////////

			///////// Hangi loger mekanizmaları aktif olsun //////
			LogExecutedSqlEnabled = true;
			LogGeneralEnabled = true;
			LogBatchJobEnabled = true;
			LogPerformanceEnabled = true;
			LogLifecycleEnabled = true;
			LogWebHooksEnabled = true;
			LogIntegrationsEnabled = true;
			/////////////////////////////////////////////////;
#endif

#if DEBUG

			///////// Log Nereye basılacak Ayarı //////
			LogToFile = true;
            LogToConsole = false;
			///////////////////////////////////////////

			///////// Hangi Tip loglar basılacak ayarı //////
			LogDebugEnabled = true;
			LogInfoEnabled = true;
			LogExceptionEnabled = true;
			/////////////////////////////////////////////////

			///////// Hangi loger mekanizmaları aktif olsun //////
			LogExecutedSqlEnabled = true;
			LogGeneralEnabled = true;
			LogBatchJobEnabled = true;
			LogPerformanceEnabled = true;
			LogLifecycleEnabled = true;
			LogWebHooksEnabled = true;
			LogIntegrationsEnabled = true;
			/////////////////////////////////////////////////

#endif


			SetPaths();
        }

        private void SetPaths()
        {
            ScriptPath = GetVariableName(() => ScriptPath);
            ScriptCachePath = GetVariableName(() => ScriptCachePath);

            ScriptCachePath = Path.Combine(HomePath, ScriptPath, ScriptCachePath);
            ScriptDebugSourcePath = GetVariableName(() => ScriptDebugSourcePath);

            ScriptDebugSourcePath = Path.Combine(HomePath, ScriptPath, ScriptDebugSourcePath);
            ScriptPath = Path.Combine(HomePath, ScriptPath);

            GeneralLogPath = GetVariableName(() => GeneralLogPath);
            GeneralLogPath = Path.Combine(LogPath, GeneralLogPath);

            ExecutedSqlLogPath = GetVariableName(() => ExecutedSqlLogPath);
            ExecutedSqlLogPath = Path.Combine(LogPath, ExecutedSqlLogPath);

			LifecycleLoggerPath = GetVariableName(() => LifecycleLoggerPath);
			LifecycleLoggerPath = Path.Combine(LogPath, LifecycleLoggerPath);

			WebHooksLoggerPath = GetVariableName(() => WebHooksLoggerPath);
			WebHooksLoggerPath = Path.Combine(LogPath, WebHooksLoggerPath);

			IntegrationLoggerPath = GetVariableName(() => IntegrationLoggerPath);
			IntegrationLoggerPath = Path.Combine(LogPath, IntegrationLoggerPath);

			RequestPerformanceLogPath = GetVariableName(() => RequestPerformanceLogPath);
			RequestPerformanceLogPath = Path.Combine(LogPath, RequestPerformanceLogPath);

			BatchJobLogPath = GetVariableName(() => BatchJobLogPath);
            BatchJobLogPath = Path.Combine(LogPath, BatchJobLogPath);

            LanguagePath = GetVariableName(() => LanguagePath);
			LanguagePath = Path.Combine(HomePath, LanguagePath);

            DefaultDataPath = GetVariableName(() => DefaultDataPath);
            DefaultDataPath = Path.Combine(HomePath, DefaultDataPath);

            ConfigurationDataPath = GetVariableName(() => ConfigurationDataPath);
            ConfigurationDataPath = Path.Combine(HomePath, ConfigurationDataPath); 

#if DEBUG
            UserFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "public", "assets", "userfiles");
            TicketFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "public", "assets", "ticketfiles");
            ProfileImagePath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "public", "assets", "img", "profileimages");

#else

			UserFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "build", "assets", "userfiles");
            TicketFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "build", "assets", "ticketfiles");
            ProfileImagePath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "build", "assets", "img", "profileimages");
#endif



/*            IdImagePath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "build", "assets", "img", "idimages");
            if (!Directory.Exists(IdImagePath))
            {
                IdImagePath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "public", "assets", "img", "idimages");
            }

            ProofImagePath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "build", "assets", "img", "proofimages");
            if (!Directory.Exists(ProofImagePath))
            {
                ProofImagePath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "public", "assets", "img", "proofimages");
            }

            StudentCertificateImagePath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "build", "assets", "img", "studentcertificateimages");
            if (!Directory.Exists(StudentCertificateImagePath))
            {
                StudentCertificateImagePath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "public", "assets", "img", "studentcertificateimages");
            }
*/
        }

        protected string GetVariableName<T>(Expression<Func<T>> _Expr)
        {
            var __Body = (MemberExpression)_Expr.Body;
            return __Body.Member.Name;
        }

        private void OverrideConfiguration()
        {
            Console.WriteLine("Parameters Overriding....");
            Type __Type = GetType();
            foreach (var __Item in StartParameterController.ParameterList)
            {
                PropertyInfo __FieldInfo = __Type.SearchProperty(__Item.Key.ToString());
                if (__FieldInfo != null)
                {
                    try
                    {
                        __FieldInfo.SetValue(this, Convert.ChangeType(__Item.Value, __FieldInfo.PropertyType));
                        Console.WriteLine(__Item.Key.ToString() + " : " + __Item.Value + " -> Override success...");
                    }
                    catch(Exception _Ex)
                    {
						App.Loggers.CoreLogger.LogError(_Ex);
						throw new cCoreException(App, "Parametre hatası : " + __Item.Value);
                    }

                }

            }
        }
        public string TryGetParameter(String _ParameterName)
        {
            return StartParameterController.ParameterList[_ParameterName];
        }
    }
}
