using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace csv22
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Guardar como CSV"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                // Agregar encabezados
                string[] columnNames = new string[dataGridView1.Columns.Count];
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    columnNames[i] = dataGridView1.Columns[i].HeaderText;
                }
                sb.AppendLine(string.Join(",", columnNames));

                // Agregar filas
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (!dataGridView1.Rows[i].IsNewRow)
                    {
                        string[] rowData = new string[dataGridView1.Columns.Count];
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            rowData[j] = dataGridView1.Rows[i].Cells[j].Value?.ToString() ?? "";
                        }
                        sb.AppendLine(string.Join(",", rowData));
                    }
                }

                File.WriteAllText(saveFileDialog.FileName, sb.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Abrir CSV"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] csvLines = File.ReadAllLines(openFileDialog.FileName);
                if (csvLines.Length > 0)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();

                    // Agregar encabezados
                    string[] headers = csvLines[0].Split(',');
                    foreach (string header in headers)
                    {
                        dataGridView1.Columns.Add(header, header);
                    }

                    // Agregar filas
                    for (int i = 1; i < csvLines.Length; i++)
                    {
                        string[] rowData = csvLines[i].Split(',');
                        dataGridView1.Rows.Add(rowData);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(); // Añade una nueva fila vacía
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //delete all rows
            //delete all columns
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
        }
    }
}
