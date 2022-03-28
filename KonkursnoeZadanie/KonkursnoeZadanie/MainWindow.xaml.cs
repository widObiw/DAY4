using System;
using System.Collections.Generic;
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

namespace KonkursnoeZadanie
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public VIN_LIB.Class1 CompetitorLibVIN = new VIN_LIB.Class1();
        public REG_MARK_LIB.Class1 CompetitorLibMark = new REG_MARK_LIB.Class1();
        public MainWindow()
        {
            InitializeComponent();
        }

        private List<String> loadLinesFromFile(string fname)
        {
            var dataFiles = File.ReadAllLines("./dates/" + fname + ".txt");
            var datalist = new List<String>(dataFiles);
            return datalist;
        }

        private void BtnChVin_Click(object sender, RoutedEventArgs e)
        {
            cleanRichText();

            var validVins = loadLinesFromFile("valid_vin");
            var invalidVins = loadLinesFromFile("invalid_vin");
            writeTextR1("Действительный VIN");
            writeTextR2("Действителеный VIN");
            foreach (var vin in validVins)
            {
                writeTextR1(vin);
                if (CompetitorLibVIN.CheckVIN(vin))
                {
                    writeTextR2("Не верно");
                }
                else
                {

                    writeTextR2("Верно");
                }
            }
            writeTextR1("VIN не действителен");
            writeTextR2("VIN не действителен");
            foreach (var vin in invalidVins)
            {
                writeTextR1(vin);
                if (CompetitorLibVIN.CheckVIN(vin) == false)
                {
                    writeTextR2("Верно");
                }
                else
                {

                    writeTextR2("Не верно");
                }
            }

        }

        private void cleanRichText()
        {
            rtbDataRich.Document.Blocks.Clear();
            rtbResRich.Document.Blocks.Clear();
        }
        private void writeTextR1(string text)
        {
            Run r = new Run(text);


            Paragraph p = new Paragraph();
            p.Inlines.Add(r);

            rtbDataRich.Document.Blocks.Add(p);

        }
        private void writeTextR2(string text)
        {
            Run r = new Run(text);
            Paragraph p = new Paragraph();
            p.Inlines.Add(r);
            rtbResRich.Document.Blocks.Add(p);
        }

        private void BtnTransportYear_Click(object sender, RoutedEventArgs e)
        {
            cleanRichText();
            var vinYears = loadLinesFromFile("TransportYear");
            foreach (var vin_year in vinYears)
            {
                var blocks = vin_year.Split(' ');
                var result_year = CompetitorLibVIN.GetTransportYear(blocks[0]);
                writeTextR1(blocks[1]);
                writeTextR2(result_year.ToString() == blocks[1] ? "Верно" : "Ошибка");
            }
        }

        private void BtnVinCountry_Click(object sender, RoutedEventArgs e)
        {
            cleanRichText();
            var vinCountries = loadLinesFromFile("VinCountry");
            foreach (var vin_country in vinCountries)
            {
                var blocks = vin_country.Split(' ');
                var resultCountry = CompetitorLibVIN.GetVINCountry(blocks[0]);
                writeTextR1(blocks[1]);
                writeTextR2(resultCountry == blocks[1] ? resultCountry + " верно" : resultCountry + " ошибка");
            }
        }

        private void BtnCheckMark_Click(object sender, RoutedEventArgs e)
        {
            cleanRichText();
            var marks = loadLinesFromFile("CheckMark");
            foreach (var mark in marks)
            {
                var blocks = mark.Split(' ');
                var resultMark = CompetitorLibMark.CheckMark(blocks[0]);
                writeTextR1(blocks[0] + " является ли " + (Boolean.Parse(blocks[1]) ? "действительным" : "недействительным"));
                writeTextR2(resultMark == Boolean.Parse(blocks[1]) ? " Не является" : " Является");
            }
        }

        private void BtnNextMarkAfter_Click(object sender, RoutedEventArgs e)
        {
            cleanRichText();
            var marks = loadLinesFromFile("NextMark");
            foreach (var mark in marks)
            {
                var blocks = mark.Split(' ');
                var resultMark = CompetitorLibMark.GetNextMarkAfter(blocks[0]);
                writeTextR1("Ввод: " + blocks[0] + "->" + blocks[1] + " вывод " + resultMark);
                writeTextR2((resultMark.ToLower() == blocks[1].ToLower() ? " Верно" : " Ошибка"));
            }
        }

        private void BtnNextMarkAfterInRange_Click(object sender, RoutedEventArgs e)
        {
            cleanRichText();
            var marks = loadLinesFromFile("NextInRange");
            foreach (var mark in marks)
            {
                var blocks = mark.Split(' ');
                var resultMark = CompetitorLibMark.GetNextMarkAfterInRange(blocks[0], blocks[1], blocks[2]);
                if (blocks[3] == "0")
                {
                    blocks[3] = "Нет в наличии";
                }
                writeTextR1("пред: " + blocks[0] + " " + blocks[1] + "..." + blocks[2] + "->" + blocks[3] + " вывод:" + resultMark);
                writeTextR2(resultMark == blocks[3] ? " Верно" : " Ошибка");
            }
        }
    }
}
