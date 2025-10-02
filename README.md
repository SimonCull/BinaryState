# BinaryState
An experiment into storing collections of binary states in 64-bit integers.

## Why?
The concept came up when discussing infinite loop detection - how was most efficient to store and compare states when deciding if an infinite loop was occuring - and it felt like a fun techinical investigation and an opportunity to play with bitwise operators.

## Was it worth it?
It was fun, and I learned a lot about optimisation, data structures, and benchmarking.

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
| Method | Mean | Error | StdDev
| :----------------------   | --------------:   | -----------:  | ----------:
| List_Compare | 1,437.5 us | 15.55 us | 12.98 us
| Dictionary_Compare | 10,067.3 us | 119.98 us | 106.36 us
| StringBuilder_Compare | 4,883.3 us | 33.67 us | 26.29 us
| Array_Compare | 956.5 us | 2.99 us | 2.65 us
| States_Compare | 331.0 us | 5.81 us | 8.33 us 
