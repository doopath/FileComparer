namespace FileComparer

module Main =
    open System
    open NLog
    open Spectre.Console
    open FileComparer.Comparing


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

        let path = Console.ReadLine()
        Comparer.compare path 100 Color.Purple3

        fileLogger.Debug "FilesComparer has finished working.\n\n"
        0