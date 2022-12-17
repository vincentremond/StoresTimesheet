namespace StoresTimesheet.Helpers

[<RequireQualifiedAccess>]
module MemoryStream =
    let ofString (str: string) =
        let ms = new System.IO.MemoryStream()
        let sw = new System.IO.StreamWriter(ms)
        sw.Write(str)
        sw.Flush()
        ms.Position <- 0L
        ms
