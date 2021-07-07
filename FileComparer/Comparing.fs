module FileComparer.Comparing
    open System
    open System.IO
    open Spectre.Console
    open FileComparer.Utils
    open FileComparer.Exceptions


    let mapBytesAs (sizeFormat: string) (number: int64) =
        match sizeFormat.ToLower() with
        | "bytes" -> number
        | "kib" -> number / 1024L
        | "mib" -> number / 1024L / 1024L
        | "gib" -> number / 1024L / 1024L / 1024L
        | "kb" -> number / 1000L
        | "mb" -> number / 1000L / 1000L
        | "gb" -> number / 1000L / 1000L / 1000L
        | unsupported -> raise (BadSizeFormatTypeException $"No size format named %s{unsupported}")


    let isValidPathToDirectory (path: string) = path.EndsWith "/"  || path.EndsWith @"\" 

    let toUnixPath (path: string) = path.Replace(@"\", "/")
    
    let getFileSize (path: string) = (FileInfo path).Length
        

    let setBarChartWidth (width: int) (item: BarChart) =
        item.Width <- width
        item

    let setBarChartLabel (label: string) (item: BarChart) =
        item.Label <- label
        item


    let createBarChart (width: int) (label: string) = new BarChart() |> setBarChartWidth width |> setBarChartLabel label
        

    let modifyDirectoryPath (path: string) =
        match path.EndsWith "/" || path.EndsWith @"\" with
        | true -> toUnixPath path
        | false -> toUnixPath (path + "/")


    let takeFileName (path: string) =
        (toUnixPath path).Split("/")
        |> List.ofArray
        |> List.rev
        |> takeFirst
        
        
    let fileSizesQuery (sizeFormat: string) (files: string list) =
        query {
            for file in files do
            select  (file |> getFileSize |> mapBytesAs sizeFormat)
        }
        
        
        
    let getFilesSizes (sizeFormat: string) (files: string list) =
        fileSizesQuery sizeFormat files |> List.ofSeq
            

    let failIfArgumentsForInEntriesMergingAreIncorrect (files: string list) (sizes: int64 list) =
        if files.Length <> sizes.Length then
            failwith "Length of an array of files should be equal an array of file sizes."


    let mergeInEntries (files: string list) (sizes: int64 list) =
        failIfArgumentsForInEntriesMergingAreIncorrect files sizes
        
        [|0..(files.Length-1)|]
            |> Array.map (fun i -> (files.[i], sizes.[i]))
            |> List.ofArray

    
    let requireExistingDirectory (path: string) =
        if not (Directory.Exists path) then
            failwith $"A directory at path=%s{path} does not exist!"

    
    let getFilesFrom (path: string) =
        requireExistingDirectory path
        Directory.GetFiles path |> List.ofArray
        
        
    let getFileNames (width: int) (files: string list) =
        // width is a width of the BarChart. Then the BarChart will
        // be rendered it will be split by two columns: file names and file sizes.
        // So, file name will have width=(BarChart's width / 2 - 1).
        // "- 3" is length of "...".
        let maxNameWidth = int (width / 2) - 1
        let modifiedNameWidth = maxNameWidth - 3
        let getFileName (file: string) =
            if file.Length > maxNameWidth then
                file.[..modifiedNameWidth+1] + "..."
            else
                file
                
        files |> List.map getFileName


    let createBarChartItem (color: Color) (name: string, value: int64) =
        BarChartItem((takeFileName name), (float) value, color)
        
        
    
    let fillBarChart (entries: ('a * 'b) list) (barChartFactory: Func<('a * 'b), BarChartItem>) (barChart: BarChart) =
        barChart.AddItems(entries, barChartFactory)
        
        
    let renderBarChart message (barChart: BarChart) =
        match barChart.Data.Count with
        | 0 -> AnsiConsole.Markup message
        | _ -> AnsiConsole.Render barChart


    let compare (path: string) (width: int) (color: Color) (sizeFormat: string) =
        // This message will be shown if a target directory does not contain any files.
        let message = $"[italic bold %s{color.ToMarkup()}]There are no files at path=%s{path}\n[/]"
        let barChart = createBarChart width $"[underline {color.ToMarkup()} bold]File sizes ({sizeFormat})\n[/]"

        requireExistingDirectory path

        let files = getFilesFrom (modifyDirectoryPath path)
        let sizes = getFilesSizes sizeFormat files
        let fileNames = getFileNames width files 
        let entries = mergeInEntries fileNames sizes |> List.sortBy (fun (_, s) -> -s)
        let barCharItemFactoryFunc = Func<(string * int64), BarChartItem>(createBarChartItem color)

        barChart |> fillBarChart entries barCharItemFactoryFunc |> renderBarChart message
