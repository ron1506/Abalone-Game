using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Diagnostics;

namespace AbaloneGame.model
{
    public class BitSet
    {
        private readonly bool[] bits;

        public BitSet(IEnumerable<bool> bits)
        {
            this.bits = bits.ToArray();
        }
        public BitSet(int length)
        {
            this.bits = new bool[length];
        }
        public int Length { get { return bits.Length; } }
        public void set(int fromindex, int toindex)
        {
            if (toindex < 0 && fromindex < 0 && toindex < fromindex)
                throw new IndexOutOfRangeException();
            for (int i = fromindex; i < toindex; i++)
            {
                this.bits[i] = true;
            }
        }
        public void set(int index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException();
            this.bits[index] = true;
        }
        public bool get(int bitIndex)
        {
            if (bitIndex < 0)
                throw new IndexOutOfRangeException();
            return bits[bitIndex];
        }
        /// <summary>
        /// returns the first index that after the fromindex (including fromIndex) in the bitset that is on.
        /// </summary>
        /// <param name="fromIndex">the index from which we starting to check. </param>
        /// <returns></returns>
        public int nextSetBit(int fromIndex)
        {
            if (fromIndex < 0)
                throw new IndexOutOfRangeException();
            for (int i = fromIndex; i < this.Length; i++)
            {
                if (bits[i] == true)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// sets the bit in the given index, to the given value.
        /// </summary>
        /// <param name="bitIndex">the given index</param>
        /// <param name="value">given value. </param>
        public void set(int bitIndex, bool value)
        {
            if (bitIndex < 0)
                throw new IndexOutOfRangeException();
            bits[bitIndex] = value;
        }
        /// <summary>
        /// cloning the bit set
        /// </summary>
        /// <returns>new identical bitset. </returns>
        public BitSet clone()
        {
            BitSet clonedBitSet = new BitSet(Length);
            for (int i = 0; i < Length; i++)
            {
                if (get(i))
                    clonedBitSet.set(i);
            }
            return clonedBitSet;
        }
    }
}
