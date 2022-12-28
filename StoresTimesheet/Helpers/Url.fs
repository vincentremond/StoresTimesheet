namespace StoresTimesheet.Helpers

open System.Web

[<RequireQualifiedAccess>]
module Url =
    let encode (s: string) = HttpUtility.UrlEncode(s)
