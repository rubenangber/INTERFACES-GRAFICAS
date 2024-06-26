﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static PACTOMETRO.Tablas;

namespace PACTOMETRO {
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// public class EleccionSeleccionadoEventArgs : EventArgs {
    public partial class MainWindow : Window {
        Tablas t;
        ObservableCollection<Eleccion> listaElecciones = new ObservableCollection<Eleccion>();
        ObservableCollection<Eleccion> eleccionesSeleccionadas = new ObservableCollection<Eleccion>();
        bool Normal = true;
        bool Pactometro = false;
        bool Comparativo = false;
        bool Circular = false;
        Eleccion eleccionSeleccionada;

        public MainWindow() {
            InitializeComponent();
            CargaDeDatos();
            t = new Tablas(listaElecciones);
            t.Show();
            t.EleccionSeleccionada += T_EleccionSeleccionada;
            t.eleccionesListView.SelectionChanged += EleccionesListView_SelectionChanged;
            t.Closing += TablasClosing;
            this.SizeChanged += MainWindow_SizeChanged;
        }

        // Eleccion seleccionada simple
        private void T_EleccionSeleccionada(object sender, EleccionSeleccionadaEventArgs e) {
            // Manejar la elección seleccionada en el MainWindow
            eleccionSeleccionada = e.EleccionSeleccionada;
            foreach (Partido p in eleccionSeleccionada.Partidos) {
                p.PosPactometro = 2;
            }
            if (eleccionSeleccionada != null) {
                if (Normal) {
                    GraficoNormal(eleccionSeleccionada);
                } else if (Pactometro) {
                    GraficoPactometro(eleccionSeleccionada);
                } else if (Comparativo) {
                    GraficoComparativo(eleccionesSeleccionadas);
                } else if (Circular) { 
                    GraficoCircular(eleccionSeleccionada);
                }
            } else {
                CanvaFondo.Children.Clear();
            }
        }

        // Eleccion seleccionada múltiple
        private void EleccionesListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (Comparativo == true) {
                // Obtener elementos que se han agregado a la selección
                foreach (Eleccion eleccionAgregada in e.AddedItems) {
                    eleccionesSeleccionadas.Add(eleccionAgregada);
                    GraficoComparativo(eleccionesSeleccionadas);
                }

                // Obtener elementos que se han eliminado de la selección
                foreach (Eleccion eleccionEliminada in e.RemovedItems) {
                    eleccionesSeleccionadas.Remove(eleccionEliminada);
                    GraficoComparativo(eleccionesSeleccionadas);
                }
            }
        }

        // BOTONES
        private void Mostrar_Tablas(object sender, RoutedEventArgs e) {
            if (t.Visibility == Visibility.Visible) {
                t.Hide();
            } else {
                t.Show();
            }
        }

        // Gráfico de barras
        private void Grafico_De_Barras(object sender, RoutedEventArgs e) {
            Normal = true;
            Pactometro = false;
            Comparativo = false;
            Circular = false;
            if (eleccionSeleccionada != null) {
                GraficoNormal(eleccionSeleccionada);
            }
            t.eleccionesListView.SelectionMode = SelectionMode.Single;
            eleccionesSeleccionadas.Clear();
        }

        // Gráfico pactómetro
        private void Pactómetro(object sender, RoutedEventArgs e) {
            Normal = false;
            Pactometro = true;
            Comparativo = false;
            Circular = false;
            if (eleccionSeleccionada != null) {
                GraficoPactometro(eleccionSeleccionada);
            }
            t.eleccionesListView.SelectionMode = SelectionMode.Single;
            eleccionesSeleccionadas.Clear();
        }

        // Gráfico comparativo
        private void Grafico_Comparativo(object sender, RoutedEventArgs e) {
            Normal = false;
            Pactometro = false;
            Comparativo = true;
            Circular = false;
            t.eleccionesListView.SelectionMode = SelectionMode.Multiple;
            GraficoComparativo(eleccionesSeleccionadas);
        }

        // Gráfico circular
        private void Grafico_Circular(object sender, RoutedEventArgs e) {
            Normal = false;
            Pactometro = false;
            Comparativo = false;
            Circular = true;

            if (eleccionSeleccionada != null) {
                GraficoCircular(eleccionSeleccionada);
            }
            t.eleccionesListView.SelectionMode = SelectionMode.Single;
            eleccionesSeleccionadas.Clear();
        }

