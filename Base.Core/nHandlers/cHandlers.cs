using Base.Core.nApplication;
using Base.Core.nApplication.nCoreLoggers;
//using Base.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Base.Core.nAttributes;
using Base.Core.nCore;
using Base.Core.nHandlers.nAssemblyHandler;
using Base.Core.nHandlers.nFileHandler;
using Base.Core.nHandlers.nLambdaHandler;
using Base.Core.nHandlers.nReflectionHandler;
using Base.Core.nHandlers.nStringHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Core.nHandlers.nHashTableHandler;
using Base.Core.nHandlers.nDateTimeHandler;
using Base.Core.nHandlers.nProcessHandler;
using Base.Core.nHandlers.nHashHandler;
using Base.Core.nHandlers.nValidationHandler;
using Base.Core.nHandlers.nContextHandler;
using Base.Core.nHandlers.nLanguageHandler;
using Base.Core.nHandlers.nDefaultDataHandler;
using Base.Core.nHandlers.nEmailHandler;
using Base.Core.nHandlers.nExcelHandler;
using Base.Core.nHandlers.nStackHandler;

namespace Base.Core.nHandlers
{
    public class cHandlers : cCoreObject
    {
        public cAssemblyHandler AssemblyHandler { get; set; }
        public cLambdaHandler LambdaHandler { get; set; }
        public cReflectionHandler ReflectionHandler { get; set; }
        public cFileHandler FileHandler { get; set; }
        public cStringHandler StringHandler { get; set; }
        public cHashTableHandler HashTableHandler { get; set; }
        public cDateTimeHandler DateTimeHandler { get; set; }
        public cProcessHandler ProcessHandler { get; set; }
        public cHashHandler HashHandler { get; set; }
        public cValidationHandler ValidationHandler { get; set; }
        public cContextHandler ContextHandler { get; set; }
        public cLanguageHandler LanguageHandler { get; set; }
        public cDefaultDataHandler DefaultDataHandler { get; set; }
        
        public cEmailHandler EmailHandler { get; set; }
		public cExcelHandler ExcelHandler { get; set; }
		public cStackHandler StackHandler { get; set; }
		public cHandlers(cApp _App)
            : base(_App)
        {
            AssemblyHandler = new cAssemblyHandler(_App);
            LambdaHandler = new cLambdaHandler(_App);
            ReflectionHandler = new cReflectionHandler(_App);
            FileHandler = new cFileHandler(_App);
            StringHandler = new cStringHandler(_App);
            HashTableHandler = new cHashTableHandler(_App);
            DateTimeHandler = new cDateTimeHandler(_App);
            ProcessHandler = new cProcessHandler(_App);
            HashHandler = new cHashHandler(_App);
            ValidationHandler = new cValidationHandler(_App);
            ContextHandler = new cContextHandler(_App);
            LanguageHandler = new cLanguageHandler(App);
            DefaultDataHandler = new cDefaultDataHandler(_App);
            EmailHandler = new cEmailHandler(_App);
			ExcelHandler = new cExcelHandler(_App);
			StackHandler = new cStackHandler(_App);

		}

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cHandlers>(this);
			StackHandler.Init();
			AssemblyHandler.Init();
            LambdaHandler.Init();
            ReflectionHandler.Init();
            FileHandler.Init();
            StringHandler.Init();
            HashTableHandler.Init();
            DateTimeHandler.Init();
            ProcessHandler.Init();
            HashHandler.Init();
            ValidationHandler.Init();
            ContextHandler.Init();
            LanguageHandler.Init();
            DefaultDataHandler.Init();
            EmailHandler.Init();
			ExcelHandler.Init();
		}
    }
}
