using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb; 

namespace CAN_SAT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            tabControl1.Dock = DockStyle.Fill;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                    serialPort1.Close();
                serialPort1.PortName = comboBox1.Text;
                serialPort1.Open();
                label1.Text = "connected";
                button1.Enabled = false;
                button2.Enabled = true;
                comboBox1.Enabled = false;
                textBox1.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "error");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_CAN_SATDataSet1.meassures' table. You can move, or remove it, as needed.
            this.meassuresTableAdapter1.Fill(this._CAN_SATDataSet1.meassures);
            // TODO: This line of code loads data into the '_CAN_SATDataSet.meassures' table. You can move, or remove it, as needed.
            this.meassuresTableAdapter.Fill(this._CAN_SATDataSet.meassures);
            string[] serialports = System.IO.Ports.SerialPort.GetPortNames();
            for (int i = 0; i <= serialports.Length - 1; i++)
            {
                comboBox1.Items.Add(serialports[i]);
            }
            button2.Enabled = false;

            try
            {
                string strDSN = "Provider=Microsoft.ACE.OLEDB.12.0;DataSource=C:\\Users\\mouhamed\\Documents\\Visual Studio 2010\\Projects\\CAN-SAT\\CAN-SAT\\bin\\Debug\\CAN-SAT.accdb;Persist Security Info=False";
                // string strDSN = "Provider=Microsoft.Jet.OLEDB.4.0;DataSource=C:\\Users\\mouhamed\\Documents\\Visual Studio 2010\\Projects\\CAN-SAT\\CAN-SAT\\CAN-SAT.accdb;"; 
                string strSQL = "INSERT INTO meassures(altitude, latitude ) VALUES('60', '32')";
                OleDbConnection myConn = new OleDbConnection(strDSN);
                myConn.ConnectionString = strDSN;
               
                myConn.Open();
                OleDbCommand myCmd = new OleDbCommand(strSQL, myConn);
                myCmd.CommandText = strSQL;
                
                myCmd.ExecuteNonQuery();
                myConn.Close();
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                
                
                string port_data = serialPort1.ReadLine();
                Invoke(new Action(() => richTextBox1.AppendText(port_data)));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            serialPort1.Close();
            label1.Text = "disconnected";
            button1.Enabled = true;
            button2.Enabled = false;
            comboBox1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {
                try
                {
                    serialPort1.WriteLine("Remote Port:" + textBox1.Text + Environment.NewLine);
                    richTextBox1.AppendText("This Port:" + textBox1.Text + Environment.NewLine);
                    textBox1.Text = string.Empty;
                    textBox1.Focus();
                    richTextBox1.ScrollToCaret();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                return;
            }
        
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                e.KeyChar = '\0';
                button3_Click(null, null);

            }
        }

        private void meassuresDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
