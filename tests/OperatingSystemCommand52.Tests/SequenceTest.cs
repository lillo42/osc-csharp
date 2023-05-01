using System.Text;
using FluentAssertions;

namespace OperatingSystemCommand52.Tests;

public class SequenceTest
{
    private readonly Sequence _sequence = new();

    [Theory]
    [InlineData("hello world", Clipboard.System, Mode.Default, 0, "\x1b]52;c;aGVsbG8gd29ybGQ=\x07")]
    [InlineData("", Clipboard.System, Mode.Default, 0, "\x1b]52;c;\x07")]
    [InlineData("hello world", Clipboard.Primary, Mode.Default, 0, "\x1b]52;p;aGVsbG8gd29ybGQ=\x07")]
    [InlineData("hello world", Clipboard.System, Mode.Tmux, 0, "\x1bPtmux;\x1b\x1b]52;c;aGVsbG8gd29ybGQ=\x07\x1b\\")]
    [InlineData("hello world", Clipboard.System, Mode.Screen, 0, "\x1bP\x1b]52;c;aGVsbG8gd29ybGQ=\x07\x1b\\")]
    [InlineData("hello world hello world hello world hello world hello world hello world hello world hello world",
        Clipboard.System, Mode.Screen, 0,
        "\x1bP\x1b]52;c;aGVsbG8gd29ybGQgaGVsbG8gd29ybGQgaGVsbG8gd29ybGQgaGVsbG8gd29ybGQgaGVsbG8gd29y\x1b\\\x1bPbGQgaGVsbG8gd29ybGQgaGVsbG8gd29ybGQgaGVsbG8gd29ybGQ=\a\x1b\\")]
    [InlineData("hello world", Clipboard.System, Mode.Default, 11, "\x1b]52;c;aGVsbG8gd29ybGQ=\x07")]
    [InlineData("hello world", Clipboard.System, Mode.Default, 10, "")]
    public void Copy(string content, Clipboard clipboard, Mode mode, int limit, string expected) => _sequence
        .SetContent(content)
        .SetClipboard(clipboard)
        .SetMode(mode)
        .SetLimit(limit)
        .ToString()
        .Should().Be(expected);

    [Theory]
    [InlineData(Mode.Default, Clipboard.System, "\x1b]52;c;?\x07")]
    [InlineData(Mode.Default, Clipboard.Primary, "\x1b]52;p;?\x07")]
    [InlineData(Mode.Tmux, Clipboard.System, "\x1bPtmux;\x1b\x1b]52;c;?\x07\x1b\\")]
    [InlineData(Mode.Screen, Clipboard.System, "\x1bP\x1b]52;c;?\x07\x1b\\")]
    [InlineData(Mode.Tmux, Clipboard.Primary, "\x1bPtmux;\x1b\x1b]52;p;?\x07\x1b\\")]
    [InlineData(Mode.Screen, Clipboard.Primary, "\x1bP\x1b]52;p;?\x07\x1b\\")]
    public void Query(Mode mode, Clipboard clipboard, string expected) => _sequence
        .SetQueryOperation()
        .SetClipboard(clipboard)
        .SetMode(mode)
        .ToString()
        .Should().Be(expected);

    [Theory]
    [InlineData(Mode.Default, Clipboard.System, "\x1b]52;c;!\x07")]
    [InlineData(Mode.Tmux, Clipboard.System, "\x1bPtmux;\x1b\x1b]52;c;!\x07\x1b\\")]
    [InlineData(Mode.Screen, Clipboard.System, "\x1bP\x1b]52;c;!\x07\x1b\\")]
    public void Clear(Mode mode, Clipboard clipboard, string expected) => _sequence
        .SetClearOperation()
        .SetClipboard(clipboard)
        .SetMode(mode)
        .ToString()
        .Should().Be(expected);

    [Theory]
    [InlineData("hello world", Clipboard.System, Mode.Default, 0, "\x1b]52;c;aGVsbG8gd29ybGQ=\x07")]
    [InlineData("", Clipboard.System, Mode.Default, 0, "\x1b]52;c;\x07")]
    public void Write(string content, Clipboard clipboard, Mode mode, int limit, string expected)
    {
        using var stream = new MemoryStream();
        _sequence
            .SetContent(content)
            .SetClipboard(clipboard)
            .SetMode(mode)
            .SetLimit(limit)
            .Write(stream);
        
        stream.Flush();
        stream.Position = 0;

        using var reader = new StreamReader(stream, Encoding.UTF8);
        var text = reader.ReadToEnd();
        text.Should().Be(expected);
    }
}
