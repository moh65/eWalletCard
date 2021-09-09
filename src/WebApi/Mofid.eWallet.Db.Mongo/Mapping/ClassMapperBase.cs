using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mofid.eWallet.Db.Mongo.Mapping
{
    public abstract class ClassMapperBase
    {
        public static void MapAllClasses()
        {
            var types = Assembly.GetExecutingAssembly().DefinedTypes;
            foreach(TypeInfo type in types)
            {
                if (type.IsClass && type.BaseType == typeof(ClassMapperBase))
                {
                    var mapper = Activator.CreateInstance(type) as ClassMapperBase;
                    mapper.Map();
                }
            }
        }
        public abstract void Map();
    }
}