        // Exportar
        private void ExportarCSV_Click(object sender, RoutedEventArgs e) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos CSV (*.csv)|*.csv";
            saveFileDialog.Title = "Selecciona donde guardar el archivo CSV";

            if (saveFileDialog.ShowDialog() == true) {
                string filePath = saveFileDialog.FileName;
                try {
                    using (var writer = new StreamWriter(filePath)) {
                        foreach (var eleccion in listaElecciones) {
                            string partidosComoCadena = ObtenerPartidosComoCadena(eleccion.Partidos);
                            writer.WriteLine($"{eleccion.Nombre},{eleccion.Escaños},{eleccion.Fecha},{eleccion.Mayoria},{partidosComoCadena}");
                        }
                    }
                    MessageBox.Show("Datos exportados correctamente.", "Exportar CSV", MessageBoxButton.OK, MessageBoxImage.Information);
                } catch (Exception ex) {
                    MessageBox.Show($"Error al exportar datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string ObtenerPartidosComoCadena(ObservableCollection<Partido> partidos) {
            if (partidos == null || partidos.Count == 0) {
                return string.Empty;
            }

            // Utiliza StringBuilder para poder hacer el return de un string
            StringBuilder sb = new StringBuilder();

            foreach (var partido in partidos) {
                sb.Append($"{partido.Nombre},{partido.Escaños},{partido.Color},");
            }

            // Elimina la última "," 
            sb.Length--;

            return sb.ToString();
        }

        // Importar
        private void ImportarCSV_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos CSV (*.csv)|*.csv";
            openFileDialog.Title = "Selecciona un archivo CSV";

            if (openFileDialog.ShowDialog() == true) {
                string filePath = openFileDialog.FileName;

                try {
                    // Eliminar todas las elecciones cuando la apertura del fichero es correcta
                    listaElecciones.Clear();
                    var lines = File.ReadLines(filePath);

                    foreach (var line in lines) {
                        string[] values = line.Split(',');

                        if (values.Length >= 4) {
                            Eleccion eleccion = new Eleccion(values[0], int.Parse(values[1]), new ObservableCollection<Partido>(), DateTime.Parse(values[2]));
                            for (int i = 4; i < values.Length; i += 3) {
                                Partido partido = new Partido(values[i], int.Parse(values[i + 1]), values[i + 2]);
                                eleccion.Partidos.Add(partido);
                            }
                            listaElecciones.Add(eleccion);
                        } else {
                            MessageBox.Show("Formato de línea incorrecto en el archivo CSV", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                } catch (Exception ex) {
                    MessageBox.Show($"Error al importar el archivo CSV: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Añadir
        private void AñadirCSV_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos CSV (*.csv)|*.csv";
            openFileDialog.Title = "Selecciona un archivo CSV";

            if (openFileDialog.ShowDialog() == true) {
                string filePath = openFileDialog.FileName;

                try {
                    var lines = File.ReadLines(filePath);

                    foreach (var line in lines) {
                        string[] values = line.Split(',');

                        if (values.Length >= 4) {
                            Eleccion eleccion = new Eleccion(values[0], int.Parse(values[1]), new ObservableCollection<Partido>(), DateTime.Parse(values[2]));
                            for (int i = 4; i < values.Length; i += 3) {
                                Partido partido = new Partido(values[i], int.Parse(values[i + 1]), values[i + 2]);
                                eleccion.Partidos.Add(partido);
                            }
                            listaElecciones.Add(eleccion);
                        } else {
                            MessageBox.Show("Formato de línea incorrecto en el archivo CSV", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                } catch (Exception ex) {
                    MessageBox.Show($"Error al importar el archivo CSV: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // CARGA DE DATOS
        private void CargaDeDatos() {
            ObservableCollection<Partido> lp1 = new ObservableCollection<Partido>();
            Partido p1 = new Partido("PP", 136, "#1e4b8f");
            Partido p2 = new Partido("PSOE", 122, "#da291c");
            Partido p3 = new Partido("VOX", 33, "#63BE21");
            Partido p4 = new Partido("SUMAR", 31, "#E51C55");
            Partido p5 = new Partido("ERC", 7, "#ffc147");
            Partido p6 = new Partido("JUNTS", 7, "#3ee1d1");
            Partido p7 = new Partido("EH_BILDU", 6, "#00d0b6");
            Partido p8 = new Partido("EAJ_PNV", 5, "#00822a");
            Partido p9 = new Partido("BNG", 1, "#77b5de");
            Partido p10 = new Partido("CCA", 1, "#ffd855");
            Partido p11 = new Partido("UPN", 1, "#0255b4");
            
            lp1.Add(p1);
            lp1.Add(p2);
            lp1.Add(p3);
            lp1.Add(p4);
            lp1.Add(p5);
            lp1.Add(p6);
            lp1.Add(p7);
            lp1.Add(p8);
            lp1.Add(p9);
            lp1.Add(p10);
            lp1.Add(p11);
            listaElecciones.Add(new Eleccion("Generales 1", 350, lp1, new DateTime(2023, 7, 23)));

            ObservableCollection<Partido> lp2 = new ObservableCollection<Partido>();
            Partido p12 = new Partido("PSOE", 120, "#da291c");
            Partido p13 = new Partido("PP", 89, "#1e4b8f");
            Partido p14 = new Partido("VOX", 52, "#63BE21");
            Partido p15 = new Partido("PODEMOS", 35, "#693279");
            Partido p16 = new Partido("ERC", 13, "#ffc147");
            Partido p17 = new Partido("JUNTS", 8, "#3ee1d1");
            Partido p18 = new Partido("CS", 10, "#ff5a23");
            Partido p19 = new Partido("EAJ_PNV", 6, "#00822a");
            Partido p20 = new Partido("EH_BILDU", 5, "#00d0b6");
            Partido p21 = new Partido("MASPAIS", 3, "#12796a");
            Partido p22 = new Partido("CUP_PR", 2, "#fff200");
            Partido p23 = new Partido("CCA", 2, "#ffd855");
            Partido p24 = new Partido("BNG", 1, "#77b5de");
            Partido p25 = new Partido("OTROS", 4, "#000000");

            lp2.Add(p12);
            lp2.Add(p13);
            lp2.Add(p14);
            lp2.Add(p15);
            lp2.Add(p16);
            lp2.Add(p17);
            lp2.Add(p18);
            lp2.Add(p19);
            lp2.Add(p20);
            lp2.Add(p21);
            lp2.Add(p22);
            lp2.Add(p23);
            lp2.Add(p24);
            lp2.Add(p25);
            listaElecciones.Add(new Eleccion("Generales 2", 350, lp2, new DateTime(2019, 11, 10)));

            ObservableCollection<Partido> lp3 = new ObservableCollection<Partido>();
            Partido p26 = new Partido("PP", 31, "#1e4b8f");
            Partido p27 = new Partido("PSOE", 28, "#da291c");
            Partido p28 = new Partido("VOX", 13, "#63BE21");
            Partido p29 = new Partido("UPL", 3, "#b91267");
            Partido p30 = new Partido("SY", 3, "#1c1c1a");
            Partido p31 = new Partido("PODEMOS", 1, "#693279");
            Partido p32 = new Partido("CS", 1, "#ff5a23");
            Partido p33 = new Partido("XAV", 1, "#f7d806");

            lp3.Add(p26);
            lp3.Add(p27);
            lp3.Add(p28);
            lp3.Add(p29);
            lp3.Add(p30);
            lp3.Add(p31);
            lp3.Add(p32);
            lp3.Add(p33);

            listaElecciones.Add(new Eleccion("Autonómicas Castilla y León 1", 81, lp3, new DateTime(2022, 2, 14)));

            ObservableCollection<Partido> lp4 = new ObservableCollection<Partido>();
            Partido p34 = new Partido("PSOE", 35, "#da291c");
            Partido p35 = new Partido("PP", 29, "#1e4b8f");
            Partido p36 = new Partido("CS", 12, "#ff5a23");
            Partido p37 = new Partido("PODEMOS", 2, "#693279");
            Partido p38 = new Partido("VOX", 1, "#63BE21");
            Partido p39 = new Partido("UPL", 1, "#b91267");
            Partido p40 = new Partido("XAV", 1, "#f7d806");

            lp4.Add(p34);
            lp4.Add(p35);
            lp4.Add(p36);
            lp4.Add(p37);
            lp4.Add(p38);
            lp4.Add(p39);
            lp4.Add(p40);

            listaElecciones.Add(new Eleccion("Autonómicas Castilla y León 2", 81, lp4, new DateTime(2019, 5, 26)));
        }

        // GRÁFICO CIRCULAR
        private void GraficoCircular(Eleccion el) {
            CanvaFondo.Children.Clear();

            Label top = new Label();
            top.Content = el.Nombre.Length >= 30 ? el.Nombre.Substring(0, 30) + " " + el.Fecha.ToString("dd/MM/yyyy") : el.Nombre.ToString() + " " + el.Fecha.ToString("dd/MM/yyyy");
            top.FontWeight = FontWeights.Bold;

            CanvaFondo.Children.Add(top);

            Canvas.SetTop(top, -23);

            int numPartidos = el.Partidos.Count;
            double[] porcentajes = new double[numPartidos];

            int i = 0;
            foreach (Partido p in el.Partidos) {
                porcentajes[i] = p.Escaños * 100.0 / el.Escaños;
                i++;
            }

            double centroX = CanvaFondo.ActualWidth / 2;
            double centroY = CanvaFondo.ActualHeight / 2;
            double radio = Math.Min(centroX, centroY) - 30;

            double total = 0;
            foreach (double porcentaje in porcentajes) {
                total += porcentaje;
            }

            double anguloInicial = 0;
            i = 0;

            foreach (Partido p in el.Partidos) {
                double anguloBarrido = 360 * (porcentajes[i] / total);

                System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
                path.Fill = (Brush)new BrushConverter().ConvertFromString(p.Color);

                PathGeometry pathGeometry = new PathGeometry();
                PathFigure pathFigure = new PathFigure();

                pathFigure.StartPoint = new Point(centroX, centroY);
                pathFigure.Segments.Add(new LineSegment(new Point(centroX + radio * Math.Cos(anguloInicial * Math.PI / 180), centroY + radio * Math.Sin(anguloInicial * Math.PI / 180)), true));
                pathFigure.Segments.Add(new ArcSegment(new Point(centroX + radio * Math.Cos((anguloInicial + anguloBarrido) * Math.PI / 180), centroY + radio * Math.Sin((anguloInicial + anguloBarrido) * Math.PI / 180)), new Size(radio, radio), 0, anguloBarrido > 180, SweepDirection.Clockwise, true));
                pathFigure.Segments.Add(new LineSegment(new Point(centroX, centroY), true));

                pathGeometry.Figures.Add(pathFigure);
                path.Data = pathGeometry;

                // Agregar tooltip al path
                ToolTip tooltip = new ToolTip();
                porcentajes[i] = Math.Round(porcentajes[i], 2);
                tooltip.Content = p.Nombre + " " + porcentajes[i].ToString() + "%";

                path.ToolTip = tooltip;

                CanvaFondo.Children.Add(path);

                // Añadir el nombre del partido como texto al lado del segmento
                TextBlock nomPartido = new TextBlock();
                nomPartido.Text = p.Nombre.Length >= 10 ? p.Nombre.Substring(0, 10) : p.Nombre.ToString();
                nomPartido.Foreground = (Brush)new BrushConverter().ConvertFromString(p.Color);
                nomPartido.FontWeight = FontWeights.Bold;

                // Calcular la posición del texto
                double textX = centroX + 1.2 * radio * Math.Cos((anguloInicial + anguloBarrido / 2) * Math.PI / 180);
                double textY = centroY + 1.1 * radio * Math.Sin((anguloInicial + anguloBarrido / 2) * Math.PI / 180);
                CanvaFondo.Children.Add(nomPartido);

                Canvas.SetLeft(nomPartido, textX);
                Canvas.SetTop(nomPartido, textY);

                anguloInicial += anguloBarrido;
                i++;
            }
        }

        // GRAFICO DE BARRAS
        private void GraficoNormal(Eleccion el) {
            // LIMPIAMOS EL CANVAS
            CanvaFondo.Children.Clear();

            // METODO DE GENERAR LA GRÁFICA
            float altocanva = (float)CanvaFondo.ActualHeight;
            float anchocanva = (float)CanvaFondo.ActualWidth;
            int max = el.ObtenerMaximo(el.Partidos);
            int[] posdatos = new int[10];
            int[] datosescaños = new int[10];
            int max2 = el.ObtenerMaximo(el.Partidos);

            // MARGEN IZQ
            for (int z = 0; z < 10; z++) {
                posdatos[z] = (int)((altocanva - 20) / 10 * z);
                datosescaños[z] = max2 / 10 * (z + 1);

                TextBlock datacoste = new TextBlock {
                    Text = "-" + datosescaños[z].ToString(),
                    Foreground = Brushes.Red,
                };
                CanvaFondo.Children.Add(datacoste);

                Canvas.SetBottom(datacoste, posdatos[z] + (altocanva - 20) / 10 - 10);
                Canvas.SetLeft(datacoste, 4);
            }

            // TOP
            Label top = new Label();
            top.Content = el.Nombre.Length >= 30 ? el.Nombre.Substring(0, 30) + " " + el.Fecha.ToString("dd/MM/yyyy") : el.Nombre.ToString() + " " + el.Fecha.ToString("dd/MM/yyyy");
            top.FontWeight = FontWeights.Bold;

            CanvaFondo.Children.Add(top);

            Canvas.SetTop(top, -23);

            int j = 2;
            foreach (Partido partido in el.Partidos) {
                // RECTANGULO
                Rectangle r = new Rectangle();
                r.Height = ((((altocanva - 20) * partido.Escaños) / max));
                r.Width = anchocanva / ((el.Partidos.Count + 1) * 2);

                // Convierte el nombre del color a un objeto Brush
                Brush colorBrush = (Brush)new BrushConverter().ConvertFromString(partido.Color);
                r.Fill = colorBrush;
                r.ToolTip = partido.Escaños == 1 ? $"{partido.Nombre} - {partido.Escaños} escaño" : $"{partido.Nombre} - {partido.Escaños} escaños";

                CanvaFondo.Children.Add(r);

                Canvas.SetBottom(r, 0);
                Canvas.SetLeft(r, j * anchocanva / ((el.Partidos.Count + 1) * 2));

                // TEXTO
                Label l = new Label();
                l.Content = partido.Nombre.Length >= 10 ? partido.Nombre.Substring(0, 10) : partido.Nombre.ToString();
                l.Foreground = colorBrush;
                l.FontWeight = FontWeights.Bold;

                CanvaFondo.Children.Add(l);

                Canvas.SetBottom(l, -25);
                Canvas.SetLeft(l, (j * anchocanva / ((el.Partidos.Count + 1) * 2)) - 5);

                j += 2;
            }
        }

        // GRAFICO COMPARATIVO
        private void GraficoComparativo(ObservableCollection<Eleccion> listaElecciones) {
            // LIMPIAMOS EL CANVAS
            CanvaFondo.Children.Clear();

            float altocanva = (float)CanvaFondo.ActualHeight;
            float anchocanva = (float)CanvaFondo.ActualWidth;

            int alturaMax = 0;
            int i = 0;

            int postop = 0;
            int numElecciones = listaElecciones.Count();
            int sum = 0;

            foreach (Eleccion ele in listaElecciones) {
                sum += ele.Partidos.Count();
            }

            foreach (Eleccion ele in listaElecciones) {
                if (alturaMax < ele.ObtenerMaximo(ele.Partidos)) {
                    alturaMax = ele.ObtenerMaximo(ele.Partidos);
                }
            }

            ObservableCollection<string> nombrePartidos = new ObservableCollection<string>();
            foreach (Eleccion ele in listaElecciones) {
                foreach (Partido p in ele.Partidos) {
                    if (!nombrePartidos.Contains(p.Nombre)) {
                        nombrePartidos.Add(p.Nombre);
                    }
                }
            }

            // Calcular el ancho dividendo el ancho del Canvas entre el total de elementos a mostrar
            int ancho = (int)anchocanva / (nombrePartidos.Count() * listaElecciones.Count() * 2 + 1);
            int posleft = ancho;

            foreach (string nombreP in nombrePartidos) {
                Label l = new Label();
                l.Content = nombreP.Length >= 10 ? nombreP.Substring(0, 10) : nombreP.ToString();

                CanvaFondo.Children.Add(l);

                Canvas.SetBottom(l, -23);
                Canvas.SetLeft(l, posleft);

                bool partidoEncontrado = false;

                foreach (Eleccion ele in listaElecciones) {
                    // Variable para determinar si se encontró el partido en esta elección
                    bool partidoEnEstaEleccion = false;
                    int escaños = ele.Escaños;
                    foreach (Partido partido in ele.Partidos) {
                        if (partido.Nombre == nombreP) {
                            Rectangle r = new Rectangle();
                            //r.Height = ((((altocanva - 20) * partido.Escaños) / alturaMax));
                            r.Height = (partido.Escaños * (altocanva - 20) / escaños);
                            r.Width = ancho;
                            r.Fill = (Brush)new BrushConverter().ConvertFromString(partido.Color);
                            r.Opacity = 1.0 / (double)Math.Pow(2, i);
                            CanvaFondo.Children.Add(r);

                            Canvas.SetBottom(r, 0);
                            Canvas.SetLeft(r, posleft);

                            posleft += ancho;
                            partidoEncontrado = true;
                            partidoEnEstaEleccion = true;
                        }
                    }

                    // Si no se encontró el partido en esta elección, agregar un espacio en blanco
                    if (!partidoEnEstaEleccion) {
                        posleft += ancho;
                    }

                    posleft += ancho;
                    i++;
                }

                // Si no se encontró un partido con el nombre en ninguna elección, agregar un espacio en blanco
                if (!partidoEncontrado) {
                    posleft += ancho;
                }
                i = 0;
            }

            // Leyenda elecciones
            i = 0;
            foreach (Eleccion ele in listaElecciones) {
                Label l = new Label();
                l.Content = ele.Nombre.Length >= 30 ? ele.Nombre.Substring(0, 30) + " " + ele.Fecha.ToString("dd/MM/yyyy") : ele.Nombre.ToString() + " " + ele.Fecha.ToString("dd/MM/yyyy");
                l.FontWeight = FontWeights.Bold;
                l.Opacity = 1.0 / (double)Math.Pow(2, i);

                CanvaFondo.Children.Add(l);

                Canvas.SetTop(l, postop);
                Canvas.SetRight(l, 0);

                postop += 20;
                i++;
                posleft += (int)anchocanva / sum;
            }

            int[] posdatos = new int[5];
            int[] datosescaños = new int[5];
            int diez = 20;

            // MARGEN IZQ
            for (int z = 0; z < 5; z++) {
                posdatos[z] = (int)((altocanva - 20) / 5 * z);
                datosescaños[z] = diez;
                diez += 20;

                TextBlock datacoste = new TextBlock {
                    Text = "-" + datosescaños[z].ToString() + "%",
                    Foreground = Brushes.Red,
                };
                CanvaFondo.Children.Add(datacoste);

                Canvas.SetBottom(datacoste, posdatos[z] + (altocanva - 20) / 5);
                Canvas.SetLeft(datacoste, 4);
            }
        }

        // PACTOMETRO
        private void GraficoPactometro(Eleccion el) {
            int altura1 = 0;
            int altura2 = 0;

            int votos1 = 0;
            int votos2 = 0;
            // LIMPIAMOS EL CANVAS
            CanvaFondo.Children.Clear();

            // METODO DE GENERAR LA GRÁFICA
            float altocanva = (float)CanvaFondo.ActualHeight;
            float anchocanva = (float)CanvaFondo.ActualWidth;

            // TOP
            Label top = new Label();
            top.Content = el.Nombre.Length >= 30 ? el.Nombre.Substring(0, 30) + " " + el.Fecha.ToString("dd/MM/yyyy") : el.Nombre.ToString() + " " + el.Fecha.ToString("dd/MM/yyyy");
            top.FontWeight = FontWeights.Bold;

            CanvaFondo.Children.Add(top);

            Canvas.SetTop(top, -23);
            
            foreach (Partido partido in el.Partidos) {
                if (partido.Escaños >= el.Mayoria) {
                    partido.PosPactometro = 1;
                } 
            }

            foreach (Partido partido in el.Partidos) {
                Rectangle r = new Rectangle();
                r.Height = ((((altocanva - 20) * partido.Escaños) / el.Escaños));
                r.Width = anchocanva / 5;

                CanvaFondo.Children.Add(r);

                Brush colorBrush = (Brush)new BrushConverter().ConvertFromString(partido.Color);
                r.Fill = colorBrush;

                Label l = new Label();
                if (partido.Nombre.Length >= 10) {
                    l.Content = partido.Escaños < 0.05 * el.Escaños ? $"" : $"{partido.Nombre.Substring(0, 10)} - {partido.Escaños}"; // Si no constituye el 5% de los escaños de la eleccion, no mostrar
                } else {
                    l.Content = partido.Escaños < 0.05 * el.Escaños ? $"" : $"{partido.Nombre} - {partido.Escaños}"; // Si no constituye el 5% de los escaños de la eleccion, no mostrar
                }
                

                CanvaFondo.Children.Add(l);

                if (partido.PosPactometro == 1) {
                    Canvas.SetBottom(r, altura1);
                    Canvas.SetLeft(r, anchocanva / 5);

                    Canvas.SetBottom(l, ((((altocanva - 20) * partido.Escaños) / el.Escaños)) + altura1 - 20);
                    Canvas.SetLeft(l, anchocanva / 5 * 2);

                    altura1 += (int)((((altocanva - 20) * partido.Escaños) / el.Escaños));
                    votos1 += partido.Escaños;
                } else if (partido.PosPactometro == 2) {
                    Canvas.SetBottom(r, altura2);
                    Canvas.SetLeft(r, (anchocanva / 5) * 3);

                    Canvas.SetBottom(l, ((((altocanva - 20) * partido.Escaños) / el.Escaños)) + altura2 - 20);
                    Canvas.SetLeft(l, (anchocanva / 5) * 3 + (anchocanva / 5));

                    altura2 += (int)((((altocanva - 20) * partido.Escaños) / el.Escaños));
                    votos2 += partido.Escaños;
                }
                r.MouseLeftButtonDown += (sender, e) => CambiarPosicionRectangulo(partido);
            }

            // LINEA MAYORIA
            Rectangle linea = new Rectangle();
            linea.Height = 2;
            linea.Width = anchocanva;
            linea.Fill = new SolidColorBrush(Colors.Black);
            CanvaFondo.Children.Add(linea);
            Canvas.SetBottom(linea, ((((altocanva - 20) * el.Mayoria) / el.Escaños)));
            Canvas.SetLeft(linea, 0);

            Label v1 = new Label();
            v1.Content = votos1.ToString();
            CanvaFondo.Children.Add(v1);
            Canvas.SetBottom(v1, -23);
            Canvas.SetLeft(v1, anchocanva / 5);

            Label v2 = new Label();
            v2.Content = votos2.ToString();
            CanvaFondo.Children.Add(v2);
            Canvas.SetBottom(v2, -23);
            Canvas.SetLeft(v2, (anchocanva / 5) * 3);
        }

        // Cambio posicion pactómetro
        private void CambiarPosicionRectangulo(Partido p) {
            if (p.PosPactometro == 1) {
                p.PosPactometro = 2;
                GraficoPactometro(eleccionSeleccionada);
            } else if (p.PosPactometro == 2) {
                p.PosPactometro = 1;
                GraficoPactometro(eleccionSeleccionada);
            } 
        }

        // CAMBIO TAMAÑO
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e) {
            double minWidth = 300; // Establece el ancho mínimo deseado
            double minHeight = 200; // Establece la altura mínima deseada

            if (e.NewSize.Width < minWidth) {
                this.Width = minWidth;
            }

            if (e.NewSize.Height < minHeight) {
                this.Height = minHeight;
            }

            if (eleccionSeleccionada != null) {
                if (Normal) {
                    GraficoNormal(eleccionSeleccionada);
                } else if (Pactometro) {
                    GraficoPactometro(eleccionSeleccionada);
                } else if (Comparativo) {
                    GraficoComparativo(eleccionesSeleccionadas);
                } else if (Circular) { 
                    GraficoCircular(eleccionSeleccionada);
                }
            }
        }

        //CIERRES
        private void MainWindow_Closing(object sender, CancelEventArgs e) {
            // Obtener todas las ventanas abiertas
            foreach (Window window in App.Current.Windows) {
                if (window != this) {
                    // Manejar el evento Closing de la ventana t por separado
                    if (window is Tablas t) {
                        t.Closing += TablasClosingMain;
                        t.Close();
                    } else {
                        window.Close();
                    }
                }
            }
            e.Cancel = false;
        }

        private void TablasClosing(object sender, CancelEventArgs e) {
            // Cancela el cierre de la ventana
            e.Cancel = true;
            // Ocultamos la ventana
            t.Hide();
        }

        private void TablasClosingMain(object sender, CancelEventArgs e) {
            // Permite que la ventana t se cierre cuando se cierra el Main
            e.Cancel = false;
        }
    }
}