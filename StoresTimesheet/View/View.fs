namespace StoresTimesheet.View

open StoresTimesheet
open StoresTimesheet.View

module View =
    open Giraffe.ViewEngine.Svg

    let renderStore () = [

        rect [
            _x (Unit.mm C.pageMargins.left)
            _y (Unit.mm 20.<mm>)
            _width (Unit.mm (C.pageWidth - C.pageMargins.left - C.pageMargins.right))
            _height (Unit.mm C.lineHeight)
            _style [ _fill (Color.hex "#BAD9B5") ]
        ]

        rect [
            _x (Unit.mm (C.pageMargins.left + 110.<mm>))
            _y (Unit.mm 20.<mm>)
            _width (Unit.mm 50.<mm>)
            _height (Unit.mm C.lineHeight)
            _style [ _fill (Color.hex "#5DA551") ]
        ]
    ]

    let weekDay weekday stores =
        let index, dayName, color =
            match weekday with
            | Lun -> 0, @"Lundi", PaleVioletRed
            | Mar -> 1, @"Mardi", Coral
            | Mer -> 2, @"Mercredi", Gold 
            | Jeu -> 3, @"Jeudi", LightYellow
            | Ven -> 4, @"Vendredi", PaleGreen
            | Sam -> 5, @"Samedi", LightSkyBlue
            | Dim -> 6, @"Dimanche", Plum

        svg [
            _width (Unit.mm C.pageWidth)
            _height (Unit.mm C.pageHeight)
            _xmlns "http://www.w3.org/2000/svg"
        ] [
            defs [] [
                pattern [
                    _id "diagonalHatch"
                    _width (Unit.none 10.)
                    _height (Unit.none 10.)
                    _pattern_transform [ (Transform.rotate 45.<deg>) ]
                    _pattern_units PattenUnits.UserSpaceOnUse
                ] [
                    line [
                        _x1 (Unit.none 0.)
                        _y1 (Unit.none 0.)
                        _x2 (Unit.none 0.)
                        _y2 (Unit.none 10.)
                        _style [
                            _stroke (Color.named Black)
                            _stroke_width (Unit.none 1.)
                        ]
                    ]
                ]
            ]

            text [
                _x (Unit.mm (C.pageMargins.left + C.textSpacing))
                _y (Unit.mm (C.secondLineY - C.textSpacing))
                _style [ _font_family [ FontFamily.generic SansSerif ] ]
            ] [ str dayName ]

            g [ _id "stores" ] (stores |> List.mapi (Place.render Lun))

            PageLayout.render index dayName color
        ]
