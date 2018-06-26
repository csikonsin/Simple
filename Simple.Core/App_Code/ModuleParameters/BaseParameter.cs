using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple.Core.Code.ModuleParameters
{

    public abstract class BaseParameter
    {

    }

    public static class BaseParameterExtensions
    {
        public static string Serialize(this BaseParameter obj)
        {
            string result = String.Empty;
            try
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
                using (var writer = new System.IO.StringWriter())
                {
                    serializer.Serialize(writer, obj);
                    result = writer.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public static T Deserialize<T>(this string xml) where T : BaseParameter
        {
            if (string.IsNullOrWhiteSpace(xml)) return null;

            T result = default(T);

            try
            {
                using (var reader = new System.IO.StringReader(xml))
                {
                    var deserializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    result = (T)deserializer.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}