using Bogus;

namespace FixImporter.Tests;

public sealed class ImportProcessorTests
{
    #region InvalidParameters

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\r\n")]
    public void Process_ShouldReturnInvalidParameters_WhenInputIsMissing(string? input)
    {
        var clipboard = new FakeClipboardService();
        var sut = new ImportProcessor(clipboard);

        var result = sut.Process(input, sqlFormat: false);

        Assert.Equal(ProcessingStatus.InvalidParameters, result.Status);
        Assert.Contains("nenhum dado", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Equal(string.Empty, clipboard.StoredText);
    }

    #endregion

    #region Unauthorized

    [Fact]
    public void Process_ShouldReturnUnauthorized_WhenClipboardThrowsUnauthorizedAccessException()
    {
        var faker = new Faker("pt_BR");
        var randomLine = faker.Commerce.ProductName();

        var clipboard = new FakeClipboardService
        {
            ExceptionToThrow = new UnauthorizedAccessException("Clipboard access denied")
        };

        var sut = new ImportProcessor(clipboard);

        var result = sut.Process(randomLine, sqlFormat: false);

        Assert.Equal(ProcessingStatus.Unauthorized, result.Status);
        Assert.Contains("autorizacao", result.Message, StringComparison.OrdinalIgnoreCase);
    }

    #endregion

    #region ProcessingError

    [Fact]
    public void Process_ShouldReturnProcessingError_WhenUnexpectedExceptionOccurs()
    {
        var faker = new Faker("pt_BR");
        var input = faker.Lorem.Lines(2);

        var clipboard = new FakeClipboardService
        {
            ExceptionToThrow = new InvalidOperationException("Unexpected clipboard state")
        };

        var sut = new ImportProcessor(clipboard);

        var result = sut.Process(input, sqlFormat: false);

        Assert.Equal(ProcessingStatus.ProcessingError, result.Status);
        Assert.Contains("Erro ao processar", result.Message, StringComparison.OrdinalIgnoreCase);
    }

    #endregion

    #region Success

    [Fact]
    public void Process_ShouldNormalizeDistinctAndCopyOutput_WhenSqlFormatIsDisabled()
    {
        var faker = new Faker("pt_BR");
        var items = faker.Make(8, () => faker.Random.AlphaNumeric(6).ToUpperInvariant());

        var input = string.Join("\n", items) + "\n" + items[0] + "\n   " + items[1] + "   ";

        var clipboard = new FakeClipboardService();
        var sut = new ImportProcessor(clipboard);

        var result = sut.Process(input, sqlFormat: false);

        Assert.Equal(ProcessingStatus.Success, result.Status);
        Assert.Equal(items.Count + 2, result.TotalRecords);
        Assert.Equal(items.Count, result.DistinctRecords);
        Assert.Equal(2, result.Duplicates);

        var expectedOutput = string.Join(Environment.NewLine, items);
        Assert.Equal(expectedOutput, result.Output);
        Assert.Equal(expectedOutput, clipboard.StoredText);
    }

    [Fact]
    public void Process_ShouldEscapeSingleQuotes_WhenSqlFormatIsEnabled()
    {
        var faker = new Faker("pt_BR");
        var raw1 = $"{faker.Name.FirstName()} d'agua";
        var raw2 = faker.Company.CompanyName();
        var input = $"{raw1}\\n{raw2}\\n{raw1}";

        var clipboard = new FakeClipboardService();
        var sut = new ImportProcessor(clipboard);

        var result = sut.Process(input, sqlFormat: true);

        Assert.Equal(ProcessingStatus.Success, result.Status);
        Assert.Equal(3, result.TotalRecords);
        Assert.Equal(2, result.DistinctRecords);
        Assert.Equal(1, result.Duplicates);

        var expectedOutput = $"'{raw1.Replace("'", "''")}',\r\n'{raw2.Replace("'", "''")}'";
        Assert.Equal(expectedOutput, result.Output);
        Assert.Equal(expectedOutput, clipboard.StoredText);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenClipboardServiceIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new ImportProcessor(clipboardService: null!));
    }

    #endregion

    private sealed class FakeClipboardService : IClipboardService
    {
        public string StoredText { get; private set; } = string.Empty;
        public Exception? ExceptionToThrow { get; init; }

        public void SetText(string value)
        {
            if (ExceptionToThrow is not null)
            {
                throw ExceptionToThrow;
            }

            StoredText = value;
        }
    }
}
