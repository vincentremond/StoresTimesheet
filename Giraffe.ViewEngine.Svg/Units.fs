namespace Giraffe.ViewEngine.Svg

open Giraffe.ViewEngine.Svg.StandardUnits

type Unit =
    | Centimeters of float<cm>
    | Pixels of float<px>
    | Millimeters of float<mm>
    | Inches of float<inch>
    | Points of float<pt>
    | Picas of float<pc>
    | Percent of float<percent>
    | None of float

    static member value =
        function
        | Pixels v -> $"%f{v}px"
        | Millimeters v -> $"%f{v}mm"
        | Inches v -> $"%f{v}in"
        | Points v -> $"%f{v}pt"
        | Picas v -> $"%f{v}pc"
        | Percent v -> $"%f{v}%%"
        | Centimeters v -> $"%f{v}cm"
        | None v -> $"%f{v}"

[<RequireQualifiedAccess>]
module Unit =

    let mm v = Millimeters v
    let px v = Pixels v
    let cm v = Centimeters v
    let inch v = Inches v
    let pt v = Points v
    let pc v = Picas v
    let percent v = Percent v
    let none v = None v
