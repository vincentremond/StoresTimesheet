namespace StoresTimesheet.Helpers

[<RequireQualifiedAccess>]
module String =
    let toUpperInvariant (s: string) = s.ToUpperInvariant()
    let ofChars count char = new string (char, count)
