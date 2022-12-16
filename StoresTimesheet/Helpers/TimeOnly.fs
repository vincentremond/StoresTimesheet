namespace StoresTimesheet.Helpers

open System
open StoresTimesheet

[<RequireQualifiedAccess>]
module TimeOnly =
    let init (hour: int<hour>) (minute: int<minute>) =
        TimeOnly(int hour, int minute)
