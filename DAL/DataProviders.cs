using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;

namespace DAL
{
    public interface IDataProvider
    {
        public void Write(string fileName, object data, Type[] dataTypes);

        public object? Read(string fileName, Type[] dataTypes);
    }

    public class JSONDataProvider : IDataProvider
    {
        public void Write(string fileName, object data, Type[] dataTypes)
        {
            File.Delete(fileName);
            using FileStream fs = File.Open(fileName, FileMode.OpenOrCreate);

            DataContractJsonSerializer serializer = new(typeof(object), dataTypes);

            serializer.WriteObject(fs, data);
        }

        public object? Read(string fileName, Type[] dataTypes)
        {
            using FileStream fs = File.Open(fileName, FileMode.Open);

            DataContractJsonSerializer serializer = new(typeof(object), dataTypes);

            return serializer.ReadObject(fs);
        }
    }

    public class XMLDataProvider : IDataProvider
    {
        public void Write(string fileName, object data, Type[] dataTypes)
        {
            File.Delete(fileName);
            using FileStream fs = File.Open(fileName, FileMode.OpenOrCreate);
            using XmlDictionaryWriter writer = XmlDictionaryWriter.CreateBinaryWriter(fs);

            DataContractSerializer serializer = new(typeof(object), dataTypes);

            serializer.WriteObject(fs, data);
        }

        public object? Read(string fileName, Type[] dataTypes)
        {
            using FileStream fs = File.Open(fileName, FileMode.Open);
            using XmlDictionaryReader reader = XmlDictionaryReader.CreateBinaryReader(fs, XmlDictionaryReaderQuotas.Max);

            DataContractSerializer serializer = new(typeof(object), dataTypes);

            return serializer.ReadObject(fs);
        }
    }

    public class BinaryDataProvider : IDataProvider
    {
        public void Write(string fileName, object data, Type[] dataTypes)
        {
            File.Delete(fileName);
            using FileStream fs = File.Open(fileName, FileMode.OpenOrCreate);

            DataContractSerializer serializer = new(typeof(object), dataTypes);

            serializer.WriteObject(fs, data);
        }

        public object Read(string fileName, Type[] dataTypes)
        {
            using FileStream fs = File.Open(fileName, FileMode.Open);

            DataContractSerializer serializer = new(typeof(object), dataTypes);

            return serializer.ReadObject(fs);
        }
    }
}

