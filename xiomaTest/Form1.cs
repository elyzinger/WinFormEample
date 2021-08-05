using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xiomaTest.Service;
using Newtonsoft.Json;

namespace xiomaTest
{
    public partial class Form1 : Form
    {

        Service.WebService1 ws = new Service.WebService1();
        DataTable dt = new DataTable();
       
        string name;
        string lastName;
        string TZ;
        string BD;
        string job;
        string department;
        string startDate;

        public Form1()
        {
            InitializeComponent();
            inTZ.TextChanged += TextBox_TextChanged;
            inName.TextChanged += TextBox_TextChanged;
            inLastName.TextChanged += TextBox_TextChanged;
            inJob.TextChanged += TextBox_TextChanged;
            inDep.TextChanged += TextBox_TextChanged;
            inCity.TextChanged += TextBox_TextChanged;
            inStreet.TextChanged += TextBox_TextChanged;
            inHouseNumber.TextChanged += TextBox_TextChanged;
            inPostal.TextChanged += TextBox_TextChanged;
            displayTable();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetDefaultValues();
            btnSave.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Address home = new Address(Convert.ToInt32(inTZ.Text), 1, inCity.Text, inStreet.Text, Convert.ToInt32(inHouseNumber.Text), Convert.ToInt32(inPostal.Text));     
            Address shipping = new Address(Convert.ToInt32(inTZ.Text), 2, inShipCity.Text, inShipStreet.Text, Convert.ToInt32(inShipNum.Text), Convert.ToInt32(inShipPostal.Text));
            Employee emp = new Employee(int.Parse(inTZ.Text), inName.Text, inLastName.Text,DateTime.Parse(inBT.Text) , inJob.Text, inDep.Text, DateTime.Parse(inStartDate.Text), inFreeText.Text, shipping, home);
            label5.Text = ws.AddEmployee(emp);
            displayTable();
            clearInput();
            SetDefaultValues();

    }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void displayTable()
        {
           
            dt = JsonConvert.DeserializeObject<DataTable>(ws.GetEmployeeTable());
            dtDataGridView.DataSource = dt;
 
        }
        public void clearInput()
        {
            inHouseNumber.Text = "";
            inShipCity.Text = "";
            inName.Text = "";
            inLastName.Text = "";
            inTZ.Text = "";
            inShipPostal.Text = "";
            inShipNum.Text = "";
            inShipStreet.Text = "";
            inStartDate.Text = "";
            inJob.Text = "";
            inDep.Text = "";
            inStartDate.Text = "";
            inFreeText.Text = "";
            inBT.Text = "";
            inCity.Text = "";
            inStreet.Text = "";
            inPostal.Text = "";
           
        }
        public void SetDefaultValues()
        {
            inHouseNumber.Text = "0";
            inShipNum.Text = "0";
            inPostal.Text = "0";
            inShipPostal.Text = "0";
        }
        private void button2_Click(object sender, EventArgs e)
        {

            dt = JsonConvert.DeserializeObject<DataTable>(ws.SearchEmployeeByName(inSearch.Text));
           // dtDataGridView.Update();
           //dtDataGridView.Update();
            dtDataGridView.DataSource = dt;
        }

   

        private void inTZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void inHouseNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void inPostal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void inShipNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void inShipPostal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = ValidateTextBoxes();
            if (btnSave.Enabled)
            {
                btnSave.BackColor = Color.Blue;
            }
            else
            {
                btnSave.BackColor = Color.DarkGray;
               // label5.Text = " חובה למלא את הפרטים המסומנים";
            }
            //Anything else you might want to do...
        }
        private bool ValidateTextBoxes()
        {
            if (string.IsNullOrEmpty(inLastName.Text) || string.IsNullOrEmpty(inName.Text) || string.IsNullOrEmpty(inPostal.Text)
                    || string.IsNullOrEmpty(inTZ.Text) || string.IsNullOrEmpty(inJob.Text) || string.IsNullOrEmpty(inDep.Text)
                    || string.IsNullOrEmpty(inCity.Text) || string.IsNullOrEmpty(inStreet.Text) || string.IsNullOrEmpty(inHouseNumber.Text))
            {
                return false;
            }

            //Any other validation you may want... e.g length, regex pattern etc.

            return true;
        }
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dtDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void btnGetEmp_Click(object sender, EventArgs e)
        {
            //inTZ.Text;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Address home = new Address(Convert.ToInt32(inTZ.Text), 1, inCity.Text, inStreet.Text, Convert.ToInt32(inHouseNumber.Text), Convert.ToInt32(inPostal.Text));
            Address shipping = new Address(Convert.ToInt32(inTZ.Text), 2, inShipCity.Text, inShipStreet.Text, Convert.ToInt32(inShipNum.Text), Convert.ToInt32(inShipPostal.Text));
            Employee emp = new Employee(int.Parse(inTZ.Text), inName.Text, inLastName.Text, DateTime.Parse(inBT.Text), inJob.Text, inDep.Text, DateTime.Parse(inStartDate.Text), inFreeText.Text, shipping, home);
            label5.Text = ws.UpdateEmployeeByTZ(emp);
            displayTable();
            clearInput();
            SetDefaultValues();
        }
    }
}
