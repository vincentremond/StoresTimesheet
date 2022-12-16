namespace StoresTimesheet

module Path =
    let (</>) a b = System.IO.Path.Combine(a, b)

[<RequireQualifiedAccess>]
module File =

    let writeAllText (path: string) (text: string) = System.IO.File.WriteAllText(path, text)
