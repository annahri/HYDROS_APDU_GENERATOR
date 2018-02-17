using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYDROS_APDU_GENERATOR
{
    static class GetData
    {
        public static List<Byte> Payload = new List<byte>();
        public static Byte[] NomTotem = new Byte[3];
        public static Byte CodeFonction;
        public static Byte[] NumPaquet = new Byte[2];
        public static Byte[,] Donnees = new Byte[4,4];

        public static List<Byte> Populate(List<Byte> list)
        {
            list.AddRange(NomTotem);
            list.Add(CodeFonction);
            list.AddRange(NumPaquet);
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    list.Add(Donnees[i, j]);
                }
            }
            return list;
        }

        public static string Output()
        {
            Payload = Populate(Payload);
            var hex = BitConverter.ToString(Payload.ToArray());
            return hex.Replace("-", " ");
        }

        public static void Clear()
        {
            Payload = new List<byte>();
            NomTotem = new Byte[3];
            CodeFonction = new Byte();
            NumPaquet = new Byte[2];
            Donnees = new Byte[4, 4];
        }


        public static Byte[] Get2Byte(int number)
        {
            var _2byte = new Byte[2];

            _2byte[0] = (byte)((number >> 8) & 0xFF);
            _2byte[1] = (byte)(number & 0xFF);

            return _2byte;
        }  

        public static Byte[] GetByte(float number)
        {
            return BitConverter.GetBytes(number);
        }
    }
}
