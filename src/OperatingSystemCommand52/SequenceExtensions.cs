using System.Text;

namespace OperatingSystemCommand52;

/// <summary>
/// The <see cref="Sequence"/> extensions.
/// </summary>
public static class SequenceExtensions
{
    /// <summary>
    /// Set the <see cref="Sequence.Limit" /> as <paramref name="limit"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <param name="limit">The limit.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Limit" /> as <paramref name="limit"/>.</returns>
    public static Sequence SetLimit(this Sequence sequence, int limit)
    {
        sequence.Limit = limit;
        return sequence;
    }

    /// <summary>
    /// Set the <see cref="Sequence.Encoding" /> as <paramref name="encoding"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <param name="encoding">The <see cref="Encoding"/>.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Encoding" /> as <paramref name="encoding"/>.</returns>
    public static Sequence SetEncoding(this Sequence sequence, Encoding encoding)
    {
        sequence.Encoding = encoding;
        return sequence;
    }

    /// <summary>
    /// Set the <see cref="Sequence.Encoding" /> as <see cref="Encoding.UTF8"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Encoding" /> as <see cref="Encoding.UTF8"/>.</returns>
    public static Sequence SetUTF8Encoding(this Sequence sequence) 
        => SetEncoding(sequence, Encoding.UTF8);
    
    /// <summary>
    /// Set the <see cref="Sequence.Encoding" /> as <see cref="Encoding.Unicode"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Encoding" /> as <see cref="Encoding.Unicode"/>.</returns>
    public static Sequence SetUnicodeEncoding(this Sequence sequence) 
        => SetEncoding(sequence, Encoding.Unicode);

    /// <summary>
    /// Set the <see cref="Sequence.Content" /> as <paramref name="str"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <param name="str">The <see cref="string"/>.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Content" /> as <paramref name="str"/>.</returns>
    public static Sequence SetContent(this Sequence sequence, string str)
    {
        sequence.Content = str;
        return sequence;
    }

    /// <summary>
    /// Set the <see cref="Sequence.Content" /> as <paramref name="str"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <param name="str">The <see cref="string"/> array.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Content" /> as <paramref name="str"/> join with space.</returns>
    public static Sequence SetContent(this Sequence sequence, params string[] str)
    {
        sequence.Content = string.Join(' ', str);
        return sequence;
    }

    /// <summary>
    /// Set the <see cref="Sequence.Content" /> as <paramref name="str"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <param name="str">The <see cref="string"/> array.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Content" /> as <paramref name="str"/> join with space.</returns>
    public static Sequence SetContent(this Sequence sequence, IEnumerable<string> str)
    {
        sequence.Content = string.Join(' ', str);
        return sequence;
    }

    /// <summary>
    /// Set the <see cref="Sequence.Mode" /> as <paramref name="mode"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <param name="mode">The <see cref="Mode"/>.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Mode" /> as <paramref name="mode"/>.</returns>
    public static Sequence SetMode(this Sequence sequence, Mode mode)
    {
        sequence.Mode = mode;
        return sequence;
    }

    /// <summary>
    /// Set the <see cref="Sequence.Mode" /> as <see cref="Mode.Tmux"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Mode" /> as <see cref="Mode.Tmux"/>.</returns>
    public static Sequence SetTmuxMode(this Sequence sequence)
        => SetMode(sequence, Mode.Tmux);

    /// <summary>
    /// Set the <see cref="Sequence.Mode" /> as <see cref="Mode.Screen"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Mode" /> as <see cref="Mode.Screen"/>.</returns>
    public static Sequence SetScreenMode(this Sequence sequence)
        => SetMode(sequence, Mode.Screen);

    /// <summary>
    /// Set the <see cref="Sequence.Mode" /> as <see cref="Mode.Default"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Mode" /> as <see cref="Mode.Default"/>.</returns>
    public static Sequence SetDefaultMode(this Sequence sequence)
        => SetMode(sequence, Mode.Default);

    /// <summary>
    /// Set the <see cref="Sequence.Clipboard" /> as <paramref name="clipboard"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <param name="clipboard">The <see cref="Clipboard"/>.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Clipboard" /> as <paramref name="clipboard"/>.</returns>
    public static Sequence SetClipboard(this Sequence sequence, Clipboard clipboard)
    {
        sequence.Clipboard = clipboard;
        return sequence;
    }

    /// <summary>
    /// Set the <see cref="Sequence.Clipboard" /> as <see cref="Clipboard.Primary"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Clipboard" /> as <see cref="Clipboard.Primary"/>.</returns>
    public static Sequence SetPrimaryClipboard(this Sequence sequence)
        => SetClipboard(sequence, Clipboard.Primary);

    /// <summary>
    /// Set the <see cref="Sequence.Clipboard" /> as <see cref="Clipboard.System"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Clipboard" /> as <see cref="Clipboard.System"/>.</returns>
    public static Sequence SetSystemClipboard(this Sequence sequence)
        => SetClipboard(sequence, Clipboard.System);

    /// <summary>
    /// Set the <see cref="Sequence.Operation" /> as <paramref name="operation"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <param name="operation">The <see cref="Operation"/>.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Operation" /> as <paramref name="operation"/>.</returns>
    public static Sequence SetOperation(this Sequence sequence, Operation operation)
    {
        sequence.Operation = operation;
        return sequence;
    }

    /// <summary>
    /// Set the <see cref="Sequence.Operation" /> as <see cref="Operation.Query"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Operation" /> as <see cref="Operation.Query"/>.</returns>
    public static Sequence SetQueryOperation(this Sequence sequence)
        => SetOperation(sequence, Operation.Query);

    /// <summary>
    /// Set the <see cref="Sequence.Operation" /> as <see cref="Operation.Clear"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Operation" /> as <see cref="Operation.Clear"/>.</returns>
    public static Sequence SetClearOperation(this Sequence sequence)
        => SetOperation(sequence, Operation.Clear);

    /// <summary>
    /// Set the <see cref="Sequence.Operation" /> as <see cref="Operation.Set"/>.
    /// </summary>
    /// <param name="sequence">The <see cref="Sequence"/>.</param>
    /// <returns>The <paramref name="sequence"/> with <see cref="Sequence.Operation" /> as <see cref="Operation.Set"/>.</returns>
    public static Sequence SetSetOperation(this Sequence sequence)
        => SetOperation(sequence, Operation.Set);
}
