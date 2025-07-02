using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLTrainer
{
    internal class SignatureManager
    {
        public static List<int[]> capitalismSignatures = new List<int[]>();
        public static List<int[]> fuelSignatures = new List<int[]>();
        public static List<int[]> rocketsSignatures = new List<int[]>();
        public static List<int[]> dronePartsSignatures = new List<int[]>();

        public static List<byte[]> capitalismBuffers = new List<byte[]>();
        public static List<byte[]> fuelBuffers = new List<byte[]>();
        public static List<byte[]> rocketsBuffers = new List<byte[]>();
        public static List<byte[]> dronePartsBuffers = new List<byte[]>();

        public static List<byte[]> capitalismNops = new List<byte[]>();
        public static List<byte[]> fuelNops = new List<byte[]>();
        public static List<byte[]> rocketsNops = new List<byte[]>();
        public static List<byte[]> dronePartsNops = new List<byte[]>();

        public static void LoadSignatures ()
        {
            int[] dummy = { 0x01, 0xB3, -1, -1, -1, -1, 0x8B, 0x83, -1, -1, -1, -1, 0x85, 0xC0, 0xB8 };
            capitalismSignatures.Add(dummy);
            dummy = new int[] { 0x8B, 0x80, -1, -1, -1, -1, 0x39, 0x81, -1, -1, -1, -1, 0x7F, 0x19, 0x8B, 0x01 };
            capitalismSignatures.Add(dummy);
            dummy = new int[] { 0x8B, 0x81, -1, -1, -1, -1, 0x39, 0x83, -1, -1, -1, -1, 0x7F, 0x77, 0x8B, 0x83 };
            capitalismSignatures.Add(dummy);
            dummy = new int[] { 0x3B, 0x81, -1, -1, -1, -1, 0x0F, 0x8E, -1, -1, -1, -1, 0xC7, 0x04, 0x24, -1, -1, -1, -1, 0xB9, -1, -1, -1, -1, 0xE8, -1, -1, -1, -1, 0x8D, 0x45, 0xD8, 0xBA, -1, -1, -1, -1, 0x83, 0xEC, 0x04, 0xC7, 0x45, -1, -1, -1, -1, -1, 0xC7, 0x45, -1, -1, -1, -1, -1, 0x89, 0x45, 0xD0, 0x66, 0x89, 0x55, 0xE0, 0xC6, 0x45, 0xE2, 0x6C, 0xC7, 0x45, -1, -1, -1, -1, -1, 0xC6, 0x45, 0xE3, 0x00, 0x8D, 0x45, 0xD0, 0xC7, 0x44, 0x24, -1, -1, -1, -1, -1, 0xC7, 0x44, 0x24, -1, -1, -1, -1, -1, 0xB9, -1, -1, -1, -1, 0x8D, 0x7D, 0xD8, 0x89, 0x04, 0x24, 0xE8 };
            capitalismSignatures.Add(dummy);
            dummy = new int[] { 0x8B, 0xB1, -1, -1, -1, -1, 0xE8, -1, -1, -1, -1, 0x8B, 0x4B, 0x6C, 0xB8, -1, -1, -1, -1, 0x01, 0xD1, 0x83, 0xF9, 0x04, 0x7E, 0x14, 0x89, 0xC8, 0xBA, -1, -1, -1, -1, 0xC1, 0xF9, 0x1F, 0xF7, 0xEA, 0xD1, 0xFA, 0x29, 0xCA, 0x8D, 0x44, 0x92, 0x0F, 0x39, 0xC6, 0x0F, 0x8D, -1, -1, -1, -1, 0x80, 0x7B, 0x41, 0x00, 0x74, 0xA7, 0x8B, 0x4B, 0x70 };
            capitalismSignatures.Add(dummy);

            int[] dummy2 = { 0x83, 0xA9, 0x94, 0x04, 0x00, 0x00, 0x01, 0xC6, 0x81, 0xD8, 0x04, 0x00, 0x00, 0x01, 0x8B, 0x81, -1, -1, -1, -1, 0x85, 0xC0 };
            fuelSignatures.Add(dummy2);

            int[] dummy3 = { 0x29, 0x87, -1, -1, -1, -1, 0x8B, 0x97, -1, -1, -1, -1, 0x83, 0xC3, 0x01, 0x8B, 0x87, -1, -1, -1, -1, 0x29, 0xD0 };
            rocketsSignatures.Add(dummy3);

            int[] dummy4 = { 0x83, 0xAB, 0xCC, 0x01, 0x00, 0x00, 0x01, 0x89, 0xF1, 0xC7, 0x44, 0x24, -1, -1, -1, -1, -1, 0xC7, 0x04, 0x24, -1, -1, -1, -1, 0xFF, 0x50, -1, 0x8B, 0x06 };
            dronePartsSignatures.Add(dummy4);
        }

        public static void LoadNops()
        {
            byte[] cNops = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };
            capitalismNops.Add(cNops);
            capitalismNops.Add(cNops);
            capitalismNops.Add(cNops);
            capitalismNops.Add(cNops);
            capitalismNops.Add(cNops);

            byte[] fNops = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };
            fuelNops.Add(fNops);

            byte[] rNops = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };
            rocketsNops.Add(rNops);

            byte[] dNops = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };
            dronePartsNops.Add(dNops);
        }
    }
}
