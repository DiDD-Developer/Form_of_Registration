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

namespace FormofRegistration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Добавление списка стран для Checkbox
            List<string> countries = new List<string>();
            countries.Add("Русский"); //Russian - Русский
            countries.Add("English"); //English - Английский
            countries.Add("Español"); //Spanish - Испанский
            countries.Add("Português"); //Portuguese - Португальский
            countries.Add("Français"); //French - Французский
            countries.Add("বাংলা"); //Bangla - Бангла
            countries.Add("हिंदी"); //Hindi - Хинди
            countries.Add("日本"); //Japan - Японский
            countries.Add("عرب"); //Arab - Арабский

            cmbBox.ItemsSource = countries;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //1. Условие, чтобы логин был > 10 символов и начинался с буквы
            if (login_txt.Text.Length < 10 || !Char.IsLetter(login_txt.Text[0]))
            {
                MessageBox.Show($"Ошибка! Вы ввели {login_txt.Text.Length} символов в логине или начали его ввод с цифры, а в нем должно быть больше 10 символов и начинаться с буквы!");
                login_txt.Text = ""; // очищаем поле
            }

            //2. Условие, в котором проверяем, есть ли спец. символ в тексте
            if (!password_txt.Text.Contains("!"))
            {
                MessageBox.Show("Ошибка! Пароль должен содержать спец. символ \'!\'");
                password_txt.Text = ""; // очищаем поле
            }

            //3. Условие, при котором логин обязан быть не идентичен с паролем
            if (login_txt.Text == password_txt.Text)
            {
                MessageBox.Show("Ошибка! Пароль похож с логином. Так быть не должно!");
                login_txt.Text = ""; // очищаем поля
                password_txt.Text = "";
            }

            //4. Условие, при котором пароль должен совпадать с повторным паролем
            if(password_txt.Text != RetryPassword_txt.Text)
            {
                MessageBox.Show("Ошибка! Пароли различаются в полях \"Пароль\" и \"Повтор пароля\". Так быть не должно!");
                password_txt.Text = ""; // очищаем поля
                RetryPassword_txt.Text = "";
            }

            //Вывод информации о новом пользователе в текстовый документ
                StreamWriter str = new StreamWriter(@"D:\Project\FormofRegistration\FormofRegistration\person.txt", true, System.Text.Encoding.Default);

            // Если условие выполняется, то ничего не записывается
            if (login_txt.Text == "" || password_txt.Text == "" || RetryPassword_txt.Text == "" || cmbBox.SelectedValue.ToString() == null)
            {
                MessageBox.Show("Одно или несколько из полей являются пустыми! Регистрация не произведена! Повторите попытку!");
            }
            else
            {
                str.WriteLine($"Новый пользователь!\n Логин: \"{login_txt.Text}\";\n Пароль: \"{password_txt.Text}\"; \n Язык: {cmbBox.SelectedValue.ToString()}");
                MessageBox.Show("Регистрация прошла успешно!");

                //Ecли пользователь указал свой e-mail, то его тоже впишут с остальными данными пользователя в текстовый документ
                if(email_txt.Visibility == Visibility.Visible)
                {
                    str.WriteLine($"E-mail: {email_txt.Text}\n");
                }
                str.Close();
            }
        }
            //4. Если мы нажимаем на Checkbox, то становится активным поле ввода email
            //Для моего чек бокса я прописываю два события: CheckBox_Checked, в котором чек бокс активирован, из-за чего появляется поле ввода email
            // и CheckBox_Unchecked, при котором чек бокс не активирован, соответственно поле ввода email не появляется в форме регистрации
            private void CheckBox_Checked(object sender, RoutedEventArgs e)
            {
                email_txtstatic.Visibility = Visibility.Visible; //Поля скрыты, пока не нажат чек бокс
                email_txt.Visibility = Visibility.Visible; //Обработчик события Сhecked устанавливает свойство Visibility на Visible, чтобы показать поле ввода email.
            }

            private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
            {
                email_txtstatic.Visibility = Visibility.Collapsed; //Обработчик события Unchecked устанавливает свойство Visibility на Collapsed, чтобы скрыть поле ввода email,
                email_txt.Visibility = Visibility.Collapsed;       //когда CheckBox не выбран.
            }
    }
}
