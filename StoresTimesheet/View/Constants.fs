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

    let private initColorSet h c = {
        PrimaryText = Color.hsl h (35 + c) (18 + c)
        SecondaryText = Color.hsl h (10 + c) (8 + c)
        Opened = Color.hsl h (35 + c) (30 + c)
        Background = Color.hsl h (22 + c) (70 + c)
    }

    let colorSet1 = initColorSet 102 0
    let colorSet2 = initColorSet 70 10
