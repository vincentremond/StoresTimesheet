namespace StoresTimesheet.Helpers

[<RequireQualifiedAccess>]
module Color =
    open ColorMine.ColorSpaces

    let hexFromHsv h s v =
        let hsb = Hsv(H = h, S = s/100., V = v/100.)
        let hex = hsb.To<Hex>()
        hex.ToString()
