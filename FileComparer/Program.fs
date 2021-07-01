module FileComparer.Main
    open System
    open NLog
    open FileComparer.CompareRequestBuilding


    let consoleLogger = LogManager.GetLogger "FileComparer.Main.Cosole"
    let fileLogger = LogManager.GetLogger "FileComparer.Main.File" 


    let logReceivedArguments (args: string[]) =
        let message =
            match args.Length with
            | 0 -> "No arguments were received."
            | _ -> $"Received arguments: %s{String.Join(' ', args)}"
    
        fileLogger.Debug message
        
        
    let logErrorEverywhere (level: LogLevel) (error: Exception) =
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
        | error -> logErrorEverywhere LogLevel.Error error

        fileLogger.Debug "FilesComparer has finished working.\n\n"
        0