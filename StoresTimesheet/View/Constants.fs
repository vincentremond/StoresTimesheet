namespace StoresTimesheet.View

open Giraffe.ViewEngine.Svg
open StoresTimesheet

type ColorSet = {
    PrimaryText: Color
    SecondaryText: Color
    Opened: Color
    ExtendedOpened: Color
    Background: Color
}

[<RequireQualifiedAccess>]
module C =

    let lineWidth = {|
        Small = Unit.mm 0.10<mm>
        Standard = Unit.mm 0.25<mm>
    |}

    let nbsp = '\u00A0'

    let daysPerWeek = 7.

    let pageWidth = 297.<mm>
    let pageHeight = 210.<mm>

    let pageMargins = {|
        top = 5.<mm>
        right = 15.<mm>
        left = 17.<mm>
    |}

    let lineHeight = 6.<mm>
    let defaultFontSize = 10<pt>
    let leftSpaceForLabels = 72.<mm>

    let secondLineY = pageMargins.top + 7.<mm>
    let textSpacing = 1.6<mm>

    let firstHour = 6<hour>
    let lastHour = 23<hour>

    let hoursStartX = leftSpaceForLabels + pageMargins.left

    let hourWidth =
        (pageWidth - leftSpaceForLabels - pageMargins.left - pageMargins.right)
        / float (lastHour - firstHour + 1<hour>)

    let mkColorSet h backgroundSaturationAdjustment = {
        PrimaryText = Color.named Black
        SecondaryText = Color.named DimGray
        Opened = Helpers.Color.hexFromHsv h 80. 100. |> Color.hex
        ExtendedOpened = Helpers.Color.hexFromHsv (h + 10.) 45. 80. |> Color.hex
        Background =
            Helpers.Color.hexFromHsv h (6. + backgroundSaturationAdjustment) 95.
            |> Color.hex
    }

    let colorSet1 = mkColorSet 120. 0.

    let colorSet2 = mkColorSet 220. 3.

    let iconMargin = 1.<mm>
