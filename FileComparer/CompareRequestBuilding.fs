namespace FileComparer.CompareRequestBuilding

module CompareRequestBuilder =
    open System.IO
    open FileComparer.Utils
    open FileComparer.Comparing
    open FileComparer.Colors


    let skipItem (i: int) (l: 'a list) = l.[1+i..] |> List.append l.[..i-1]

    let findArgPointerIndex (pn: 'a) (args: 'a list) = args |> List.tryFindIndex ((=) pn)
        

    let findArgWithPointer (pn: 'a) (def: 'a) (args: 'a list) =
        match (args |> List.tryFindIndex ((=) pn)) with
        | None -> def
        | i -> args.[i.Value + 1]


    let takeArgument (ap: 'a) (def: 'a) (args: 'a list) =
        let arg = args |> findArgWithPointer ap def

        if (args |> List.contains ap) then
            let pointerIndex = (args |> findArgPointerIndex ap).Value

            // After skipping an argument at index=pointerIndex next one (in the list) will get the same index.
            // [1; (2); 3; 4] |> skipItem 1 => [1; (3); 4]
            let newArgs = args |> skipItem pointerIndex |> skipItem pointerIndex
            (newArgs, arg)
        else
            (args, arg)


    let takePath (def: string) (args: 'a list) =
        printfn "takePath: %A" args
        let existingDirs = args |> List.where (fun arg -> Directory.Exists (arg.ToString()))
        let path =
            match existingDirs.Length with
            |0 -> def
            |_ -> Util.takeFirst existingDirs

        if args |> List.contains path then
            let selectedArgIndex = args |> List.findIndex ((=) path)
            let newArgs = args |> skipItem selectedArgIndex
            (newArgs, path)
        else
            (args, path)


    let takeColor (def: string) (args: string list) =
        printfn "takeColor: %A" args
        args |> takeArgument "-c" def

    let takeWidth (def: string) (args: string list) =
        printfn "takeWidth: %A" args
        args |> takeArgument "-w" def


    let buildCompareRequest (args: string list) =
        let pathArgsPair = args |> takePath "./"
        let colorArgsPair = (fst pathArgsPair) |> takeColor "purple"
        let widthArgsPair = (fst colorArgsPair) |> takeWidth "100"

        printfn "After all: %A" (fst widthArgsPair)

        let path = snd pathArgsPair
        let width = int (snd widthArgsPair)
        let color = Colors.colorsMap.[snd colorArgsPair]

        (fun () -> Comparer.compare path width color)
