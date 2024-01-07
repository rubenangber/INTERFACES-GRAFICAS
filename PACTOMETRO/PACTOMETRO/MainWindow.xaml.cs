﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        bool Normal = true;
        bool Pactometro = false;
        bool Comparativo = false;
        Eleccion eleccionSeleccionada;

        public MainWindow() {
            InitializeComponent();
            CargaDeDatos();
            t = new Tablas(listaElecciones);
            t.Show();
            t.EleccionSeleccionada += T_EleccionSeleccionada;
            this.SizeChanged += MainWindow_SizeChanged;
        }

        private void T_EleccionSeleccionada(object sender, EleccionSeleccionadaEventArgs e) {
            // Manejar la elección seleccionada en el MainWindow
            eleccionSeleccionada = e.EleccionSeleccionada;
            if (eleccionSeleccionada != null) {
                if (Normal) {
                    GraficoNormal(eleccionSeleccionada);
                } else if (Pactometro) {
                    GraficoPactometro(eleccionSeleccionada);
                } else if (Comparativo) {
                    GraficoComparativo(listaElecciones);
                }
            } else {
                CanvaFondo.Children.Clear();
            }
        }



        //BOTONES
        private void Mostrar_Tablas(object sender, RoutedEventArgs e) {
            if (t.Visibility == Visibility.Visible) {
                t.Hide();
            } else {
                t.Show();
            }
        }

        private void Grafico_De_Barras(object sender, RoutedEventArgs e) {
            Normal = true;
            Pactometro = false;
            Comparativo = false;
            if(eleccionSeleccionada != null) {
                GraficoNormal(eleccionSeleccionada);
            }
        }

        private void Pactómetro(object sender, RoutedEventArgs e) {
            Normal = false;
            Pactometro = true;
            Comparativo = false;
            if (eleccionSeleccionada != null) {
                GraficoPactometro(eleccionSeleccionada);
            }
        }

        private void Grafico_Comparativo(object sender, RoutedEventArgs e) {
            Normal = false;
            Pactometro = false;
            Comparativo = true;
            GraficoComparativo(listaElecciones);
        }

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
            listaElecciones.Add(new Eleccion("Generales 1", lp1, new DateTime(2023, 7, 23)));

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
            listaElecciones.Add(new Eleccion("Generales 2", lp2, new DateTime(2019, 11, 10)));

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

            listaElecciones.Add(new Eleccion("Autonómicas CyL", lp3, new DateTime(2022, 2, 14)));

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

            listaElecciones.Add(new Eleccion("Autonómicas CyL", lp4, new DateTime(2019, 5, 26)));
        }

        //GRAFICO DE BARRAS
        private void GraficoNormal(Eleccion el) {
            //LIMPIAMOS EL CANVAS
            CanvaFondo.Children.Clear();

            //METODO DE GENERAR LA GRÁFICA
            float altocanva = (float)CanvaFondo.ActualHeight;
            float anchocanva = (float)CanvaFondo.ActualWidth;
            int[] posdatos = new int[10];
            int[] datosescaños = new int[10];

            //MARGEN IZQ
            int max = el.ObtenerMaximo(el.Partidos);
            for (int i = 0; i < 10; i++) {
                posdatos[i] = (int)(altocanva) / 10 * i;
                datosescaños[i] = (max / 10) * (i + 1);

                TextBlock datacoste = new TextBlock {
                    Text = "-" + datosescaños[i].ToString(),
                    Foreground = Brushes.Red,
                };
                CanvaFondo.Children.Add(datacoste);

                Canvas.SetBottom(datacoste, posdatos[i] + 15);
                Canvas.SetLeft(datacoste, 4);
            }

            //TOP
            Label top = new Label();
            top.Content = el.Nombre.ToString() + " " + el.Fecha.ToString("dd/MM/yyyy");
            top.FontWeight = FontWeights.Bold;

            CanvaFondo.Children.Add(top);

            Canvas.SetTop(top, -23);

            int j = 2;
            foreach (Partido partido in el.Partidos) {
                //RECTANGULO
                Rectangle r = new Rectangle();
                r.Height = ((((altocanva - 20) * partido.Escaños) / max));
                r.Width = anchocanva / ((el.Partidos.Count + 1) * 2);

                // Convierte el nombre del color a un objeto Brush
                Brush colorBrush = (Brush)new BrushConverter().ConvertFromString(partido.Color);
                r.Fill = colorBrush;

                CanvaFondo.Children.Add(r);

                Canvas.SetBottom(r, 0);
                Canvas.SetLeft(r, j * anchocanva / ((el.Partidos.Count + 1) * 2));

                r.MouseEnter += (sender, e) => MostrarNumeroEscaños(partido.Escaños, r);
                r.MouseLeave += (sender, e) => OcultarNumeroEscaños(r);

                //TEXTO
                Label l = new Label();
                l.Content = partido.Nombre.ToString();
                l.Foreground = colorBrush;
                l.FontWeight = FontWeights.Bold;

                CanvaFondo.Children.Add(l);

                Canvas.SetBottom(l, -25);
                Canvas.SetLeft(l, (j * anchocanva / ((el.Partidos.Count + 1) * 2)) -5);

                j += 2;
            }
        }

        //GRAFICO COMPARATIVO
        private void GraficoComparativo(ObservableCollection<Eleccion> listaElecciones) {
            //LIMPIAMOS EL CANVAS
            CanvaFondo.Children.Clear();

            float altocanva = (float)CanvaFondo.ActualHeight;
            float anchocanva = (float)CanvaFondo.ActualWidth;

            int max = 0;
            int i = 0;
            int posleft = 20;
            int postop = 0;

            int numElecciones = listaElecciones.Count();
            if (numElecciones > 3) { 
                numElecciones = 3;
            }

            int sum = 0;
            for (int x = 0; x < numElecciones; x++) {
                sum += listaElecciones[x].Partidos.Count() + 1;
            }

            for (int x = 0; x < numElecciones; x++) {
                if (max < listaElecciones[x].ObtenerMaximo(listaElecciones[x].Partidos)) {
                    max = listaElecciones[x].ObtenerMaximo(listaElecciones[x].Partidos);
                }
            }

            for (int x = 0; x < numElecciones; x++) { 
                foreach (Partido partido in listaElecciones[i].Partidos) {
                    //RECTANGULO
                    Rectangle r = new Rectangle();
                    r.Height = ((((altocanva - 20) * partido.Escaños) / max));
                    r.Width = anchocanva / sum;

                    // Convierte el nombre del color a un objeto Brush
                    Brush colorBrush = (Brush)new BrushConverter().ConvertFromString(partido.Color);
                    r.Fill = colorBrush;
                    r.Opacity = 1.0 / (double)Math.Pow(2, i);

                    CanvaFondo.Children.Add(r);

                    Canvas.SetBottom(r, 0);
                    Canvas.SetLeft(r, posleft);

                    posleft += (int)anchocanva / sum;
                }
                Label l = new Label();
                l.Content = listaElecciones[i].Nombre.ToString() + " " + listaElecciones[i].Fecha.ToString("dd/MM/yyyy");
                l.FontWeight = FontWeights.Bold;
                l.Opacity = 1.0 / (double)Math.Pow(2, i);

                CanvaFondo.Children.Add(l);

                Canvas.SetTop(l, postop);
                Canvas.SetRight(l, 0);

                postop += 20;
                i++;
                posleft += (int)anchocanva / sum;
            }
        }

        //PACTOMETRO
        private void GraficoPactometro(Eleccion el) {
            int altura1 = 0;
            int altura2 = 0;
            int altura3 = 0;
            //LIMPIAMOS EL CANVAS
            CanvaFondo.Children.Clear();

            //METODO DE GENERAR LA GRÁFICA
            float altocanva = (float)CanvaFondo.ActualHeight;
            float anchocanva = (float)CanvaFondo.ActualWidth;

            //TOP
            Label top = new Label();
            top.Content = el.Nombre.ToString() + " " + el.Fecha.ToString("dd/MM/yyyy");
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
                r.Width = anchocanva / 7;

                CanvaFondo.Children.Add(r);

                Brush colorBrush = (Brush)new BrushConverter().ConvertFromString(partido.Color);
                r.Fill = colorBrush;

                if (partido.PosPactometro == 1) {
                    Canvas.SetBottom(r, altura1);
                    Canvas.SetLeft(r, anchocanva / 7);
                    altura1 += (int)((((altocanva - 20) * partido.Escaños) / el.Escaños));
                } else if (partido.PosPactometro == 2) {
                    Canvas.SetBottom(r, altura2);
                    Canvas.SetLeft(r, (anchocanva / 7) * 3);
                    altura2 += (int)((((altocanva - 20) * partido.Escaños) / el.Escaños));
                } else if (partido.PosPactometro == 3) {
                    Canvas.SetBottom(r, altura3);
                    Canvas.SetLeft(r, (anchocanva / 7) * 5);
                    altura3 += (int)((((altocanva - 20) * partido.Escaños) / el.Escaños));
                }
                r.MouseLeftButtonDown += (sender, e) => CambiarPosicionRectangulo(partido);
            }

            //LINEA MAYORIA
            Rectangle linea = new Rectangle();
            linea.Height = 2;
            linea.Width = anchocanva;
            linea.Fill = new SolidColorBrush(Colors.Black);
            CanvaFondo.Children.Add(linea);
            Canvas.SetBottom(linea, ((((altocanva - 20) * el.Mayoria) / el.Escaños)));
            Canvas.SetLeft(linea, 0);
        }

        //Cambio posicion pactómetro
        private void CambiarPosicionRectangulo(Partido p) {
            if (p.PosPactometro == 3) {
                p.PosPactometro = 1;
                GraficoPactometro(eleccionSeleccionada);
            } else if(p.PosPactometro == 1) {
                p.PosPactometro = 2;
                GraficoPactometro(eleccionSeleccionada);
            } else if(p.PosPactometro == 2) {
                p.PosPactometro = 3;
                GraficoPactometro(eleccionSeleccionada);
            } 
        }

        // Método para mostrar el número de escaños
        private void MostrarNumeroEscaños(int numeroEscaños, UIElement relativeTo) {
            if(numeroEscaños == 1) {
                Label etiqueta = new Label();
                etiqueta.Content = $"{numeroEscaños} escaño";
                etiqueta.Foreground = Brushes.Black;
                // Agrega la etiqueta al Canvas y establece su posición relativa al rectángulo
                CanvaFondo.Children.Add(etiqueta);
                Canvas.SetBottom(etiqueta, Canvas.GetBottom(relativeTo) + 10); // Ajusta la posición según tus necesidades
                Canvas.SetLeft(etiqueta, Canvas.GetLeft(relativeTo));

                // Guarda una referencia a la etiqueta para poder eliminarla más tarde
                etiquetaActual = etiqueta;
            } else {
                Label etiqueta = new Label();
                etiqueta.Content = $"{numeroEscaños} escaños";
                etiqueta.Foreground = Brushes.Black;

                CanvaFondo.Children.Add(etiqueta);
                Canvas.SetBottom(etiqueta, Canvas.GetBottom(relativeTo) + 10);
                Canvas.SetLeft(etiqueta, Canvas.GetLeft(relativeTo));

                etiquetaActual = etiqueta;
            }
        }

        // Método para ocultar el número de escaños
        private void OcultarNumeroEscaños(UIElement elemento) {
            // Remueve la etiqueta del Canvas
            if (etiquetaActual != null) {
                CanvaFondo.Children.Remove(etiquetaActual);
                etiquetaActual = null;
            }
        }
        private Label etiquetaActual;

        public static bool MainWindowClosed { get; internal set; }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e) {
            // Verifica si hay una elección seleccionada antes de redibujar el gráfico
            if (eleccionSeleccionada != null) {
                // Llama al método correspondiente según el tipo de gráfico seleccionado
                if (Normal) {
                    GraficoNormal(eleccionSeleccionada);
                } else if (Pactometro) {
                    GraficoPactometro(eleccionSeleccionada);
                } else if (Comparativo) {
                    GraficoComparativo(listaElecciones);
                }
            }
            if (Comparativo) {
                GraficoComparativo(listaElecciones);
            }
        }


        //CIERRE
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            // Obtener todas las ventanas abiertas
            foreach (Window window in App.Current.Windows) {
                if (window != this) {
                    window.Close();
                } 
            }
        }

        //EXPORTAR
        private void ExportarCSV_Click(object sender, RoutedEventArgs e) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos CSV (*.csv)|*.csv";
            saveFileDialog.Title = "Selecciona donde guardar el archivo CSV";

            if (saveFileDialog.ShowDialog() == true) {
                string filePath = saveFileDialog.FileName;
                try {
                    using (var writer = new StreamWriter(filePath)) {
                        foreach (var eleccion in listaElecciones) {
                            // Se asume que el formato de las fechas se puede convertir directamente a una cadena
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

            //Utiliza StringBuilder para poder hacer el return de un string
            StringBuilder sb = new StringBuilder();

            foreach (var partido in partidos) {
                sb.Append($"{partido.Nombre},{partido.Escaños},{partido.Color},");
            }

            // Elimina la última "," 
            sb.Length--;

            return sb.ToString();
        }

        //IMPORTAR
        private void ImportarCSV_Click(object sender, RoutedEventArgs e) {
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
                            Eleccion eleccion = new Eleccion(values[0], new ObservableCollection<Partido>(), DateTime.Parse(values[2]));

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
    }
}
