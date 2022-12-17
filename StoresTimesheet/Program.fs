namespace StoresTimesheet

open System
open System.Drawing
open Microsoft.FSharp.Core
open Path
open Spire.Pdf
open Spire.Pdf.Graphics
open StoresTimesheet.Helpers
open StoresTimesheet.View

module Program =

    let t ((fh, fm), (th, tm)) =
        let f = TimeOnly(fh, fm, 0)
        let t = TimeOnly(th, tm, 0)
        (f, t)

    let outputDirectory = @"C:\Users\vremond\Downloads\Timesheet"

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
            Place.create @"🛒 Au cœur des Halles" @"Primeur rue Nationnale" [
                [ Mar; Mer; Jeu; Ven; Sam ],
                [
                    ((8<hour>, 00<minute>), (12<hour>, 30<minute>))
                    ((15<hour>, 00<minute>), (19<hour>, 30<minute>))
                ]
                [ Dim ], [ ((9<hour>, 00<minute>), (12<hour>, 30<minute>)) ]
            ]
            Place.create @"🛒 Leclerc" @"Amboise" [
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
            Place.create @"🛒 Biocoop" @"La Boitardière" [
                [ Lun; Mar; Mer ], [ ((9<hour>, 00<minute>), (19<hour>, 00<minute>)) ]
                [ Jeu; Ven; Sam ], [ ((9<hour>, 30<minute>), (19<hour>, 00<minute>)) ]
            ]
            Place.create @"🛒 Marché" @"Amboise" [ [ Ven; Dim ], [ ((8<hour>, 00<minute>), (13<hour>, 00<minute>)) ] ]
            Place.create @"🛠️ Baobab" @"Amboise" [
                [ Lun; Mar; Mer; Jeu; Ven ],
                [
                    ((9<hour>, 30<minute>), (12<hour>, 30<minute>))
                    ((14<hour>, 15<minute>), (18<hour>, 30<minute>))
                ]
                [ Sam ], [ ((9<hour>, 30<minute>), (19<hour>, 00<minute>)) ]
                [ Dim ],
                [
                    ((9<hour>, 30<minute>), (12<hour>, 30<minute>))
                    ((14<hour>, 00<minute>), (18<hour>, 30<minute>))
                ]
            ]
            Place.create @"🛠️ Bricomarché" @"Pocé-sur-Cisse" [
                [ Lun; Mar; Mer; Jeu; Ven; Sam ],
                [
                    ((8<hour>, 15<minute>), (12<hour>, 30<minute>))
                    ((14<hour>, 00<minute>), (19<hour>, 00<minute>))
                ]
            ]
            Place.create @"🛠️ LaMaison.fr" @"Amboise" [
                [ Lun; Mar; Mer; Jeu; Ven; Sam ],
                [
                    ((9<hour>, 00<minute>), (12<hour>, 00<minute>))
                    ((14<hour>, 00<minute>), (18<hour>, 30<minute>))
                ]
            ]
            Place.create @"🛒 Carrefour City" @"quai du Général de Gaulle" [
                [ Lun; Mar; Mer; Jeu; Ven; Sam ], [ ((7<hour>, 00<minute>), (21<hour>, 00<minute>)) ]
                [ Dim ], [ ((9<hour>, 00<minute>), (13<hour>, 00<minute>)) ]
            ]
            Place.create @"⚕️ Pharmacie Centrale" @"rue Nationale" [
                [ Lun ], [ ((14<hour>, 00<minute>), (19<hour>, 30<minute>)) ]
                [ Mar; Mer; Jeu; Ven ],
                [
                    ((9<hour>, 00<minute>), (12<hour>, 30<minute>))
                    ((14<hour>, 00<minute>), (19<hour>, 30<minute>))
                ]
                [ Sam ],
                [
                    ((9<hour>, 00<minute>), (12<hour>, 30<minute>))
                    ((14<hour>, 00<minute>), (19<hour>, 00<minute>))
                ]
            ]
        ]
        |> List.sortBy (fun place -> (place.Name, place.Description))

    let renderDay weekday =

        View.weekDay weekday places |> Render.asXml

    [<EntryPoint>]
    let main _ =

        let days =
            [ Lun; Mar; Mer; Jeu; Ven; Sam; Dim ]
            |> List.mapi (fun index weekday ->
                let svgAsString = renderDay weekday

                let svgFilePath = outputDirectory </> $"%d{index + 1}-%s{string weekday}.svg"
                let pngFilePath = outputDirectory </> $"%d{index + 1}-%s{string weekday}.png"

                svgAsString |> File.writeAllText svgFilePath
                Inkscape.convertToPng svgFilePath pngFilePath

                Image.FromFile pngFilePath)

        let toLandscape (s: SizeF) : SizeF = SizeF(s.Height, s.Width)

        let a4size = PdfPageSize.A4 |> toLandscape
        use doc = new PdfDocument()

        for day in days do
            let page = doc.Pages.Add(a4size, PdfMargins(0f))
            let image = PdfImage.FromImage(day)
            page.Canvas.DrawImage(image, 0f, 0f, a4size.Width, a4size.Height)

        doc.SaveToFile(outputDirectory </> "output.pdf")

        0
