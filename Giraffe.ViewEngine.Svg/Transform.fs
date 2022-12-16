namespace Giraffe.ViewEngine.Svg

[<RequireQualifiedAccess>]
type Transform =
    | Rotate of float<deg>
    | RotateAround of float<deg> * Unit * Unit
    
    static member rotate (deg: float<deg>) = Rotate deg
    static member rotateAround (deg: float<deg>) (x: Unit) (y: Unit) = RotateAround (deg, x, y)

    static member value =
        function
        | Rotate f -> $"rotate(%f{f})"
        | RotateAround (f, x, y) -> $"rotate(%f{f}, %s{Unit.value x}, %s{Unit.value y})"
