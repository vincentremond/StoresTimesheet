namespace StoresTimesheet

open System.Diagnostics

[<RequireQualifiedAccess>]
module Inkscape =
    let private inkscapePath = @"C:\Program Files\Inkscape\bin\inkscape.exe"

    let private runInkscape args =
        let psi = ProcessStartInfo(inkscapePath, args)
        psi.UseShellExecute <- false
        psi.RedirectStandardOutput <- true
        psi.RedirectStandardError <- true
        // psi.CreateNoWindow <- true
        let p = Process.Start(psi)
        p.WaitForExit()

    let convertToPng (inputFile: string) (outputFile: string) =
        runInkscape $"--export-type png -d 600 --export-filename \"{outputFile}\" \"{inputFile}\""
