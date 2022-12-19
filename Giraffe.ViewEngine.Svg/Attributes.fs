namespace Giraffe.ViewEngine.Svg

[<AutoOpen>]
module Attributes =

    open Giraffe.ViewEngine

    let private sizeAttr n size = attr n (Unit.value size)
    let private colorAttr n color = attr n (Color.value color)
    let private floatAttr n (f: float) = attr n (string f)

    let _text_anchor a =
        attr "text-anchor" (TextAnchor.toString a)

    let _xmlns = attr "xmlns"
    let _xmlns__xlink = attr "xmlns:xlink"
    let _xlink__href = attr "xlink:href"

    let _x = sizeAttr "x"
    let _y = sizeAttr "y"
    let _x1 = sizeAttr "x1"
    let _y1 = sizeAttr "y1"
    let _x2 = sizeAttr "x2"
    let _y2 = sizeAttr "y2"

    let private transformAttr n values =
        attr n (values |> (List.map Transform.value) |> String.concat " ")

    let _transform = transformAttr "transform"

    type TransformOrigin =
        | Center
        | Positions of TransformOriginPosition * TransformOriginPosition
        | XY of Unit * Unit
        | XYZ of Unit * Unit * Unit

        static member value =
            function
            | Center -> "center"
            | Positions (x, y) -> $"%s{TransformOriginPosition.value x} %s{TransformOriginPosition.value y}"
            | XY (x, y) -> $"%s{Unit.value x} %s{Unit.value y}"
            | XYZ (x, y, z) -> $"%s{Unit.value x} %s{Unit.value y} %s{Unit.value z}"

        static member positions v h = Positions(v, h)
        static member xy x y = XY(x, y)
        static member xyz x y z = XYZ(x, y, z)
        static member center = Center

    and TransformOriginPosition =
        | Center
        | Top
        | Bottom
        | Left
        | Right

        static member value =
            function
            | Center -> "center"
            | Top -> "top"
            | Bottom -> "bottom"
            | Left -> "left"
            | Right -> "right"

    let _transform_origin v =
        attr "transform-origin" (TransformOrigin.value v)

    let _pattern_transform = transformAttr "patternTransform"

    type PattenUnits =
        | UserSpaceOnUse
        | ObjectBoundingBox

        static member value =
            function
            | UserSpaceOnUse -> "userSpaceOnUse"
            | ObjectBoundingBox -> "objectBoundingBox"

    let _pattern_units v =
        attr "patternUnits" (PattenUnits.value v)

    let _style (values: CssProperty list) =
        attr "style" (values |> List.map (fun x -> $"%s{x.Name}:%s{x.Value}") |> String.concat ";")

    let _std_deviation = floatAttr "stdDeviation"
    let _type = attr "type"
    let _values = attr "values"
    let _width = sizeAttr "width"
    let _height = sizeAttr "height"
    let _preserve_aspect_ratio = attr "preserveAspectRatio"
    let _id = attr "id"

    [<RequireQualifiedAccess>]
    type DominantBaseline =
        | Auto
        | UseScript
        | NoChange
        | ResetSize
        | Ideographic
        | Alphabetic
        | Hanging
        | Mathematical
        | Central
        | Middle
        | TextAfterEdge
        | TextBeforeEdge

        static member value =
            function
            | Auto -> "auto"
            | UseScript -> "use-script"
            | NoChange -> "no-change"
            | ResetSize -> "reset-size"
            | Ideographic -> "ideographic"
            | Alphabetic -> "alphabetic"
            | Hanging -> "hanging"
            | Mathematical -> "mathematical"
            | Central -> "central"
            | Middle -> "middle"
            | TextAfterEdge -> "text-after-edge"
            | TextBeforeEdge -> "text-before-edge"

    let _dominant_baseline v =
        attr "dominant-baseline" (DominantBaseline.value v)
