using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;

namespace DAL
{
    public interface IDataProvider
    {
        protected static Type[] _serializationTypes =
        {
            typeof(Project),
            typeof(Task),
            typeof(TaskStatus),
            typeof(DateTime),
            typeof(TeamMember),
        };

        public void Write(string fileName, object data);

        public object? Read(string fileName);
    }

    public class JSONDataProvider : IDataProvider
    {
        public void Write(string fileName, object data)
        {
            File.Delete(fileName);
            using FileStream fs = File.Open(fileName, FileMode.OpenOrCreate);

            DataContractJsonSerializer serializer = new(typeof(object), IDataProvider._serializationTypes);

            serializer.WriteObject(fs, data);
        }

        public object? Read(string fileName)
        {
            using FileStream fs = File.Open(fileName, FileMode.Open);

            DataContractJsonSerializer serializer = new(typeof(object), IDataProvider._serializationTypes);

            return serializer.ReadObject(fs);
        }
    }

    public class XMLDataProvider : IDataProvider
    {
        public void Write(string fileName, object data)
        {
            File.Delete(fileName);
            using FileStream fs = File.Open(fileName, FileMode.OpenOrCreate);
            using XmlDictionaryWriter writer = XmlDictionaryWriter.CreateBinaryWriter(fs);

            DataContractSerializer serializer = new(typeof(object), IDataProvider._serializationTypes);

            serializer.WriteObject(fs, data);
        }

        public object? Read(string fileName)
        {
            using FileStream fs = File.Open(fileName, FileMode.Open);
            using XmlDictionaryReader reader = XmlDictionaryReader.CreateBinaryReader(fs, XmlDictionaryReaderQuotas.Max);

            DataContractSerializer serializer = new(typeof(object), IDataProvider._serializationTypes);

            return serializer.ReadObject(fs);
        }
    }

    public class BinaryDataProvider : IDataProvider
    {
        public void Write(string fileName, object data)
        {
            File.Delete(fileName);
            using FileStream fs = File.Open(fileName, FileMode.OpenOrCreate);

            DataContractSerializer serializer = new(typeof(object), IDataProvider._serializationTypes);

            serializer.WriteObject(fs, data);
        }

        public object? Read(string fileName)
        {
            using FileStream fs = File.Open(fileName, FileMode.Open);

            DataContractSerializer serializer = new(typeof(object), IDataProvider._serializationTypes);

            return serializer.ReadObject(fs);
        }
    }
}