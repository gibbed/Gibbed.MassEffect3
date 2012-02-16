/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

// is this code terrible?
// yup!

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gibbed.MassEffect3.FileFormats.Huffman
{
    public class Encoder
    {
        private Node Root;
        private Dictionary<char, BitArray> Codes = new Dictionary<char, BitArray>();

        public int TotalBits { get; private set; }

        public Encoder()
        {
        }

        public void Build(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            this.Root = null;
            var frequencies = new Dictionary<char, int>();
            this.Codes.Clear();

            for (int i = 0; i < text.Length; i++)
            {
                int frequency = 0;
                frequencies.TryGetValue(text[i], out frequency);
                frequencies[text[i]] = frequency + 1;
            }

            var nodes = new List<Node>();
            foreach (var symbol in frequencies)
            {
                nodes.Add(new Node()
                    {
                        Symbol = symbol.Key,
                        Frequency = symbol.Value,
                    });
            }

            while (nodes.Count > 1)
            {
                var orderedNodes = nodes
                    .OrderBy(n => n.Frequency).ToList<Node>();

                if (orderedNodes.Count >= 2)
                {
                    var taken = orderedNodes.Take(2);
                    var first = taken.First();
                    var second = taken.Last();

                    var parent = new Node()
                    {
                        Symbol = '\0',
                        Frequency = first.Frequency + second.Frequency,
                        Left = first,
                        Right = second,
                    };

                    nodes.Remove(first);
                    nodes.Remove(second);
                    nodes.Add(parent);
                }

                this.Root = nodes.FirstOrDefault();
            }

            foreach (var frequency in frequencies)
            {
                var bits = Traverse(this.Root, frequency.Key, new List<bool>());
                if (bits == null)
                {
                    throw new InvalidOperationException(string.Format(
                        "could not traverse '{0}'", frequency.Key));
                }
                this.Codes.Add(frequency.Key, new BitArray(bits.ToArray()));
            }

            this.TotalBits = GetTotalBits(this.Root);
        }

        private static int GetTotalBits(Node root)
        {
            var queue = new Queue<Node>();
            queue.Enqueue(root);

            int totalBits = 0;
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node.Left == null && node.Right == null)
                {
                    continue;
                }

                totalBits += node.Frequency;

                if (node.Left.Left != null &&
                    node.Left.Right != null)
                {
                    queue.Enqueue(node.Left);
                }

                if (node.Right.Left != null &&
                    node.Right.Right != null)
                {
                    queue.Enqueue(node.Right);
                }
            }

            return totalBits;
        }

        private static List<bool> Traverse(Node node, char symbol, List<bool> data)
        {
            if (node.Left == null &&
                node.Right == null)
            {
                return symbol == node.Symbol ? data : null;
            }
            else
            {
                if (node.Left != null)
                {
                    var path = new List<bool>();
                    path.AddRange(data);
                    path.Add(false);

                    var left = Traverse(node.Left, symbol, path);
                    if (left != null)
                    {
                        return left;
                    }
                }

                if (node.Right != null)
                {
                    var path = new List<bool>();
                    path.AddRange(data);
                    path.Add(true);
                    
                    var right = Traverse(node.Right, symbol, path);
                    if (right != null)
                    {
                        return right;
                    }
                }

                return null;
            }
        }

        private int Encode(char symbol, BitArray bits, int offset)
        {
            var code = this.Codes[symbol];
            for (int i = 0; i < code.Length; i++)
            {
                bits[offset + i] = code[i];
            }
            return code.Length;
        }

        public int Encode(string text, BitArray bits, int offset)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            var bitCount = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (this.Codes.ContainsKey(text[i]) == false)
                {
                    throw new ArgumentException(string.Format(
                        "could not lookup '{0}'", text[i]), "text");
                }

                bitCount += this.Encode(text[i], bits, offset + bitCount);
            }

            return bitCount;
        }

        public Pair[] GetPairs()
        {
            var pairs = new List<Pair>();
            var mapping = new Dictionary<Node, Pair>();

            var queue = new Queue<Node>();
            queue.Enqueue(this.Root);

            var root = new Pair();
            mapping.Add(this.Root, root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                var pair = mapping[node];

                if (node.Left == null && node.Right == null)
                {
                    throw new InvalidOperationException();
                }

                if (node.Left.Left == null &&
                    node.Left.Right == null)
                {
                    pair.Left = -1 - (int)(node.Left.Symbol);
                }
                else
                {
                    var left = new Pair();
                    mapping.Add(node.Left, left);
                    pairs.Add(left);

                    queue.Enqueue(node.Left);

                    pair.Left = pairs.IndexOf(left);
                }

                if (node.Right.Left == null &&
                    node.Right.Right == null)
                {
                    pair.Right = -1 - (int)(node.Right.Symbol);
                }
                else
                {
                    var right = new Pair();
                    mapping.Add(node.Right, right);
                    pairs.Add(right);

                    queue.Enqueue(node.Right);

                    pair.Right = pairs.IndexOf(right);
                }
            }

            pairs.Add(root);
            return pairs.ToArray();
        }
    }
}
