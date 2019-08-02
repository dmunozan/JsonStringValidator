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
            const int Backslash = 92;

            Assert.Equal("Invalid", Program.IsValidJsonString("\"2" + Convert.ToChar(Backslash) + "\""));
        }

        [Fact]
        public void IsValidJsonStringWhenAllowedCharacterAndQuotationMarkShouldReturnInvalid()
        {
            const int QuotationMark = 34;

            Assert.Equal("Invalid", Program.IsValidJsonString("\"2" + Convert.ToChar(QuotationMark) + "\""));
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
            const int QuotationMark = 34;

            Assert.True(Program.IsEscapableCharacter(Convert.ToChar(QuotationMark)));
        }

        [Fact]
        public void IsEscapableCharacterWhenBackslashShouldReturnTrue()
        {
            const int Backslash = 92;

            Assert.True(Program.IsEscapableCharacter(Convert.ToChar(Backslash)));
        }

        [Fact]
        public void IsEscapableCharacterWhenSlashShouldReturnTrue()
        {
            const int Slash = 47;

            Assert.True(Program.IsEscapableCharacter(Convert.ToChar(Slash)));
        }

        [Fact]
        public void IsEscapableCharacterWhenBackspaceShouldReturnTrue()
        {
            const char Backspace = 'b';

            Assert.True(Program.IsEscapableCharacter(Backspace));
        }

        [Fact]
        public void IsEscapableCharacterWhenFormfeedShouldReturnTrue()
        {
            const char Formfeed = 'f';

            Assert.True(Program.IsEscapableCharacter(Formfeed));
        }

        [Fact]
        public void IsEscapableCharacterWhenNewlineShouldReturnTrue()
        {
            const char Newline = 'n';

            Assert.True(Program.IsEscapableCharacter(Newline));
        }

        [Fact]
        public void IsEscapableCharacterWhenCarriageReturnShouldReturnTrue()
        {
            const char CarriageReturn = 'r';

            Assert.True(Program.IsEscapableCharacter(CarriageReturn));
        }
    }
}