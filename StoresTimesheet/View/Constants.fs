namespace StoresTimesheet.View

open Giraffe.ViewEngine.Svg
open StoresTimesheet

type ColorSet = {
    PrimaryText: Color
    SecondaryText: Color
    Opened: Color
    Background: Color
}

[<RequireQualifiedAccess>]
module C =

    let nbsp = '\u00A0'

    let daysPerWeek = 7.

    let pageWidth = 297.<mm>
    let pageHeight = 210.<mm>

    let pageMargins = {|
        top = 5.<mm>
        right = 15.<mm>
        bottom = 5.<mm>
        left = 17.<mm>
    |}

    let lineHeight = 6.<mm>
    let defaultFontSize = 10<pt>
    let leftSpaceForLabels = 72.<mm>

    let secondLineY = pageMargins.top + 7.<mm>
    let textSpacing = 1.6<mm>

    let firstHour = 6<hour>
    let lastHour = 21<hour>

    let hoursStartX = leftSpaceForLabels + pageMargins.left

    let hourWidth =
        (pageWidth - leftSpaceForLabels - pageMargins.left - pageMargins.right)
        / float (lastHour - firstHour + 1<hour>)

    let colorSet1 = {
        PrimaryText = Color.named Black
        SecondaryText = Color.named DimGray
        Opened = Color.hex "#2aeba7"
        Background = Color.hex "#d9f0e8" // PaleGreen
    }

    let colorSet2 = {
        PrimaryText = Color.named Black
        SecondaryText = Color.named DimGray
        Opened = Color.hex "#aaeb2a"
        Background = Color.hex "#e8f0d9" // PaleYellow
    }

    let iconMargin = 1.<mm>
