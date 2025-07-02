using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLTrainer
{
    internal class OffsetManager
    {
        public static int baseAddress;
        public static int playerDataAddress;

        public const int playerDataOffset = 0x51348C;

        public const int scrapOffset = 0x4D4;
        public const int fuelOffset = 0x494;

        public const int healthOffset = 0xCC;
    }
}
