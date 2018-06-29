namespace dotnet_common.Interface
{
    /// <summary>
    /// Marshaller interface (used for serialization and deserialization)
    /// </summary>
    public interface IMarshaller
    {
        /// <summary>
        /// Serializes the object to a string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        string SerializeObject<T>(T entity);

        /// <summary>
        /// Deserializes the object from a string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T DeserializeObject<T>(string entity);
    }
}
