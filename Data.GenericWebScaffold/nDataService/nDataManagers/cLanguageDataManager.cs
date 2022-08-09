using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Base.Data.nDataService.nDatabase.nSql;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Base.Data.nDataServiceManager;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Base.Data.nDataService.nDatabase.nEntity;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;

namespace Data.GenericWebScaffold.nDataService.nDataManagers
{
    public class cLanguageDataManager : cBaseDataManager
    {
        public cLanguageDataManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager
            , IFileDateService _FileDataService
            )
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {

        }

        public cLanguageEntity GetLanguageByCode(string _LanguageCode)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cLanguageEntity __LanguageEntity = __DataService.Database.Query<cLanguageEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.Code).Eq(_LanguageCode)
                .ToQuery()
                .ToList()
                .FirstOrDefault();
            return __LanguageEntity;
        }


        public cLanguageEntity GetLanguageByID(long _LanguageID)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cLanguageEntity __LanguageEntity = __DataService.Database.GetEntityByID<cLanguageEntity>(_LanguageID);
            return __LanguageEntity;
        }

        public List<cLanguageEntity> GetLanguages()
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            List<cLanguageEntity> __LanguageEntityList = __DataService.Database.Query<cLanguageEntity>()
                .SelectAll()
                .Where()
                .ToQuery()
                .ToList();
            return __LanguageEntityList;
        }


        public cLanguageEntity AddLanguage(string _Code, string _Name, string _IconCode)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cLanguageEntity __LanguageEntity = __DataService.Database.CreateNew<cLanguageEntity>();
            __LanguageEntity.Name = _Name;
            __LanguageEntity.Code = _Code;
            __LanguageEntity.IconCode = _IconCode;
            __LanguageEntity.Save();
            return __LanguageEntity;
        }

        public cLanguageEntity CreateLanguageIfNotExists(string _Code, string _Name, string _IconCode)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cLanguageEntity __LanguageEntity = GetLanguageByCode(_Code);
            if (__LanguageEntity == null)
            {
                __LanguageEntity = AddLanguage(_Code, _Name, _IconCode);
            }

            return __LanguageEntity;
        }
        public cLanguageHrefLangEntity AddLanguageHref(cLanguageEntity _LanguageEntity, string _Code)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cLanguageHrefLangEntity __LanguageHrefLangEntity = __DataService.Database.CreateNew<cLanguageHrefLangEntity>();
            __LanguageHrefLangEntity.Code = _Code;
            __LanguageHrefLangEntity.Save(_LanguageEntity);
            return __LanguageHrefLangEntity;
        }
        public cLanguageWordEntity AddLanguageWord(cLanguageEntity _LanguageEntity, string _Code, string _Word, string _Description, int _ParamCount, string _CheckSum)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cLanguageWordEntity __LanguageWordEntity = __DataService.Database.CreateNew<cLanguageWordEntity>();
            __LanguageWordEntity.Code = _Code;
            __LanguageWordEntity.Word = _Word;
            __LanguageWordEntity.CheckSum = _CheckSum;
            __LanguageWordEntity.Description = _Description;
            __LanguageWordEntity.ParamCount = _ParamCount;
            __LanguageWordEntity.Save(_LanguageEntity);
            return __LanguageWordEntity;
        }
        public cLanguageWordEntity UpdateLanguageWordCheckSum(cLanguageWordEntity __LanguageWordEntity, string _Word, string _CheckSum)
        {
            IDataService __DataService = DataServiceManager.GetDataService();


            __LanguageWordEntity.Word = _Word;
            __LanguageWordEntity.CheckSum = _CheckSum;


            cLanguageEntity _LanguageEntity = __LanguageWordEntity.GetOwnerEntity<cLanguageEntity>();

            __LanguageWordEntity.Save(_LanguageEntity);
            return __LanguageWordEntity;
        }
        public cLanguageWordEntity UpdateLanguageWord(long _LanguageWordID, string _Word)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cLanguageWordEntity __LanguageWordEntity = __DataService.Database.GetEntityByID<cLanguageWordEntity>(_LanguageWordID);
            __LanguageWordEntity.Word = _Word;
            cLanguageEntity _LanguageEntity = __LanguageWordEntity.GetOwnerEntity<cLanguageEntity>();

            __LanguageWordEntity.Save(_LanguageEntity);
            return __LanguageWordEntity;
        }
        public cQuery<cLanguageWordEntity> GetLanguages(string _SearchString)
        {
            Regex __NameSeparator = new Regex("\\s+");
            List<string> __SearchStrings = __NameSeparator.Split(_SearchString).ToList();

            IDataService __DataService = DataServiceManager.GetDataService();

            cLanguageWordEntity __LanguageWordEntity = null;
            cLanguageEntity __LanguageEntity = null;

            cQuery<cLanguageWordEntity> __Query = __DataService.Database.Query<cLanguageWordEntity>(() => __LanguageWordEntity)
                .SelectAliasColumn<cLanguageWordEntity>(() => __LanguageWordEntity, __Item => __Item.Word, "Word")
                .SelectAliasColumn<cLanguageWordEntity>(() => __LanguageWordEntity, __Item => __Item.Code, "Code")
                .SelectAliasColumn<cLanguageEntity>(() => __LanguageEntity, __Item => __Item.Name)
                .SelectAliasColumn<cLanguageWordEntity>(() => __LanguageWordEntity, __Item => __Item.ID)
                .SelectAliasColumn<cLanguageWordEntity>(() => __LanguageWordEntity, __Item => __Item.CreateDate)
                .SelectAliasColumn<cLanguageWordEntity>(() => __LanguageWordEntity, __Item => __Item.ParamCount);

            __Query.Inner<cLanguageEntity>().Join(() => __LanguageEntity)
                        .On()
                        .Operand<cLanguageEntity>(() => __LanguageEntity, __Item => __Item.ID).Eq<cLanguageEntity>(() => __LanguageWordEntity)
                        .ToQuery();

            if (__SearchStrings.Count > 0)
            {
                __Query.Where();
            }

            IBaseFilterForOperands<cLanguageWordEntity, cLanguageWordEntity> __Where = __Query.Where();

            int __CountSearchWord = 0;
            __SearchStrings.Remove("");
            if (__SearchStrings.Count > 0)
            {
                __Query.Where().PrOpen.ToQuery();
                for (var i = 0; i < __SearchStrings.Count; i++)
                {
                    if (!__SearchStrings[i].IsNullOrEmpty())
                    {
                        if (__CountSearchWord > 0)
                        {
                            __Where.And.ToQuery();
                        }
                        __Query = __Where.Operand(__Item => __Item.Word).Like("%" + __SearchStrings[i] + "%").ToQuery();
                        __CountSearchWord += 1;

                    }
                }
                __Query.Where().PrClose.ToQuery();
            }


            if (__SearchStrings.Count > 0)
            {
                __CountSearchWord = 0;
                __Query.Where().Or.PrOpen.ToQuery();
                for (var i = 0; i < __SearchStrings.Count; i++)
                {
                    if (!__SearchStrings[i].IsNullOrEmpty())
                    {
                        if (__CountSearchWord > 0)
                        {
                            __Where.And.ToQuery();
                        }
                        __Query = __Where.Operand(__Item => __Item.Code).Like("%" + __SearchStrings[i] + "%").ToQuery();
                        __CountSearchWord += 1;

                    }
                }
                __Query.Where().PrClose.ToQuery();
            }


            return __Query;
        }
        public cLanguageHrefLangEntity GetLanguageHrefByCode(cLanguageEntity _LanguageEntity, string _Code)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cLanguageHrefLangEntity __LanguageWordEntity = __DataService.Database.Query<cLanguageHrefLangEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.Code).Eq(_Code)
                .And
                .Operand<cLanguageEntity>().Eq(_LanguageEntity.ID)
                .ToQuery()
                .OrderBy().Asc(Item => Item.ID).ToQuery()
                .ToList()
                .FirstOrDefault();
            return __LanguageWordEntity;
        }
        public cLanguageWordEntity GetLanguageWordByCode(cLanguageEntity _LanguageEntity, string _Code)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cLanguageWordEntity __LanguageWordEntity = __DataService.Database.Query<cLanguageWordEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.Code).Eq(_Code)
                .And
                .Operand<cLanguageEntity>().Eq(_LanguageEntity.ID)
                .ToQuery()
                .OrderBy().Asc(Item => Item.ID).ToQuery()
                .ToList()
                .FirstOrDefault();
            return __LanguageWordEntity;
        }
        public cLanguageHrefLangEntity CreateLanguageHrefIfNotExists(cLanguageEntity _LanguageEntity, string _Code)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cLanguageHrefLangEntity __LanguageHrefLangEntity = GetLanguageHrefByCode(_LanguageEntity, _Code);
            if (__LanguageHrefLangEntity == null)
            {
                __LanguageHrefLangEntity = AddLanguageHref(_LanguageEntity, _Code);
            }

            return __LanguageHrefLangEntity;
        }
        public cLanguageWordEntity CreateLanguageWordIfNotExists(cLanguageEntity _LanguageEntity, string _Code, string _Word, string _Description, int _ParamCount,bool _Updatable)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cLanguageWordEntity __LanguageWordEntity = GetLanguageWordByCode(_LanguageEntity, _Code);
            string __NewCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(_Word);
            if (__LanguageWordEntity == null)
            {
                __LanguageWordEntity = AddLanguageWord(_LanguageEntity, _Code, _Word, _Description, _ParamCount, __NewCheckSum);
            }
            else if (__LanguageWordEntity.CheckSum != __NewCheckSum)
            {
                if (_Updatable)
                {
                    UpdateLanguageWordCheckSum(__LanguageWordEntity, _Word, __NewCheckSum);
                }
                
            }

            return __LanguageWordEntity;
        }

        public List<cLanguageWordEntity> GetLanguageWords(cLanguageEntity _LanguageEntity)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            List<cLanguageWordEntity> __LanguageWordEntityList = __DataService.Database.Query<cLanguageWordEntity>()
                .SelectAll()
                .Where()
                .Operand<cLanguageEntity>().Eq(_LanguageEntity.ID)
                .ToQuery()
                .ToList();
            return __LanguageWordEntityList;
        }
        public List<dynamic> GetWordByCode(string _Code)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cLanguageEntity __LanguageEntity = null;
            cLanguageWordEntity __LanguageWordEntity = null;

            List<dynamic> __LanguageWordEntityList = __DataService.Database.Query<cLanguageWordEntity>(() => __LanguageWordEntity)
                .SelectAliasColumn<cLanguageWordEntity>(() => __LanguageWordEntity, __Item => __Item.Word, "Word")
                .SelectAliasColumn<cLanguageEntity>(() => __LanguageEntity, __Item => __Item.Name)
                .SelectAliasColumn<cLanguageWordEntity>(() => __LanguageWordEntity, __Item => __Item.ID)
                .SelectAliasColumn<cLanguageWordEntity>(() => __LanguageWordEntity, __Item => __Item.ParamCount)
                .Inner<cLanguageEntity>().Join(() => __LanguageEntity)
                        .On()
                        .Operand<cLanguageEntity>(() => __LanguageEntity, __Item => __Item.ID).Eq<cLanguageEntity>(() => __LanguageWordEntity).ToQuery()
                .Where()
                .Operand(__Item => __Item.Code).Eq(_Code)
                .ToQuery()
                .ToDynamicObjectList();

            return __LanguageWordEntityList;
        }

        public List<cLanguageWordEntity> GetLanguageWords(string _LanguageCode)
        {
            return GetLanguageWords(GetLanguageByCode(_LanguageCode));
        }

        public void RefreshLanguageFromDB(IDataService _DataService)
        {
            foreach (var __LanguageItem in App.Handlers.LanguageHandler.LanguageList)
            {
                //__LanguageItem.Value
                JObject __JObject = new JObject();
                List<cLanguageWordEntity> __Words = GetLanguageWords(__LanguageItem.Key);
                List<string> __WordList = new List<string>();
                for (int i = 0; i < __Words.Count; i++)
                {
                    JObject __MesaageObject = new JObject();
                    __MesaageObject.Add("message", __Words[i].Word);
                    __MesaageObject.Add("description", __Words[i].Description);

                    if (__WordList.IndexOf(__Words[i].Code) == -1)
                    {
                        __WordList.Add(__Words[i].Code);
                        __JObject.Add(__Words[i].Code, __MesaageObject);
                    }
                    else
                    {
                        __JObject.Remove(__Words[i].Code);
                        __JObject.Add(__Words[i].Code, __MesaageObject);
                    }
                }
                __LanguageItem.Value.LanguageObject = __JObject;
            }
        }
    }
}
