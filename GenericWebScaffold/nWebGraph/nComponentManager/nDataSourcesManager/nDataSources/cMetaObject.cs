using Base.Data.nDataService.nDatabase.nEntity;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.
    nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetMetaDataCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources
{
    public class cMetaObject<TEntity> where TEntity : cBaseEntity
    {
        public string Title { get; set; }
        public string FieldName { get; set; }
        public string Type { get; set; }
        public bool Visible { get; set; }
        public string Editable { get; set; }
        public bool Removable { get; set; }
        public bool TranslateValue { get; set; }
        public bool Readonly { get; set; }
        public object LookUpDataSource { get; set; }

        public cMetaObject(
            IController _Controller,
            string _FieldName
            , string _Type
            , cDataSourceFieldTypeProps<TEntity> _Props
        )
        {
            Title = _Controller.GetWordValue(_Props.Title);
            TranslateValue = _Props.TranslateValue;
            FieldName = _FieldName;
            string __TypeLowerName = _Type.ToLower();
            if (__TypeLowerName == "string")
            {
                Type = "string";
            }
            else if (__TypeLowerName == "Int" || __TypeLowerName == "Int64" || __TypeLowerName == "ınt" ||
                     __TypeLowerName == "ınt64")
            {
                Type = "numeric";
            }
            else if (__TypeLowerName == "boolean")
            {
                Type = "boolean";
            }

            Visible = _Props.Visible;
            Editable = _Props.Editable.Code;
            Removable = _Props.Removable;
            Readonly = _Props.Readonly;
        }

        public void LoadLookUp(cListenerEvent _ListenerEvent, IController _Controller,
            cDataSource_GetMetaDataCommandData _ReceivedData, cDataSourceFieldTypeProps<TEntity> _Props)
        {
            if (_Props.LookUpDataSource != null)
            {
                LookUpDataSource = _Props.LookUpDataSource.ToLookUpObject(_ListenerEvent, _Controller, _ReceivedData);
            }
        }
    }
}