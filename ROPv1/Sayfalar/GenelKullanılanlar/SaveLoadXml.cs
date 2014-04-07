using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace ROPv1
{
    public class XmlSave
    {
        public static void SaveRestoran(object IClass, string filename)
        {
            StreamWriter writer = null;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(IClass.GetType()); //serializerı oluşturuyoruz
                writer = new StreamWriter(filename);
                xmlSerializer.Serialize(writer, IClass);
            }
            finally
            {
                if(writer != null)
                    writer.Close();
                writer = null;
            }
        }
    }

    public class XmlLoad<T>
    {
        public static Type type;

        public XmlLoad()
        {
            type = typeof(T[]);
        }

        public T[] LoadRestoran(string filename)
        {
            T[] result;
            XmlSerializer xmlSerializer = new XmlSerializer(type);
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            result = (T[])xmlSerializer.Deserialize(fs);
            fs.Close();
            return result;
        }
    }
}
