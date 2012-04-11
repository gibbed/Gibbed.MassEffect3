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

using System.Collections.Generic;
using System.Windows.Forms;

namespace Gibbed.MassEffect3.SaveEdit.BasicTable
{
    internal static class Character
    {
        public static List<TableItem> Build(Editor editor)
        {
            return new List<TableItem>()
            {
                new TableItem()
                {
                    Name = Properties.Localization.Editor_BasicTable_Character_Name,
                    Control = new TextBox()
                    {
                        Dock = DockStyle.Fill,
                    },
                    Binding =
                        new Binding("Text",
                                    editor._RootSaveFileBindingSource,
                                    "Player.FirstName",
                                    true,
                                    DataSourceUpdateMode.OnPropertyChanged)
                },
                new TableItem()
                {
                    Name = Properties.Localization.Editor_BasicTable_Character_Level,
                    Control = new NumericUpDown()
                    {
                        Minimum = 1,
                        Maximum = 60,
                        Increment = 1,
                        Dock = DockStyle.Fill,
                    },
                    Binding =
                        new Binding("Value",
                                    editor._RootSaveFileBindingSource,
                                    "Player.Level",
                                    true,
                                    DataSourceUpdateMode.OnPropertyChanged)
                },
                new TableItem()
                {
                    Name = Properties.Localization.Editor_BasicTable_Character_Class,
                    Control = new ComboBox()
                    {
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        DisplayMember = "Name",
                        ValueMember = "Type",
                        DataSource = PlayerClass.GetClasses(),
                        Dock = DockStyle.Fill,
                    },
                    Binding =
                        new Binding("SelectedValue",
                                    editor._RootSaveFileBindingSource,
                                    "Player.ClassName",
                                    true,
                                    DataSourceUpdateMode.OnPropertyChanged)
                },
                new TableItem()
                {
                    Name = Properties.Localization.Editor_BasicTable_Character_Gender,
                    Control = new ComboBox()
                    {
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        DisplayMember = "Name",
                        ValueMember = "Type",
                        DataSource = PlayerGender.GetGenders(),
                        Dock = DockStyle.Fill,
                    },
                    Binding =
                        new Binding("SelectedValue",
                                    editor._RootSaveFileBindingSource,
                                    "Player.IsFemale",
                                    true,
                                    DataSourceUpdateMode.OnPropertyChanged)
                },
                new TableItem()
                {
                    Name = Properties.Localization.Editor_BasicTable_Character_Origin,
                    Control = new ComboBox()
                    {
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        DisplayMember = "Name",
                        ValueMember = "Type",
                        DataSource = PlayerOrigin.GetOrigins(),
                        Dock = DockStyle.Fill,
                    },
                    Binding =
                        new Binding("SelectedValue",
                                    editor._RootSaveFileBindingSource,
                                    "Player.Origin",
                                    true,
                                    DataSourceUpdateMode.OnPropertyChanged)
                },
                new TableItem()
                {
                    Name = Properties.Localization.Editor_BasicTable_Character_Notoriety,
                    Control = new ComboBox()
                    {
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        DisplayMember = "Name",
                        ValueMember = "Type",
                        DataSource = PlayerNotoriety.GetNotorieties(),
                        Dock = DockStyle.Fill,
                    },
                    Binding =
                        new Binding("SelectedValue",
                                    editor._RootSaveFileBindingSource,
                                    "Player.Notoriety",
                                    true,
                                    DataSourceUpdateMode.OnPropertyChanged)
                },
                new TableItem()
                {
                    Name = Properties.Localization.Editor_BasicTable_Character_TalentPoints,
                    Control = new NumericUpDown()
                    {
                        Minimum = int.MinValue,
                        Maximum = int.MaxValue,
                        Increment = 1,
                        Dock = DockStyle.Fill,
                    },
                    Binding =
                        new Binding("Value",
                                    editor._RootSaveFileBindingSource,
                                    "Player.TalentPoints",
                                    true,
                                    DataSourceUpdateMode.OnPropertyChanged)
                },
            };
        }
    }
}
