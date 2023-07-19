namespace StoresTimesheet.View

open Giraffe.ViewEngine.Svg.StandardUnits
open StoresTimesheet
open StoresTimesheet.View

module View =
    open Giraffe.ViewEngine.Svg

    let render model =

        svg [
            _width (Unit.mm C.pageWidth)
            _height (Unit.mm C.pageHeight)
            _xmlns "http://www.w3.org/2000/svg"
            _xmlns__xlink "http://www.w3.org/1999/xlink"
        ] [
            (Sodipodi.namedView [] [
                for dayModel in model.WeekDays do
                    Inkscape.page [
                        _x (Unit.none 0.0)
                        _y (Unit.mm (float dayModel.Index * (C.pageHeight + 10.0<mm>)))
                        _width (Unit.mm C.pageWidth)
                        _height (Unit.mm C.pageHeight)
                        _id $"Page-{dayModel.Name}"
                    ]
            ])

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

            for dayModel in model.WeekDays do

                let pageVerticalOffset = float dayModel.Index * (C.pageHeight + 10.0<mm>)

                // Day name
                text [
                    _x (Unit.mm (C.pageMargins.left + C.textSpacing))
                    _y (Unit.mm (pageVerticalOffset + C.secondLineY - C.textSpacing))
                    _style [ _font_family [ FontFamily.generic SansSerif ] ]
                ] [ str dayModel.Name ]

                g [] (dayModel.Places |> List.map (PlaceView.renderMain pageVerticalOffset))
                g [] (dayModel.Places |> List.map (PlaceView.renderBottomLines pageVerticalOffset))
                g [] (PageLayout.render pageVerticalOffset dayModel)
        ]
