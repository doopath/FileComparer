module FileComparer.Colors
    open System.Collections.Generic
    open Spectre.Console


    let purple = new KeyValuePair<string, Color>("purple", Color.Purple)
    let blue = new KeyValuePair<string, Color>("blue", Color.Blue)
    let red = KeyValuePair<string, Color>("red", Color.Red)
    let yellow = KeyValuePair<string, Color>("yellow", Color.Yellow)
    let white = KeyValuePair<string, Color>("white", Color.White)
    let black = KeyValuePair<string, Color>("black", Color.Black)
    let green = KeyValuePair<string, Color>("green", Color.Green)


    let colorsMap = new Dictionary<string, Color>([
        purple;
        blue;
        red;
        yellow;
        white;
        black;
        green;
    ])