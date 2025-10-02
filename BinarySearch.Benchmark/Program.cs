using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BinarySearch.Benchmark;


BenchmarkRunner.Run(
    [
        typeof(StatesBenchmarks_SetItem), 
        typeof(StatesBenchmarks_GetItem), 
        typeof(StatesBenchmarks_Compare)
    ]);

//BenchmarkRunner.Run(
//    [
//        //typeof(StatesBenchmarks_SetItem), 
//        //typeof(StatesBenchmarks_GetItem), 
//        //typeof(StatesBenchmarks_Compare)
//    ], DefaultConfig.Instance.WithOptions(ConfigOptions.DisableOptimizationsValidator));


//| Method | Mean | Error | StdDev |
//| ----------------------  | --------------:   | -----------:  | ----------: 
//| List_SetItem | 1.3268 ns | 0.0379 ns | 0.0317 ns
//| Dictionary_SetItem | 4.6041 ns | 0.0864 ns | 0.0766 ns
//| StringBuilder_SetItem | 2,994.7222 ns | 10.5633 ns | 9.8809 ns
//| Array_SetItem | 0.0104 ns | 0.0048 ns | 0.0040 ns
//| States_SetItem | 14.8301 ns | 0.1044 ns | 0.0872 ns

//| Method | Mean | Error | StdDev |
//| ----------------------  | --------------:   | -----------:  | ----------:
//| List_GetItem | 0.3262 ns | 0.0337 ns | 0.0315 ns
//| Dictionary_GetItem | 4.4549 ns | 0.0438 ns | 0.0365 ns
//| StringBuilder_GetItem | 2,974.3999 ns | 13.7274 ns | 12.1690 ns
//| Array_GetItem | 0.0586 ns | 0.0149 ns | 0.0139 ns
//| States_GetItem | 14.2449 ns | 0.0456 ns | 0.0426 ns

//| Method | Mean | Error | StdDev |
//| ----------------------  | --------------:   | -----------:  | ----------:
//| List_Compare | 1,216.24 us | 18.215 us | 17.890 us |
//| Dictionary_Compare | 10,520.83 us | 193.107 us | 322.639 us |
//| StringBuilder_Compare | 4,989.88 us | 96.439 us | 103.189 us |
//| Array_Compare | 1,227.57 us | 23.660 us | 30.764 us |
//| States_Compare | 54.02 us | 1.205 us | 3.476 us |