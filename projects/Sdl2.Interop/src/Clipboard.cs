using Sdl2.Interop.Exceptions;
using Sdl2.Interop.Internal;
using Sdl2.Interop.NativeDelegates;

namespace Sdl2.Interop;

public partial class Sdl
{
    /// <summary>Get/Set UTF-8 text into the clipboard.</summary>
    /// <value>A <see cref="string" /> with the clipboard text or a <see cref="string.Empty" /> if there is no text.</value>
    /// <exception cref="NativeException">When SDL is unable to set the clipboard text.</exception>
    /// <remarks>This property is available since SDL 2.0.0.</remarks>
    public string ClipboardText
    {
        get
        {
            string result = Common.GetExport<ClipboardDelegates.GetClipboardTextDelegate>(this, "SDL_GetClipboardText",
                new Version(2, 0, 0))();

            return result;
        }
        set
        {
            int errorCode = Common.GetExport<ClipboardDelegates.SetClipboardTextDelegate>(this, "SDL_SetClipboardText",
                new Version(2, 0, 0))(value);

            if (errorCode < 0)
            {
                throw new NativeException(LastError, errorCode);
            }
        }
    }

    /// <summary>Query whether the clipboard exists and contains a non-empty text string.</summary>
    /// <value>A <see cref="bool" /> with whether the clipboard contains text or not.</value>
    public bool HasClipboardText =>
        Common.GetExport<ClipboardDelegates.HasClipboardText>(this, "SDL_HasClipboardText", new Version(2, 0, 0))();

    /// <summary>Get/Set UTF-8 text into the primary selection.</summary>
    /// <value>A <see cref="string" /> with the primary selection text or a <see cref="string.Empty" /> if there is no text.</value>
    /// <exception cref="NativeException">When SDL is unable to set the primary selection text.</exception>
    /// <remarks>This property is available since SDL 2.26.0.</remarks>
    public string PrimarySelectionText
    {
        get
        {
            string result = Common.GetExport<ClipboardDelegates.GetPrimarySelectionTextDelegate>(this,
                "SDL_GetPrimarySelectionText",
                new Version(2, 26, 0))();

            return result;
        }
        set
        {
            int errorCode = Common.GetExport<ClipboardDelegates.SetPrimarySelectionTextDelegate>(this,
                "SDL_SetPrimarySelectionText",
                new Version(2, 26, 0))(value);

            if (errorCode < 0)
            {
                throw new NativeException(LastError, errorCode);
            }
        }
    }

    /// <summary>Query whether the primary selection exists and contains a non-empty text string.</summary>
    /// <value>A <see cref="bool" /> with whether the clipboard contains text or not.</value>
    public bool HasPrimarySelectionText =>
        Common.GetExport<ClipboardDelegates.HasPrimarySelectionText>(this, "SDL_HasPrimarySelectionText",
            new Version(2, 26, 0))();
}