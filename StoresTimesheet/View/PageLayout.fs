namespace StoresTimesheet.View

open StoresTimesheet

[<RequireQualifiedAccess>]
module PageLayout =

    open Giraffe.ViewEngine.Svg
    open StoresTimesheet.Helpers

    let renderPageLines pageVerticalOffset dayModel = [

        let maxY =
            dayModel.Places
            |> List.map (PlaceView.calculatePlaceVerticalPosition >> snd)
            |> List.max

        // margin top
        line [
            _x1 (Unit.mm C.pageMargins.left)
            _y1 (Unit.mm (pageVerticalOffset + C.pageMargins.top))
            _x2 (Unit.mm (C.pageWidth - C.pageMargins.right))
            _y2 (Unit.mm (pageVerticalOffset + C.pageMargins.top))
            _style [
                _stroke (Color.named Black)
                _stroke_linecap Square
            ]
        ]

        // margin bottom
        line [
            _x1 (Unit.mm C.pageMargins.left)
            _y1 (Unit.mm (pageVerticalOffset + maxY))
            _x2 (Unit.mm (C.pageWidth - C.pageMargins.right))
            _y2 (Unit.mm (pageVerticalOffset + maxY))
            _style [
                _stroke (Color.named Black)
                _stroke_linecap Square
            ]
        ]

        // margin left
        line [
            _x1 (Unit.mm C.pageMargins.left)
            _y1 (Unit.mm (pageVerticalOffset + C.pageMargins.top))
            _x2 (Unit.mm C.pageMargins.left)
            _y2 (Unit.mm (pageVerticalOffset + maxY))
            _style [
                _stroke (Color.named Black)
                _stroke_linecap Square
            ]
        ]

        // margin right
        line [
            _x1 (Unit.mm (C.pageWidth - C.pageMargins.right))
            _y1 (Unit.mm (pageVerticalOffset + C.pageMargins.top))
            _x2 (Unit.mm (C.pageWidth - C.pageMargins.right))
            _y2 (Unit.mm (pageVerticalOffset + maxY))
            _style [
                _stroke (Color.named Black)
                _stroke_linecap Square
            ]
        ]

        // second horizontal line for the header
        line [
            _x1 (Unit.mm C.pageMargins.left)
            _y1 (Unit.mm (pageVerticalOffset + C.secondLineY))
            _x2 (Unit.mm (C.pageWidth - C.pageMargins.right))
            _y2 (Unit.mm (pageVerticalOffset + C.secondLineY))
            _style [
                _stroke (Color.named Black)
                _stroke_linecap Square
            ]
        ]

        for index, hour in ([ C.firstHour .. 1<hour> .. C.lastHour ] |> List.mapi') do
            let x = C.hoursStartX + (C.hourWidth * float index)

            line [
                _x1 (Unit.mm x)
                _y1 (Unit.mm (pageVerticalOffset + C.secondLineY))
                _x2 (Unit.mm x)
                _y2 (Unit.mm (pageVerticalOffset + maxY))
                _style [
                    _stroke (Color.named Black)
                    _stroke_width C.lineWidth.Standard
                    _stroke_linecap Square
                ]
            ]

            for quarter in [ 1..3 ] do
                let x = x + (C.hourWidth / 4.0 * float quarter)

                line [
                    _x1 (Unit.mm x)
                    _y1 (Unit.mm (pageVerticalOffset + C.secondLineY))
                    _x2 (Unit.mm x)
                    _y2 (Unit.mm (pageVerticalOffset + maxY))
                    _style [
                        _stroke (Color.rgb 20 20 20)
                        _stroke_width C.lineWidth.Small
                        _stroke_linecap Square
                    ]
                ]

            text [
                _x (Unit.mm x)
                _y (Unit.mm (pageVerticalOffset + C.secondLineY - C.textSpacing))
                _text_anchor TextAnchor.Middle
                _style [
                    _text_align TextAlign.Center
                    _font_family [ FontFamily.generic SansSerif ]
                ]
            ] [ str (string hour) ]

    ]

    let renderRightDayName pageVerticalOffset (dayModel: WeekDayViewModel) =
        let verticalSize = C.pageHeight / C.daysPerWeek
        let width = 10.<mm>
        let x = C.pageWidth - width
        let dayBackgroundY = (float dayModel.Index) * verticalSize
        let toCutY = dayBackgroundY + verticalSize
        let toCutHeight = C.pageHeight - toCutY

        [
            // day back
            rect [
                _x (Unit.mm x)
                _y (Unit.mm (pageVerticalOffset + dayBackgroundY))
                _width (Unit.mm width)
                _height (Unit.mm verticalSize)
                _style [ _fill dayModel.DayColor ]
            ]

            // to cut
            let cutMargin = 1.<mm>

            rect [
                _x (Unit.mm (x + cutMargin))
                _y (Unit.mm (pageVerticalOffset + toCutY + cutMargin))
                _width (Unit.mm (width - cutMargin))
                _height (Unit.mm (toCutHeight - cutMargin))
                _style [ _fill (Color.url "#diagonalHatch") ]
            ]

            text [
                _x (Unit.mm (pageVerticalOffset + dayBackgroundY + (verticalSize / 2.0)))
                _y (Unit.mm (-1. * (x + (width / 2.0))))
                _text_anchor TextAnchor.Middle
                _dominant_baseline DominantBaseline.Central
                _transform_origin (TransformOrigin.xy (Unit.none 0.) (Unit.none 0.))
                _transform [ (Transform.rotate 90.<deg>) ]
                _style [
                    _text_align TextAlign.Center
                    _font_family [ FontFamily.generic SansSerif ]
                    _fill (Color.named Black)
                    _letter_spacing (Unit.px 3.<px>)
                ]
            ] [ str (dayModel.Name |> String.toUpperInvariant) ]
        ]

    let render pageVerticalOffset (weekDayViewModel: WeekDayViewModel) = [
        g [] (renderPageLines pageVerticalOffset weekDayViewModel)
        g [] (renderRightDayName pageVerticalOffset weekDayViewModel)
    ]
