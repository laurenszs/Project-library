using UnityEngine;

namespace RD.Scripts
{
    public static class GlobalValues
    {
        public static readonly string
            VeryEarly = "Very Early",
            Early = "Early",
            Good = "Good",
            Great = "Great",
            Perfect = "Perfect";

        public static readonly Color
            ColorVeryEarly = Color.red,
            ColorEarly = new Color32(255, 100, 0, 255),
            ColorGood = Color.yellow,
            ColorGreat = Color.cyan,
            ColorPerfect = Color.green;

        public const float
            ThresholdVeryEarly = .99f,
            ThresholdEarly = .75f,
            ThresholdGood = .5f,
            ThresholdGreat = .25f,
            ThresholdPerfect = .1f;

        public const int BaseScore = 500;
    }
}