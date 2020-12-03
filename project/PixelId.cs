namespace Iot.Device.RgbXmasTree
{
    public static class PixelId
    {
        public const byte FrontRightTreeBottom = 0;
        public const byte FrontRightTreeMiddle = 1;
        public const byte FrontRightTreeTop = 2;
        public const byte Star = 3;
        public const byte FrontLeftTreeTop = 4;
        public const byte FrontLeftTreeMiddle = 5;
        public const byte FrontLeftTreeBottom = 6;
        public const byte RearRightTreeBottom = 7;
        public const byte RearRightTreeMiddle = 8;
        public const byte RearRightTreeTop = 9;
        public const byte RearLeftTreeTop = 10;
        public const byte RearLeftTreeMiddle = 11;
        public const byte RearLeftTreeBottom = 12;
        public const byte FrontLeftFinTop = 13;
        public const byte FrontLeftFinMiddle = 14;
        public const byte FrontLeftFinBottom = 15;
        public const byte FrontRightFinBottom = 16;
        public const byte FrontRightFinMiddle = 17;
        public const byte FrontRightFinTop = 18;
        public const byte RearRightFinBottom = 19;
        public const byte RearRightFinMiddle = 20;
        public const byte RearRightFinTop = 21;
        public const byte RearLeftFinTop = 22;
        public const byte RearLeftFinMiddle = 23;
        public const byte RearLeftFinBottom = 24;

        public static readonly byte[] AllExStar = {
            FrontRightTreeBottom,
            FrontRightTreeMiddle,
            FrontRightTreeTop,
            FrontLeftTreeTop,
            FrontLeftTreeMiddle,
            FrontLeftTreeBottom,
            RearRightTreeBottom,
            RearRightTreeMiddle,
            RearRightTreeTop,
            RearLeftTreeTop,
            RearLeftTreeMiddle,
            RearLeftTreeBottom,
            FrontLeftFinTop,
            FrontLeftFinMiddle,
            FrontLeftFinBottom,
            FrontRightFinBottom,
            FrontRightFinMiddle,
            FrontRightFinTop,
            RearRightFinBottom,
            RearRightFinMiddle,
            RearRightFinTop,
            RearLeftFinTop,
            RearLeftFinMiddle,
            RearLeftFinBottom
        };

        public static readonly byte[] All = {
            FrontRightTreeBottom,
            FrontRightTreeMiddle,
            FrontRightTreeTop,
            Star,
            FrontLeftTreeTop,
            FrontLeftTreeMiddle,
            FrontLeftTreeBottom,
            RearRightTreeBottom,
            RearRightTreeMiddle,
            RearRightTreeTop,
            RearLeftTreeTop,
            RearLeftTreeMiddle,
            RearLeftTreeBottom,
            FrontLeftFinTop,
            FrontLeftFinMiddle,
            FrontLeftFinBottom,
            FrontRightFinBottom,
            FrontRightFinMiddle,
            FrontRightFinTop,
            RearRightFinBottom,
            RearRightFinMiddle,
            RearRightFinTop,
            RearLeftFinTop,
            RearLeftFinMiddle,
            RearLeftFinBottom
        };
    }
}