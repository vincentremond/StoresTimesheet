namespace Giraffe.ViewEngine.Svg

open Giraffe.ViewEngine.Svg.StandardUnits

[<AutoOpen>]
module Path =

    [<RequireQualifiedAccess>]
    type PathCommand =
        | Move of Move
        | Line of Line
        | CubicBezierCurve of CubicBezierCurve
        | QuadraticBezierCurve of QuadraticBezierCurve
        | EllipticalArc of EllipticalArc
        | ClosePath

        static member value =
            function
            | Move m -> Move.value m
            | Line l -> Line.value l
            | CubicBezierCurve c -> CubicBezierCurve.value c
            | QuadraticBezierCurve q -> QuadraticBezierCurve.value q
            | EllipticalArc e -> EllipticalArc.value e
            | ClosePath -> "Z"

        static member values commands =
            commands |> List.map PathCommand.value |> String.concat " "

    and Type =
        | Absolute
        | Relative

    and [<RequireQualifiedAccess>] Move =
        | Point of (Type * float * float)

        static member value(move: Move) =
            match move with
            | Point(Absolute, x, y) -> $"M %f{x},%f{y}"
            | Point(Relative, x, y) -> $"m %f{x},%f{y}"

        static member toPoint t x y = PathCommand.Move(Point(t, x, y))

    and [<RequireQualifiedAccess>] Line =
        | Point of (Type * float * float)
        | Horizontal of (Type * float)
        | Vertical of (Type * float)

        static member toPoint t x y = PathCommand.Line(Point(t, x, y))
        static member horizontal t x = PathCommand.Line(Horizontal(t, x))
        static member vertical t y = PathCommand.Line(Vertical(t, y))

        static member value =
            function
            | Point(Absolute, x, y) -> $"L %f{x},%f{y}"
            | Point(Relative, x, y) -> $"l %f{x},%f{y}"
            | Horizontal(Absolute, x) -> $"H %f{x}"
            | Horizontal(Relative, x) -> $"h %f{x}"
            | Vertical(Absolute, y) -> $"V %f{y}"
            | Vertical(Relative, y) -> $"v %f{y}"

    and [<RequireQualifiedAccess>] CubicBezierCurve =
        | Cubic of Type * float * float * float * float * float * float
        | Smooth of Type * float * float * float * float

        static member cubic t x1 y1 x2 y2 x y =
            PathCommand.CubicBezierCurve(Cubic(t, x1, y1, x2, y2, x, y))

        static member smooth t x2 y2 x y =
            PathCommand.CubicBezierCurve(Smooth(t, x2, y2, x, y))

        static member value =
            function
            | Cubic(Absolute, x1, y1, x2, y2, x, y) -> $"C %f{x1},%f{y1} %f{x2},%f{y2} %f{x},%f{y}"
            | Cubic(Relative, x1, y1, x2, y2, x, y) -> $"c %f{x1},%f{y1} %f{x2},%f{y2} %f{x},%f{y}"
            | Smooth(Absolute, x2, y2, x, y) -> $"S %f{x2},%f{y2} %f{x},%f{y}"
            | Smooth(Relative, x2, y2, x, y) -> $"s %f{x2},%f{y2} %f{x},%f{y}"

    and [<RequireQualifiedAccess>] QuadraticBezierCurve =
        | Quadratic of Type * float * float * float * float
        | Smooth of Type * float * float

        static member quadratic t x1 y1 x y =
            PathCommand.QuadraticBezierCurve(Quadratic(t, x1, y1, x, y))

        static member smooth t x y =
            PathCommand.QuadraticBezierCurve(Smooth(t, x, y))

        static member value =
            function
            | Quadratic(Absolute, x1, y1, x, y) -> $"Q %f{x1},%f{y1} %f{x},%f{y}"
            | Quadratic(Relative, x1, y1, x, y) -> $"q %f{x1},%f{y1} %f{x},%f{y}"
            | Smooth(Absolute, x, y) -> $"T %f{x},%f{y}"
            | Smooth(Relative, x, y) -> $"t %f{x},%f{y}"

    and [<RequireQualifiedAccess>] EllipticalArc =
        | EllipticalArc of
            t: Type *
            rx: float *
            ry: float *
            angle: float<deg> *
            largeArcFlag: LargeArcFlag *
            sweepFlag: SweepFlag *
            x: float *
            y: float

        static member arc t rx ry angle largeArcFlag sweepFlag x y =
            PathCommand.EllipticalArc(EllipticalArc(t, rx, ry, angle, largeArcFlag, sweepFlag, x, y))

        static member value =
            function
            | EllipticalArc(Absolute, rx, ry, angle, largeArcFlag, sweepFlag, x, y) ->
                $"A %f{rx},%f{ry} %f{angle} %d{LargeArcFlag.value largeArcFlag} %d{SweepFlag.value sweepFlag} %f{x},%f{y}"
            | EllipticalArc(Relative, rx, ry, angle, largeArcFlag, sweepFlag, x, y) ->
                $"a %f{rx},%f{ry} %f{angle} %d{LargeArcFlag.value largeArcFlag} %d{SweepFlag.value sweepFlag} %f{x},%f{y}"

    and LargeArcFlag =
        | Large
        | Small

        static member value =
            function
            | Large -> 1
            | Small -> 0

    and SweepFlag =
        | Clockwise
        | CounterClockwise

        static member value =
            function
            | Clockwise -> 1
            | CounterClockwise -> 0
