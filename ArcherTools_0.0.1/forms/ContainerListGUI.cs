using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArcherTools_0._0._1.boxes;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.classes;

namespace ArcherTools_0._0._1.forms
{
    public partial class ContainerListGUI : Form
    {

        public static Form _instanceForm;
        public static ContainerListGUI _instance;
        public ContainerListGUI(string title)
        {
            InitializeComponent();
            this.Load += ContainerListGUI_Load;
            this.title_Label.Text = title;
            this.VisibleChanged += ContainerListGUI_ShownOrVisible;
            this.Shown += ContainerListGUI_ShownOrVisible;
        }

        private void ContainerListGUI_Load(object? sender, EventArgs e)
        {
            ColorConfig currentPreset = ColorPresets._instance.GetCurrentPreset();
            _instanceForm = this.FindForm();
            _instance = this;
            this.addCtn_btn.Click += addContainer_Clicked;
            this.delCtn_btn.Click += delContainer_Clicked;
            this.Invalidated += ContainerListGUI_Invalidated;

            Label title = title_Label;
            title.ForeColor = currentPreset.PrimaryLabelColor;
            title.BackColor = Color.Transparent;


        }

        private void ContainerListGUI_Invalidated(object? sender, InvalidateEventArgs e)
        {
            UpdateContainerList();
            UpdateSelectedContainer();
        }

        private static void UpdateSelectedContainer()
        {
            if (_instanceForm != null)
            {
                Control[] findlbl = _instanceForm.Controls.Find("selectCtn_lbl", true);
                Label selectedLabel = (Label)findlbl[0];
                if (classes.Container.SelectedContainer != null)
                {
                    selectedLabel.Text = selectedLabel.Text.Split(':')[0] + $": {ArcherTools_0._0._1.classes.Container.SelectedContainer.ToString()}";
                }
                else
                {
                    selectedLabel.Text = selectedLabel.Text.Split(':')[0] + $": Nothing.";
                }
            }
        }


        private static void UpdateContainerList()
        {
            if (_instanceForm != null)
            {
                Control[] FindBox = _instanceForm.Controls.Find("containerList_listbox", true);
                ListBox listBox = (ListBox)FindBox[0];
                if (ArcherTools_0._0._1.classes.Container.AllContainers == null)
                {
                    return;
                }
                listBox.Items.Clear();
                foreach (var cont in ArcherTools_0._0._1.classes.Container.AllContainers)
                {
                    if (!listBox.Items.Contains(cont))
                    {
                        listBox.Items.Add(cont);
                    }
                }
            }
        }

        private void ContainerListGUI_ShownOrVisible(object? sender, EventArgs e)
        {
            var containerListForm = _instanceForm;
            if (containerList_listbox != null)
            {
                if (containerListForm.Visible)
                {
                    containerListForm.Invalidate();
                }
            }
        }

        private void containerList_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (sender.GetType() == typeof(ListBox))
                {
                    var listbox = (ListBox)sender;
                    if (listbox.SelectedItem != null)
                    {
                        classes.Container.SetSelectedContainer((classes.Container)listbox.SelectedItem);
                        Debug.WriteLine(classes.Container.SelectedContainer.ToString());
                    }
                }
            }
        }

        private void addContainer_Clicked(object sender, EventArgs e)
        {
            var boxNames = new List<string> { "Container", "Release", "Owner" };
            List<String> dibf = DynamicInputBoxForm.Show("Please enter the container code, release (in number) and owner", boxNames, true);
            var isValid = true;

            if (dibf != null && dibf.Count == 3)
            {
                foreach (var thing in dibf)
                {
                    if (boxNames.Contains(thing))
                    {
                        isValid = false;
                    }
                }
                if (isValid)
                {
                    ConcurrentDictionary<int, ConcurrentDictionary<int, Item>> releasesAndItems = new ConcurrentDictionary<int, ConcurrentDictionary<int, Item>>();
                    ConcurrentDictionary<int, Item> itemList = new ConcurrentDictionary<int, Item>();
                    releasesAndItems.TryAdd(int.Parse(dibf[1]), itemList);
                    classes.Container newCtn = new classes.Container(dibf[0], releasesAndItems);
                    classes.Container.AddContainer(newCtn);
                    newCtn.SerializeToFileAsync(Path.Combine(ConfigData.appContainersFolder, newCtn.ContainerId));
                    UpdateContainerList();
                }
            }
        }

        private async void delContainer_Clicked(object sender, EventArgs e)
        {
            Control[] FindBox = _instanceForm.Controls.Find("containerList_listbox", true);
            ListBox listBox = (ListBox)FindBox[0];

            if (ArcherTools_0._0._1.classes.Container.SelectedContainer != null)
            {
                var currentSelectedContainer = ArcherTools_0._0._1.classes.Container.SelectedContainer;
                DialogResult dr = MessageBox.Show($"Are you sure you want to delete {currentSelectedContainer.ToString()}?\nThis action is irreversible.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    await ArcherTools_0._0._1.classes.Container.DeleteContainerFileAsync(currentSelectedContainer);
                    ArcherTools_0._0._1.classes.Container.RemoveContainer(currentSelectedContainer);
                    _instanceForm.Invalidate();
                }
            }
        }


    }
}
