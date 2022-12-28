namespace StoresTimesheet.Helpers

open System

[<RequireQualifiedAccess>]
module Image =
    let toBase64 (contentType, bytes) =
        match contentType with
        | "image/jpeg" -> "data:image/jpeg;base64," + Convert.ToBase64String(bytes)
        | "image/png" -> "data:image/png;base64," + Convert.ToBase64String(bytes)
        | _ -> failwith $"Unsupported image type {contentType}"
