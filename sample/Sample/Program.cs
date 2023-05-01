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



