namespace StoresTimesheet

open System
open StoresTimesheet.Helpers

type WeekDay =
    | Lun
    | Mar
    | Mer
    | Jeu
    | Ven
    | Sam
    | Dim

type OpeningHours = Map<WeekDay, (TimeOnly * TimeOnly) list>

type Place =
    {
        Icon: string
        Name: string
        Description: string
        Domain: string
        Favicon: string * byte array
        OpeningHours: OpeningHours
        ExtendedOpeningHours: OpeningHours
    }

    static member mapOpeningHours(openingHours: (WeekDay list * ((Hour * Minute) * (Hour * Minute)) list) list) =
        openingHours
        |> List.collect (fun (days, times) ->
            days
            |> List.map (fun day ->
                day,
                times
                |> List.map (fun ((h1, m1), (h2, m2)) -> TimeOnly(int h1, int m1), TimeOnly(int h2, int m2))))
        |> Map.ofList

    static member create icon name description domain openingHours extendedOpeningHours =
        let favicon =
            Http.download $"https://www.google.com/s2/favicons?domain={domain |> Url.encode}&sz=64"

        {
            Icon = icon
            Name = name
            Description = description
            Domain = domain
            Favicon = favicon
            OpeningHours = openingHours |> Place.mapOpeningHours
            ExtendedOpeningHours = extendedOpeningHours |> (Option.defaultValue []) |> Place.mapOpeningHours
        }
