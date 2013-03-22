using System.Collections.Generic;
using System.Linq;

namespace Gibbed.MassEffect3.SaveEdit
{
    public class WriteDump
    {

        public List<string> lines { get; set; }
        public int type { get; set; }

        public void AddItem(int key, string value)
        {
            if (this.lines == null) this.lines = new List<string>();
            while (key > this.lines.Count)
            {
                this.lines.Add("-1");
            }
            if (key == this.lines.Count) 
                this.lines.Insert(key, value);
            else 
                this.lines[key] = value;
        }

        public void ProcessBooleans(System.Collections.BitArray array)
        {
            this.type = 1;
            if (this.lines == null) this.lines = new List<string>();

            for (int i = 0; i < array.Count; i++)
            {
                if (array[i]) this.AddItem(i, "true");
                else this.AddItem(i, "false");
            }
        }

        public void ProcessInts(List<SaveFormats.PlotTable.IntVariablePair> list)
        {
            this.type = 2;
            if (this.lines == null) this.lines = new List<string>();

            for (int i = 0; i < list.Count; i++)
            {
                var variable = list.FirstOrDefault(v => v.Index == i);

                if (variable == null)
                    this.AddItem(i, "0");
                else
                    this.AddItem(i, variable.Value.ToString());
            }
        }

        public void ProcessGAWAssets(List<SaveFormats.GAWAsset> list)
        {
            this.type = 1;
            if (this.lines == null) this.lines = new List<string>();

            foreach (var variable in list)
            {
                this.AddItem(variable.Id, variable.Strength.ToString());
            }
        }

        public void ProcessIntList(List<int> list)
        {
            this.type = 1;
            if (this.lines == null) this.lines = new List<string>();

            for (int i = 0; i < list.Count; i++)
            {
                this.AddItem(i, list[i].ToString());
            }
        }

        public void ProcessFloats(List<SaveFormats.PlotTable.FloatVariablePair> list)
        {
            this.type = 2;
            if (this.lines == null) this.lines = new List<string>();

            for (int i = 0; i < list.Count; i++)
            {
                var variable = list.FirstOrDefault(v => v.Index == i);

                if (variable == null)
                    this.AddItem(i, "0");
                else
                    this.AddItem(i, variable.Value.ToString());
            }
        }

        public void WriteIt(string filename = "dump.txt")
        {
            if (this.lines != null)
            {
                string final;
                switch(this.type)
                {
                    case 2:
                        final = string.Join(
                            "\n",
                            this.lines.Select((val, idx) => System.String.Format("{0:d} => {1}", idx, val)).Where(x => !x.Contains("=> 0"))
                            );
                        break;

                    default:
                        final = string.Join(
                            "\n",
                            this.lines.Select((val, idx) => System.String.Format("{0:d} => {1}", idx, val)).Where(x => !x.Contains("=> -1"))
                            );
                        break;
                }
                System.IO.File.WriteAllText(filename, final);

                this.lines = new List<string>();
            }
        }
    }
}
