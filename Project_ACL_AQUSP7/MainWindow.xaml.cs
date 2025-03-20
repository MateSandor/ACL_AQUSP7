using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AclApp
{
    public partial class MainWindow : Window
    {
        private List<User> users; // Lista a felhasználók tárolására

        public MainWindow()
        {
            InitializeComponent();
            users = new List<User>(); // Felhasználók inicializálása
            LoadUsersFromFile(); // Felhasználók betöltése a fájlból
        }

        // Felhasználók betöltése a fájlból
        private void LoadUsersFromFile()
        {
            string filePath = "users_permissions.txt"; // A fájl neve és helye

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath); // Fájl sorainak beolvasása
                foreach (var line in lines)
                {
                    // Sor feldolgozása: felhasználónév - titkosított jelszó - jogosultságok
                    var parts = line.Split(new[] { " - " }, 3, StringSplitOptions.None);
                    if (parts.Length == 3)
                    {
                        string username = parts[0];
                        string hashedPassword = parts[1];
                        string permissionsString = parts[2];
                        List<string> permissions = permissionsString.Split(new[] { ", " }, StringSplitOptions.None).ToList();

                        User newUser = new User(username, hashedPassword, permissions);
                        users.Add(newUser); // Felhasználó hozzáadása a listához
                        UserListBox.Items.Add($"{newUser.Username} - {string.Join(", ", newUser.Permissions)}"); // Felhasználó megjelenítése a ListBoxban
                    }
                }
            }
        }

        // Új felhasználó hozzáadása
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text; // Felhasználónév
            string password = PasswordBox.Password; // Jelszó

            // Jogosultságok összegyűjtése
            List<string> permissions = new List<string>();
            if (ReadCheckBox.IsChecked == true) permissions.Add("read");
            if (WriteCheckBox.IsChecked == true) permissions.Add("write");
            if (AppendCheckBox.IsChecked == true) permissions.Add("append");
            if (ExecuteCheckBox.IsChecked == true) permissions.Add("execute");
            if (OwnCheckBox.IsChecked == true) permissions.Add("own");

            if (!string.IsNullOrEmpty(username) && permissions.Count > 0)
            {
                string hashedPassword = HashPassword(password); // Jelszó titkosítása
                User newUser = new User(username, hashedPassword, permissions);
                users.Add(newUser); // Új felhasználó hozzáadása a listához
                UserListBox.Items.Add($"{newUser.Username} - {string.Join(", ", newUser.Permissions)}"); // Felhasználó megjelenítése a ListBoxban
                ClearInputFields(); // Beviteli mezők törlése

                SaveUsersToFile(); // Felhasználók mentése a fájlba
            }
            else
            {
                MessageBox.Show("Kérjük, adjon meg egy felhasználónevet és válasszon jogosultságokat.");
            }
        }

        // Felhasználók mentése a fájlba
        private void SaveUsersToFile()
        {
            string filePath = "users_permissions.txt"; // A fájl neve és helye

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var user in users)
                {
                    // Felhasználó adatok írása a fájlba
                    string permissions = string.Join(", ", user.Permissions);
                    writer.WriteLine($"{user.Username} - {user.HashedPassword} - {permissions}");
                }
            }
        }

        // Jelszó titkosítása SHA256 algoritmus segítségével
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Hexadecimális reprezentáció
                }
                return builder.ToString();
            }
        }

        // Bejelentkezés kezelése
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginUsernameTextBox.Text; // Bejelentkezési felhasználónév
            string password = LoginPasswordBox.Password; // Bejelentkezési jelszó

            // Jelszó titkosítása a bejelentkezéshez
            string hashedPassword = HashPassword(password);

            // Ellenőrizni, hogy a felhasználó létezik-e
            var user = users.FirstOrDefault(u => u.Username == username && u.HashedPassword == hashedPassword);
            if (user != null)
            {
                MessageBox.Show("Sikeres bejelentkezés!");

                // Jogosultságok ellenőrzése
                if (user.Permissions.Contains("read"))
                {
                    UserListBox.Visibility = Visibility.Visible; // Lista láthatóvá tétele
                }
                else
                {
                    MessageBox.Show("Nincs jogosultsága a lista megtekintésére.");
                }

                // Felhasználói mezők engedélyezése "write" jogosultság esetén
                if (user.Permissions.Contains("write"))
                {
                    Write_EnableUserInputFields(true);
                    UpdateLabelsColor(Colors.Black); // Feliratok színének frissítése
                }

                // Felhasználói mezők engedélyezése "admin" jogosultság esetén
                if (user.Permissions.Contains("own"))
                {
                    Admin_EnableUserInputFields(true);
                    UpdateLabelsColor(Colors.Black); // Feliratok színének frissítése
                }
            }
            else
            {
                MessageBox.Show("Hibás felhasználónév vagy jelszó.");
            }
        }

        // Feliratok színének frissítése
        private void UpdateLabelsColor(Color color)
        {
            // Az összes Label színének frissítése az első StackPanel-ben
            foreach (var child in ((StackPanel)UsernameTextBox.Parent).Children)
            {
                if (child is Label label)
                {
                    label.Foreground = new SolidColorBrush(color);
                }
            }

            // Az összes Label színének frissítése a második StackPanel-ben
            foreach (var child in ((StackPanel)LoginUsernameTextBox.Parent).Children)
            {
                if (child is Label label)
                {
                    label.Foreground = new SolidColorBrush(color);
                }
            }
        }

        // Felhasználói mezők engedélyezése "write" jogosultság esetén
        private void Write_EnableUserInputFields(bool isEnabled)
        {
            UsernameTextBox.IsEnabled = isEnabled;
            PasswordBox.IsEnabled = isEnabled;
            ReadCheckBox.IsEnabled = isEnabled;
            WriteCheckBox.IsEnabled = isEnabled;
            AppendCheckBox.IsEnabled = false; // Append nem engedélyezett
            ExecuteCheckBox.IsEnabled = false; // Execute nem engedélyezett
            OwnCheckBox.IsEnabled = false; // Own nem engedélyezett
            AddUserButton.IsEnabled = isEnabled; // Gomb engedélyezése
        }

        // Felhasználói mezők engedélyezése "admin" jogosultság esetén
        private void Admin_EnableUserInputFields(bool isEnabled)
        {
            UsernameTextBox.IsEnabled = isEnabled;
            PasswordBox.IsEnabled = isEnabled;
            ReadCheckBox.IsEnabled = isEnabled;
            WriteCheckBox.IsEnabled = isEnabled;
            AppendCheckBox.IsEnabled = isEnabled; // Append engedélyezett
            ExecuteCheckBox.IsEnabled = isEnabled; // Execute engedélyezett
            OwnCheckBox.IsEnabled = isEnabled; // Own engedélyezett
            AddUserButton.IsEnabled = isEnabled; // Gomb engedélyezése
        }

        // Beviteli mezők törlése
        private void ClearInputFields()
        {
            UsernameTextBox.Clear();
            PasswordBox.Clear();
            ReadCheckBox.IsChecked = false;
            WriteCheckBox.IsChecked = false;
            AppendCheckBox.IsChecked = false;
            ExecuteCheckBox.IsChecked = false;
            OwnCheckBox.IsChecked = false;
        }
    }

    // Felhasználó osztály
    public class User
    {
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public List<string> Permissions { get; set; }

        public User(string username, string hashedPassword, List<string> permissions)
        {
            Username = username;
            HashedPassword = hashedPassword;
            Permissions = permissions;
        }
    }
}