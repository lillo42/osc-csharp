# osc-csharp

A C# library to work with the [ANSI OSC52](https://invisible-island.net/xterm/ctlseqs/ctlseqs.html#h3-Operating-System-Commands) terminal sequence.

It is heavily inspired by the Go library [go-osc52](https://github.com/aymanbagabas/go-osc52).

## Usage

You can use this small library to construct an ANSI OSC52 sequence suitable for
your terminal.

### Example

```csharp
using OperatingSystemCommand52;

const string content = "Hello World";

var stderr = Console.OpenStandardError();

// Copy `content` to clipboard
new Sequence(content).Write(stderr);

// Copy `content` to primary clipboard (X11) 
new Sequence(content)
    .SetPrimaryClipboard()
    .Write(stderr);

// Query the clipboard
new Sequence()
    .SetQueryOperation()
    .Write(stderr);

// Clear the clipboard
new Sequence()
    .SetClearOperation()
    .Write(stderr);

// Use the write to copy `content` to clipboard
Console.Write(new Sequence(content));

// Or to primary clipboard 
Console.Write(new Sequence(content).SetPrimaryClipboard());
```

## Tmux

Make sure you have `set-clipboard on` in your config, otherwise, tmux won't
allow your application to access the clipboard [^1].

Using the tmux option, `Mode.Tmux` or `new Sequence().SetTmuxMode@()`, wraps the
OSC52 sequence in a special tmux DCS sequence and pass it to the outer
terminal. This requires `allow-passthrough on` in your config.
`allow-passthrough` is no longer enabled by default
[since tmux 3.3a](https://github.com/tmux/tmux/issues/3218#issuecomment-1153089282) [^2].

[^1]: See [tmux clipboard](https://github.com/tmux/tmux/wiki/Clipboard)
[^2]: [What is allow-passthrough](https://github.com/tmux/tmux/wiki/FAQ#what-is-the-passthrough-escape-sequence-and-how-do-i-use-it)

