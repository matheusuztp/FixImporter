using System.Diagnostics.CodeAnalysis;
using FixImporter.Core;

namespace FixImporter;

[ExcludeFromCodeCoverage]
public sealed class WindowsClipboardService : IClipboardService
{
    public void SetText(string value)
    {
        Clipboard.SetText(value);
    }
}
