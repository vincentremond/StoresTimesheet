namespace StoresTimesheet.View

open StoresTimesheet
open StoresTimesheet.Helpers

[<RequireQualifiedAccess>]
module Place =

    open Giraffe.ViewEngine.Svg

    let render day (index: int) (place: Place) =

        let x = C.pageMargins.left + C.textSpacing
        let y = C.secondLineY + ((float index) * C.lineHeight)

        let colorSet = if index % 2 = 0 then C.colorSet1 else C.colorSet2

        g [ _id $"Place_{index}" ] [

            rect [
                _x (Unit.mm C.pageMargins.left)
                _y (Unit.mm y)
                _width (Unit.mm (C.pageWidth - C.pageMargins.left - C.pageMargins.right))
                _height (Unit.mm C.lineHeight)
                _style [ _fill colorSet.Background ]
            ]

            text [
                _x (Unit.mm x)
                _y (Unit.mm (y - C.textSpacing + C.lineHeight))
                _style [ _font_family [ FontFamily.generic SansSerif ] ]
            ] [
                tspan [
                    _style [
                        _font_size (FontSize.absolute Small)
                        _font_weight Bold
                        _fill (Color.named Black)
                    ]
                ] [ str place.Name ]
                tspan [] [ str " " ]
                tspan [
                    _style [
                        _font_size (FontSize.absolute XSmall)
                        _font_style Italic
                        _fill colorSet.SecondaryText
                    ]
                ] [ str place.Description ]
            ]

            let openingHours = place.OpeningHours |> Map.tryFind day |> Option.defaultValue []

            for startTime, endTime in openingHours do
                let start = TimeOnly.init C.firstHour 0<minute>
                let fromX = C.hoursStartX + ((startTime - start).TotalHours * C.hourWidth)
                let width = (endTime - startTime).TotalHours * C.hourWidth

                rect [
                    _x (Unit.mm fromX)
                    _y (Unit.mm y)
                    _width (Unit.mm width)
                    _height (Unit.mm C.lineHeight)
                    _style [ _fill colorSet.Opened ]
                ]

            line [
                _x1 (Unit.mm C.pageMargins.left)
                _y1 (Unit.mm (y + C.lineHeight))
                _x2 (Unit.mm (C.pageWidth - C.pageMargins.right))
                _y2 (Unit.mm (y + C.lineHeight))
                _style [
                    _stroke (Color.named Black)
                ]
            ]
        ]
