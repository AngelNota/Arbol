using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void abrirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Archivos de texto|*txt";
            DialogResult res = od.ShowDialog();

            String contenido = File.ReadAllText(od.FileName);
            LeerArchivo(od.FileName);
             
        }

        private void LeerArchivo(String ruta)
        {
            try
            {
                using (StreamReader sr = new StreamReader(ruta))
                {
                    String linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        
                        if (linea.Contains("\t"))
                            treeView1.Nodes.Add(linea + Environment.NewLine);
                            
                        else
                            treeView1.Nodes.Add(linea + Environment.NewLine);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error papi");
            }
        }
    }
}
