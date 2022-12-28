namespace StoresTimesheet

[<RequireQualifiedAccess>]
module Render =
    open System
    open Giraffe.ViewEngine

    let asHtml svg =
        RenderView.AsString.htmlDocument
        <| html [] [
            head [] [
                meta [ _httpEquiv "refresh"; _content "5" ]

            ]
            body [] [
                p [] [ str (DateTime.Now.ToString("yyyy - MM - dd • HH : mm : ss")) ]
                div [ _style "border: 1px black solid; display: inline-block;" ] [ svg ]
            ]
        ]

    let asXml svg = RenderView.AsString.xmlNodes <| [ svg ]
