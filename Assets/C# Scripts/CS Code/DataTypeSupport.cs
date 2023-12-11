using System;

namespace NeuralNetworks
{
    public static class AcceptableSetupTypes
    {
        public readonly static Type[] acceptableTypes = new Type[12]
        {
            typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint),
            typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal), typeof(Func<double>)
        };

        public readonly static Type[] acceptableTypes1D = new Type[12]
        {
            typeof(sbyte[]), typeof(byte[]), typeof(short[]), typeof(ushort[]), typeof(int[]), typeof(uint[]),
            typeof(long[]), typeof(ulong[]), typeof(float[]), typeof(double[]), typeof(decimal[]), typeof(Func<double>[])
        };

        public readonly static Type[] acceptableTypesJagged2D = new Type[12]
        {
            typeof(sbyte[][]), typeof(byte[][]), typeof(short[][]), typeof(ushort[][]), typeof(int[][]), typeof(uint[][]),
            typeof(long[][]), typeof(ulong[][]), typeof(float[][]), typeof(double[][]), typeof(decimal[][]), typeof(Func<double>[][])
        };

        public readonly static Type[] acceptableTypes3D = new Type[12]
        {
            typeof(sbyte[][][]), typeof(byte[][][]), typeof(short[][][]), typeof(ushort[][][]), typeof(int[][][]), typeof(uint[][][]),
            typeof(long[][][]), typeof(ulong[][][]), typeof(float[][][]), typeof(double[][][]), typeof(decimal[][][]), typeof(Func<double>[][][])
        };

        public readonly static Type[] acceptableTypes2D = new Type[11]
        {
            typeof(sbyte[,]), typeof(byte[,]), typeof(short[,]), typeof(ushort[,]), typeof(int[,]), typeof(uint[,]),
            typeof(long[,]), typeof(ulong[,]), typeof(float[,]), typeof(double[,]), typeof(decimal[,])
        };
    }
}