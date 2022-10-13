using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryE
{
    public partial class Form1 : Form
    {
        //Instancias de clases
        ParticionEstatica miParticionEstatica = new ParticionEstatica();
        Listas<Particion> LP = new Listas<Particion>();
        Listas<Trabajo> LT = new Listas<Trabajo>();
        Listas<Estado> LE = new Listas<Estado>();
        //Variable global, para nombre de la particion
        int C = 1, i = 0;
        bool O = false;
        int Libre;
        int Resto = 0;
        int MemoriaTotal = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuChartCanvas1_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboMemoria.Text == "Kb")
                {
                    miParticionEstatica.TamañoMemoria = int.Parse(txtMemoria.Text);
                }
                if (cboMemoria.Text == "Mb")
                {
                    miParticionEstatica.TamañoMemoria = int.Parse(txtMemoria.Text) * 1024;
                }
                if (cboMemoria.Text == "Gb")
                {
                    miParticionEstatica.TamañoMemoria = (int.Parse(txtMemoria.Text) * 1024) * 1024;
                }
                MemoriaTotal = miParticionEstatica.TamañoMemoria;
                MessageBox.Show("Tamaño capturado", "Memoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Grafica
                GraficoTiempoReal.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
                GraficoTiempoReal.Titles.Add("Memoria");
                GraficoTiempoReal.Series.Add("Memoria").ChartType =
                    System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                GraficoTiempoReal.Series["Memoria"].Points.AddXY("Memoria", miParticionEstatica.TamañoMemoria.ToString());
                GraficoTiempoReal.Series["Memoria"].IsValueShownAsLabel = true;
            }
            catch (Exception x)
            {
                MessageBox.Show("Error: " + x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnAceptar1.Enabled = false;
        }

        private void btnAceptar2_Click(object sender, EventArgs e)
        {
            if (cboSO.Text == "Kb")
            {
                miParticionEstatica.TamañoSO = int.Parse(txtSO.Text);
            }
            if (cboSO.Text == "Mb")
            {
                miParticionEstatica.TamañoSO = int.Parse(txtSO.Text) * 1024;
            }
            if (cboSO.Text == "Gb")
            {
                miParticionEstatica.TamañoSO = (int.Parse(txtSO.Text) * 1024) * 1024;
            }
            if (miParticionEstatica.TamañoSO <= Math.Round(Convert.ToDouble
                    (miParticionEstatica.TamañoMemoria * 0.30), 2))
            {
                MessageBox.Show("Tamaño capturado", "Sistema operativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                miParticionEstatica.TamañoMemoria = miParticionEstatica.TamañoMemoria - miParticionEstatica.TamañoSO;
                Resto = miParticionEstatica.TamañoMemoria;

                txtPartNombre.Text = "P" + C.ToString();

                //Grafica
                GraficoTiempoReal.Series.Clear();
                GraficoTiempoReal.Titles.Clear();
                GraficoTiempoReal.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
                GraficoTiempoReal.Titles.Add("Memoria");
                GraficoTiempoReal.Series.Add("SO").ChartType =
                    System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                GraficoTiempoReal.Series["SO"].Points.AddXY("Memoria", miParticionEstatica.TamañoSO.ToString());
                GraficoTiempoReal.Series["SO"].IsValueShownAsLabel = true;
                GraficoTiempoReal.Series.Add("Memoria").ChartType =
                    System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                GraficoTiempoReal.Series["Memoria"].Points.AddXY("Memoria", miParticionEstatica.TamañoMemoria.ToString());
                GraficoTiempoReal.Series["Memoria"].IsValueShownAsLabel = true;
            }
            else
            {
                MessageBox.Show("No puede ser mayor al 30% el tamaño", "Sistema operativo", MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
            }
            btnAceptar2.Enabled = false;
        }

        private void btnAceptar3_Click(object sender, EventArgs e)
        {
            Particion miParticion = new Particion();
            miParticion.Nombre = txtPartNombre.Text;
            if (cboParticiones.Text == "Kb")
            {
                miParticion.Tamaño = int.Parse(txtPartTamaño.Text);
            }
            if (cboParticiones.Text == "Mb")
            {
                miParticion.Tamaño = int.Parse(txtPartTamaño.Text) * 1024;
            }
            if (cboParticiones.Text == "Gb")
            {
                miParticion.Tamaño = (int.Parse(txtPartTamaño.Text) * 1024) * 1024;
            }
            if (miParticion.Tamaño <= Resto)
            {
                LP.Insertar(miParticion);
                miParticionEstatica.TamañoMemoria = miParticionEstatica.TamañoMemoria - miParticion.Tamaño;
                Resto = miParticionEstatica.TamañoMemoria;
                MessageBox.Show("Particion capturada", "Particiones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                C++;
                txtPartNombre.Text = "P" + C.ToString();
                if (Resto == 0)
                {
                    //grpParticiones.Enabled = false;
                    MessageBox.Show("No existe memoria disponible", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //Grafica
                GraficoTiempoReal.Series.Clear();
                GraficoTiempoReal.Titles.Clear();
                //GraficoTiempoReal.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
                GraficoTiempoReal.Titles.Add("Memoria");
                GraficoTiempoReal.Series.Add("SO").ChartType =
                    System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                GraficoTiempoReal.Series["SO"].Points.AddXY("Memoria", miParticionEstatica.TamañoSO.ToString());
                GraficoTiempoReal.Series["SO"].IsValueShownAsLabel = true;
                foreach (Particion P in LP)
                {
                    GraficoTiempoReal.Series.Add(P.Nombre).ChartType =
                        System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                    GraficoTiempoReal.Series[P.Nombre].Points.AddXY("Memoria", P.Tamaño.ToString());
                    GraficoTiempoReal.Series[P.Nombre].IsValueShownAsLabel = true;
                }
                if (miParticionEstatica.TamañoMemoria != 0)
                {
                    GraficoTiempoReal.Series.Add("Memoria").ChartType =
                    System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                    GraficoTiempoReal.Series["Memoria"].Points.AddXY("Memoria",
                        miParticionEstatica.TamañoMemoria.ToString());
                    GraficoTiempoReal.Series["Memoria"].IsValueShownAsLabel = true;
                }
                if (Libre != 0)
                {
                    GraficoTiempoReal.Series.Add("Libre").ChartType =
                    System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                    GraficoTiempoReal.Series["Libre"].Color = System.Drawing.Color.Gray;
                    GraficoTiempoReal.Series["Libre"].Points.AddXY("Memoria", Libre.ToString());
                    GraficoTiempoReal.Series["Libre"].IsValueShownAsLabel = true;
                }
            }
            else
            {
                MessageBox.Show("El tamaño rebasa la cantidad de memoria disponible", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }
        public void LlenarLE()
        {
            foreach (Particion P in LP)
            {
                Estado E = new Estado();
                E.NombreP = P.Nombre;
                E.TamañoP = P.Tamaño;
                E.NombreT = "-";
                E.Situacion = "Disponible";
                E.Desperdicio = 0;
                LE.Insertar(E);
            }
        }

        public void MostrarEstado()
        {
            dtgEstado.Rows.Clear();
            foreach (Estado E in LE)
            {
                dtgEstado.Rows.Add(E.NombreP, E.TamañoP, E.NombreT, E.Situacion, E.Desperdicio.ToString());
            }
        }

        //public void MostrarTrabajo()
        //{
        //    dtgTrabajosActivos.Rows.Clear();
        //    dtgTrabajosEspera.Rows.Clear();
        //    dtgTrabajosTJE.Rows.Clear();
        //    foreach (Trabajo T in LT)
        //    {
        //        if (T.Situacion == "Activa")
        //        {
        //            dtgTrabajosActivos.Rows.Add(T.Nombre, T.Tamaño, T.Situacion);
        //        }
        //        if (T.Situacion == "Espera")
        //        {
        //            dtgTrabajosEspera.Rows.Add(T.Nombre, T.Tamaño, T.Situacion);
        //        }
        //        if (T.Situacion == "Terminada" || T.Situacion == "J/E")
        //        {
        //            dtgTrabajosTJE.Rows.Add(T.Nombre, T.Tamaño, T.Situacion);
        //        }
        //    }
        //}

        private void AsignarTrabajo(Trabajo T)
        {
            bool Salir = false;
            bool Entro = true;
            if (T.Situacion == "Activa")
            {
                string Particion;
                foreach (Estado E1 in LE)
                {
                    Entro = true;
                    if (E1.Situacion == "Disponible")
                    {
                        Particion = E1.NombreP;
                        foreach (Particion P in LP)
                        {
                            if (P.Nombre == Particion)
                            {
                                if (P.Tamaño >= T.Tamaño)
                                {
                                    foreach (Estado E2 in LE)
                                    {
                                        if (E2.NombreP == P.Nombre)
                                        {
                                            E2.NombreT = T.Nombre;
                                            E2.Situacion = "Ocupada";
                                            E2.Desperdicio = P.Tamaño - T.Tamaño;
                                            Salir = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    Entro = false;
                                }
                            }
                            if (Salir == true)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        Entro = false;
                    }
                    if (Salir == true)
                    {
                        break;
                    }
                }
                if (Entro == false)
                {
                    T.Situacion = "Espera";
                }
            }
        }

        public void VerificarTrabajos()
        {
            int Cont = 0;
            foreach (Estado E in LE)
            {
                if (E.Situacion == "Ocupada")
                {
                    Cont = Cont + 1;
                }
            }
            if (Cont == LE.Contar())
            {
                O = true;
            }
            else
            {
                O = false;
            }
        }

        private void VerificarDuplicado(Trabajo T)
        {
            int Contador = 0;
            bool Salir = false;
            if (LT.Contar() != 1)
            {
                foreach (Trabajo Original in LT)
                {
                    if (T.Nombre == Original.Nombre)
                    {
                        if (Original.Situacion == "Activa")
                        {
                            foreach (Estado E in LE)
                            {
                                if (E.NombreT == Original.Nombre)
                                {
                                    E.NombreT = "-";
                                    E.Situacion = "Disponible";
                                    E.Desperdicio = 0;
                                    Original.Situacion = "Terminada";
                                    T.Situacion = "Terminada";
                                    VerificarEspera();

                                    Salir = true;
                                    break;
                                }
                            }
                        }
                        if (Original.Situacion == "Espera")
                        {
                            Original.Situacion = "Terminada";
                            T.Situacion = "Terminada";
                            VerificarEspera();

                            Salir = true;
                            break;
                        }
                    }
                    if (Salir == true)
                    {
                        break;
                    }
                    else
                    {
                        Contador++;
                    }
                }
            }
            if (Contador == LT.Contar())
            {
                AsignarTrabajo(T);
            }
            else
            {
                AsignarTrabajo(T);
            }
        }

        private void VerificarEspera()
        {
            foreach (Trabajo T in LT)
            {
                if (T.Situacion == "Espera")
                {
                    T.Situacion = "Activa";
                    AsignarTrabajo(T);

                }
            }
        }

        public int Mayor()
        {
            int M = 0;
            foreach (Particion P in LP)
            {
                if (M < P.Tamaño)
                {
                    M = P.Tamaño;
                }
            }
            return M;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtgEstado.Columns.Add("Partición", "Partición");
            dtgEstado.Columns.Add("Tamaño partición", "Tamaño partición");
            dtgEstado.Columns.Add("Trabajo", "Trabajo");
            dtgEstado.Columns.Add("Estado", "Estado");
            dtgEstado.Columns.Add("Desperdicio", "Desperdicio");
            //Las propiedades del dataGridView3
            dtgEstado.ReadOnly = true;
            dtgEstado.AllowUserToAddRows = false;
            dtgEstado.AllowUserToResizeColumns = false;
            dtgEstado.AllowUserToResizeRows = false;
            dtgEstado.AllowUserToDeleteRows = false;
            dtgEstado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgEstado.MultiSelect = false;
            dtgEstado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Trabajo miTrabajo = new Trabajo();
                Estado miEstadoSeleccionado = new Estado();
                if (dtgEstado.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un objeto de la lista", "Terminar",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                miEstadoSeleccionado.NombreT = dtgEstado.CurrentRow.Cells[2].Value.ToString();
                miTrabajo.Nombre = miEstadoSeleccionado.NombreT;
                if (MessageBox.Show("¿Esta seguro de terminar el trabajo seleccionado?", "Terminar", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                {
                    MessageBox.Show("Acción cancelada", "Terminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    foreach (Estado E in LE)
                    {
                        if (E.NombreT == miEstadoSeleccionado.NombreT)
                        {
                            E.NombreT = "-";
                            E.Situacion = "Disponible";
                            E.Desperdicio = 0;
                            break;
                        }
                    }
                    foreach (Trabajo T in LT)
                    {
                        if (T.Nombre == miTrabajo.Nombre)
                        {
                            T.Situacion = "Terminada";
                            break;
                        }
                    }
                    MostrarEstado();
                    //MostrarTrabajo();
                    MessageBox.Show("Se ha terminado el trabajo", "Terminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    VerificarEspera();
                    MostrarEstado();
                    //MostrarTrabajo();
                    //GraficaOriginal();
                }
                btnAceptar3.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "ERROR"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            if (i == 0)
            {
                LlenarLE();
                i++;
            }
            VerificarTrabajos();
            Trabajo miTrabajo = new Trabajo();
            miTrabajo.Nombre = txtAsigNombre.Text;
            if (cboAsignacion.Text == "Kb")
            {
                miTrabajo.Tamaño = int.Parse(txtAsigTamaño.Text);
            }
            if (cboAsignacion.Text == "Mb")
            {
                miTrabajo.Tamaño = int.Parse(txtAsigTamaño.Text) * 1024;
            }
            if (cboAsignacion.Text == "Gb")
            {
                miTrabajo.Tamaño = (int.Parse(txtAsigTamaño.Text) * 1024) * 1024;
            }
            if (miTrabajo.Tamaño >= MemoriaTotal || miTrabajo.Tamaño > Mayor())
            {
                miTrabajo.Situacion = "J/E";
            }
            else
            {
                if (O == true)
                {
                    miTrabajo.Situacion = "Espera";
                }
                else
                {
                    miTrabajo.Situacion = "Activa";
                }
            }
            VerificarDuplicado(miTrabajo);
            LT.Insertar(miTrabajo);
            //MostrarTrabajo();
            MostrarEstado();
            //GraficaOriginal();
            MessageBox.Show("Trabajo capturado", "Asignado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

