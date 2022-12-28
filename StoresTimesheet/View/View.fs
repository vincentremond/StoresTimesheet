namespace StoresTimesheet.View

open StoresTimesheet
open StoresTimesheet.View

module View =
    open Giraffe.ViewEngine.Svg

    let placeYPosition index =
        let y = C.secondLineY + ((float index) * C.lineHeight)
        (y, y + C.lineHeight)

    let weekDay weekday places =
        let index, dayName, dayColor =
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
            _xmlns__xlink "http://www.w3.org/1999/xlink"
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
                            _stroke_linecap Square
                        ]
                    ]
                ]
                filter [
                    _id "blur"
                    _x (Unit.none -0.5)
                    _y (Unit.none -0.5)
                    _width (Unit.none 2.)
                    _height (Unit.none 2.)
                    _style [ _color_interpolation_filters ColorInterpolationFilters.LinearRGB ]
                ] [
                    fe_color_matrix [
                        _type "matrix"
                        _values
                            """0.000  0.000  0.000  1.000  0.000 
0.000  0.000  0.000  1.000  0.000 
0.000  0.000  0.000  1.000  0.000 
0.000  0.000  0.000  1.000  0.000"""
                    ] []
                    fe_gaussian_blur [ _std_deviation 2. ] []
                ]
            ]

            // global background
            rect [
                _x (Unit.none 0.)
                _y (Unit.none 0.)
                _width (Unit.mm C.pageWidth)
                _height (Unit.mm C.pageHeight)
                _style [ _fill (Color.named White) ]
            ]

            text [
                _x (Unit.mm (C.pageMargins.left + C.textSpacing))
                _y (Unit.mm (C.secondLineY - C.textSpacing))
                _style [ _font_family [ FontFamily.generic SansSerif ] ]
            ] [ str dayName ]

            let places =
                places
                |> List.indexed
                |> List.map (fun (index, place) ->
                    let position = placeYPosition index
                    let colorSet = if index % 2 = 0 then C.colorSet1 else C.colorSet2
                    (place, position, colorSet))

            g
                [ _id "stores" ]
                ((places |> List.map (Place.renderMain weekday))
                 @ (places |> List.map (Place.renderBottomLine weekday)))

            let maxY = places |> List.map (fun (_, (_, y2), _) -> y2) |> List.max

            PageLayout.render index (dayName, dayColor) maxY
        ]
