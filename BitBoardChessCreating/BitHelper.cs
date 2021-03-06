﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitBoardChessCreating
{
    class BitHelper
    {
        static int[] table = new int[]{0, 9, 1, 10, 13, 21, 2, 29, 11, 14, 16, 18, 22, 25, 3, 30,
                8, 12, 20, 28, 15, 17, 24, 7, 19, 27, 23, 6, 26, 5, 4, 31};
        public static int getNumberOfTrailingZeros(ulong x)
        
        {
            const ulong debruijn64 = 0x03f79d71b4cb0a89;
            if (x == 0) return 64;
            return index64[((x ^ (x - 1)) * debruijn64) >> 58];
            //UInt32 First32Bit, Last32Bit;
            //First32Bit = (UInt32)(x & 0xffffffff);
            //Last32Bit = (UInt32)(x >> 32) & (0xffffffff);
            //if (First32Bit == 0)
            //    return 32 + getNumberOfTrailingZerosInt32(Last32Bit);
            //else return getNumberOfTrailingZerosInt32(First32Bit);
        }
        public static int getNumberOfLeadingZeros(ulong x)
        {
            UInt32 First32Bit,Last32Bit;
            First32Bit=(UInt32)(x&0xffffffff);
            Last32Bit=(UInt32)((x>>32)&0xffffffff);
            if(Last32Bit==0) 
            return getNumberOfLeadingZerosInt32(Last32Bit)+getNumberOfLeadingZerosInt32(First32Bit);
            return getNumberOfLeadingZerosInt32(Last32Bit);
        }
        public static int getNumberOfTrailingZerosInt32(UInt32 x)
        {
            //int[] arr=new int[] {1, 2, 4, 8, 16};
            //      foreach(int y in arr)
            //{
            //   x = x | (x >> y);
            //}
            //return table[(x * 0x07C4ACDD) >> 27];       
            return MultiplyDeBruijnBitPosition[((UInt32)((x & -x) * 0x077CB531U)) >> 27];
        }
        static int[] clz_table_4bit =new int[16] { 4, 3, 2, 2, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
        static int[] MultiplyDeBruijnBitPosition = new int[32] 
{
  0, 1, 28, 2, 29, 14, 24, 3, 30, 22, 20, 15, 25, 17, 4, 8, 
  31, 27, 13, 23, 21, 19, 16, 7, 26, 12, 18, 6, 11, 5, 10, 9
};
        public static int getNumberOfLeadingZerosInt32(UInt32 x )
        {
            int n;
            if ((x & 0xFFFF0000) == 0) { n = 16; x <<= 16; } else { n = 0; }
            if ((x & 0xFF000000) == 0) { n += 8; x <<= 8; }
            if ((x & 0xF0000000) == 0) { n += 4; x <<= 4; }
            n += (int)clz_table_4bit[x >> (28)];
            return n;
            
        }
        public static ulong reverse(ulong i)
        {

        // HD, Figure 7-1
        i = (i & 0x5555555555555555L) <<  1 | (i  >> 1) & 0x5555555555555555L;
        i = (i & 0x3333333333333333L) <<  2 | (i  >> 2) & 0x3333333333333333L;
        i = (i & 0x0f0f0f0f0f0f0f0fL) <<  4 | (i  >> 4) & 0x0f0f0f0f0f0f0f0fL;
        i = (i & 0x00ff00ff00ff00ffL) <<  8 | (i  >> 8) & 0x00ff00ff00ff00ffL;
        i = (i <<  48) | ((i & 0xffff0000L) <<  16) |
            ((i  >> 16) & 0xffff0000L) | (i  >> 48);
        return i;
        }
        //public static ulong reverse(ulong value)
        //{
        //    return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 |
        //           (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8 |
        //           (value & 0x000000FF00000000UL) >> 8 | (value & 0x0000FF0000000000UL) >> 24 |
        //           (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
        //}
        //public static ulong reverse(ulong x)
        //{
        //    ulong y = 0;
        //    for (int i = 0; i < 64; ++i)
        //    {
        //        y <<= 1;
        //        y |= (x & (1));
        //        x >>= 1;
        //    }
        //    return y;
        //}
        //    UInt32 first32Bit, last32Bit;
        //    ulong rs=0;
        //    first32Bit = (UInt32)(x & 0xffffffff);
        //    last32Bit = (UInt32)(x >> 32) & (0xffffffff);
        //    first32Bit = resever32(first32Bit);
        //    last32Bit = resever32(last32Bit);
        //    rs |= (ulong)first32Bit;
        //    rs <<= 32;
        //    rs |= (ulong)last32Bit;
        //    return rs;
        //}
        static byte[] BitReverseTable256 = new byte[256]
{
  0x00, 0x80, 0x40, 0xC0, 0x20, 0xA0, 0x60, 0xE0, 0x10, 0x90, 0x50, 0xD0, 0x30, 0xB0, 0x70, 0xF0, 
  0x08, 0x88, 0x48, 0xC8, 0x28, 0xA8, 0x68, 0xE8, 0x18, 0x98, 0x58, 0xD8, 0x38, 0xB8, 0x78, 0xF8, 
  0x04, 0x84, 0x44, 0xC4, 0x24, 0xA4, 0x64, 0xE4, 0x14, 0x94, 0x54, 0xD4, 0x34, 0xB4, 0x74, 0xF4, 
  0x0C, 0x8C, 0x4C, 0xCC, 0x2C, 0xAC, 0x6C, 0xEC, 0x1C, 0x9C, 0x5C, 0xDC, 0x3C, 0xBC, 0x7C, 0xFC, 
  0x02, 0x82, 0x42, 0xC2, 0x22, 0xA2, 0x62, 0xE2, 0x12, 0x92, 0x52, 0xD2, 0x32, 0xB2, 0x72, 0xF2, 
  0x0A, 0x8A, 0x4A, 0xCA, 0x2A, 0xAA, 0x6A, 0xEA, 0x1A, 0x9A, 0x5A, 0xDA, 0x3A, 0xBA, 0x7A, 0xFA,
  0x06, 0x86, 0x46, 0xC6, 0x26, 0xA6, 0x66, 0xE6, 0x16, 0x96, 0x56, 0xD6, 0x36, 0xB6, 0x76, 0xF6, 
  0x0E, 0x8E, 0x4E, 0xCE, 0x2E, 0xAE, 0x6E, 0xEE, 0x1E, 0x9E, 0x5E, 0xDE, 0x3E, 0xBE, 0x7E, 0xFE,
  0x01, 0x81, 0x41, 0xC1, 0x21, 0xA1, 0x61, 0xE1, 0x11, 0x91, 0x51, 0xD1, 0x31, 0xB1, 0x71, 0xF1,
  0x09, 0x89, 0x49, 0xC9, 0x29, 0xA9, 0x69, 0xE9, 0x19, 0x99, 0x59, 0xD9, 0x39, 0xB9, 0x79, 0xF9, 
  0x05, 0x85, 0x45, 0xC5, 0x25, 0xA5, 0x65, 0xE5, 0x15, 0x95, 0x55, 0xD5, 0x35, 0xB5, 0x75, 0xF5,
  0x0D, 0x8D, 0x4D, 0xCD, 0x2D, 0xAD, 0x6D, 0xED, 0x1D, 0x9D, 0x5D, 0xDD, 0x3D, 0xBD, 0x7D, 0xFD,
  0x03, 0x83, 0x43, 0xC3, 0x23, 0xA3, 0x63, 0xE3, 0x13, 0x93, 0x53, 0xD3, 0x33, 0xB3, 0x73, 0xF3, 
  0x0B, 0x8B, 0x4B, 0xCB, 0x2B, 0xAB, 0x6B, 0xEB, 0x1B, 0x9B, 0x5B, 0xDB, 0x3B, 0xBB, 0x7B, 0xFB,
  0x07, 0x87, 0x47, 0xC7, 0x27, 0xA7, 0x67, 0xE7, 0x17, 0x97, 0x57, 0xD7, 0x37, 0xB7, 0x77, 0xF7, 
  0x0F, 0x8F, 0x4F, 0xCF, 0x2F, 0xAF, 0x6F, 0xEF, 0x1F, 0x9F, 0x5F, 0xDF, 0x3F, 0xBF, 0x7F, 0xFF
};

UInt32 v; // reverse 32-bit value, 8 bits at time
 UInt32 c; // c will get v reversed

// Option 1:
 public static UInt32 resever32(UInt32 v)
 {
         return (UInt32)((BitReverseTable256[v & 0xff] << 24) |
         (BitReverseTable256[(v >> 8) & 0xff] << 16) |
         (BitReverseTable256[(v >> 16) & 0xff] << 8) |
         (BitReverseTable256[(v >> 24) & 0xff]));
 }
// Option 2:
//unsigned char * p = (unsigned char *) &v;
//unsigned char * q = (unsigned char *) &c;
//q[3] = BitReverseTable256[p[0]]; 
//q[2] = BitReverseTable256[p[1]]; 
//q[1] = BitReverseTable256[p[2]]; 
//q[0] = BitReverseTable256[p[3]];
        static int[] index64 = new int[64]{
    0, 47,  1, 56, 48, 27,  2, 60,
   57, 49, 41, 37, 28, 16,  3, 61,
   54, 58, 35, 52, 50, 42, 21, 44,
   38, 32, 29, 23, 17, 11,  4, 62,
   46, 55, 26, 59, 40, 36, 15, 53,
   34, 51, 20, 43, 31, 22, 10, 45,
   25, 39, 14, 33, 19, 30,  9, 24,
   13, 18,  8, 12,  7,  6,  5, 63
};
 
    }
}
