namespace OperatingSystemCommand52;

/// <summary>
/// The clipboard buffer to use. 
/// </summary>
public enum Clipboard
{
    /// <summary>
    /// The System clipboard buffer.
    /// </summary>
    System = 'c',
    
    /// <summary>
    /// The primary clipboard buffer (X11).
    /// </summary>
    Primary = 'p'
}
