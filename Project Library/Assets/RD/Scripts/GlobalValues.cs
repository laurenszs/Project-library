using UnityEngine;

namespace RD.Scripts
{
    public static class GlobalValues
    {
        public static readonly string
            VeryEarly = "Very Early",
            Early = "Early",
            Good = "Good",
            Perfect = "Perfect";

        public static readonly Color
            ColorVeryEarly = new Color32(200, 100, 0, 255),
            ColorEarly = Color.yellow,
            ColorGood = Color.cyan,
            ColorPerfect = Color.green;

        public const float
            ThresholdVeryEarly = .25f,
            ThresholdEarly = .5f,
            ThresholdGood = .75f,
            ThresholdPerfect = .99f;

        public const int BaseScore = 500;
    }
}