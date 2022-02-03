using System;
using System.Text;

namespace CryptoEncodeAlgorythm
{
    public static class Sha1
    {
        private static uint LeftRot(uint Value, int move)
        {
            return ((Value << move) | (Value >> (32 - move)));
        }

        // W
        private static uint[] ModifyChunk(uint[] nums, byte[] messageInBytes)
        {
            for (int i = 0; i < 16; i++)
                nums[i] = (uint)((messageInBytes[i * 4] << 24) + (messageInBytes[i * 4 + 1] << 16) 
                    + (messageInBytes[i * 4 + 2] << 8) + messageInBytes[i * 4 + 3]);
            for (int i = 16; i < 80; i++)
                nums[i] = LeftRot(nums[i - 3] ^ nums[i - 8] ^ nums[i - 14] ^ nums[i - 16], 1);
            return nums;
        }

        private static byte[] LastBlockPadding(byte[] messageInBytes, ulong Size, int SingleBitPos)
        {
            messageInBytes[SingleBitPos] = 1 << 7;
            // fill reserved 64 bits
            for (int i = 0; i < 8; i++)
            {
                //  Obtain the 2-word representation of l, the number of bits in the original message.
                //  If l< 2 ^ 32 then the first word is all zeroes. Append these two words to the padded message.
                messageInBytes[messageInBytes.Length - 1 - i] = (byte)Size;
                Size >>= 8;
            }
            return messageInBytes;
        }

        private static void AlgorythmCycle(ref uint[] hashedNames, ref uint a, ref uint b, ref uint c, ref uint d, ref uint e)
        {
            uint f = 0, k = 0, temp;
            for (int i = 0; i < 80; i++)
            {
                if (i < 20)
                {
                    f = (b & c) | ((~b) & d);
                    k = 0x5A827999;
                }
                else if (i < 40)
                {
                    f = b ^ c ^ d;
                    k = 0x6ED9EBA1;
                }
                else if (i < 60)
                {
                    f = (b & c) | (b & d) | (c & d);
                    k = 0x8F1BBCDC;
                }
                else
                {
                    f = b ^ c ^ d;
                    k = 0xCA62C1D6;
                }
                temp = LeftRot(a, 5) + f + e + k + hashedNames[i];
                e = d;
                d = c;
                c = LeftRot(b, 30);
                b = a;
                a = temp;
            }
        }

        public static uint[] CreateSHA1Text(string input)
        {
            //    uint h0 = 0x67452301, h1 = 0xEFCDAB89, h2 = 0x98BADCFE, h3 = 0x10325476, h4 = 0xC3D2E1F0, 
            uint a, b, c, d, e;

            // Initialize Hs
            uint h0 = 0x67452301;
            uint h1 = 0xEFCDAB89;
            uint h2 = 0x98BADCFE;
            uint h3 = 0x10325476;
            uint h4 = 0xC3D2E1F0;

            // Length of input message in bits
            ulong messLength = (ulong)input.Length * 8;

            //message processing
            var wholeText = Encoding.UTF8.GetBytes(input);
            var numOfChunks = wholeText.Length / 64 + 1;
            int index = 0;
            int lengthToCopy = 64;
            for (int i = 0; i < numOfChunks; i++)
            {
                if (i == numOfChunks - 1)
                    lengthToCopy = wholeText.Length % 64;

                byte[] messageInBytes = new byte[64];
                uint[] hashedNames = new uint[80];
                Array.Copy(wholeText, index, messageInBytes, 0, lengthToCopy);
                if (!(lengthToCopy * 8 > 448))
                {
                    messageInBytes = LastBlockPadding(messageInBytes, (ulong)input.Length * 8, lengthToCopy);
                }
                hashedNames = ModifyChunk(hashedNames, messageInBytes);
                a = h0;
                b = h1;
                c = h2;
                d = h3;
                e = h4;
                AlgorythmCycle(ref hashedNames, ref a, ref b, ref c, ref d, ref e);
                h0 += a; h1 += b; h2 += c; h3 += d; h4 += e;
                index += 64;
            }

            return new uint[] { h0, h1, h2, h3, h4 };
        }
    }
}