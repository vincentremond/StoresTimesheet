namespace StoresTimesheet

open System
open Microsoft.FSharp.Core
open Path
open StoresTimesheet.View

module Program =

    let t ((fh, fm), (th, tm)) =
        let f = TimeOnly(fh, fm, 0)
        let t = TimeOnly(th, tm, 0)
        (f, t)

    let places =
        [
            Place.create @"💌 La Poste" @"Quai du Général de Gaulle" [
                [ Lun; Mer; Jeu; Ven ],
                [
                    ((9<hour>, 00<minute>), (12<hour>, 30<minute>))
                    ((14<hour>, 00<minute>), (18<hour>, 00<minute>))
                ]
                [ Mar ],
                [
                    ((9<hour>, 00<minute>), (12<hour>, 15<minute>))
                    ((14<hour>, 15<minute>), (18<hour>, 00<minute>))
                ]
                [ Sam ], [ ((9<hour>, 00<minute>), (12<hour>, 15<minute>)) ]
            ]
            Place.create @"💌 La Poste" @"Guichet Gare Amboise" [
                [ Lun; Mar; Mer; Jeu; Ven ], [ ((14<hour>, 00<minute>), (17<hour>, 30<minute>)) ]
            ]
            Place.create @"🍅 Au cœur des Halles" @"Primeur rue Nationnale" [
                [ Mar; Mer; Jeu; Ven; Sam ],
                [
                    ((8<hour>, 00<minute>), (12<hour>, 30<minute>))
                    ((15<hour>, 00<minute>), (19<hour>, 30<minute>))
                ]
                [ Dim ], [ ((9<hour>, 00<minute>), (12<hour>, 30<minute>)) ]
            ]
            Place.create @"🍅 Leclerc" @"Amboise" [
                [ Lun; Mar; Mer; Jeu; Sam ], [ ((9<hour>, 00<minute>), (19<hour>, 30<minute>)) ]
                [ Ven ], [ ((9<hour>, 00<minute>), (20<hour>, 00<minute>)) ]
            ]
            Place.create @"🛋️ Ikea" @"Tours" [
                [ Lun; Mar; Mer; Jeu; Ven; Sam ], [ ((10<hour>, 00<minute>), (20<hour>, 00<minute>)) ]
            ]
            Place.create @"🛠️ Leroy Merlin" @"Chambray-lès-Tours" [
                [ Lun; Mar; Mer; Jeu; Ven; Sam ], [ ((9<hour>, 00<minute>), (20<hour>, 00<minute>)) ]
                [ Dim ],
                [
                    ((9<hour>, 00<minute>), (13<hour>, 00<minute>))
                    ((14<hour>, 00<minute>), (18<hour>, 00<minute>))
                ]
            ]
            Place.create @"🛠️ Leroy Merlin" @"Tours Nord" [
                [ Lun; Mar; Mer; Jeu; Ven; Sam ], [ ((9<hour>, 00<minute>), (20<hour>, 00<minute>)) ]
                [ Dim ],
                [
                    ((9<hour>, 00<minute>), (13<hour>, 00<minute>))
                    ((14<hour>, 00<minute>), (18<hour>, 00<minute>))
                ]
            ]
            Place.create @"♻️ Déchetterie" @"Amboise (hivers, du 01 oct. au 31 mar.)" [
                [ Lun; Mar; Mer ],
                [
                    ((9<hour>, 00<minute>), (12<hour>, 30<minute>))
                    ((14<hour>, 30<minute>), (17<hour>, 00<minute>))
                ]
                [ Jeu ], [ ((9<hour>, 00<minute>), (12<hour>, 30<minute>)) ]
                [ Ven ],
                [
                    ((9<hour>, 00<minute>), (12<hour>, 30<minute>))
                    ((13<hour>, 30<minute>), (17<hour>, 00<minute>))
                ]
                [ Sam ], [ ((9<hour>, 00<minute>), (17<hour>, 00<minute>)) ]
            ]
            Place.create @"♻️ Déchetterie" @"Amboise (été, du 01 avr. au 30 sep.)" [
                [ Lun; Mar; Mer ],
                [
                    ((8<hour>, 30<minute>), (12<hour>, 30<minute>))
                    ((14<hour>, 30<minute>), (18<hour>, 00<minute>))
                ]
                [ Jeu ], [ ((8<hour>, 30<minute>), (12<hour>, 30<minute>)) ]
                [ Ven ],
                [
                    ((8<hour>, 30<minute>), (12<hour>, 30<minute>))
                    ((13<hour>, 30<minute>), (19<hour>, 00<minute>))
                ]
                [ Sam ], [ ((8<hour>, 30<minute>), (19<hour>, 00<minute>)) ]
            ]
            Place.create @"🚂 Gare Amboise" @"Guichets" [
                [ Lun; Mar; Mer; Jeu; Ven ],
                [
                    ((6<hour>, 15<minute>), (13<hour>, 20<minute>))
                    ((13<hour>, 50<minute>), (21<hour>, 00<minute>))
                ]
                [ Sam ],
                [
                    ((7<hour>, 15<minute>), (13<hour>, 20<minute>))
                    ((13<hour>, 50<minute>), (21<hour>, 00<minute>))
                ]
                [ Dim ],
                [
                    ((9<hour>, 00<minute>), (14<hour>, 45<minute>))
                    ((15<hour>, 05<minute>), (21<hour>, 15<minute>))
                ]
            ]
            Place.create @"🍅 Biocoop" @"La Boitardière" [
                [ Lun; Mar; Mer ], [ ((9<hour>, 00<minute>), (19<hour>, 00<minute>)) ]
                [ Jeu; Ven; Sam ], [ ((9<hour>, 30<minute>), (19<hour>, 00<minute>)) ]
            ]
        ]
        |> List.sortBy (fun place -> (place.Name, place.Description))

    let renderDay weekday =

        let weekDayView = View.weekDay weekday places

        let outputFolder = @"C:\Users\vremond\Downloads\Timesheet"

        weekDayView
        |> Render.asXml
        |> File.writeAllText (outputFolder </> $"{weekday}.svg")

        weekDayView
        |> Render.asHtml
        |> File.writeAllText (outputFolder </> $"{weekday}.html")

    [<EntryPoint>]
    let main _ =

        [ Lun; Mar; Mer; Jeu; Ven; Sam; Dim ] |> List.iter renderDay
        0
