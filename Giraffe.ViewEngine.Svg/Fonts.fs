namespace Giraffe.ViewEngine.Svg

type ComputedProperty =
    | Inherit
    | Initial
    | Revert
    | RevertLayer
    | Unset

    static member value =
        function
        | Inherit -> "inherit"
        | Initial -> "initial"
        | Revert -> "revert"
        | RevertLayer -> "revert-layer"
        | Unset -> "unset"

type FontWeight =
    | Normal
    | Bold
    | Bolder
    | Lighter
    | W100
    | W200
    | W300
    | W400
    | W500
    | W600
    | W700
    | W800
    | W900

    static member value =
        function
        | Normal -> "normal"
        | Bold -> "bold"
        | Bolder -> "bolder"
        | Lighter -> "lighter"
        | W100 -> "100"
        | W200 -> "200"
        | W300 -> "300"
        | W400 -> "400"
        | W500 -> "500"
        | W600 -> "600"
        | W700 -> "700"
        | W800 -> "800"
        | W900 -> "900"

type FontStyle =
    | Normal
    | Italic
    | Oblique

    static member value =
        function
        | Normal -> "normal"
        | Italic -> "italic"
        | Oblique -> "oblique"

type FontSize =
    | Absolute of FontSizeAbsolute
    | Relative of FontSizeRelative
    | Unit of Unit

    static member value =
        function
        | Absolute x -> FontSizeAbsolute.value x
        | Relative x -> FontSizeRelative.value x
        | Unit x -> Unit.value x

    static member absolute v = Absolute v
    static member relative v = Relative v
    static member unit v = Unit v

and FontSizeAbsolute =
    | XXSmall
    | XSmall
    | Small
    | Medium
    | Large
    | XLarge
    | XXLarge

    static member value =
        function
        | XXSmall -> "xx-small"
        | XSmall -> "x-small"
        | Small -> "small"
        | Medium -> "medium"
        | Large -> "large"
        | XLarge -> "x-large"
        | XXLarge -> "xx-large"

and FontSizeRelative =
    | Smaller
    | Larger

    static member value =
        function
        | Smaller -> "smaller"
        | Larger -> "larger"

type FontFamily =
    | Generic of FontFamilyGeneric
    | Computed of ComputedProperty
    | Named of string

    static member value =
        function
        | Generic x -> FontFamilyGeneric.value x
        | Computed x -> ComputedProperty.value x
        | Named x -> $"\"{x}\"" // TODO need escaping ?

    static member generic v = Generic v
    static member computed v = Computed v
    static member named v = Named v

and FontFamilyGeneric =
    | Serif
    | SansSerif
    | Monospace
    | Cursive
    | Fantasy
    | SystemUi
    | UiSerif
    | UiSansSerif
    | UiMonospace
    | UiRounded
    | Emoji
    | Fangsong
    | Math

    static member value =
        function
        | Serif -> "serif"
        | SansSerif -> "sans-serif"
        | Monospace -> "monospace"
        | Cursive -> "cursive"
        | Fantasy -> "fantasy"
        | SystemUi -> "system-ui"
        | UiSerif -> "ui-serif"
        | UiSansSerif -> "ui-sans-serif"
        | UiMonospace -> "ui-monospace"
        | UiRounded -> "ui-rounded"
        | Emoji -> "emoji"
        | Fangsong -> "fangsong"
        | Math -> "math"
