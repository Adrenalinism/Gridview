using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grid_Second_Try
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {           
            dataGridView1.DataSource = peopleDataSet.People;
            // TODO: This line of code loads data into the 'peopleDataSet.People' table. You can move, or remove it, as needed.
            this.peopleTableAdapter.Fill(this.peopleDataSet.People);

            comboBox1.Text = "Choose a column to search:";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("convert([ID Number], 'System.String') LIKE '*{0}*'", textBox1.Text);
            if (comboBox1.SelectedIndex == 1)
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Surname LIKE '*{0}*'", textBox1.Text);
            if (comboBox1.SelectedIndex == 2)                
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '*{0}*'", textBox1.Text);
            if (comboBox1.SelectedIndex == 3)
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("convert(DOB, 'System.String') LIKE '*{0}*'", textBox1.Text);
            if (comboBox1.SelectedIndex == 4)
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("University LIKE '*{0}*'", textBox1.Text);
            if (comboBox1.SelectedIndex == 5)
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Program LIKE '*{0}*'", textBox1.Text);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
            if (peopleDataSet.People.GetChanges() != null)
            { 
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to save ALL changes to the grid?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    this.peopleTableAdapter.Update(this.peopleDataSet.People);
                }
            }
            Close();
        }

        private void button2_Click(object sender, EventArgs e) // deletes search box contents and updates database if no changes were detected
        {
            textBox1.Text = String.Empty;
            if (peopleDataSet.People.GetChanges() != null)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save the changes you've made?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    this.peopleTableAdapter.Update(this.peopleDataSet.People);
                }
            }
            else
            {
                MessageBox.Show("No chnages were detected.","Announcement");
            }

        }

        private void button3_Click(object sender, EventArgs e) // deletes row if its not new
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (!row.IsNewRow)
                    dataGridView1.Rows.Remove(row);
            }
        }
    }
}
