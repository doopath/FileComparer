module Tests.UtilsTests

open NUnit.Framework
open FileComparer.Utils

[<SetUp>]
let setup () = ()

[<Test>]
let takeFirstOfListTest () =
    let expectedFirst = 1
    let testList = [expectedFirst..10]
    let actualFirst = takeFirst testList
    
    Assert.AreEqual(expectedFirst, actualFirst)
    ()
    
    
[<Test>]
let takeFirstOfArrayTest () =
    let expectedFirst = 1
    let testArray = [| expectedFirst..10 |]
    let actualFirst = takeFirst testArray
    
    Assert.AreEqual(expectedFirst, actualFirst)
    ()
