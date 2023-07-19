namespace Giraffe.ViewEngine.Svg

type NamedColor =
    | Black
    | Gray
    | White
    | Yellow
    | PaleVioletRed
    | Coral
    | Gold
    | LightYellow
    | PaleGreen
    | LightSkyBlue
    | Plum
    | DimGray

    static member value =
        function
        | Black -> "black"
        | Gray -> "gray"
        | White -> "white"
        | Yellow -> "yellow"
        | PaleVioletRed -> "palevioletred"
        | Coral -> "coral"
        | Gold -> "gold"
        | LightYellow -> "lightyellow"
        | PaleGreen -> "palegreen"
        | LightSkyBlue -> "lightskyblue"
        | Plum -> "plum"
        | DimGray -> "dimgray"

[<RequireQualifiedAccess>]

type Color =
    | NamedColor of NamedColor
    | RGB of int * int * int
    | HSL of int * int * int
    | HEX of string
    | Url of string
    | None

    static member named color = NamedColor color
    static member rgb r g b = RGB(r, g, b)
    static member hsl h s l = HSL(h, s, l)
    static member hex hex = HEX hex
    static member url url = Url url
    static member none = None

    static member value =
        function
        | NamedColor color -> NamedColor.value color
        | RGB (r, g, b) -> $"rgb(%d{r}, %d{g}, %d{b})"
        | HSL (h, s, l) -> $"hsl(%d{h}, %d{s}%%, %d{l}%%)"
        | HEX hex -> hex
        | Url url -> $"url(%s{url})"
        | None -> "none"
