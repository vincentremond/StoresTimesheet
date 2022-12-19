namespace Giraffe.ViewEngine.Svg

[<AutoOpen>]
module Tags =

    open Giraffe.ViewEngine

    let g = tag "g"
    let line = voidTag "line"
    let rect = voidTag "rect"
    let svg = tag "svg"
    let defs = tag "defs"
    let pattern = tag "pattern"
    let text = tag "text"
    let tspan = tag "tspan"
    let str = str
    let image = voidTag "image"
    let filter = tag "filter"
    let fe_gaussian_blur = tag "feGaussianBlur"
    let fe_color_matrix = tag "feColorMatrix"
