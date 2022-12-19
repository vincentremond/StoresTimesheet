namespace StoresTimesheet

open System
open StoresTimesheet.Path

[<RequireQualifiedAccess>]
module Emoji =

    let mutable folderPath: string option = None

    let getEmojiAsBase64 (emoji: string) =
        let id = Char.ConvertToUtf32(emoji, 0).ToString("X4")
        let fileName = $"%s{id}.png"
        let filePath = folderPath.Value </> fileName
        let bytes = filePath |> File.readAllBytes
        let base64 = System.Convert.ToBase64String(bytes)
        $"data:image/png;base64,{base64}"
