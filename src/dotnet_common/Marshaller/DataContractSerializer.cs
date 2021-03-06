﻿using System.IO;
using dotnet_common.Interface;

namespace dotnet_common.Marshaller
{
    /// <summary>
    /// Data contract serializer implementation of the marshaller
    /// </summary>
    /// <seealso cref="dotnet_common.Interface.IMarshaller" />
    public class DataContractSerializer : IMarshaller
    {
        /// <summary>
        /// Serializes the object to a string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public string SerializeObject<T>(T entity)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var reader = new StreamReader(memoryStream))
                {
                    var serializer = new System.Runtime.Serialization.DataContractSerializer(entity.GetType());
                    serializer.WriteObject(memoryStream, entity);
                    memoryStream.Position = 0;
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Deserializes the object from a string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public T DeserializeObject<T>(string entity)
        {
            using (Stream stream = new MemoryStream())
            {
                var data = System.Text.Encoding.UTF8.GetBytes(entity);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var deserializer = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
                return (T)deserializer.ReadObject(stream);
            }
        }
    }
}
