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

    let css name value = { Name = name; Value = value }

    open Giraffe.ViewEngine

    let _font_size size = css "font-size" (FontSize.value size)

    let _font_style style =
        css "font-style" (FontStyle.value style)

    let _font_weight weight =
        css "font-weight" (FontWeight.value weight)

    let _font_family familyList =
        css "font-family" (familyList |> List.map FontFamily.value |> String.concat ",")

    let _fill color = css "fill" (Color.value color)
    let _text_align a = css "text-align" (TextAlign.value a)
    let _letter_spacing spacing = css "letter-spacing" (Unit.value spacing)

    let _opacity (f: float) = css "opacity" (string f)
    let _stroke color = css "stroke" (Color.value color)
    let _stroke_width v = css "stroke-width" (Unit.value v)
    let _stroke_opacity (f: float) = css "stroke-opacity" (string f)
