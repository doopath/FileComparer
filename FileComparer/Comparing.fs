namespace FileComparer.Comparing

module Comparer =
    open System
    open System.IO
    open Spectre.Console
    open System.Collections.Generic


    let isValidPathToDirectory (path: string) = path.EndsWith("/") || path.EndsWith(@"\")

    let toUnixPath (path: string) = path.Replace(@"\", "/")
    
    let getFileSize (path: string) = (new FileInfo(path)).Length
        

    let setBarChartWidth (width: int) (item: BarChart) =
        item.Width <- width
        item

    let setBarChartLabel (label: string) (item: BarChart) =
        item.Label <- label
        item


    let createBarChart (width: int) (label: string) = new BarChart() |> setBarChartWidth width |> setBarChartLabel label


    let takeFirst (l: 'a IEnumerable) =
        let enum = l.GetEnumerator()

        match enum.MoveNext() with
        | true -> enum.Current
        | false -> failwith "Cannot get first element of an empty IEnumerable object."
        

    let modifyPathToDirectory (path: string) =
        match path.EndsWith "/" || path.EndsWith @"\" with
        | true -> toUnixPath path
        | false -> toUnixPath (path + "/")


    let takeFileName (path: string) =
        (toUnixPath path).Split("/")
        |> List.ofArray
        |> List.rev
        |> takeFirst


    let getFilesSizes (files: string[]) =
        let sizesQuery =
            query {
                for file in files do
                select (getFileSize file)
            }

        sizesQuery |> Seq.toArray |> Array.map (fun i -> i)


    let failIfArgumentsForInEntriesMergingAreIncorrect (files: string[]) (sizes: int64[]) =
        if files.Length <> sizes.Length then
            failwith "Length of an array of files should be equal an array of file sizes."


    let mergeInEntries (files: string[]) (sizes: int64[]) =
        failIfArgumentsForInEntriesMergingAreIncorrect files sizes
        
        [|0..(files.Length-1)|]
                |> Array.map (fun i -> (files.[i], sizes.[i]))
                |> List.ofArray

    
    let requireExistingDirectory (path: string) =
        if not (Directory.Exists path) then
            failwith $"A directory at path=%s{path} does not exist!"

    
    let getFilesFrom (path: string) =
        requireExistingDirectory path
        Directory.GetFiles path


    let createBarChartItem (color: Color) (name: string, value: int64) = new BarChartItem((takeFileName name), (float) value, color)


    let compare (path: string) (width: int) (color: Color) =
        let barChart = createBarChart width "[underline purple3 bold]Files sizes (bytes)\n[/]"

        requireExistingDirectory(path)

        let files = getFilesFrom (modifyPathToDirectory path)
        let sizes = getFilesSizes files
        let entries = mergeInEntries files sizes |> List.sortBy (fun (_, s) -> -s)
        let barCharItemFactoryFunc = new Func<(string * int64), BarChartItem> (createBarChartItem color)

        AnsiConsole.Render (barChart.AddItems(entries, barCharItemFactoryFunc))