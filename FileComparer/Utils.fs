module FileComparer.Utils
    open System.Collections.Generic


    let takeFirst (l: 'a IEnumerable) =
        let enum = l.GetEnumerator()

        match enum.MoveNext() with
        | true -> enum.Current
        | false -> failwith "Cannot get first element of an empty IEnumerable object."