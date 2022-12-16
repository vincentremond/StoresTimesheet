namespace StoresTimesheet

open System

[<Measure>]
type hour

[<Measure>]
type minute

type Hour = int<hour>
type Minute = int<minute>

type WeekDay =
    | Lun
    | Mar
    | Mer
    | Jeu
    | Ven
    | Sam
    | Dim

type Place =
    {
        Name: string
        Description: string
        OpeningHours: Map<WeekDay, (TimeOnly * TimeOnly) list>
    }

    static member create
        name
        description
        (openingHours: (WeekDay list * ((Hour * Minute) * (Hour * Minute)) list) list)
        =

        {
            Name = name
            Description = description
            OpeningHours =
                openingHours
                |> List.collect (fun (days, times) ->
                    days
                    |> List.map (fun day ->
                        day,
                        times
                        |> List.map (fun ((h1, m1), (h2, m2)) -> TimeOnly(int h1, int m1), TimeOnly(int h2, int m2))))
                |> Map.ofList
        }
