module FileComparer.Main
    open System
    open NLog
    open FileComparer.CompareRequestBuilding
    open FileComparer.Exceptions


    let consoleLogger = LogManager.GetLogger "FileComparer.Main.Cosole"
    let fileLogger = LogManager.GetLogger "FileComparer.Main.File" 


    let logReceivedArguments (args: string[]) =
        let message =
            match args.Length with
            | 0 -> "No arguments were received."
            | _ -> $"Received arguments: %s{String.Join(' ', args)}"
    
        fileLogger.Debug message
        
        
    let logErrorEverywhere (level: LogLevel) (error: 'a) =
        fileLogger.Log(level, error)
        consoleLogger.Log(level, error)


    [<EntryPoint>]
    let main args =
        fileLogger.Debug "FilesComparer is starting..."
        logReceivedArguments args

        try
            let compareRequest = buildCompareRequest (List.ofArray args)
            compareRequest()
        with
        | BadColorType exc | BadSizeFormatType exc -> logErrorEverywhere LogLevel.Error exc
        | error -> logErrorEverywhere LogLevel.Error error

        fileLogger.Debug "FilesComparer has finished working.\n\n"
        0