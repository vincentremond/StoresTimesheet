namespace StoresTimesheet.Helpers

[<RequireQualifiedAccess>]
module List =
    let mapi' l = List.mapi (fun i x -> (i, x)) l
