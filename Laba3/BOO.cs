using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices.ComTypes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Laba3
{
    public partial class BOO : Form
    {
        Loader loader = new Loader(); // экземпляр Loader
        List<CarAndTruck> carModels = new List<CarAndTruck>(); 
        List<CarAndTruck> DoubleC = new List<CarAndTruck> (); // список для проверки открыта ли уже форма
        List<Table2> Forms = new List<Table2> (); // список, который содержит в себе открытые формы
        List<CarAndTruck> ForRemove = new List<CarAndTruck>(); // Список, который хранит загруженные машины
        Dictionary<CarAndTruck, List<string>> Comb = new Dictionary<CarAndTruck, List<string>> ();// Словарь, который хранит все модели для отдельных марок (отдельно машины, грузовики и самолеты)
        public BOO()
        {
            InitializeComponent();
            bindingCars.DataSource = carModels;
            dataGridView1.DataSource = bindingCars;
            dataGridView1.Columns["CarType"].Visible = false;
            DataGridViewComboBoxColumn cmbCol = new DataGridViewComboBoxColumn();
            cmbCol.HeaderText = "CarType";
            cmbCol.Name = "CarTip";
            cmbCol.Items.AddRange("passenger car", "Truck", "Airplane");
            dataGridView1.Columns.Add(cmbCol);
            dataGridView1.Columns["CarTip"].DisplayIndex = dataGridView1.ColumnCount - 1;
            pictureBox1.Image = Image.FromFile("C:/Users/Артём Морозов/Desktop/Хлам/кот.gif");
        }          
        private void SetupDataGridView()
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Green;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.PapayaWhip;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font =
                new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView1.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView1.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.GridColor = Color.Black;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns["CarTip"].DefaultCellStyle.Font =
                new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Italic);
            dataGridView1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;        
            dataGridView1.Columns["CarName"].Width = 100;
            dataGridView1.Columns["CarModel"].Width = 100;
            dataGridView1.Columns["CarTip"].Width = 100;
            dataGridView1.Columns["Рorsepower"].Width = 100;
            dataGridView1.Columns["MaxSpeed"].Width = 100;
            dataGridView1.MultiSelect = false;                     
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (Convert.ToString(dataGridView1["CarTip", i].Value) == "passenger car")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                    }
                    else if (Convert.ToString(dataGridView1["CarTip", i].Value) == "Truck")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LimeGreen;
                    }
                    else if (Convert.ToString(dataGridView1["CarTip", i].Value) == "Airplane")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.YellowGreen;
                    }
                    else
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }  
            // при изменении строки, таблица, относящаяся к этой строке со старыми данными закрывается 
            if (e.RowIndex > -1)
            {
                if (dataGridView1.Rows[e.RowIndex].Tag != null && DoubleC.Contains((CarAndTruck)dataGridView1.Rows[e.RowIndex].Tag) == true)
                {
                    int i = DoubleC.IndexOf((CarAndTruck)dataGridView1.Rows[e.RowIndex].Tag);
                    Application.OpenForms[i + 1].Dispose();
                    DoubleC.RemoveAt(i);
                    Forms.RemoveAt(i);
                }
            }
            if (e.RowIndex > -1)
            {
                int count = dataGridView1.Rows[e.RowIndex].Cells.Count;
                var value = dataGridView1.Rows[e.RowIndex].Cells[count - 1].Value;
                if (value != null)
                {
                    dataGridView1.Rows[e.RowIndex].Tag = new CarAndTruck((string)dataGridView1.Rows[e.RowIndex].Cells["CarName"].Value, (string)dataGridView1.Rows[e.RowIndex].Cells["CarModel"].Value, (string)dataGridView1.Rows[e.RowIndex].Cells["Рorsepower"].Value, (string)dataGridView1.Rows[e.RowIndex].Cells["MaxSpeed"].Value, (string)dataGridView1.Rows[e.RowIndex].Cells["CarTip"].Value);
                }
                dataGridView1.Rows[e.RowIndex].Cells["CarType"].Value = dataGridView1.Rows[e.RowIndex].Cells["CarTip"].Value;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++) // удаляю одинаковые строки
            {
                if(dataGridView1.Rows[i].Tag != null)
                {
                    CarAndTruck Strocka = (CarAndTruck)dataGridView1.Rows[i].Tag;
                    for (int q = i + 1; q < dataGridView1.Rows.Count; q++)
                    {
                        if (dataGridView1.Rows[q].Tag != null)
                        {
                            CarAndTruck Strocka2 = (CarAndTruck)dataGridView1.Rows[q].Tag;
                            if (Strocka2.CarModel == Strocka.CarModel && Strocka2.CarType == Strocka.CarType && Strocka2.CarName == Strocka.CarName)
                            {
                                dataGridView1.Rows.RemoveAt(q);
                            }
                        }
                    }
                }
            }
        }
        private  void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((dataGridView1.CurrentCell.RowIndex != -1) && ((dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value) != null)
                && ((dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value) != null) && ((dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value) != null)
                && ((dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value) != null) && ((dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value) != null)
                 && (((string)dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value) != "")
                && (((string)dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value) != "") && (((string)dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value) != "")
                && (((string)dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value) != "") && (((string)dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value) != ""))
            {
                Comb.Clear(); // создание словаря для ComB
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((CarAndTruck)dataGridView1.Rows[i].Tag != null)
                    {
                        CarAndTruck stroka = (CarAndTruck)dataGridView1.Rows[i].Tag;
                        Comb.Add(stroka, new List<string>());
                        for (int q = 0; q < dataGridView1.Rows.Count; q++)
                        {
                            if (dataGridView1.Rows[q].Tag != null)
                            {
                                CarAndTruck stroka2 = (CarAndTruck)dataGridView1.Rows[q].Tag;
                                if (stroka2.CarName == stroka.CarName && stroka2.CarType == stroka.CarType && stroka2.CarType != null && stroka2.CarType != "" && stroka2.CarName != null && stroka2.CarName != "")
                                {
                                    Comb[stroka].Add(stroka2.CarModel);
                                }
                            }
                        }
                    }
                }
                CarAndTruck Carcik = (CarAndTruck)dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Tag;
                if(DoubleC.Contains(Carcik) == false) // проверка, открыта ли уже форма
                {
                    DoubleC.Add(Carcik);  
                    ForRemove.Add(Carcik);
                    Table2 tablee = new Table2(Carcik, loader, Comb, DoubleC, Forms, ForRemove);
                    Forms.Add(tablee);
                    tablee.Show();
                }                             
            }

        }      
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void сохранитьСписокМарокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveCars = new SaveFileDialog();
            saveCars.Filter = "Extensible Markup files (*.xml)|*.xml|All files(*.*)|*.*";
            saveCars.FilterIndex = 0;
            saveCars.RestoreDirectory = true;
            saveCars.InitialDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            saveCars.Title = "Сохранение списка машин";
            if (saveCars.ShowDialog() == DialogResult.OK)
            {
                if (saveCars.FileName != "")
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<CarAndTruck>));
                    using (FileStream fs = new FileStream(saveCars.FileName, FileMode.OpenOrCreate))
                    {
                        xmlSerializer.Serialize(fs, carModels);
                    }
                }
                else
                {
                    MessageBox.Show("Вы ввели пустое имя файла", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openCars = new OpenFileDialog();
            openCars.Filter = "Extensible Markup files (*.xml)|*.xml|All files(*.*)|*.*";
            openCars.FilterIndex = 0;
            openCars.RestoreDirectory = true;
            openCars.InitialDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (openCars.ShowDialog() == DialogResult.OK)
            {
                if (openCars.FileName != "")
                {
                    loader.car_truck.Clear();
                    DoubleC.Clear();
                    Forms.Clear();
                    ForRemove.Clear();
                    Comb.Clear();
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<CarAndTruck>));
                    using (FileStream fs = new FileStream(openCars.FileName, FileMode.OpenOrCreate))
                    {
                        carModels = xmlSerializer.Deserialize(fs) as List<CarAndTruck>;
                        bindingCars.DataSource = null;
                        bindingCars.DataSource = carModels;
                        dataGridView1.DataSource = bindingCars;
                        dataGridView1.Columns["CarType"].Visible = false;
                        dataGridView1.Columns["CarTip"].DisplayIndex = dataGridView1.ColumnCount - 1;
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            dataGridView1.Rows[i].Cells["CarTip"].Value = dataGridView1.Rows[i].Cells["CarType"].Value;
                            dataGridView1.Rows[i].Tag = new CarAndTruck((string)dataGridView1.Rows[i].Cells["CarName"].Value, (string)dataGridView1.Rows[i].Cells["CarModel"].Value, (string)dataGridView1.Rows[i].Cells["Рorsepower"].Value, (string)dataGridView1.Rows[i].Cells["MaxSpeed"].Value, (string)dataGridView1.Rows[i].Cells["CarTip"].Value);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Вы ввели пустое имя файла", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int q = 0;
            int range = Application.OpenForms.Count;
            for (int i = 1; i < range; i++)
            {
                Application.OpenForms[i - q].Dispose();
                q++;
            }
            DoubleC.Clear();
            Forms.Clear();
        }
    }
}
