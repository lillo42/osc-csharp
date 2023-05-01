namespace OperatingSystemCommand52;

/// <summary>
/// The mode to use for the OSC52 sequence.
/// </summary>
public enum Mode
{
    /// <summary>
    /// The default mode OSC52 sequence mode.
    /// </summary>
    Default = 0,

    /// <summary>
    /// The OSC52 sequence for screen using DCS sequence. 
    /// </summary>
    Screen = 1,
    
    /// <summary>
    /// The OSC52 sequence for tmux. Not need if tmux clipboard mode is set to `set-clipboard on`.
    /// </summary>
    Tmux = 2
}
