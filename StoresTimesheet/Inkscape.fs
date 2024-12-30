namespace StoresTimesheet

open System.Diagnostics

[<RequireQualifiedAccess>]
module Inkscape =
    let private inkscapePath = @"C:\Program Files\Inkscape\bin\inkscape.exe"

    let private runInkscape (args: string seq) =
        let psi =
            ProcessStartInfo(
                inkscapePath,
                args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            )

        let p = Process.Start(psi)
        p.WaitForExit()

    let convertToPng (inputFile: string) (outputFile: string) =
        runInkscape [
            "--export-type"
            "png"
            "-d"
            "600"
            "--export-filename"
            outputFile
            inputFile
        ]

    let exportToPdf (inputFile: string) (outputFile: string) =
        runInkscape [
            "--export-type"
            "pdf"
            "--export-filename"
            outputFile
            inputFile
        ]
