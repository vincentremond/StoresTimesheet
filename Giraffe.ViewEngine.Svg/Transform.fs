namespace Giraffe.ViewEngine.Svg

open Giraffe.ViewEngine.Svg.StandardUnits

[<RequireQualifiedAccess>]
type Transform =
    | Rotate of float<deg>
    | RotateAround of float<deg> * Unit * Unit
    | Translate of Unit * Unit

    static member rotate(deg: float<deg>) = Rotate deg
    static member translate x y = Translate(x, y)
    static member rotateAround (deg: float<deg>) (x: Unit) (y: Unit) = RotateAround(deg, x, y)

    static member value =
        function
        | Rotate f -> $"rotate(%f{f})"
        | RotateAround(f, x, y) -> $"rotate(%f{f} %s{Unit.value x}  %s{Unit.value y})"
        | Translate(x, y) -> $"translate(%s{Unit.value x} %s{Unit.value y})"
