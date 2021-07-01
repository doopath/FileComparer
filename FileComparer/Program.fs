namespace FileComparer

module Main =
    open System
    open NLog
    open FileComparer.CompareRequestBuilding


    let consoleLogger = LogManager.GetLogger "FileComparer.Main.Cosole"
    let fileLogger = LogManager.GetLogger "FileComparer.Main.File" 


    let logReceivedArguments (args: string[]) =
        let message = match args.Length with
            | 0 -> "No argumetns were received."
            | _ -> $"Received arguments: %s{String.Join(' ', args)}"
    
        fileLogger.Debug message


    [<EntryPoint>]
    let main args =
        fileLogger.Debug "FilesComparer is starting..."
        logReceivedArguments args

        let compareRequest = CompareRequestBuilder.buildCompareRequest (args |> List.ofArray)
        compareRequest()

        fileLogger.Debug "FilesComparer has finished working.\n\n"
        0