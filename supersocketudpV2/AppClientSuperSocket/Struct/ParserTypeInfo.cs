using IMESAgent.SocketClientEngine;
using IMESAgent.SocketClientEngine.FileLoaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppClientSuperSocket
{
    public class Configurable
    {
        [Name(FieldName.FileNameFilter, false)]
        public string FileNameFilter
        {
            get;
            set;
        }
    }
    public class ParserTypeInfo : Configurable
    {
        [Name(FieldName.ParseTypeCode)]
        public virtual string ParseTypeCode
        {
            get;
            set;
        }

        [Name(FieldName.ParseTypeName)]
        public virtual string ParseTypeName
        {
            get;
            set;
        }

        [Name(FieldName.ParseGroupCode)]
        public virtual string ParseGroupCode
        {
            get;
            set;
        }

        [Name(FieldName.ParseGroupName)]
        public virtual string ParseGroupName
        {
            get;
            set;
        }

        [Name(FieldName.Plugin)]
        public virtual string Plugin
        {
            get;
            set;
        }

        [Name(FieldName.Includes)]
        public virtual string Includes
        {
            get;
            set;
        }

        [Name(FieldName.Excludes)]
        public virtual string Excludes
        {
            get;
            set;
        }

        [Name(FieldName.Prefixs)]
        public virtual string Prefixs
        {
            get;
            set;
        }

        [Name(FieldName.Interval)]
        public virtual string Interval
        {
            get;
            set;
        }
    }
}
