using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public алиначкаEntities entities;
        public MainWindow()
        {
            InitializeComponent();
            Read();
        }

        // Чтение данных
        public void Read()
        {
            // Инициализация сущности базы данных
            entities = new алиначкаEntities();
            DataContext = entities.Database;
            // Инициализация таблиц
            DataGridВакансии.ItemsSource = entities.Вакансии.ToArray();
            DataGridДолжности.ItemsSource = entities.Должности.ToArray();
            DataGridКомпании.ItemsSource = entities.Компании.ToArray();
            DataGridСоискатели.ItemsSource = entities.Соискатели.ToArray();
            DataGridСотрудники.ItemsSource = entities.Сотрудники.ToArray();
            DataGridПрочее.ItemsSource = entities.Полы.ToArray();
        }

        // Удаление
        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            //var selected = ((sender as Button).DataContext as Вакансии);
            var selected = DataGridВакансии.SelectedItem as Вакансии;
            if (MessageBox.Show($"Вы уверены, что хотите удалить запись #{selected.ID}?",
                "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                entities.Вакансии.Remove(selected);
                entities.SaveChanges();
                Read();
            }
        }

        // Изменение
        private void EditRow_Click(object sender, RoutedEventArgs e)
        {
            var selected = DataGridВакансии.SelectedItem as Вакансии;
            //var selected = ((sender as Button).DataContext as Вакансии);
            var windows = new EditVakansiiWindow(selected, entities,this);
            var result = windows.ShowDialog();
            Read();
        }

        // Добавление
        private void AddRowButton_Click(object sender, RoutedEventArgs e)
        {
            var windows = new EditVakansiiWindow(null, entities, this);
            var result = windows.ShowDialog();
            Read();
        }

        // Поиск по всем столбцам таблицы Вакансии
        private void Searching()
        {
            // Cчитывание ввода
            int tabIndex = TableTabControl.SelectedIndex;
            string input = TextBoxPoisk.Text.ToLower();
            if (!(String.IsNullOrEmpty(input))) // Проверка на пустые символы или пробел
            {
                switch (tabIndex)
                {
                    case 0:
                        DataGridВакансии.ItemsSource = entities.Вакансии.Where(x => x.Должности.Название.Contains(input) || x.Компании.Название.Contains(input) || x.Сотрудники.Фамилия.Contains(input)).ToArray();
                        break;
                    case 1:
                        DataGridДолжности.ItemsSource = entities.Должности.Where(x => x.Название.Contains(input)).ToArray();
                        break;
                    case 2:
                        DataGridКомпании.ItemsSource = entities.Компании.Where(x => x.Название.Contains(input) || x.Отрасль.Contains(input)).ToArray(); 
                        break;
                    case 3:
                        DataGridСоискатели.ItemsSource = entities.Соискатели.Where(x => x.Фамилия.Contains(input) || x.Имя.Contains(input) || x.Отчество.Contains(input)).ToArray();
                        break;
                    case 4:
                        DataGridСотрудники.ItemsSource = entities.Сотрудники.Where(x => x.Фамилия.Contains(input) || x.Имя.Contains(input) || x.Отчество.Contains(input) || x.Компании.Название.Contains(input) || x.Город.Contains(input)).ToArray();
                        break;
                    default: break;
                }
            }
            else
                Read();
        }

        // Фильтрация по зарплате
        private void Filtration()
        {
            // Вернияя граница и нижняя граница фильтрации по зарплате
            var upper = String.IsNullOrWhiteSpace(TextBoxUpperBound.Text) == false ? int.Parse(TextBoxUpperBound.Text) : 0;
            var lower = String.IsNullOrWhiteSpace(TextBoxLowerBound.Text) == false ? int.Parse(TextBoxLowerBound.Text) : 0;
            if (upper > 0 && upper >= lower && lower >= 0)
            {
                var filteredData = entities.Вакансии.Where(x =>
                x.Зарплата <= upper &&
                x.Зарплата >= lower).ToArray();
                DataGridВакансии.ItemsSource = filteredData;
            }
            else
                Read(); // Сброс фильтра в случае неверного ввода
        }


        // Ввод текста в поле поиска
        private void TextBoxPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            Searching();
        }

        // Сброс фильтров и поиска
        private void ClearPoisk_Click(object sender, RoutedEventArgs e)
        {
            TextBoxUpperBound.Text = "";
            TextBoxLowerBound.Text = "";
            TextBoxPoisk.Text = "";
            Read();
        }

        // Ввод данных в поле фильтрации
        private void FiltrationInput_TextChaned(object sender, TextChangedEventArgs e)
        {
            Filtration();
        }

        // Предотвращение ввода НЕ цифр в поля фильтров
        private void SalaryFiltrationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        { 
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Смена выделения на таблице Вакансии
        private void DataGridВакансии_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Если не выбрана ни одна ячейка, то скрываются кнопки удаления и изменения с таблицей
            int count = DataGridВакансии.SelectedItems.Count;
            if (count >= 1)
            {
                EditRowButton.IsEnabled = true;
                DeleteRowButton.IsEnabled = true;
            }
            else
            {
                EditRowButton.IsEnabled = false;
                DeleteRowButton.IsEnabled = false;
            }
        }

        // Смена текущей вкладки
        private void TableTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Если текущая вкладка не Вакансии, то скрывается блок управления таблицей
            if(TableTabControl.SelectedIndex != 0)
            {
                GroupBoxRow.Visibility = Visibility.Collapsed;
                GroupBoxFilter.Visibility = Visibility.Collapsed;
            }
            else
            {
                GroupBoxRow.Visibility = Visibility.Visible;
                GroupBoxFilter.Visibility = Visibility.Visible;
            }
        }


    }
}
