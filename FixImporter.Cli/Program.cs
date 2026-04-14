using FixImporter.Core;

var sqlFormat = args.Any(static arg => arg.Equals("--sql", StringComparison.OrdinalIgnoreCase));
var keepAlive = args.Any(static arg => arg.Equals("--keep-alive", StringComparison.OrdinalIgnoreCase));
var data = Environment.GetEnvironmentVariable("INPUT_DATA");

if (string.IsNullOrWhiteSpace(data))
{
    using var reader = new StreamReader(Console.OpenStandardInput());
    data = await reader.ReadToEndAsync();
}

var processor = new ImportProcessor(new InMemoryClipboardService());
var result = processor.Process(data, sqlFormat);

if (result.Status != ProcessingStatus.Success)
{
    Console.Error.WriteLine(result.Message);
    return 1;
}

Console.WriteLine(result.Output);
Console.WriteLine();
Console.WriteLine($"Total: {result.TotalRecords} | Unicos: {result.DistinctRecords} | Duplicidades: {result.Duplicates}");

if (keepAlive)
{
    Console.WriteLine("Modo keep-alive ativo. Container permanecera em execucao.");
    await Task.Delay(Timeout.InfiniteTimeSpan);
}

return 0;

internal sealed class InMemoryClipboardService : IClipboardService
{
    public string Value { get; private set; } = string.Empty;

    public void SetText(string value)
    {
        Value = value;
    }
}
