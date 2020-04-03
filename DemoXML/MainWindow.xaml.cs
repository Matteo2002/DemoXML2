using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Xml.Linq;

namespace DemoXML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource ct;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_Aggiorna_Click(object sender, RoutedEventArgs e)
        {
            ct = new CancellationTokenSource();

            Btn_Aggiorna.IsEnabled = false;
            Btn_Stop.IsEnabled = true;
            Lst_Persone.Items.Clear();


            Task.Factory.StartNew(() => CaricaDati());
        }

        private void CaricaDati()
        {
            string path = @"ListaPersone.xml";
            XDocument xmlDoc = XDocument.Load(path);
            XElement xmlpersone = xmlDoc.Element("persone");
            var xmlpersona = xmlpersone.Elements("persona");

            Thread.Sleep(800);

            foreach (var item in xmlpersona)
            {
                XElement xmlFirstName = item.Element("nome");
                XElement xmlLastName = item.Element("cognome");
                XElement xmlAnni = item.Element("anni");
                XElement xmlNascita = item.Element("data");

                Persona p = new Persona();
                {
                    p.Nome = xmlFirstName.Value;
                    p.Cognome = xmlLastName.Value;
                    p.Anni = Convert.ToInt32(xmlAnni.Value);
                    p.DataDiNascita = Convert.ToString(xmlNascita.Value);
                }
                Dispatcher.Invoke(() => Lst_Persone.Items.Add(p));

                if (ct.Token.IsCancellationRequested)
                {
                    break;
                }

                Thread.Sleep(800);
            }

            Dispatcher.Invoke(() =>
            {
                Btn_Aggiorna.IsEnabled = true;
                Btn_Stop.IsEnabled = false;
                ct = null;
            });
        }

        private void Btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            ct.Cancel();
        }

        private void Btn_Modifica_Click(object sender, RoutedEventArgs e)
        {
            string path = @"ListaPersone.xml";
            XDocument xmlDoc = XDocument.Load(path);
            XElement xmlpersone = xmlDoc.Element("persone");
            var xmlpersona = xmlpersone.Elements("persona");

            foreach (var item in xmlpersona)
            {
                XElement xmlNome = item.Element("nome");
                XElement xmlCognome = item.Element("cognome");
                XElement xmlAnni = item.Element("anni");
                XElement xmlNascita = item.Element("data");

                Persona p = new Persona();
                p.Nome = xmlNome.Value;
                p.Cognome = xmlCognome.Value;
                p.Anni = Convert.ToInt32(xmlAnni.Value);
                p.DataDiNascita = Convert.ToString(xmlNascita.Value);

                if (Convert.ToString(Lst_Persone.SelectedItem) == p.Nome)
                {
                    Txt_Nome.Text = p.Nome;
                    Txt_Cognome.Text = p.Cognome;
                    Txt_Anni.Text = p.Anni.ToString();
                    Txt_DataDiNascita.Text = p.DataDiNascita;
                    break;
                }
                flag++;
            }
        }

        int flag = 0;

        private void Btn_Salva_Click(object sender, RoutedEventArgs e)
        {
            int flag2 = 0;

            string path = @"ListaPersone.xml";
            XDocument xmlDoc = XDocument.Load(path);
            XElement xmlpersone = xmlDoc.Element("persone");
            var xmlpersona = xmlpersone.Elements("persona");

            foreach (var item in xmlpersona)
            {
                XElement xmlNome = item.Element("nome");
                XElement xmlCognome = item.Element("cognome");
                XElement xmlAnni = item.Element("anni");
                XElement xmlNascita = item.Element("data");

                if (flag == flag2)
                {
                    item.SetElementValue("nome", Txt_Nome.Text);
                    item.SetElementValue("cognome", Txt_Cognome.Text);
                    item.SetElementValue("anni", Txt_Anni.Text);
                    item.SetElementValue("data", Txt_DataDiNascita.Text);
                    break;
                }
                flag2++;
            }
            xmlDoc.Save("ListaPersone.xml");
        }
    }
}
