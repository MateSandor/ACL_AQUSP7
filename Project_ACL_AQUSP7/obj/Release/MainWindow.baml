   M S B A M L   `   `   `  ����   Project_ACL_AQUSP7    AclApp.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Access Control List��$	��600��$	��800��.���  � RowDefinitions �� $	��370 ���  � ColumnDefinitions �� $��* ��� $��* �.+��� $��10q�$��0��$��0��.+��� $,��&Felhasználó hozzáadása a listához��$
��Bold"�$��16(�$��0,0,0,10q�
��������� $��Felhasználónév��$
��Bold"�$��0,0,0,5q�
��������� -    ��Name$ UsernameTextBox��$��30��$��0,0,0,10q�{�.  �� $��Jelszó��$
��Bold"�$��0,0,0,5q�
�������G� -    G�Name$ PasswordBox��$��30��$��0,0,0,10q�{�.  �� $��Jogosultságok:��$��0,0,0,5q�
��������� .+��� -    ��Name$ ReadCheckBox��$
��read��$��0,0,0,5q�{�.  �� -   $ WriteCheckBox��$��write��$��0,0,0,5q�{�.  �� -   $ AppendCheckBox��$��append��$��0,0,0,5q�{�.  �� -   $ ExecuteCheckBox��$��execute��$��0,0,0,5q�{�.  �� -   $ OwnCheckBox��$	��own��$��0,0,0,5q�{�.  �� -    ��Name$ AddUserButton��$ ��Felhasználó Hozzáadása��$��	0,10,0,10q�{�.  �� $��10q�$��0��$��1��.+��� $��Bejelentkezés��$
��Bold"�$��16(�$��0,0,0,10q��� $��Felhasználónév��$
��Bold"�$��0,0,0,5q��� -	   $ LoginUsernameTextBox��$��30��$��0,0,0,10q��� $��Jelszó��$
��Bold"�$��0,0,0,5q�G� -
   $ LoginPasswordBox��$��30��$��0,0,0,10q��� -   $��Bejelentkezés��$��	0,10,0,10q��� -    ��Name$ UserListBox��$��1��$��2��$��10q�$y�	Collapsed=