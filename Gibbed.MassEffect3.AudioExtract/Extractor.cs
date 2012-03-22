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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gibbed.IO;
using Registry = Microsoft.Win32.Registry;

namespace Gibbed.MassEffect3.AudioExtract
{
    public partial class Extractor : Form
    {
        public Extractor()
        {
            this.InitializeComponent();
            this.DoubleBuffered = true;
        }

        private static string GetExecutablePath()
        {
            return Path.GetDirectoryName(Application.ExecutablePath);
        }

        private string _ConverterPath = null;
        private string _RevorbPath = null;
        private string _PackagePath = null;
        private List<WwiseLocation> _Index = new List<WwiseLocation>();

        private void OnLoad(object sender, EventArgs e)
        {
            var exePath = GetExecutablePath();

            string path;
            path = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\BioWare\Mass Effect 3", "Install Dir", null);
            if (path == null)
            {
                path =
                    (string)
                    Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\BioWare\Mass Effect 3",
                                      "Install Dir",
                                      null);
            }

            if (path != null)
            {
                path = Path.Combine(path, "BioGame");
                path = Path.Combine(path, "CookedPCConsole");
                this._PackagePath = path;
            }
            else
            {
                this._PackagePath = null;
            }

            var converterPath = Path.Combine(exePath, "ww2ogg.exe");
            if (File.Exists(converterPath) == false)
            {
                this.convertCheckBox.Checked = false;
                this.convertCheckBox.Enabled = false;
                this.LogError("ww2ogg.exe is not present in \"{0}\"!", exePath);
            }
            else
            {
                this._ConverterPath = converterPath;
            }

            var revorbPath = Path.Combine(exePath, "revorb.exe");
            if (File.Exists(revorbPath) == false)
            {
                this.revorbCheckBox.Checked = false;
                this.revorbCheckBox.Enabled = false;
            }
            else
            {
                this._RevorbPath = revorbPath;
            }

            this.ToggleControls(false);

            var indexPath = Path.Combine(exePath, "Wwise.idx");
            if (File.Exists(indexPath) == false)
            {
                this.LogError("Wwise.idx is not present in \"{0}\"!", exePath);
            }
            else
            {
                this.LogMessage("Loading Wwise index...");

                var uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

                DateTime startTime = DateTime.Now;

                var task = Task<List<WwiseLocation>>.Factory.StartNew(
                    () =>
                    {
                        using (var input = File.OpenRead(indexPath))
                        {
                            var index = WwiseIndex.Load(input);
                            if (input.Position != input.Length)
                            {
                                throw new FormatException("did not consume entire file");
                            }

                            var locations = new List<WwiseLocation>();
                            foreach (var resource in index.Resources)
                            {
                                var firstInstance = resource.Instances
                                    .OrderByDescending(i => i.IsPackage == false)
                                    .First();

                                var location = new WwiseLocation()
                                {
                                    Hash = resource.Hash,
                                    Path = index.Strings[firstInstance.PathIndex],
                                    Name = index.Strings[firstInstance.NameIndex],
                                    Actor = index.Strings[firstInstance.ActorIndex],
                                    Unknown = index.Strings[firstInstance.UnknownIndex],
                                    Locale = index.Strings[firstInstance.LocaleIndex],
                                    File = index.Strings[firstInstance.FileIndex],
                                    IsPackage = firstInstance.IsPackage,
                                    Offset = firstInstance.Offset,
                                    Size = firstInstance.Size,
                                };

                                foreach (var instance in resource.Instances.Except(new[] {firstInstance}))
                                {
                                    location.Duplicates.Add(new WwiseLocation()
                                    {
                                        Hash = resource.Hash,
                                        Path = index.Strings[instance.PathIndex],
                                        Name = index.Strings[instance.NameIndex],
                                        Actor = index.Strings[instance.ActorIndex],
                                        Unknown = index.Strings[instance.UnknownIndex],
                                        Locale = index.Strings[instance.LocaleIndex],
                                        File = index.Strings[instance.FileIndex],
                                        IsPackage = instance.IsPackage,
                                        Offset = instance.Offset,
                                        Size = instance.Size,
                                    });
                                }

                                locations.Add(location);
                            }

                            return locations;
                        }
                    });

                task.ContinueWith(
                    t =>
                    {
                        var elapsed = DateTime.Now.Subtract(startTime);
                        this.LogSuccess("Loaded Wwise index in {0}m {1}s {2}ms",
                                        elapsed.Minutes,
                                        elapsed.Seconds,
                                        elapsed.Milliseconds);
                        this.LogMessage("{0} entries ({1} duplicates)",
                                        t.Result.Count,
                                        t.Result.Sum(i => i.Duplicates.Count));
                        this.OnWwiseIndexLoaded(t.Result);
                    },
                    System.Threading.CancellationToken.None,
                    TaskContinuationOptions.OnlyOnRanToCompletion,
                    uiScheduler);

                task.ContinueWith(
                    t =>
                    {
                        this.LogError("Failed to load Wwise index.");
                        if (t.Exception != null)
                        {
                            if (t.Exception.InnerException != null)
                            {
                                this.LogError(t.Exception.InnerException.ToString());
                            }
                            else
                            {
                                this.LogError(t.Exception.ToString());
                            }
                        }
                    },
                    System.Threading.CancellationToken.None,
                    TaskContinuationOptions.OnlyOnFaulted,
                    uiScheduler);
            }
        }

