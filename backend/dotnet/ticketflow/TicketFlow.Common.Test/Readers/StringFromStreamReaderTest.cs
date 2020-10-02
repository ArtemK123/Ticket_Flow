using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TicketFlow.Common.Readers;
using Xunit;

namespace TicketFlow.Common.Test.Readers
{
    public class StringFromStreamReaderTest
    {
        private const string ExpectedString = "test";
        private readonly StringFromStreamReader stringFromStreamReader;

        public StringFromStreamReaderTest()
        {
            stringFromStreamReader = new StringFromStreamReader();
        }

        [Fact]
        public async Task ReadAsync_ShouldReadTextFromStream_Async()
        {
            Encoding encoding = Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(ExpectedString);
            Stream stream = new MemoryStream(bytes);

            string actual = await stringFromStreamReader.ReadAsync(stream, encoding);

            Assert.Equal(ExpectedString, actual);
        }

        [Fact]
        public async Task ReadAsync_WrongEncoding_ShouldReturnInvalidString_Async()
        {
            byte[] bytes = Encoding.UTF8.GetBytes(ExpectedString);
            Stream stream = new MemoryStream(bytes);

            string actual = await stringFromStreamReader.ReadAsync(stream, Encoding.BigEndianUnicode);

            Assert.NotEqual(ExpectedString, actual);
        }

        [Fact]
        public async Task ReadAsync_EmptyStream_ShouldReturnEmptyString_Async()
        {
            Stream stream = new MemoryStream(Array.Empty<byte>());

            string actual = await stringFromStreamReader.ReadAsync(stream, Encoding.UTF8);

            Assert.Equal(string.Empty, actual);
        }

        [Fact]
        public async Task ReadAsync_NullStream_ShouldThrowException_Async()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => stringFromStreamReader.ReadAsync(null, Encoding.UTF8));
        }

        [Fact]
        public async Task ReadAsync_NullEncoding_ShouldNotThrowException_Async()
        {
            await stringFromStreamReader.ReadAsync(new MemoryStream(new byte[] { 1, 2 }), null);
        }
    }
}