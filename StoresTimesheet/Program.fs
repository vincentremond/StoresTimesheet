namespace StoresTimesheet

open System
open Giraffe.ViewEngine.Svg
open Microsoft.FSharp.Core
open Path
open StoresTimesheet.Helpers
open StoresTimesheet.View

module Program =

    let t ((fh, fm), (th, tm)) =
        let f = TimeOnly(fh, fm, 0)
        let t = TimeOnly(th, tm, 0)
        (f, t)

    let outputDirectory = @"C:\Users\vremond\Downloads\Timesheet"
    Emoji.folderPath <- Some @"D:\DAT\emoji\joypixels-7.0.1-free\png\unicode\128"

    let places = [
        Place.create
            "💌"
            @"La Poste"
            @"Quai du Général de Gaulle"
            "https://www.laposte.fr"
            [
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
            None
        Place.create
            "💌"
            @"La Poste"
            @"Guichet Gare Amboise"
            "https://www.laposte.fr"
            [
                [ Lun; Mar; Mer; Jeu; Ven ], [ ((14<hour>, 00<minute>), (17<hour>, 30<minute>)) ]
            ]
            None
        Place.create
            "🛒"
            @"Au cœur des Halles"
            @"Primeur rue Nationnale"
            "http://www.coursdeshalles.fr/"
            [
                [ Mar; Mer; Jeu; Ven; Sam ],
                [
                    ((8<hour>, 00<minute>), (12<hour>, 30<minute>))
                    ((15<hour>, 00<minute>), (19<hour>, 30<minute>))
                ]
                [ Dim ], [ ((9<hour>, 00<minute>), (12<hour>, 30<minute>)) ]
            ]
            None
        Place.create
            "🛒"
            @"Leclerc"
            @"Amboise"
            "https://www.e.leclerc/"
            [
                [ Lun; Mar; Mer; Jeu; Sam ], [ ((9<hour>, 00<minute>), (19<hour>, 30<minute>)) ]
                [ Ven ], [ ((9<hour>, 00<minute>), (20<hour>, 00<minute>)) ]
            ]
            None
        Place.create
            "🛋️"
            @"Ikea"
            @"Tours"
            "https://www.ikea.com/"
            [
                [ Lun; Mar; Mer; Jeu; Ven; Sam ], [ ((10<hour>, 00<minute>), (20<hour>, 00<minute>)) ]
            ]
            None
        Place.create
            "🛠️"
            @"Leroy Merlin"
            @"Chambray-lès-Tours"
            "https://www.leroymerlin.fr/"
            [
                [ Lun; Mar; Mer; Jeu; Ven; Sam ], [ ((9<hour>, 00<minute>), (20<hour>, 00<minute>)) ]
                [ Dim ],
                [
                    ((9<hour>, 00<minute>), (13<hour>, 00<minute>))
                    ((14<hour>, 00<minute>), (18<hour>, 00<minute>))
                ]
            ]
            None
        Place.create
            "🛠️"
            @"Leroy Merlin"
            @"Tours Nord"
            "https://www.leroymerlin.fr/"
            [
                [ Lun; Mar; Mer; Jeu; Ven; Sam ], [ ((9<hour>, 00<minute>), (20<hour>, 00<minute>)) ]
                [ Dim ],
                [
                    ((9<hour>, 00<minute>), (13<hour>, 00<minute>))
                    ((14<hour>, 00<minute>), (18<hour>, 00<minute>))
                ]
            ]
            None
        Place.create
            "♻️"
            @"Déchetterie"
            @"Amboise (été: 01/04→30/09, hiver: 01/10→31/03)"
            "https://www.ville-amboise.fr/"
            [
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
            (Some [
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
            ])
        Place.create
            "🚂"
            @"Gare Amboise"
            @"Guichets"
            "https://www.sncf.com/"
            [
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
            None
        Place.create
            "🛒"
            @"Biocoop"
            @"La Boitardière"
            "https://www.biocoop.fr/"
            [
                [ Lun; Mar; Mer ], [ ((9<hour>, 00<minute>), (19<hour>, 00<minute>)) ]
                [ Jeu; Ven; Sam ], [ ((9<hour>, 30<minute>), (19<hour>, 00<minute>)) ]
            ]
            None
        Place.create
            "🛒"
            @"Marché"
            @"Amboise"
            "https://www.ville-amboise.fr/"

            [ [ Ven; Dim ], [ ((8<hour>, 00<minute>), (13<hour>, 00<minute>)) ] ]
            (Some [ [ Mar ], [ ((17<hour>, 00<minute>), (0<hour>, 00<minute>)) ] ])
        Place.create
            "🛠️"
            @"Baobab"
            @"Amboise"
            "https://jardinerie-baobab.fr"
            [
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
            None
        Place.create
            "🛠️"
            @"Bricomarché"
            @"Pocé-sur-Cisse"
            "https://www.bricomarche.com/"
            [
                [ Lun; Mar; Mer; Jeu; Ven; Sam ],
                [
                    ((8<hour>, 15<minute>), (12<hour>, 30<minute>))
                    ((14<hour>, 00<minute>), (19<hour>, 00<minute>))
                ]
            ]
            None
        Place.create
            "🛠️"
            @"LaMaison.fr"
            @"Amboise"
            "https://www.lamaison.fr/"
            [
                [ Lun; Mar; Mer; Jeu; Ven; Sam ],
                [
                    ((9<hour>, 00<minute>), (12<hour>, 00<minute>))
                    ((14<hour>, 00<minute>), (18<hour>, 30<minute>))
                ]
            ]
            None
        Place.create
            "🛒"
            @"Carrefour City"
            @"quai du Général de Gaulle"
            "https://www.carrefour.fr/"
            [
                [ Lun; Mar; Mer; Jeu; Ven; Sam ], [ ((7<hour>, 00<minute>), (21<hour>, 00<minute>)) ]
                [ Dim ], [ ((9<hour>, 00<minute>), (13<hour>, 00<minute>)) ]
            ]
            None
        Place.create
            "⚕️"
            @"Pharmacie Centrale"
            @"rue Nationale"
            "https://pharmaciecentrale-amboise.pharmavie.fr/"
            [
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
            None
        Place.create
            "♻️"
            @"Emmaüs"
            @"Amboise"
            "https://emmaus-touraine.org/"
            [ [ Sam ], [ ((9<hour>, 00<minute>), (16<hour>, 00<minute>)) ] ]
            (Some [ [ Sam ], [ ((9<hour>, 00<minute>), (17<hour>, 00<minute>)) ] ])
        Place.create
            "💪"
            @"Fit Up"
            @"Amboise"
            "https://www.fitupclub.fr/"
            [
                [ Lun; Mar; Mer; Jeu; Ven ], [ ((9<hour>, 00<minute>), (21<hour>, 00<minute>)) ]
                [ Sam ], [ ((9<hour>, 00<minute>), (13<hour>, 00<minute>)) ]
            ]
            (Some [
                [ Lun; Mar; Mer; Jeu; Ven; Sam; Dim ], [ ((6<hour>, 00<minute>), (23<hour>, 00<minute>)) ]
            ])
        Place.create
            "🛒"
            @"Décathlon"
            @"Tours Nord"
            "https://www.decathlon.fr/"
            [
                [ Lun; Mar; Mer; Jeu; Ven; Sam ], [ ((9<hour>, 30<minute>), (19<hour>, 30<minute>)) ]
            ]
            None
        Place.create
            "🧀"
            @"La Passion du Fromage"
            @"rue Nationale"
            "https://lapassiondufromage.com/"
            [
                [ Mar; Mer; Jeu; Ven; Sam ],
                [
                    ((9<hour>, 00<minute>), (13<hour>, 00<minute>))
                    ((15<hour>, 30<minute>), (19<hour>, 00<minute>))
                ]
                [ Dim ], [ ((9<hour>, 00<minute>), (13<hour>, 00<minute>)) ]
            ]
            None
        Place.create
            "📚"
            @"Lu & Approuvé"
            @"Librairie Amboise"
            "http://luapprouve.com/"
            [
                [ Lun; Mar; Mer; Jeu; Ven; Sam ], [ ((9<hour>, 00<minute>), (19<hour>, 00<minute>)) ]
                [ Dim ], [ ((9<hour>, 00<minute>), (12<hour>, 30<minute>)) ]
            ]
            None
        Place.create
            "📚"
            @"Bureau Vallée"
            @"Amboise"
            "https://magasins.bureau-vallee.fr/fr/france-33/indre-et-loire-37/amboise-37003/amboise-BV507"
            [
                [ Lun; Mar; Mer; Jeu; Ven; Sam ],
                [
                    ((9<hour>, 00<minute>), (12<hour>, 30<minute>))
                    ((14<hour>, 00<minute>), (19<hour>, 00<minute>))
                ]
            ]
            None
            
        Place.create
            "🎲"
            @"L'Interlude"
            @"Amboise"
            "https://www.facebook.com/linterlude37/"
            [
                [ Mar; Mer; Ven; Sam ], [ ((10<hour>, 00<minute>), (19<hour>, 00<minute>)) ]
                [ Jeu ], [
                    ((10<hour>, 00<minute>), (13<hour>, 00<minute>))
                    ((14<hour>, 00<minute>), (19<hour>, 00<minute>))
                ]
            ]
            None
    ]

    [<EntryPoint>]
    let main _ =

        let model = {
            WeekDays =
                [
                    (0, Lun, @"Lundi", PaleVioletRed)
                    (1, Mar, @"Mardi", Coral)
                    (2, Mer, @"Mercredi", Gold)
                    (3, Jeu, @"Jeudi", LightYellow)
                    (4, Ven, @"Vendredi", PaleGreen)
                    (5, Sam, @"Samedi", LightSkyBlue)
                    (6, Dim, @"Dimanche", Plum)
                ]
                |> List.map (fun (index, day, name, color) -> {
                    Index = index
                    Name = name
                    DayColor = Color.named color
                    Places =
                        places
                        |> List.sortBy (fun place -> (place.Icon, place.Name, place.Description))
                        |> List.mapi (fun index place -> {
                            Index = index
                            Icon = place.Icon
                            Name = place.Name
                            Description = place.Description
                            Domain = place.Domain
                            Favicon = place.Favicon
                            Colors = C.colorSets[index % C.colorSets.Length]
                            OpeningHours = place.OpeningHours.GetForDay(day)
                            ExtendedOpeningHours = place.ExtendedOpeningHours.GetForDay(day)
                        })
                })
        }

        let svgFilePath = (outputDirectory </> "result.svg")
        let pdfFilePath = (outputDirectory </> "result.pdf")

        model |> View.render |> Render.asXml |> File.writeAllText svgFilePath
        Inkscape.exportToPdf svgFilePath pdfFilePath

        0