        #region ToggleControls
        private delegate void ToggleControlsDelegate(bool isExtracting);

        private void ToggleControls(bool isExtracting)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(
                    (ToggleControlsDelegate)this.ToggleControls,
                    isExtracting);
                return;
            }

            this.containerListBox.Enabled = isExtracting == false;
            this.fileListView.Enabled = isExtracting == false;
            this.selectNoneButton.Enabled = isExtracting == false;
            this.selectAllButton.Enabled = isExtracting == false;
            this.selectVisibleButton.Enabled = isExtracting == false;
            this.selectSearchButton.Enabled = isExtracting == false;
            this.listButton.Enabled = isExtracting == false;
            this.convertCheckBox.Enabled = this._ConverterPath != null && isExtracting == false;
            this.revorbCheckBox.Enabled = this._RevorbPath != null && isExtracting == false;
            this.validateCheckBox.Enabled = isExtracting == false;
            this.cancelButton.Enabled = isExtracting == true;
            this.startButton.Enabled = isExtracting == false;
        }
        #endregion

        #region SetProgress
        private delegate void SetProgressDelegate(int percent);

        private void SetProgress(int percent)
        {
            if (this.progressBar1.InvokeRequired)
            {
                this.Invoke((SetProgressDelegate)this.SetProgress, new object[] {percent});
                return;
            }

            this.progressBar1.Value = (int)percent;
        }
        #endregion

        #region Logging
        private delegate void LogMessageDelegate(Color color, string message, params object[] args);

        private void LogMessage(Color color, string message, params object[] args)
        {
            if (this.logTextBox.InvokeRequired == true)
            {
                this.logTextBox.Invoke(
                    (LogMessageDelegate)this.LogMessage,
                    color,
                    message,
                    args);
                return;
            }

            var oldColor = this.logTextBox.SelectionColor;
            this.logTextBox.SelectionStart = this.logTextBox.Text.Length;
            this.logTextBox.SelectionColor = color;
            this.logTextBox.SelectedText = string.Format(message, args);
            this.logTextBox.SelectionStart = this.logTextBox.Text.Length;
            this.logTextBox.AppendText(Environment.NewLine);
            this.logTextBox.SelectionColor = oldColor;
            this.logTextBox.ScrollToCaret();
        }

        private void LogMessage(string message, params object[] args)
        {
            this.LogMessage(Color.Black, message, args);
        }

        private void LogSuccess(string message, params object[] args)
        {
            this.LogMessage(Color.Green, message, args);
        }

        private void LogWarning(string message, params object[] args)
        {
            this.LogMessage(Color.Brown, message, args);
        }

        private void LogError(string message, params object[] args)
        {
            this.LogMessage(Color.Red, message, args);
        }
        #endregion

        private static string FixFilename(string name, bool isPackage)
        {
            return
                name +
                (isPackage == false ? ".afc" : ".pcc");
        }

        private class WwiseLocation
        {
            public WwiseIndex.FileHash Hash;
            public string Path;
            public string Name;
            public string Actor;
            public string Unknown;
            public string Locale;
            public string File;
            public bool IsPackage;
            public int Offset;
            public int Size;

            public bool Selected;

            public List<WwiseLocation> Duplicates = new List<WwiseLocation>();
        }

        private class FilterFile
        {
            public string Name;
            public string Value;
            public bool IsPackage;

            public override string ToString()
            {
                return this.Name;
            }
        }

        private class FilterItem
        {
            public string Name;
            public string Value;

            public override string ToString()
            {
                return this.Name;
            }
        }

        private void OnWwiseIndexLoaded(List<WwiseLocation> index)
        {
            this._Index.Clear();
            this._Index.AddRange(index);

            this.fileListView.BeginUpdate();
            this.fileListView.Items.Clear();
            this.fileListView.EndUpdate();

            this.containerListBox.BeginUpdate();
            this.containerListBox.Items.Clear();

            this.containerListBox.Items.Add(new FilterFile()
            {
                Name = "(any file)",
                Value = null,
                IsPackage = false,
            });

            foreach (var kv in this._Index
                .Select(i => new KeyValuePair<string, bool>(
                                 i.File, i.IsPackage)).Distinct()
                .OrderBy(kv => kv.Key))
            {
                this.containerListBox.Items.Add(new FilterFile()
                {
                    Name = FixFilename(kv.Key, kv.Value),
                    Value = kv.Key,
                    IsPackage = kv.Value,
                });
            }

            this.containerListBox.SelectedIndex = 0;
            this.containerListBox.EndUpdate();

            this.actorComboBox.BeginUpdate();
            this.actorComboBox.Items.Clear();

            this.actorComboBox.Items.Add(new FilterItem()
            {
                Name = "(any actor)",
                Value = null,
            });

            this.actorComboBox.Items.Add(new FilterItem()
            {
                Name = "(no actor)",
                Value = "",
            });

            foreach (var actor in this._Index
                .Where(i => string.IsNullOrEmpty(i.Actor) == false)
                .Select(i => i.Actor)
                .Distinct()
                .OrderBy(a => a))
            {
                this.actorComboBox.Items.Add(new FilterItem()
                {
                    Name = actor,
                    Value = actor,
                });
            }

            this.actorComboBox.SelectedIndex = 0;
            this.actorComboBox.EndUpdate();

            this.localeComboBox.BeginUpdate();
            this.localeComboBox.Items.Clear();

            this.localeComboBox.Items.Add(new FilterItem()
            {
                Name = "(any locale)",
                Value = null,
            });

            this.localeComboBox.Items.Add(new FilterItem()
            {
                Name = "(no locale)",
                Value = "",
            });

            foreach (var locale in this._Index
                .Where(i => string.IsNullOrEmpty(i.Locale) == false)
                .Select(i => i.Locale)
                .Distinct()
                .OrderBy(l => l))
            {
                this.localeComboBox.Items.Add(new FilterItem()
                {
                    Name = locale,
                    Value = locale,
                });
            }

            this.localeComboBox.SelectedIndex = 0;
            this.localeComboBox.EndUpdate();

            this.UpdateTotals();
        }

        private IEnumerable<WwiseLocation> FilterInstances(IEnumerable<WwiseLocation> instances)
        {
            var containerFilter = (FilterFile)this.containerListBox.SelectedItem;
            var actorFilter = (FilterItem)this.actorComboBox.SelectedItem;
            var localeFilter = (FilterItem)this.localeComboBox.SelectedItem;

            if (containerFilter != null &&
                containerFilter.Value != null)
            {
                instances = instances.Where(
                    i => (i.File == containerFilter.Value &&
                          i.IsPackage == containerFilter.IsPackage) ||
                         i.Duplicates.Any(
                             j => j.File == containerFilter.Value && j.IsPackage == containerFilter.IsPackage));
            }

            if (actorFilter != null &&
                actorFilter.Value != null)
            {
                instances = instances.Where(
                    i => i.Actor == actorFilter.Value || i.Duplicates.Any(f => f.Actor == actorFilter.Value));
            }

            if (localeFilter != null &&
                localeFilter.Value != null)
            {
                instances = instances.Where(
                    i => i.Locale == localeFilter.Value || i.Duplicates.Any(f => f.Locale == localeFilter.Value));
            }

            return instances;
        }

        private void OnFilter(object sender, EventArgs e)
        {
            var containerFilter = (FilterFile)this.containerListBox.SelectedItem;
            var actorFilter = (FilterItem)this.actorComboBox.SelectedItem;
            var localeFilter = (FilterItem)this.localeComboBox.SelectedItem;

            if ((containerFilter == null || containerFilter.Value == null) &&
                actorFilter.Value == null &&
                localeFilter.Value == null)
            {
                if (
                    MessageBox.Show("Are you sure you want to list every file?",
                                    "Warning",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
            }

            this.fileListView.BeginUpdate();
            this.fileListView.Items.Clear();
            foreach (var instance in this.FilterInstances(this._Index).OrderBy(i => i.Name))
            {
                var item = new ListViewItem(instance.Name)
                {
                    Checked = instance.Selected,
                    Tag = instance,
                };
                item.SubItems.Add(NativeHelper.FormatByteSize(instance.Size));
                this.fileListView.Items.Add(item);
            }
            this.fileListView.EndUpdate();

            this.LogMessage("Found {0} files.", this.fileListView.Items.Count);
        }

        private void UpdateTotals()
        {
            long totalSize = 0;
            long totalCount = 0;

            foreach (var location in this._Index.Where(l => l.Selected == true))
            {
                totalCount++;
                totalSize += location.Size;
            }

            this.totalSizeLabel.Text = string.Format(
                "Selected {0} files, {1}",
                totalCount,
                NativeHelper.FormatByteSize(totalSize));
        }

        private void UpdateFileChecks()
        {
            this.fileListView.BeginUpdate();
            this._BatchCheckUpdate = true;
            foreach (ListViewItem item in this.fileListView.Items)
            {
                var location = (WwiseLocation)item.Tag;
                if (location != null &&
                    item.Checked != location.Selected)
                {
                    item.Checked = location.Selected;
                }
            }
            this._BatchCheckUpdate = false;
            this.fileListView.EndUpdate();
        }

        private void OnSelectNone(object sender, EventArgs e)
        {
            this._Index.ForEach(l => l.Selected = false);
            this.UpdateFileChecks();
            this.UpdateTotals();
        }

        private void OnSelectAll(object sender, EventArgs e)
        {
            this._Index.ForEach(l => l.Selected = true);
            this.UpdateFileChecks();
            this.UpdateTotals();
        }

        private void OnSelectVisible(object sender, EventArgs e)
        {
            this.fileListView.BeginUpdate();
            foreach (ListViewItem item in this.fileListView.Items)
            {
                var location = (WwiseLocation)item.Tag;
                if (location != null)
                {
                    location.Selected = true;
                }
            }
            this.fileListView.EndUpdate();
            this.UpdateFileChecks();
            this.UpdateTotals();
        }

        private void OnSelectSearch(object sender, EventArgs e)
        {
            var search = new SearchBox();
            search.Owner = this;
            search.InputText = "mus";

            if (search.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var text = search.InputText.Trim().ToLowerInvariant();

            this.fileListView.BeginUpdate();
            this.fileListView.Items.Clear();
            this._BatchCheckUpdate = true;
            foreach (var instance in this._Index.Where(
                l =>
                (l.Path + "." + l.Name).Contains(text) == true ||
                l.Duplicates.Any(m => (m.Path + "." + m.Name).Contains(text) == true) == true))
            {
                instance.Selected = true;

                var item = new ListViewItem(instance.Name)
                {
                    Checked = instance.Selected,
                    Tag = instance,
                };
                item.SubItems.Add(NativeHelper.FormatByteSize(instance.Size));
                this.fileListView.Items.Add(item);
            }
            this._BatchCheckUpdate = false;
            this.fileListView.EndUpdate();

            this.containerListBox.SelectedItem = null;
            this.UpdateFileChecks();
            this.UpdateTotals();

            this.LogMessage("Found {0} files.", this.fileListView.Items.Count);
        }

        private void OnFileSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this.duplicatesTextBox.Clear();

            var location = (WwiseLocation)e.Item.Tag;
            var sb = new StringBuilder();
            if (location != null)
            {
                sb.Append(location.Path + "." + location.Name + Environment.NewLine);
                foreach (var dupe in location.Duplicates)
                {
                    sb.Append(dupe.Path + "." + dupe.Name + Environment.NewLine);
                }
            }

            this.duplicatesTextBox.Text = sb.ToString();
            this.duplicatesTextBox.SelectionStart = 0;
        }

        private bool _BatchCheckUpdate = false;

        private void OnFileChecked(object sender, ItemCheckEventArgs e)
        {
            if (this._BatchCheckUpdate == true)
            {
                return;
            }

            var location = (WwiseLocation)this.fileListView.Items[e.Index].Tag;
            if (location != null)
            {
                location.Selected = e.NewValue == CheckState.Checked;
                this.UpdateTotals();
            }
        }

        private System.Threading.CancellationTokenSource _ExtractCancellationToken = null;

        private void OnStart(object sender, EventArgs e)
        {
            var locations = this._Index.Where(l => l.Selected == true).ToList();
            if (locations.Count == 0)
            {
                this.LogError("No files selected.");
                return;
            }

            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = locations.Count;
            this.progressBar1.Value = 0;

            if (this.saveFolderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var basePath = this.saveFolderBrowserDialog.SelectedPath;

            if (Directory.GetFiles(basePath).Length > 0 ||
                Directory.GetDirectories(basePath).Length > 0)
            {
                if (MessageBox.Show(
                    this,
                    "Folder is not empty, continue anyway?",
                    "Question",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            var paths = new Dictionary<KeyValuePair<string, bool>, string>();

            foreach (var kv in locations.Select(l => new KeyValuePair<string, bool>(l.File, l.IsPackage)).Distinct())
            {
                string inputPath;

                inputPath = Path.Combine(this._PackagePath, FixFilename(kv.Key, kv.Value));
                if (File.Exists(inputPath) == false)
                {
                    var fileName = Path.GetFileName(inputPath);

                    this.openContainerFileDialog.Title = "Open " + fileName;
                    this.openContainerFileDialog.Filter = fileName + "|" + fileName;

                    if (this.openContainerFileDialog.ShowDialog() != DialogResult.OK)
                    {
                        this.LogError("Could not find \"{0}\"!", fileName);
                        inputPath = null;
                    }
                    else
                    {
                        inputPath = this.openContainerFileDialog.FileName;
                    }
                }

                paths.Add(kv, inputPath);
            }

            this.ToggleControls(true);

            var uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            DateTime startTime = DateTime.Now;

            var pcbPath = Path.Combine(Path.GetDirectoryName(_ConverterPath), "packed_codebooks.bin");

            var validating = this.validateCheckBox.Checked == true;
            var converting = this.convertCheckBox.Checked == true;
            var revorbing = this.revorbCheckBox.Checked == true;

            this._ExtractCancellationToken = new System.Threading.CancellationTokenSource();
            var token = this._ExtractCancellationToken.Token;
            var task = Task.Factory.StartNew(
                () =>
                {
                    int succeeded, failed, current;

                    var streams = new Dictionary<KeyValuePair<string, bool>, Stream>();
                    var dupeNames = new Dictionary<string, int>();
                    try
                    {
                        succeeded = failed = current = 0;

                        foreach (var location in locations)
                        {
                            if (token.IsCancellationRequested == true)
                            {
                                this.LogWarning("Extraction cancelled.");
                                break;
                            }

                            current++;
                            this.SetProgress(current);

                            var source = new KeyValuePair<string, bool>(location.File, location.IsPackage);
                            if (paths.ContainsKey(source) == false)
                            {
                                failed++;
                                continue;
                            }

                            Stream input;
                            if (streams.ContainsKey(source) == false)
                            {
                                if (source.Value == false)
                                {
                                    input = File.OpenRead(paths[source]);
                                    streams[source] = input;
                                }
                                else
                                {
                                    throw new NotImplementedException();
                                }
                            }
                            else
                            {
                                input = streams[source];
                            }

                            if (validating == true)
                            {
                                input.Seek(location.Offset, SeekOrigin.Begin);
                                var bytes = input.ReadBytes(location.Size);
                                var hash = WwiseIndex.FileHash.Compute(bytes);

                                if (hash != location.Hash)
                                {
                                    failed++;

                                    this.LogError(
                                        "Hash mismatch for \"{0}.{1}\"! ({2} vs {3})",
                                        location.File,
                                        location.Name,
                                        location.Hash,
                                        hash);

                                    continue;
                                }
                            }

                            var name = location.Name;
                            if (name.EndsWith("_wav") == true)
                            {
                                name = name.Substring(0, name.Length - 4);
                            }


                            var outputPath = Path.Combine(basePath, location.File, name);

                            int dupeCounter;
                            if (dupeNames.TryGetValue(outputPath, out dupeCounter) == false)
                            {
                                dupeCounter = 1;
                            }

                            if (dupeCounter > 1)
                            {
                                outputPath += string.Format(" [#{0}]", dupeCounter);
                            }

                            dupeCounter++;
                            dupeNames[outputPath] = dupeCounter;

                            var oggPath = outputPath + ".ogg";
                            var riffPath = outputPath + ".riff";

                            Directory.CreateDirectory(Path.GetDirectoryName(riffPath));

                            using (var output = File.Create(riffPath))
                            {
                                input.Seek(location.Offset, SeekOrigin.Begin);
                                output.WriteFromStream(input, location.Size);
                            }

                            if (converting == true)
                            {
                                var ogger = new System.Diagnostics.Process();
                                ogger.StartInfo.UseShellExecute = false;
                                ogger.StartInfo.CreateNoWindow = true;
                                ogger.StartInfo.RedirectStandardOutput = true;
                                ogger.StartInfo.FileName = this._ConverterPath;
                                ogger.StartInfo.Arguments = string.Format(
                                    "-o \"{0}\" --pcb \"{2}\" \"{1}\"",
                                    oggPath,
                                    riffPath,
                                    pcbPath);

                                ogger.Start();
                                ogger.WaitForExit();

                                if (ogger.ExitCode != 0)
                                {
                                    string stdout = ogger.StandardOutput.ReadToEnd();

                                    this.LogError("Failed to convert \"{0}.{1}\"!",
                                                  location.File,
                                                  location.Name);
                                    this.LogMessage(stdout);
                                    File.Delete(oggPath);
                                    failed++;
                                    continue;
                                }

                                File.Delete(riffPath);

                                if (revorbing == true)
                                {
                                    var revorber = new System.Diagnostics.Process();
                                    revorber.StartInfo.UseShellExecute = false;
                                    revorber.StartInfo.CreateNoWindow = true;
                                    revorber.StartInfo.RedirectStandardOutput = true;
                                    revorber.StartInfo.FileName = this._RevorbPath;
                                    revorber.StartInfo.Arguments = string.Format(
                                        "\"{0}\"",
                                        oggPath);

                                    revorber.Start();
                                    revorber.WaitForExit();

                                    if (revorber.ExitCode != 0)
                                    {
                                        string stdout = revorber.StandardOutput.ReadToEnd();

                                        this.LogError("Failed to revorb \"{0}.{1}\"!",
                                                      location.File,
                                                      location.Name);
                                        this.LogMessage(stdout);
                                    }
                                }
                            }

                            succeeded++;
                        }
                    }
                    finally
                    {
                        foreach (var stream in streams.Values)
                        {
                            if (stream != null)
                            {
                                stream.Close();
                            }
                        }
                    }

                    this.LogSuccess("Done, {0} succeeded, {1} failed, {2} total.", succeeded, failed, succeeded + failed);
                },
                this._ExtractCancellationToken.Token);

            task.ContinueWith(
                t =>
                {
                    var elapsed = DateTime.Now.Subtract(startTime);
                    this.LogSuccess("Extracted in {0}m {1}s {2}ms",
                                    elapsed.Minutes,
                                    elapsed.Seconds,
                                    elapsed.Milliseconds);
                    this.ToggleControls(false);
                },
                System.Threading.CancellationToken.None,
                TaskContinuationOptions.OnlyOnRanToCompletion,
                uiScheduler);

            task.ContinueWith(
                t =>
                {
                    this.LogError("Failed to extract!");
                    if (t.Exception != null)
                    {
                        if (t.Exception.InnerException != null)
                        {
                            this.LogError(t.Exception.InnerException.ToString());
                        }
                        else
                        {
                            this.LogError(t.Exception.ToString());
                        }
                    }
                    this.ToggleControls(false);
                },
                System.Threading.CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted,
                uiScheduler);
        }

        private void OnCancel(object sender, EventArgs e)
        {
            this._ExtractCancellationToken.Cancel();
        }
    }
}
