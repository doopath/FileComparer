module FileComparer.Colors

open System.Collections.Generic
open Spectre.Console
open FileComparer.Exceptions


let purple = KeyValuePair<string, Color>("purple", Color.Purple)

let blue = KeyValuePair<string, Color>("blue", Color.Blue)

let red = KeyValuePair<string, Color>("red", Color.Red)

let yellow = KeyValuePair<string, Color>("yellow", Color.Yellow)

let white = KeyValuePair<string, Color>("white", Color.White)

let black = KeyValuePair<string, Color>("black", Color.Black)

let green = KeyValuePair<string, Color>("green", Color.Green)


let colorsMap =
    Dictionary<string, Color>(
        [ purple
          blue
          red
          yellow
          white
          black
          green ]
    )


let getColor key =
    match colorsMap.ContainsKey key with
    | true -> colorsMap.[key]
    | false -> raise (BadColorTypeException $"FileComparer does not support this color (%s{key})")
