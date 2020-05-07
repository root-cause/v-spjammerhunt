using System;
using GTA.Math;

namespace JammerHunt
{
    public static class Constants
    {
        public const int MaxJammers = 50;
        public const int MaxCarGens = 3;
        public const float MaxJammerDistance = 250.0f * 250.0f;

        public const int LocationUpdateInterval = 1000;
        public const int HighlightTime = 7000;

        public const int DefaultJammerDestroyedReward = 2000;
        public const int DefaultAllDestroyedReward = 50000;
        public const bool DefaultBlipsEnabled = false;

        public static readonly Tuple<Vector3, Vector3>[] JammerLocations =
        {
            Tuple.Create(new Vector3(1006.372f, -2881.68f, 30.422f), new Vector3(0f, 0f, 0f)),
            Tuple.Create(new Vector3(-980.242f, -2637.703f, 88.528f), new Vector3(0f, 0f, 0.6f)),
            Tuple.Create(new Vector3(-688.195f, -1399.329f, 23.331f), new Vector3(0f, 0f, -85.6f)),
            Tuple.Create(new Vector3(1120.696f, -1539.165f, 54.871f), new Vector3(0f, 0f, 1.599f)),
            Tuple.Create(new Vector3(2455.134f, -382.585f, 112.635f), new Vector3(0f, 0f, 0.799f)),
            Tuple.Create(new Vector3(793.878f, -717.299f, 48.083f), new Vector3(0f, 0f, -45.6f)),
            Tuple.Create(new Vector3(-168.3f, -590.153f, 210.936f), new Vector3(0f, 0f, -17.6f)),
            Tuple.Create(new Vector3(-1298.343f, -435.8369f, 108.129f), new Vector3(0f, 0f, 34.8f)),
            Tuple.Create(new Vector3(-2276.484f, 335.0941f, 195.723f), new Vector3(0f, 0f, 119.399f)),
            Tuple.Create(new Vector3(-667.25f, 228.545f, 154.051f), new Vector3(0f, 0f, 0.199f)),
            Tuple.Create(new Vector3(682.561f, 567.5302f, 153.895f), new Vector3(0f, 0f, 69.999f)),
            Tuple.Create(new Vector3(2722.561f, 1538.103f, 85.202f), new Vector3(0f, 0f, 86.599f)),
            Tuple.Create(new Vector3(758.539f, 1273.687f, 445.181f), new Vector3(0f, 0f, 106.999f)),
            Tuple.Create(new Vector3(-3079.258f, 768.5189f, 31.569f), new Vector3(0f, 0f, -51.202f)),
            Tuple.Create(new Vector3(-2359.338f, 3246.831f, 104.188f), new Vector3(0f, 0f, 60.998f)),
            Tuple.Create(new Vector3(1693.732f, 2656.602f, 60.84f), new Vector3(0f, 0f, 179.997f)),
            Tuple.Create(new Vector3(3555.018f, 3684.98f, 61.27f), new Vector3(0f, 0f, 1.597f)),
            Tuple.Create(new Vector3(1869.022f, 3714.435f, 117.068f), new Vector3(0f, 0f, -60.803f)),
            Tuple.Create(new Vector3(2902.552f, 4324.699f, 101.106f), new Vector3(0f, 0f, -63.004f)),
            Tuple.Create(new Vector3(-508.6141f, 4426.661f, 87.511f), new Vector3(0f, 0f, -172.204f)),
            Tuple.Create(new Vector3(-104.417f, 6227.278f, 63.696f), new Vector3(0f, 0f, 33.196f)),
            Tuple.Create(new Vector3(1607.501f, 6437.315f, 32.162f), new Vector3(0f, 0f, 25.796f)),
            Tuple.Create(new Vector3(2792.933f, 5993.922f, 366.867f), new Vector3(0f, 0f, 99.396f)),
            Tuple.Create(new Vector3(1720.613f, 4822.467f, 59.7f), new Vector3(0f, 0f, 178.396f)),
            Tuple.Create(new Vector3(-1661.01f, -1126.742f, 29.773f), new Vector3(0f, 0f, 178.396f)),
            Tuple.Create(new Vector3(-1873.49f, 2058.357f, 154.407f), new Vector3(0f, 0f, -110.204f)),
            Tuple.Create(new Vector3(2122.46f, 1750.886f, 138.114f), new Vector3(0f, 0f, -3.005f)),
            Tuple.Create(new Vector3(-417.424f, 1153.143f, 339.128f), new Vector3(0f, 0f, 63.595f)),
            Tuple.Create(new Vector3(3303.901f, 5169.792f, 28.735f), new Vector3(0f, 0f, 51.999f)),
            Tuple.Create(new Vector3(-1005.848f, 4852.147f, 302.025f), new Vector3(0f, 1.562f, -20.0009f)),
            Tuple.Create(new Vector3(-306.627f, 2824.859f, 69.512f), new Vector3(0f, 0f, -125.001f)),
            Tuple.Create(new Vector3(1660.663f, -28.07f, 179.137f), new Vector3(0f, 0f, 14.399f)),
            Tuple.Create(new Vector3(754.647f, 2584.067f, 133.904f), new Vector3(0f, 0f, -83.002f)),
            Tuple.Create(new Vector3(-279.9081f, -1915.608f, 54.173f), new Vector3(0f, 0f, -130.202f)),
            Tuple.Create(new Vector3(-260.4421f, -2411.807f, 126.019f), new Vector3(0f, 0f, -125.202f)),
            Tuple.Create(new Vector3(552.132f, -2221.853f, 73f), new Vector3(0f, 0f, 86.598f)),
            Tuple.Create(new Vector3(394.3919f, -1402.144f, 76.267f), new Vector3(0f, 0f, -130.603f)),
            Tuple.Create(new Vector3(1609.791f, -2243.767f, 130.187f), new Vector3(0f, 0f, 2.797f)),
            Tuple.Create(new Vector3(234.2919f, 220.771f, 168.981f), new Vector3(0f, 0f, -19.6f)),
            Tuple.Create(new Vector3(-1237.121f, -850.4969f, 82.98f), new Vector3(0f, 0f, 123.4f)),
            Tuple.Create(new Vector3(-1272.732f, 317.9532f, 90.352f), new Vector3(0f, 0f, 61.4f)),
            Tuple.Create(new Vector3(0.088f, -1002.404f, 96.32f), new Vector3(0f, 0f, 68.999f)),
            Tuple.Create(new Vector3(470.5569f, -105.049f, 135.908f), new Vector3(0f, 0f, 162.599f)),
            Tuple.Create(new Vector3(-548.5471f, -197.9911f, 82.813f), new Vector3(0f, 0f, 119.399f)),
            Tuple.Create(new Vector3(2581.047f, 461.9421f, 115.095f), new Vector3(0f, 0f, 92.399f)),
            Tuple.Create(new Vector3(720.14f, 4097.634f, 38.075f), new Vector3(0f, 68.2f, 179.398f)),
            Tuple.Create(new Vector3(1242.471f, 1876.068f, 92.242f), new Vector3(0f, 0f, 39.2f)),
            Tuple.Create(new Vector3(2752.113f, 3472.779f, 67.911f), new Vector3(0f, 0f, 155.5f)),
            Tuple.Create(new Vector3(-2191.856f, 4292.408f, 55.013f), new Vector3(0f, 0f, -31.9998f)),
            Tuple.Create(new Vector3(450.475f, 5581.514f, 794.0683f), new Vector3(0f, 0f, -89.8f))
        };

        public static readonly Tuple<Vector3, float>[] ThrusterLocations =
        {
            Tuple.Create(new Vector3(1753.62f, 3323.0f, 41.04f), 210.0f),
            Tuple.Create(new Vector3(2025.72f, 4726.97f, 41.4855f), 27.0f),
            Tuple.Create(new Vector3(-947.808f, -3530.81f, 13.9186f), 24.0f)
        };
    }
}
