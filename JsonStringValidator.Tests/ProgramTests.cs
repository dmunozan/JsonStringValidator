using System;
using Xunit;

namespace JsonStringValidator.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void IsValidJsonStringWhenEmptyStringShouldReturnValid()
        {
            Assert.Equal("Valid", Program.IsValidJsonString("\"\""));
        }

        [Fact]
        public void IsValidJsonStringWhenNullShouldReturnInvalid()
        {
            Assert.Equal("Invalid", Program.IsValidJsonString(null));
        }

        [Fact]
        public void IsValidJsonStringWhenOnlySpaceShouldReturnValid()
        {
            Assert.Equal("Valid", Program.IsValidJsonString("\" \""));
        }

        [Fact]
        public void IsValidJsonStringWhenOnlyOneDoubleQuoteShouldReturnInvalid()
        {
            Assert.Equal("Invalid", Program.IsValidJsonString("\""));
        }

        [Fact]
        public void IsValidJsonStringWhenOnlyBackslashShouldReturnInvalid()
        {
            Assert.Equal("Invalid", Program.IsValidJsonString("\\"));
        }

        [Fact]
        public void IsValidJsonStringWhenOnlyControlCharacterShouldReturnInvalid()
        {
            const int EscapeChar = 27;

            Assert.Equal("Invalid", Program.IsValidJsonString("\"" + Convert.ToChar(EscapeChar) + "\""));
        }

        [Fact]
        public void IsValidJsonStringWhenAllowedCharacterAndControlCharacterShouldReturnInvalid()
        {
            const int EscapeChar = 27;

            Assert.Equal("Invalid", Program.IsValidJsonString("\"2" + Convert.ToChar(EscapeChar) + "\""));
        }

        [Fact]
        public void IsValidJsonStringWhenAllowedCharacterAndBackslashShouldReturnInvalid()
        {
            Assert.Equal("Invalid", Program.IsValidJsonString("\"2\\\""));
        }

        [Fact]
        public void IsValidJsonStringWhenAllowedCharacterAndQuotationMarkShouldReturnInvalid()
        {
            Assert.Equal("Invalid", Program.IsValidJsonString("\"2\"\""));
        }

        [Fact]
        public void IsValidJsonStringWhenIsNotWrappedOnDoubleQuotesShouldReturnInvalid()
        {
            Assert.Equal("Invalid", Program.IsValidJsonString("unquoted text"));
        }

        [Fact]
        public void IsValidJsonStringWhenBackslashAndQuotationMarkShouldReturnValid()
        {
            Assert.Equal("Valid", Program.IsValidJsonString("\"\\\"\""));
        }

        [Fact]
        public void IsValidJsonStringWhenBackslashAndBackslashShouldReturnValid()
        {
            Assert.Equal("Valid", Program.IsValidJsonString("\"\\\\\""));
        }

        [Fact]
        public void IsQuotedWhenInputDataIsNotWrappedOnDoubleQuotesShouldReturnFalse()
        {
            Assert.False(Program.IsQuoted("unquoted text"));
        }

        [Fact]
        public void IsQuotedWhenInputDataIsWrappedOnDoubleQuotesShouldReturnTrue()
        {
            Assert.True(Program.IsQuoted("\"quoted text\""));
        }

        [Fact]
        public void IsQuotedWhenInputDataIsOnlyOneDoubleQuoteShouldReturnFalse()
        {
            Assert.False(Program.IsQuoted("\""));
        }

        [Fact]
        public void IsEscapableCharacterWhenDoubleQuoteShouldReturnTrue()
        {
            Assert.True(Program.IsEscapableCharacter("0123\\\"NoHex", 4, out int incrementIndex));
        }

        [Fact]
        public void IsEscapableCharacterWhenBackslashShouldReturnTrue()
        {
            Assert.True(Program.IsEscapableCharacter("0123\\\\NoHex", 4, out int incrementIndex));
        }

        [Fact]
        public void IsEscapableCharacterWhenSlashShouldReturnTrue()
        {
            Assert.True(Program.IsEscapableCharacter("0123\\/NoHex", 4, out int incrementIndex));
        }

        [Fact]
        public void IsEscapableCharacterWhenBackspaceShouldReturnTrue()
        {
            Assert.True(Program.IsEscapableCharacter("0123\\bNoHex", 4, out int incrementIndex));
        }

        [Fact]
        public void IsEscapableCharacterWhenFormfeedShouldReturnTrue()
        {
            Assert.True(Program.IsEscapableCharacter("0123\\fNoHex", 4, out int incrementIndex));
        }

        [Fact]
        public void IsEscapableCharacterWhenNewlineShouldReturnTrue()
        {
            Assert.True(Program.IsEscapableCharacter("0123\\nNoHex", 4, out int incrementIndex));
        }

        [Fact]
        public void IsEscapableCharacterWhenCarriageReturnShouldReturnTrue()
        {
            Assert.True(Program.IsEscapableCharacter("0123\\rNoHex", 4, out int incrementIndex));
        }

        [Fact]
        public void IsEscapableCharacterWhenHorizontalTabShouldReturnTrue()
        {
            Assert.True(Program.IsEscapableCharacter("0123\\tNoHex", 4, out int incrementIndex));
        }

        [Fact]
        public void IsEscapableCharacterWhenNonEscapableCharacterShouldReturnFalse()
        {
            Assert.False(Program.IsEscapableCharacter("0123\\aNoHex", 4, out int incrementIndex));
        }

        [Fact]
        public void IsEscapableCharacterWhenUnicodeCharacterFollowedByFourHexCharactersShouldReturnTrue()
        {
            Assert.True(Program.IsEscapableCharacter("0123\\u67B9x", 4, out int incrementIndex));
        }

        [Fact]
        public void IsEscapableCharacterWhenUnicodeCharacterNotFollowedByFourHexCharactersShouldReturnFalse()
        {
            Assert.False(Program.IsEscapableCharacter("0123\\uG7B9x", 4, out int incrementIndex));
        }
    }
}