using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba3
{
    public partial class Table2 : Form
    {
        Loader loader = new Loader();
        Thread Thread2;
        Dictionary<CarAndTruck, List<CarModel>> car_truck = new Dictionary<CarAndTruck, List<CarModel>>(); 
        CarAndTruck Car_Truck_Airplane = new CarAndTruck();
        List<CarAndTruck> DoubleC = new List<CarAndTruck>();
        List<CarAndTruck> ForRemove = new List<CarAndTruck>();
        List<Table2> Forms = new List<Table2>();
        Dictionary<CarAndTruck, List<string>> Comb = new Dictionary<CarAndTruck, List<string>>();
        public Table2(CarAndTruck Car_Truck_Airplane, Loader loader, Dictionary<CarAndTruck, List<string>> Comb , List<CarAndTruck> DoubleC, List<Table2> Forms, List<CarAndTruck> ForRemove)
        {
            InitializeComponent();
            this.Car_Truck_Airplane = Car_Truck_Airplane;
            this.loader = loader;
            this.Comb = Comb;
            this.DoubleC = DoubleC;
            this.Forms = Forms;
            this.ForRemove = ForRemove;
        }
        private void Table2_Load(object sender, EventArgs e)
        {
            string Tipe = Car_Truck_Airplane.CarType;
            if (Tipe == "passenger car")
            {
                SetupDataGridView2(2);
                dataGridView2.Visible = false;
                ProcessOfLoad.Visible = true;
                ProcessOfLoad.Value = 0;
                TimerForLoad.Start();
                Thread2 = new Thread(delegate ()
                {
                    car_truck = loader.Load(Car_Truck_Airplane);
                });
                Thread2.Start();
                ProcessOfLoad.Visible = true;
            }
            else if (Tipe == "Truck")
            {
                SetupDataGridView2(1);
                dataGridView2.Visible = false;
                ProcessOfLoad.Visible = true;
                ProcessOfLoad.Value = 0;
                TimerForLoad.Start();
                Thread2 = new Thread(delegate ()
                {
                    car_truck = loader.Load(Car_Truck_Airplane);
                });
                Thread2.Start();
                ProcessOfLoad.Visible = true;
            }
            else if (Tipe == "Airplane")
            {
                SetupDataGridView2(3);
                dataGridView2.Visible = false;
                ProcessOfLoad.Visible = true;
                ProcessOfLoad.Value = 0;
                TimerForLoad.Start();
                Thread2 = new Thread(delegate ()
                {
                    car_truck = loader.Load(Car_Truck_Airplane);
                });
                Thread2.Start();
                ProcessOfLoad.Visible = true;
            }
        }
        private void SetupDataGridView2(int var)
        {
            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView2.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView2.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView2.GridColor = Color.Black;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.ColumnCount = 3;
            DataGridViewComboBoxColumn cmbCol = new DataGridViewComboBoxColumn();
            cmbCol.HeaderText = "Mодель";
            cmbCol.Name = "Model";
            ForSetub(cmbCol);
            if (var == 1)
            {
                dataGridView2.Columns.Add(cmbCol);
                dataGridView2.Columns["Model"].DisplayIndex = 0;
                dataGridView2.Columns[0].Name = "Регистрационный номер";
                dataGridView2.Columns[1].Name = "Количество колес";
                dataGridView2.Columns[2].Name = "Объем кузова";
                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.LimeGreen;
            }
            else if (var == 2)
            {
                dataGridView2.Columns.Add(cmbCol);
                dataGridView2.Columns["Model"].DisplayIndex = 0;
                dataGridView2.Columns[0].Name = "Регистрационный номер";
                dataGridView2.Columns[1].Name = "Название мультимедиа";
                dataGridView2.Columns[2].Name = "Кол-во подушек безопастности";
                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.Pink;
            }
            else if (var == 3)
            {
                dataGridView2.ColumnCount = 4;
                dataGridView2.Columns.Add(cmbCol);
                dataGridView2.Columns["Model"].DisplayIndex = 0;              
                dataGridView2.Columns[0].Name = "Регистрационный номер";
                dataGridView2.Columns[1].Name = "Кол-во двигателей";
                dataGridView2.Columns[2].Name = "Длина Самолета";
                dataGridView2.Columns[3].Name = "Вместимость пасажиров";
                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.YellowGreen;
            }
            dataGridView2.Columns[0].ReadOnly = true;
            dataGridView2.Columns[1].ReadOnly = true;
            dataGridView2.Columns[2].ReadOnly = true;
            label1.Text = Car_Truck_Airplane.CarName + " " + Car_Truck_Airplane.CarModel;
        }
        public void ForSetub(DataGridViewComboBoxColumn cmbCol) // ComB - заполняю
        {
            for (int i = 0; i < Comb[Car_Truck_Airplane].Count; i++)
            {
                cmbCol.Items.Add(Comb[Car_Truck_Airplane][i]);
            }
        }
        private void TimerForLoad_Tick_1(object sender, EventArgs e)
        {
            int process = loader.getProcess();
            ProcessOfLoad.Value = process;
            if (process == 100)
            {              
                if (car_truck.Keys.Count > 0)
                {
                    if (car_truck.Keys.Contains(Car_Truck_Airplane))
                    {
                        for (int i = 0; i < car_truck[Car_Truck_Airplane].Count; ++i)
                        {
                            if (car_truck[Car_Truck_Airplane][i] is Car CarTruckk)
                            {
                                int rowId = dataGridView2.Rows.Add();
                                dataGridView2.Rows[rowId].Cells["Model"].Value = CarTruckk.CarModel1;
                                dataGridView2.Rows[rowId].Cells[0].Value = CarTruckk.RegistrationNumber1;
                                dataGridView2.Rows[rowId].Cells[1].Value = CarTruckk.NameMultimedia1;
                                dataGridView2.Rows[rowId].Cells[2].Value = CarTruckk.NumberOfAirbags1;
                            }
                            else if (car_truck[Car_Truck_Airplane][i] is Truck Truckk)
                            {
                                int rowId = dataGridView2.Rows.Add();
                                dataGridView2.Rows[rowId].Cells["Model"].Value = Truckk.CarModel1;
                                dataGridView2.Rows[rowId].Cells[0].Value = Truckk.RegistrationNumber1;
                                dataGridView2.Rows[rowId].Cells[1].Value = Truckk.NumberOfWheels1;
                                dataGridView2.Rows[rowId].Cells[2].Value = Truckk.BodyVolume1;
                            }
                            else if (car_truck[Car_Truck_Airplane][i] is Airplane Airplane1)
                            {
                                int rowId = dataGridView2.Rows.Add();
                                dataGridView2.Rows[rowId].Cells["Model"].Value = Airplane1.CarModel1;
                                dataGridView2.Rows[rowId].Cells[0].Value = Airplane1.RegistrationNumber1;
                                dataGridView2.Rows[rowId].Cells[1].Value = Airplane1.NumberOfEngines1;
                                dataGridView2.Rows[rowId].Cells[2].Value = Airplane1.Aircraftlength1;
                                dataGridView2.Rows[rowId].Cells[3].Value = Airplane1.PassengerCapacity1;
                            }
                        }
                    }
                    dataGridView2.Visible = true;
                    TimerForLoad.Stop();
                }
            }
        }

        private void Table2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DoubleC.Remove(Car_Truck_Airplane);
            for (int i = 0; i < Forms.Count; i++)
            {
                if (Forms[i].Car_Truck_Airplane == Car_Truck_Airplane)
                {
                    Forms.RemoveAt(i);
                    break;
                }
            }
        }
        public CarAndTruck getAdd(string Marka, string Tip) // Достает машину из загруженных
        {
            for (int i = 0; i < ForRemove.Count; i++)
            {
                if (Car_Truck_Airplane.CarName == ForRemove[i].CarName && ForRemove[i].CarModel == Marka && ForRemove[i].CarType == Tip)
                {
                    return ForRemove[i];
                }
            }
            return null;
        }
        public CarAndTruck getAdd2(string Marka, string Tip) // Достает машину из открытых форм
        {
            for (int i = 0; i < DoubleC.Count; i++)
            {
                if (Car_Truck_Airplane.CarName == DoubleC[i].CarName && DoubleC[i].CarModel == Marka && DoubleC[i].CarType == Tip)
                {
                    return DoubleC[i];
                }
            }
            return null;
        }
        public void sdvig(int index, CarAndTruck TTT  ) // сдвигаю элементы массива, для правильного удаления данных форм
        {
            DoubleC.RemoveAt(index);
            DoubleC.Add(TTT);
            Forms.Add(Forms[index]);
            Forms.RemoveAt(index);
        }
        //КОСТЫЛЬ
        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string Marka1 = (string)dataGridView2.Rows[e.RowIndex].Cells["Model"].Value;
            string Tip1 = Car_Truck_Airplane.CarType;
            if (Marka1 != Car_Truck_Airplane.CarModel)
            {
                if(getAdd(Marka1, Tip1) != null) // перекидываю в нужные списки марки 
                {
                    if (car_truck[Car_Truck_Airplane][e.RowIndex] is Car CarTruckk)
                    {
                        CarTruckk.CarModel1 = getAdd(Marka1, Tip1).CarModel;
                        car_truck[getAdd(Marka1, Tip1)].Add(CarTruckk);
                    }
                    else if (car_truck[Car_Truck_Airplane][e.RowIndex] is Truck Truckk)
                    {
                        Truckk.CarModel1 = getAdd(Marka1, Tip1).CarModel;
                        car_truck[getAdd(Marka1, Tip1)].Add(Truckk);
                    }
                    else if (car_truck[Car_Truck_Airplane][e.RowIndex] is Airplane Airplane1)
                    {
                        Airplane1.CarModel1 = getAdd(Marka1, Tip1).CarModel;
                        car_truck[getAdd(Marka1, Tip1)].Add(Airplane1);
                    }
                }
                loader.car_truck[Car_Truck_Airplane].RemoveAt(e.RowIndex);
                //Перезагрузка форм
                int index1 = DoubleC.IndexOf(Car_Truck_Airplane);
                Application.OpenForms[index1+1].Dispose();
                Table2 tablee = new Table2(Forms[index1].Car_Truck_Airplane, loader, Forms[index1].Comb, Forms[index1].DoubleC, Forms[index1].Forms, Forms[index1].ForRemove);
                tablee.Location = Forms[index1].Location;
                tablee.Show();
                sdvig(index1, Car_Truck_Airplane);
                if (getAdd2( Marka1, Tip1)!= null)
                {
                    int index2 = DoubleC.IndexOf(getAdd2(Marka1, Tip1));
                    Application.OpenForms[index2 + 1].Dispose();
                    Table2 tablee2 = new Table2(Forms[index2].Car_Truck_Airplane, loader, Forms[index2].Comb, Forms[index2].DoubleC, Forms[index2].Forms, Forms[index2].ForRemove);
                    tablee2.Location = Forms[index2].Location;
                    tablee2.Show();
                    sdvig(index2, getAdd2(Marka1, Tip1));
                }                
            }
        }
    }
    
}
