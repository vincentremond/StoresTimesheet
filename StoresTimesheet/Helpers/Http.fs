namespace StoresTimesheet.Helpers

open System.Net.Http

[<RequireQualifiedAccess>]
module Http =
    let download (url: string) =
        task {
            use client = new HttpClient()
            let! response = client.GetAsync(url)
            let! content = response.Content.ReadAsByteArrayAsync()
            let contentType = response.Content.Headers.ContentType.MediaType
            return (contentType, content)
        }
        |> Async.AwaitTask
        |> Async.RunSynchronously
