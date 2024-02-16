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
                        {
                            // Dividir la línea en partes utilizando el carácter de tabulación como delimitador
                            string[] partes = linea.Split('\t');

                            // Obtener el nodo padre actual (último nodo agregado o el nodo raíz si no hay nodos)
                            TreeNode nodoPadre = treeView1.Nodes.Count > 0 ? treeView1.Nodes[treeView1.Nodes.Count - 1] : null;

                            // Agregar un nuevo nodo hijo para cada parte
                            foreach (string parte in partes)
                            {
                                // Crear un nuevo nodo hijo
                                TreeNode nodoHijo = new TreeNode(parte);

                                // Agregar el nuevo nodo hijo al nodo padre
                                if (nodoPadre != null)
                                    nodoPadre.Nodes.Add(nodoHijo);
                                else
                                    // Si no hay nodo padre, agregar al árbol directamente
                                    treeView1.Nodes.Add(nodoHijo);
                                // Actualizar el nodo padre para el próximo ciclo
                                nodoPadre = nodoHijo;
                            }
                        }
                        else
                            // Si no hay tabulación, agregar la línea directamente como un nuevo nodo
                            treeView1.Nodes.Add(linea);
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
