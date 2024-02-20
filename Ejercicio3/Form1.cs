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

            if (res == DialogResult.OK)
            {
                // Limpiar nodos existentes en el TreeView
                treeView1.Nodes.Clear();

                String contenido = File.ReadAllText(od.FileName);
                LeerArchivo(od.FileName);
            }
        }

        private void LeerArchivo(String ruta)
        {
            try
            {
                using (StreamReader sr = new StreamReader(ruta))
                {
                    String linea;
                    TreeNode nodoPadre = null;

                    while ((linea = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(linea))
                        {
                            // Contar la cantidad de tabulaciones al principio de la línea
                            int nivel = 0;
                            while (nivel < linea.Length && linea[nivel] == '\t')
                            {
                                nivel++;
                            }

                            // Crear un nuevo nodo con el contenido de la línea sin tabulaciones
                            string contenido = linea.Substring(nivel).Trim();
                            TreeNode nuevoNodo = new TreeNode(contenido);

                            if (nivel == 0)
                            {
                                // Si no hay tabulaciones, agregar el nodo directamente al árbol
                                treeView1.Nodes.Add(nuevoNodo);
                                nodoPadre = nuevoNodo; // Actualizar el nodo padre
                            }
                            else
                            {
                                // Si hay tabulaciones, agregar el nodo como hijo del nodo padre adecuado
                                if (nodoPadre != null)
                                {
                                    // Asegurarse de que el nodoPadre tenga el nivel correcto antes de agregar el nodo hijo
                                    while (nivel <= ObtenerNivel(nodoPadre))
                                    {
                                        nodoPadre = nodoPadre.Parent;
                                    }

                                    nodoPadre.Nodes.Add(nuevoNodo);
                                }
                            }

                            // Actualizar el nodo padre para el próximo ciclo
                            nodoPadre = nuevoNodo;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error papi");
            }
        }
        
        // Método para obtener el nivel de un nodo en el árbol
        private int ObtenerNivel(TreeNode nodo)
        {
            int nivel = 0;
            while (nodo.Parent != null)
            {
                nivel++;
                nodo = nodo.Parent;
            }
            return nivel;
        }
    }
}