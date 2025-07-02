using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace FTLTrainer
{
    public partial class Form1 : Form
    {

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        static Process proc;
        static nint handle;

        static bool isCapitalismTurnedOn = true;
        static bool isFuelTurnedOn = true;
        static bool isRocketsTurnedOn = true;
        static bool isDronePartsTurnedOn = true;

        static List<int> capitalismAddresses = new List<int>();
        static List<int> fuelAddresses = new List<int>();
        static List<int> rocketsAddresses = new List<int>();
        static List<int> dronePartsAddresses = new List<int>();

        private static int FindSignature(int[] signature, nint handle)
        {
            byte[] buffer = new byte[proc.MainModule.ModuleMemorySize];
            int bytesread = 0;

            ReadProcessMemory((int)handle, (int)proc.MainModule.BaseAddress, buffer, proc.MainModule.ModuleMemorySize, ref bytesread);

            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == signature[0])
                {
                    int counter = 0;
                    for (int j = 0; j < signature.Length; j++)
                    {
                        if (buffer[i + j] == signature[j] || signature[j] == -1)
                        {
                            counter++;
                            if (counter == signature.Length)
                            {
                                return i + (int)proc.MainModule.BaseAddress;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return -1;
        }

        public Form1()
        {
            proc = Process.GetProcessesByName("FTLGame").First();
            handle = OpenProcess(0x001F0FFF, false, proc.Id);

            SignatureManager.LoadSignatures();
            SignatureManager.LoadNops();

            int dummy = 0;

            OffsetManager.baseAddress = (int)proc.MainModule.BaseAddress;
            byte[] buffer = new byte[4];
            ReadProcessMemory((int)handle, OffsetManager.baseAddress + OffsetManager.playerDataOffset, buffer, buffer.Length, ref dummy);
            OffsetManager.playerDataAddress = BitConverter.ToInt32(buffer);

            for (int i = 0; i < SignatureManager.capitalismSignatures.Count; i++)
            {
                SignatureManager.capitalismBuffers.Add(new byte[SignatureManager.capitalismNops[i].Length]);
                capitalismAddresses.Add(FindSignature(SignatureManager.capitalismSignatures[i], handle));
                ReadProcessMemory((int)handle, capitalismAddresses[i], SignatureManager.capitalismBuffers[i], SignatureManager.capitalismNops[i].Length, ref dummy);
            }

            for (int i = 0; i < SignatureManager.fuelSignatures.Count; i++)
            {
                SignatureManager.fuelBuffers.Add(new byte[SignatureManager.fuelNops[i].Length]);
                fuelAddresses.Add(FindSignature(SignatureManager.fuelSignatures[i], handle));
                ReadProcessMemory((int)handle, fuelAddresses[i], SignatureManager.fuelBuffers[i], SignatureManager.fuelNops[i].Length, ref dummy);
            }

            for (int i = 0; i < SignatureManager.rocketsSignatures.Count; i++)
            {
                SignatureManager.rocketsBuffers.Add(new byte[SignatureManager.rocketsNops[i].Length]);
                rocketsAddresses.Add(FindSignature(SignatureManager.rocketsSignatures[i], handle));
                ReadProcessMemory((int)handle, rocketsAddresses[i], SignatureManager.rocketsBuffers[i], SignatureManager.rocketsNops[i].Length, ref dummy);
            }

            for (int i = 0; i < SignatureManager.dronePartsSignatures.Count; i++)
            {
                SignatureManager.dronePartsBuffers.Add(new byte[SignatureManager.dronePartsNops[i].Length]);
                dronePartsAddresses.Add(FindSignature(SignatureManager.dronePartsSignatures[i], handle));
                ReadProcessMemory((int)handle, dronePartsAddresses[i], SignatureManager.dronePartsBuffers[i], SignatureManager.dronePartsNops[i].Length, ref dummy);
            }

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int dummy = 0;

            if (isCapitalismTurnedOn)
            {
                button1.Text = "Turn on capitalism";

                for (int i = 0; i < capitalismAddresses.Count; i++)
                {
                    WriteProcessMemory((int)handle, capitalismAddresses[i], SignatureManager.capitalismNops[i], SignatureManager.capitalismNops[i].Length, ref dummy);
                }

                isCapitalismTurnedOn = false;
            }
            else
            {
                button1.Text = "Turn off capitalism";

                for (int i = 0; i < capitalismAddresses.Count; i++)
                {
                    WriteProcessMemory((int)handle, capitalismAddresses[i], SignatureManager.capitalismBuffers[i], SignatureManager.capitalismBuffers[i].Length, ref dummy);
                }

                isCapitalismTurnedOn = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int dummy = 0;

            if (isFuelTurnedOn)
            {
                button2.Text = "Turn on fuel";

                for (int i = 0; i < fuelAddresses.Count; i++)
                {
                    WriteProcessMemory((int)handle, fuelAddresses[i], SignatureManager.fuelNops[i], SignatureManager.fuelNops[i].Length, ref dummy);
                }

                isFuelTurnedOn = false;
            }
            else
            {
                button2.Text = "Turn off fuel";

                for (int i = 0; i < fuelAddresses.Count; i++)
                {
                    WriteProcessMemory((int)handle, fuelAddresses[i], SignatureManager.fuelBuffers[i], SignatureManager.fuelBuffers[i].Length, ref dummy);
                }

                isFuelTurnedOn = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int dummy = 0;

            if (isRocketsTurnedOn)
            {
                button3.Text = "Turn on rockets";

                for (int i = 0; i < rocketsAddresses.Count; i++)
                {
                    WriteProcessMemory((int)handle, rocketsAddresses[i], SignatureManager.rocketsNops[i], SignatureManager.rocketsNops[i].Length, ref dummy);
                }

                isRocketsTurnedOn = false;
            }
            else
            {
                button3.Text = "Turn off rockets";

                for (int i = 0; i < rocketsAddresses.Count; i++)
                {
                    WriteProcessMemory((int)handle, rocketsAddresses[i], SignatureManager.rocketsBuffers[i], SignatureManager.rocketsBuffers[i].Length, ref dummy);
                }

                isRocketsTurnedOn = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int dummy = 0;

            if (isDronePartsTurnedOn)
            {
                button4.Text = "Turn on drone parts";

                for (int i = 0; i < dronePartsAddresses.Count; i++)
                {
                    WriteProcessMemory((int)handle, dronePartsAddresses[i], SignatureManager.dronePartsNops[i], SignatureManager.dronePartsNops[i].Length, ref dummy);
                }

                isDronePartsTurnedOn = false;
            }
            else
            {
                button4.Text = "Turn off drone parts";

                for (int i = 0; i < dronePartsAddresses.Count; i++)
                {
                    WriteProcessMemory((int)handle, dronePartsAddresses[i], SignatureManager.dronePartsBuffers[i], SignatureManager.dronePartsBuffers[i].Length, ref dummy);
                }

                isDronePartsTurnedOn = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int dummy = 0;
            int fuelAddress = OffsetManager.playerDataAddress + OffsetManager.fuelOffset;
            byte[] buffer = new byte[4];
            ReadProcessMemory((int)handle, fuelAddress, buffer, buffer.Length, ref dummy);
            int cFuel = BitConverter.ToInt32(buffer);

            if (textBox1.Text != "")
            {
                int dFuel = int.Parse(textBox1.Text);
                buffer = BitConverter.GetBytes(cFuel + dFuel);
                WriteProcessMemory((int)handle, fuelAddress, buffer, buffer.Length, ref dummy);
                textBox1.Text = "";
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int dummy = 0;
            int scrapAddress = OffsetManager.playerDataAddress + OffsetManager.scrapOffset;
            byte[] buffer = new byte[4];
            ReadProcessMemory((int)handle, scrapAddress, buffer, buffer.Length, ref dummy);
            int cScrap = BitConverter.ToInt32(buffer);

            if (textBox4.Text != "")
            {
                int dScrap = int.Parse(textBox4.Text);
                buffer = BitConverter.GetBytes(cScrap + dScrap);
                WriteProcessMemory((int)handle, scrapAddress, buffer, buffer.Length, ref dummy);
                textBox4.Text = "";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int dummy = 0;
            byte[] buffer1 = new byte[4];
            byte[] buffer2 = new byte[4];
            ReadProcessMemory((int)handle, OffsetManager.playerDataAddress + 0x48, buffer1, buffer1.Length, ref dummy);
            int rocketAddress = BitConverter.ToInt32(buffer1) + 0x1E8;
            ReadProcessMemory((int)handle, rocketAddress, buffer2, buffer2.Length, ref dummy);
            int cRockets = BitConverter.ToInt32(buffer2);

            if (textBox2.Text != "")
            {
                int dRockets = int.Parse(textBox2.Text);
                buffer1 = BitConverter.GetBytes(cRockets + dRockets);
                WriteProcessMemory((int)handle, rocketAddress, buffer1, buffer1.Length, ref dummy);
                textBox2.Text = "";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int dummy = 0;
            byte[] buffer1 = new byte[4];
            byte[] buffer2 = new byte[4];
            byte[] buffer3 = new byte[4];
            ReadProcessMemory((int)handle, OffsetManager.playerDataAddress + 0x4C, buffer1, buffer1.Length, ref dummy);
            int dronesAddress1 = BitConverter.ToInt32(buffer1) + 0x1CC;
            int dronesAddress2 = OffsetManager.playerDataAddress + 0x800;
            ReadProcessMemory((int)handle, dronesAddress1, buffer2, buffer2.Length, ref dummy);
            ReadProcessMemory((int)handle, dronesAddress2, buffer3, buffer3.Length, ref dummy);
            int cDrones1 = BitConverter.ToInt32(buffer2);
            int cDrones2 = BitConverter.ToInt32(buffer3);

            if (textBox3.Text != "")
            {
                int dDrones = int.Parse(textBox3.Text);
                buffer1 = BitConverter.GetBytes(cDrones1 + dDrones);
                WriteProcessMemory((int)handle, dronesAddress1, buffer1, buffer1.Length, ref dummy);
                buffer1 = BitConverter.GetBytes(cDrones2 + dDrones);
                WriteProcessMemory((int)handle, dronesAddress2, buffer1, buffer1.Length, ref dummy);
                textBox3.Text = "";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int dummy = 0;
            byte[] buffer = BitConverter.GetBytes(30);
            WriteProcessMemory((int)handle, OffsetManager.playerDataAddress + OffsetManager.healthOffset, buffer, buffer.Length, ref dummy);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int dummy = 0;
            byte[] buffer = BitConverter.GetBytes(0);
            WriteProcessMemory((int)handle, OffsetManager.playerDataAddress + OffsetManager.healthOffset, buffer, buffer.Length, ref dummy);
        }
    }
}
