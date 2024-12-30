namespace StoresTimesheet.View

open StoresTimesheet
open StoresTimesheet.Helpers

[<RequireQualifiedAccess>]
module PlaceView =

    open Giraffe.ViewEngine.Svg

    let calculatePlaceVerticalPosition placeViewModel =
        let y = C.secondLineY + ((float placeViewModel.Index) * C.lineHeight)
        (y, y + C.lineHeight)

    let renderMain pageVerticalOffset (place: PlaceViewModel) =

        let minY, _ = calculatePlaceVerticalPosition place
        let colorSet = C.colorSets[place.Index % C.colorSets.Length]

        g [] [

            rect [
                _x (Unit.mm C.pageMargins.left)
                _y (Unit.mm (pageVerticalOffset + minY))
                _width (Unit.mm (C.pageWidth - C.pageMargins.left - C.pageMargins.right))
                _height (Unit.mm C.lineHeight)
                _style [ _fill place.Colors.Background ]
            ]

            let icon blurred =
                image [
                    _x (Unit.mm (C.pageMargins.left + C.iconMargin))
                    _y (Unit.mm (pageVerticalOffset + minY + C.iconMargin))
                    _width (Unit.mm (C.lineHeight - (C.iconMargin * 2.0)))
                    _height (Unit.mm (C.lineHeight - (C.iconMargin * 2.0)))
                    _preserve_aspect_ratio "none"
                    _xlink__href (Emoji.getEmojiAsBase64 place.Icon)
                    if blurred then
                        _style [ _filter "url(#blur)" ]
                ]

            icon true
            icon false

            image [
                _x (Unit.mm (C.pageMargins.left + C.lineHeight))
                _y (Unit.mm (pageVerticalOffset + minY + C.iconMargin))
                _width (Unit.mm (C.lineHeight - (C.iconMargin * 2.0)))
                _height (Unit.mm (C.lineHeight - (C.iconMargin * 2.0)))
                _preserve_aspect_ratio "none"
                _xlink__href (Image.toBase64 place.Favicon)
            ]

            text [
                _x (Unit.mm (C.pageMargins.left + C.lineHeight + C.lineHeight))
                _y (Unit.mm (pageVerticalOffset + minY - C.textSpacing + C.lineHeight))
                _style [ _font_family [ FontFamily.generic SansSerif ] ]
            ] [
                tspan [
                    _style [
                        _font_size (FontSize.absolute FontSizeAbsolute.Small)
                        _font_weight Bold
                        _fill colorSet.PrimaryText
                    ]
                ] [ str place.Name ]
                tspan [] [ str (String.ofChars 2 C.nbsp) ]
                tspan [
                    _style [
                        _font_size (FontSize.absolute XSmall)
                        _font_style Italic
                        _fill colorSet.SecondaryText
                    ]
                ] [ str $"{place.Description}" ]
            ]

            for openingHours, color in
                [
                    place.ExtendedOpeningHours, colorSet.ExtendedOpened
                    place.OpeningHours, colorSet.Opened
                ] do

                for OpenPeriod(startTime, endTime) in openingHours do
                    let start = TimeOnly.init C.firstHour 0<minute>
                    let fromX = C.hoursStartX + ((startTime - start).TotalHours * C.hourWidth)
                    let width = (endTime - startTime).TotalHours * C.hourWidth

                    rect [
                        _x (Unit.mm fromX)
                        _y (Unit.mm (pageVerticalOffset + minY))
                        _width (Unit.mm width)
                        _height (Unit.mm C.lineHeight)
                        _style [ _fill color ]
                    ]
        ]

    let renderBottomLines pageVerticalOffset placeViewModel =
        let _, maxY = calculatePlaceVerticalPosition placeViewModel

        line [
            _x1 (Unit.mm C.pageMargins.left)
            _y1 (Unit.mm (pageVerticalOffset + maxY))
            _x2 (Unit.mm (C.pageWidth - C.pageMargins.right))
            _y2 (Unit.mm (pageVerticalOffset + maxY))
            _style [
                _stroke (Color.named Black)
                _stroke_width C.lineWidth.Standard
                _stroke_linecap Square
            ]
        ]
