namespace StoresTimesheet

open System
open Giraffe.ViewEngine.Svg
open StoresTimesheet.Helpers

type WeekDay =
    | Lun
    | Mar
    | Mer
    | Jeu
    | Ven
    | Sam
    | Dim

type OpenPeriod = | OpenPeriod of From: TimeOnly * To: TimeOnly

type OpeningHours =
    | OpeningHours of Map<WeekDay, OpenPeriod list>

    member this.GetForDay(day) =
        match this with
        | OpeningHours map -> map |> Map.tryFind day |> Option.defaultValue []

type Place = {
    Icon: string
    Name: string
    Description: string
    Domain: string
    Favicon: string * byte array
    OpeningHours: OpeningHours
    ExtendedOpeningHours: OpeningHours
} with

    static member mapOpeningHours(openingHours: (WeekDay list * ((Hour * Minute) * (Hour * Minute)) list) list) =
        openingHours
        |> List.collect (fun (days, times) ->
            days
            |> List.map (fun day ->
                day,
                times
                |> List.map (fun ((h1, m1), (h2, m2)) ->
                    OpenPeriod(TimeOnly(int h1, int m1), TimeOnly(int h2, int m2))
                )
            )
        )
        |> Map.ofList
        |> OpeningHours

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

type ColorSet = {
    PrimaryText: Color
    SecondaryText: Color
    Opened: Color
    ExtendedOpened: Color
    Background: Color
}

type ViewModel = { WeekDays: WeekDayViewModel list }

and WeekDayViewModel = {
    Index: int
    Name: string
    DayColor: Color
    Places: PlaceViewModel list
}

and PlaceViewModel = {
    Index: int
    Icon: string
    Name: string
    Description: string
    Domain: string
    Favicon: string * byte array
    Colors: ColorSet
    OpeningHours: OpeningHoursModel list
    ExtendedOpeningHours: OpeningHoursModel list
}

and OpeningHoursModel = OpenPeriod
