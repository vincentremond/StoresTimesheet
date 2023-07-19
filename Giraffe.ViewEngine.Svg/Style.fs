namespace Giraffe.ViewEngine.Svg

[<AutoOpen>]
module Style =

    type TextAlign =
        | Left
        | Center
        | Right
        | Justify
        | Start
        | End

        static member value =
            function
            | Left -> "left"
            | Center -> "center"
            | Right -> "right"
            | Justify -> "justify"
            | Start -> "start"
            | End -> "end"

    type TextAnchor =
        | Start
        | Middle
        | End

        static member toString =
            function
            | Start -> "start"
            | Middle -> "middle"
            | End -> "end"

    type CssProperty = { Name: string; Value: string }
    with
        static member value x = $"%s{x.Name}:%s{x.Value}"
        static member values x = x |> List.map CssProperty.value |> String.concat ";"

    let css name value = { Name = name; Value = value }

    let _font_size size = css "font-size" (FontSize.value size)

    let _font_style style =
        css "font-style" (FontStyle.value style)

    let _font_weight weight =
        css "font-weight" (FontWeight.value weight)

    let _font_family familyList =
        css "font-family" (familyList |> List.map FontFamily.value |> String.concat ",")

    let _fill color = css "fill" (Color.value color)
    let _text_align a = css "text-align" (TextAlign.value a)

    let _letter_spacing spacing =
        css "letter-spacing" (Unit.value spacing)

    let _opacity (f: float) = css "opacity" (string f)
    let _stroke color = css "stroke" (Color.value color)
    let _stroke_width v = css "stroke-width" (Unit.value v)

    type LineCap =
        | Butt
        | Round
        | Square

        static member value =
            function
            | Butt -> "butt"
            | Round -> "round"
            | Square -> "square"

    let _stroke_linecap cap =
        css "stroke-linecap" (LineCap.value cap)

    type LineJoin =
        | Miter
        | Round
        | Bevel

        static member value =
            function
            | Miter -> "miter"
            | Round -> "round"
            | Bevel -> "bevel"

    let _stroke_linejoin join =
        css "stroke-linejoin" (LineJoin.value join)

    let _stroke_opacity (f: float) = css "stroke-opacity" (string f)

    let _filter = css "filter"

    type ColorInterpolationFilters =
        | SRGB
        | LinearRGB

        static member value =
            function
            | SRGB -> "sRGB"
            | LinearRGB -> "linearRGB"

    let _color_interpolation_filters v =
        css "color-interpolation-filters" (ColorInterpolationFilters.value v)

    type Marker =
        | Named of string

        static member named = Named

        static member value m =
            match m with
            | Named s -> $"url(#{s})"

    let _marker_start marker =
        css "marker-start" (Marker.value marker)

    let _marker_mid marker = css "marker-mid" (Marker.value marker)

    let _marker_end marker = css "marker-end" (Marker.value marker)
