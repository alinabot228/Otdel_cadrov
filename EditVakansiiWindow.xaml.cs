using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class EditVakansiiWindow : Window
    {
        алиначкаEntities database;
        Вакансии vakansia;
        MainWindow mainWindow;
        bool IsAdding = false;

        public EditVakansiiWindow(Вакансии вакансия, алиначкаEntities entities, MainWindow main)
        {
            InitializeComponent();
            if (вакансия == null)
            {
                Title = "Добавление";
                vakansia = new Вакансии();
                IsAdding = true;
            }
            else
            {
                Title = "Изменение";
                vakansia = вакансия;
            }
            database = entities;
            mainWindow = main;
            DataContext = vakansia;
        }

        protected void SaveChanges()
        {
            ReadDataFromInput();

            if (IsAdding)
            {
                database.Вакансии.Add(vakansia);
            }
            database.SaveChanges();
            mainWindow.Read();
        }

        void ReadDataFromInput()
        {
            // Чтение данных
            vakansia.Должность = ComboBoxДолжность.SelectedIndex + 1;
            vakansia.Компания = ComboBoxКомпания.SelectedIndex + 1;
            vakansia.Зарплата = Decimal.Parse(TextBoxЗарплата.Text);
            vakansia.График_работы = TextBoxГрафикРаботы.Text;
            vakansia.Сотрудник = ComboBoxСотрудник.SelectedIndex + 1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Инициализация данных
            ComboBoxКомпания.ItemsSource = database.Компании.ToList();
            ComboBoxДолжность.ItemsSource = database.Должности.ToList();
            ComboBoxСотрудник.ItemsSource = database.Сотрудники.ToList();


            if (vakansia.Компания == null)
                ComboBoxКомпания.SelectedIndex = -1;
            else
                ComboBoxКомпания.SelectedItem = vakansia.Компании;

            if (vakansia.Должность == null)
                ComboBoxДолжность.SelectedIndex = -1;
            else
                ComboBoxДолжность.SelectedItem = vakansia.Должности;

            if (vakansia.Сотрудник == null)
                ComboBoxСотрудник.SelectedIndex = -1;
            else
                ComboBoxСотрудник.SelectedItem = vakansia.Сотрудники;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges();
            Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}