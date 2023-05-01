using System.Text;

namespace OperatingSystemCommand52;

/// <summary>
/// The OSC52 sequence.
/// </summary>
public class Sequence
{
    /// <summary>
    /// Initialize a new instance of <see cref="Sequence"/>.
    /// </summary>
    public Sequence()
        : this(string.Empty)
    {
    }

    /// <summary>
    /// Initialize a new instance of <see cref="Sequence"/>.
    /// </summary>
    /// <param name="content">The string value.</param>
    public Sequence(string content)
    {
        Content = content;
        _limit = 0;
        Mode = Mode.Default;
        Operation = Operation.Set;
        Clipboard = Clipboard.System;
    }

    /// <summary>
    /// Initialize a new instance of <see cref="Sequence"/>.
    /// </summary>
    /// <param name="content">The string value.</param>
    public Sequence(IEnumerable<string> content)
        : this(string.Join(' ', content))
    {
    }

    /// <summary>
    /// Initialize a new instance of <see cref="Sequence"/>.
    /// </summary>
    /// <param name="content">The string value.</param>
    public Sequence(params string[] content)
        : this(string.Join(' ', content))
    {
    }

    /// <summary>
    /// The string for the OSC52 sequence.
    /// </summary>
    public string Content { get; set; }

    private int _limit;

    /// <summary>
    /// The limit for the OSC52 sequence.
    /// </summary>
    /// <remarks>
    /// The default limit is 0 (no limit).
    /// <para />
    /// Strings longer than the limit get ignored. Setting the limit to 0 or a
    /// negative value disables the limit. Each terminal defines its own escape
    /// sequence limit.
    /// </remarks>
    public int Limit
    {
        get => _limit;
        set => _limit = value < 0 ? 0 : value;
    }

    /// <summary>
    /// The <see cref="OperatingSystemCommand52.Clipboard"/>.
    /// </summary>
    public Clipboard Clipboard { get; set; }

    /// <summary>
    /// The <see cref="OperatingSystemCommand52.Mode"/>.
    /// </summary>
    public Mode Mode { get; set; }

    /// <summary>
    /// The <see cref="OperatingSystemCommand52.Operation"/>.
    /// </summary>
    public Operation Operation { get; set; }
    
    /// <summary>
    /// The <see cref="System.Text.Encoding"/>.
    /// </summary>
    public Encoding Encoding { get; set; } = Encoding.UTF8;

    /// <summary>
    /// Write the OSC52 sequence to the stream.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/>.</param>
    public void Write(Stream stream) 
        => stream.Write(Encoding.GetBytes(ToString()));

    /// <summary>
    /// Write the OSC52 sequence to the stream asynchronously.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>The <see cref="ValueTask"/>.</returns>
    public ValueTask WriteAsync(Stream stream, CancellationToken cancellationToken = default)
        => stream.WriteAsync(Encoding.GetBytes(ToString()), cancellationToken);

    /// <inheritdoc />
    public override string ToString()
    {
        var seq = new StringBuilder();

        // mode escape sequence start
        seq.Append(SeqStart());

        // actual OSC52 sequence start
        seq.Append($"{Escape}]52;{(char)Clipboard};");

        // operation
        if (Operation == Operation.Set)
        {
            if (Limit > 0 && Content.Length > Limit)
            {
                return string.Empty;
            }

            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(Content));
            if (Mode == Mode.Screen)
            {
                // Screen doesn't support OSC52 but will pass the contents of a DCS
                // sequence to the outer terminal unchanged.
                //
                // Here, we split the encoded string into 76 bytes chunks and then
                // join the chunks with <end-dsc><start-dsc> sequences. Finally,
                // wrap the whole thing in
                // <start-dsc><start-osc52><joined-chunks><end-osc52><end-dsc>.
                var tmp = new List<string>(base64.Length / 76 + 1);
                for (var i = 0; i < base64.Length; i += 76)
                {
                    var end = Math.Min(i + 76, base64.Length);
                    tmp.Add(base64[i..end]);
                }

                seq.Append(string.Join($"{Escape}\\{Escape}P", tmp));
            }
            else
            {
                seq.Append(base64);
            }
        }
        else if (Operation == Operation.Query)
        {
            // OSC52 queries the clipboard using "?"
            seq.Append('?');
        }
        else if (Operation == Operation.Clear)
        {
            // OSC52 clears the clipboard if the data is neither a base64 string nor "?"
            // we're using "!" as a default
            seq.Append('!');
        }

        seq.Append('\x07');
        seq.Append(SeqEnd());

        return seq.ToString();
    }

    private const string Escape = "\x1b";

    private string SeqStart() =>
        Mode switch
        {
            Mode.Tmux => $"{Escape}Ptmux;{Escape}",
            Mode.Screen => $"{Escape}P",
            _ => string.Empty
        };

    private string SeqEnd() =>
        Mode switch
        {
            Mode.Tmux => $"{Escape}\\",
            Mode.Screen => $"{Escape}\x5c",
            _ => string.Empty
        };

    /// <summary>
    /// Create a new <see cref="Sequence"/> with <see cref="Operation"/> as <see cref="OperatingSystemCommand52.Operation.Query"/>.
    /// </summary>
    /// <returns>
    /// New instance of <see cref="Sequence"/> with <see cref="Operation"/> as <see cref="OperatingSystemCommand52.Operation.Query"/>.
    /// </returns>
    public static Sequence NewQuery() => new Sequence()
        .SetQueryOperation();

    /// <summary>
    /// Create a new <see cref="Sequence"/> with <see cref="Operation"/> as <see cref="OperatingSystemCommand52.Operation.Clear"/>. 
    /// </summary>
    /// <remarks>
    /// New instance of <see cref="Sequence"/> with <see cref="Operation"/> as <see cref="OperatingSystemCommand52.Operation.Clear"/>. 
    /// </remarks>
    public static Sequence NewClear() => new Sequence()
        .SetClearOperation();
}
