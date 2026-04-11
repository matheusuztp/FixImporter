using System.Diagnostics.CodeAnalysis;

namespace FixImporter;

public enum ProcessingStatus
{
    InvalidParameters,
    Unauthorized,
    ProcessingError,
    Success
}

public sealed class ProcessResult
{
    public required ProcessingStatus Status { get; init; }
    public required string Message { get; init; }
    public int TotalRecords { get; init; }
    public int DistinctRecords { get; init; }
    public int Duplicates { get; init; }
    public string Output { get; init; } = string.Empty;
}

public interface IClipboardService
{
    void SetText(string value);
}

[ExcludeFromCodeCoverage]
public sealed class WindowsClipboardService : IClipboardService
{
    public void SetText(string value)
    {
        Clipboard.SetText(value);
    }
}

public sealed class ImportProcessor
{
    private readonly IClipboardService _clipboardService;

    public ImportProcessor(IClipboardService clipboardService)
    {
        _clipboardService = clipboardService ?? throw new ArgumentNullException(nameof(clipboardService));
    }

    public ProcessResult Process(string? data, bool sqlFormat)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            return new ProcessResult
            {
                Status = ProcessingStatus.InvalidParameters,
                Message = "Erro: nenhum dado foi inserido."
            };
        }

        try
        {
            var lines = data
                .Replace("\\n", "\n")
                .Split(["\r\n", "\r", "\n"], StringSplitOptions.None);

            var filteredLines = lines
                .Where(static line => !string.IsNullOrWhiteSpace(line))
                .Select(static line => line.Trim())
                .ToList();

            var totalRecords = filteredLines.Count;
            var distinctList = filteredLines.Distinct().ToList();
            var distinctCount = distinctList.Count;
            var duplicates = totalRecords - distinctCount;

            var output = sqlFormat
                ? string.Join(",\r\n", distinctList.Select(static item => $"'{item.Replace("'", "''")}'"))
                : string.Join(Environment.NewLine, distinctList);

            _clipboardService.SetText(output);

            return new ProcessResult
            {
                Status = ProcessingStatus.Success,
                Message = "Processamento concluido com sucesso.",
                Output = output,
                TotalRecords = totalRecords,
                DistinctRecords = distinctCount,
                Duplicates = duplicates
            };
        }
        catch (UnauthorizedAccessException ex)
        {
            return new ProcessResult
            {
                Status = ProcessingStatus.Unauthorized,
                Message = $"Erro de autorizacao ao acessar o clipboard: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ProcessResult
            {
                Status = ProcessingStatus.ProcessingError,
                Message = $"Erro ao processar dados: {ex.Message}"
            };
        }
    }
}
