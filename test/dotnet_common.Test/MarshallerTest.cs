using System;
using dotnet_common.Interface;
using dotnet_common.Test.Model;
using Xunit;

namespace dotnet_common.Test
{
    /// <summary>
    /// Marshaller tests
    /// </summary>
    public class MarshallerTest
    {
        /// <summary>
        /// The marshaller
        /// </summary>
        private readonly IMarshaller _marshaller = new DataContractSerializerMarshaller();

        /// <summary>
        /// Tests that SerializeObject for a model executes positive.
        /// </summary>
        [Fact]
        public void SerializeObject_Model_Executes_Positive()
        {
            var model = new MarshallerTestObject
            {
                Name = "John",
                Surname = "Doe",
                Age = 25,
                DateOfBirth = new DateTime(1980, 1, 1)
            };

            var result = _marshaller.SerializeObject(model);

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that SerializeObject for a value type executes positive.
        /// </summary>
        [Fact]
        public void SerializeObject_ValueType_Executes_Positive()
        {
            var model = new DateTime(1980, 10, 1);

            var result = _marshaller.SerializeObject(model);

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that DeserializeObject for a model executes positive.
        /// </summary>
        [Fact]
        public void DeserializeObject_Model_Executes_Positive()
        {
            var model = new MarshallerTestObject
            {
                Name = "John",
                Surname = "Doe",
                Age = 25,
                DateOfBirth = new DateTime(1980, 1, 1)
            };

            var result = _marshaller.SerializeObject(model);

            Assert.NotNull(result);

            var deserializedResult = _marshaller.DeserializeObject<MarshallerTestObject>(result);

            Assert.NotNull(deserializedResult);
            Assert.Equal(model.Name, deserializedResult.Name);
            Assert.Equal(model.Surname, deserializedResult.Surname);
            Assert.Equal(model.Age, deserializedResult.Age);
            Assert.Equal(model.DateOfBirth, deserializedResult.DateOfBirth);
        }

        /// <summary>
        /// Tests that DeserializeObject for a value type executes positive.
        /// </summary>
        [Fact]
        public void DeserializeObject_ValueType_Executes_Positive()
        {
            var model = new DateTime(1980, 10, 1);

            var result = _marshaller.SerializeObject(model);

            Assert.NotNull(result);

            var deserializedResult = _marshaller.DeserializeObject<DateTime>(result);

            Assert.Equal(deserializedResult, model);
        }
    }
}
