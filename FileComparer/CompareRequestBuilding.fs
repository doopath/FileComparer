module FileComparer.CompareRequestBuilding

open System
open System.IO
open FileComparer.Utils
open FileComparer.Comparing
open FileComparer.Colors
open FileComparer.Exceptions


let skipItem (i: int) (l: 'a list) = l.[1 + i..] |> List.append l.[..i - 1]

let findArgPointerIndex (pn: 'a) (args: 'a list) = args |> List.tryFindIndex ((=) pn)


let findArgWithPointer (pn: 'a) (def: 'a) (args: 'a list) =
    match (args |> List.tryFindIndex ((=) pn)) with
    | None -> def
    | i -> args.[i.Value + 1]


let takeArgument (ap: 'a) (def: 'a) (args: 'a list) =
    let arg = args |> findArgWithPointer ap def

    if (args |> List.contains ap) then
        let pointerIndex = (args |> findArgPointerIndex ap).Value
        let newArgs =
            args
            |> skipItem pointerIndex
            |> skipItem pointerIndex

        (newArgs, arg)
    else
        (args, arg)


let takePath (def: string) (args: string list) =
    let existingDirs =
        args
        |> List.where (fun arg -> Directory.Exists(arg.ToString()))

    let path =
        match existingDirs.Length with
        | 0 -> def
        | _ -> takeFirst existingDirs

    if args |> List.contains path then
        let selectedArgIndex = args |> List.findIndex ((=) path)
        let newArgs = args |> skipItem selectedArgIndex
        (newArgs, path)
    else
        (args, path)


let takeColor (def: string) (args: string list) = args |> takeArgument "-c" def

let takeWidth (def: string) (args: string list) = args |> takeArgument "-w" def

let takeSizeFormat (def: string) (args: string list) = args |> takeArgument "-sf" def


let buildCompareRequest (args: string list) =
    let pathArgsPair = args |> takePath "./"
    let colorArgsPair = fst pathArgsPair |> takeColor "purple"
    let widthArgsPair = fst colorArgsPair |> takeWidth "100"

    let sizeFormatArgsPair =
        fst widthArgsPair |> takeSizeFormat "bytes"

    try
        let path = snd pathArgsPair
        let color = getColor (snd colorArgsPair)
        let sizeFormat = snd sizeFormatArgsPair
        let width = int (snd widthArgsPair)

        (fun () -> compare path width color sizeFormat)
    with
    | :? OverflowException as exc -> raise (UnrealConvertingException exc.Message)
    | other -> failwith other.Message
