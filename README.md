# BinaryState
An experiment into storing collections of binary states in 64-bit integers.

## Why?
The concept came up when discussing infinite loop detection - how was most efficient to store and compare states when deciding if an infinite loop was occuring - and it felt like a fun techinical investigation and an opportunity to play with bitwise operators.

## Was it worth it?
It was fun, and I learned a lot about optimisation, data structures, and benchmarking.

## What came out of it
### State.cs 
[link]()
This is a wrapper for a `ulong` that provides accessors to each position in the binary representation of the number.

### States.cs
[link]()
This is a wrapper for a State[] that provides methods for accessing each state in the collection.

## Does it work?
Surprisingly yes, I compared setting a specific index, getting a specific index, and comparing two identical objects, and while the getting and setting were slower than (most of) the alternatives, when it comes to comparison it blew the others out of the water.

### Test Data
The test data contains 1,000,000 randomly boolean values which is then stored in various formats:
* List<bool>
* Dictionary<int,bool>
* StringBuilder
* bool[]
* States - the result of this project
  
There is also a target index which is the target for both Get and Set tests (initially 500,000, now random from the range).

### Set Item
Sets a specific index to `true`
| Method | Mean | Error | StdDev
| :----------------------   | --------------:   | -----------:  | ----------:
| List_SetItem | 1.3268 ns | 0.0379 ns | 0.0317 ns
| Dictionary_SetItem | 4.6041 ns | 0.0864 ns | 0.0766 ns
| StringBuilder_SetItem | 2,994.7222 ns | 10.5633 ns | 9.8809 ns
| Array_SetItem | 0.0104 ns | 0.0048 ns | 0.0040 ns
| States_SetItem | 14.8301 ns | 0.1044 ns | 0.0872 ns

### Get Item
Gets a specific index
| Method | Mean | Error | StdDev
| :----------------------   | --------------:   | -----------:  | ----------:
| List_GetItem | 0.3262 ns | 0.0337 ns | 0.0315 ns
| Dictionary_GetItem | 4.4549 ns | 0.0438 ns | 0.0365 ns
| StringBuilder_GetItem | 2,974.3999 ns | 13.7274 ns | 12.1690 ns
| Array_GetItem | 0.0586 ns | 0.0149 ns | 0.0139 ns
| States_GetItem | 14.2449 ns | 0.0456 ns | 0.0426 ns

### Compare
Compares two separate but identical collections
| Method | Mean | Error | StdDev |
| ----------------------  | --------------:   | -----------:  | ----------:
| List_Compare | 1,216.24 us | 18.215 us | 17.890 us |
| Dictionary_Compare | 10,520.83 us | 193.107 us | 322.639 us |
| StringBuilder_Compare | 4,989.88 us | 96.439 us | 103.189 us |
| Array_Compare | 1,227.57 us | 23.660 us | 30.764 us |
| States_Compare | 54.02 us | 1.205 us | 3.476 us |
