namespace StoresTimesheet.View

[<RequireQualifiedAccess>]
module PageLayout =

    open Giraffe.ViewEngine.Svg
    open StoresTimesheet
    open StoresTimesheet.Helpers

    let renderPageLines () = [

        // margin top
        line [
            _x1 (Unit.mm C.pageMargins.left)
            _y1 (Unit.mm C.pageMargins.top)
            _x2 (Unit.mm (C.pageWidth - C.pageMargins.right))
            _y2 (Unit.mm C.pageMargins.top)
            _style [ _stroke (Color.named Black) ]
        ]

        // margin bottom
        line [
            _x1 (Unit.mm C.pageMargins.left)
            _y1 (Unit.mm (C.pageHeight - C.pageMargins.bottom))
            _x2 (Unit.mm (C.pageWidth - C.pageMargins.right))
            _y2 (Unit.mm (C.pageHeight - C.pageMargins.bottom))
            _style [ _stroke (Color.named Black) ]
        ]

        // margin left
        line [
            _x1 (Unit.mm C.pageMargins.left)
            _y1 (Unit.mm C.pageMargins.top)
            _x2 (Unit.mm C.pageMargins.left)
            _y2 (Unit.mm (C.pageHeight - C.pageMargins.bottom))
            _style [ _stroke (Color.named Black) ]
        ]

        // margin right
        line [
            _x1 (Unit.mm (C.pageWidth - C.pageMargins.right))
            _y1 (Unit.mm C.pageMargins.top)
            _x2 (Unit.mm (C.pageWidth - C.pageMargins.right))
            _y2 (Unit.mm (C.pageHeight - C.pageMargins.bottom))
            _style [ _stroke (Color.named Black) ]
        ]

        // second horizontal line for the header
        line [
            _x1 (Unit.mm C.pageMargins.left)
            _y1 (Unit.mm C.secondLineY)
            _x2 (Unit.mm (C.pageWidth - C.pageMargins.right))
            _y2 (Unit.mm C.secondLineY)
            _style [ _stroke (Color.named Black) ]
        ]

        for index, hour in ([ C.firstHour .. 1<hour> .. C.lastHour ] |> List.mapi') do
            let x = C.hoursStartX + (C.hourWidth * float index)

            line [
                _x1 (Unit.mm x)
                _y1 (Unit.mm C.secondLineY)
                _x2 (Unit.mm x)
                _y2 (Unit.mm (C.pageHeight - C.pageMargins.bottom))
                _style [
                    _stroke (Color.named Black)
                    _stroke_width (Unit.px 2.<px>)
                ]
            ]

            for quarter in [ 1..3 ] do
                let x = x + (C.hourWidth / 4.0 * float quarter)

                line [
                    _x1 (Unit.mm x)
                    _y1 (Unit.mm C.secondLineY)
                    _x2 (Unit.mm x)
                    _y2 (Unit.mm (C.pageHeight - C.pageMargins.bottom))
                    _style [
                        _stroke (Color.rgb 0 0 0)
                        _stroke_opacity 0.3
                    ]
                ]

            text [
                _x (Unit.mm x)
                _y (Unit.mm (C.secondLineY - C.textSpacing))
                _text_anchor TextAnchor.Middle
                _style [
                    _text_align TextAlign.Center
                    _font_family [ FontFamily.generic SansSerif ]
                ]
            ] [ str (string hour) ]

    ]

    let renderRightDayName index dayName color =
        let verticalSize = C.pageHeight / C.daysPerWeek
        let width = 10.<mm>
        let x = C.pageWidth - width
        let dayBackgroundY = index * verticalSize
        let toCutY = dayBackgroundY + verticalSize
        let toCutHeight = C.pageHeight - toCutY

        [
            // day back
            rect [
                _x (Unit.mm x)
                _y (Unit.mm dayBackgroundY)
                _width (Unit.mm width)
                _height (Unit.mm verticalSize)
                _style [ _fill (Color.named color) ]
            ]

            // to cut
            let cutMargin = 1.<mm>
            rect [
                _x (Unit.mm (x+ cutMargin))
                _y (Unit.mm (toCutY + cutMargin))
                _width (Unit.mm (width - cutMargin))
                _height (Unit.mm (toCutHeight - cutMargin))
                _style [ _fill (Color.url "#diagonalHatch") ]
            ]
            
            let textCenterX = Unit.mm (x + (width / 2.0))
            let textCenterY = Unit.mm (dayBackgroundY + (verticalSize / 2.0))
            text [
                _id "day-name"
                _x textCenterX
                _y textCenterY
                _text_anchor TextAnchor.Middle
                _dominant_baseline DominantBaseline.Central
                _transform [ (Transform.rotate 90.<deg>) ]
                _transform_origin (TransformOrigin.xy textCenterX textCenterY)
                _style [
                    _text_align TextAlign.Center
                    _font_family [ FontFamily.generic SansSerif ]
                    _fill (Color.named Black)
                    _letter_spacing (Unit.px 3.<px>)
                ]
                              
            ] [ str (dayName |> String.toUpperInvariant) ]
        ]

    let render index dayName color =
        g [] (renderPageLines () @ (renderRightDayName index dayName color))
