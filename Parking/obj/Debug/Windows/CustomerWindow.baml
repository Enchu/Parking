   M S B A M L   `   `   `  ����B  >Parking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Parking.WindowsParking.Windows  5   	   $    Parking.Windows.CustomerWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   )localclr-namespace:Parking.Windows  5   	     � Title$  Форма Клиента��5	   	   $	��450��6   $	��800��6/   $��Tahoma)�5
   	   $��14(�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowState$ 	Maximized=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $	��160 �6   �� 5      .+�5   
   � $��Center=�6   $��0��6*   .+�5      �� $
��Left=�6   $��60��6/   $��60��6;   $��/Resources/60526.png��6F   $��
5, 0, 0, 0q�6d   �� 5      -   5        $��
Назад��5   '   $��Right=�67   $��
0, 0, 5, 0q�6S   �� 5      $��Форма Клиента��6   $��18(�6-   $��Medium"�6;   $��Center=�6O   $��Center=�6l   5      � 5   
   $��5q�6   $��1��6    � ColumnDefinitions 5      �� 5      $
��0.6* �6#   �� 5       �� 5!      $
��0.6* �6#   �� 5"      �� 5#      .+�5%      �� $��0��6   .+�5&      �� $��Имя��6   �� 5'      $��Фамилия��6   �� 5(      $��Отчество��6   �� 5)      $��Телефон��6   5*      �� 5+      $��5q�6   $��1��6$   .+�5,      �� -   5        $��TBFirst_name��5,      6.   �� 5-      -   5        $��TBLast_name��5-      6-   �� 5.      -   5        $��TBPatronomyc��5.      6.   %  Xceed.Wpf.Toolkit.MaskedTextBox  5/      -   5        $��TBPhone��5/   q   $��Center=�6%   $��2q�6@   $��35��6L      Mask$ +7 (000) 000-0000��6X   6q   50      �� 51      $��5q�6   $��2��6$   .+�52      �� $��Email��6   �� 53      $��Пароль��6   �� 54      $��Пол��6   55      �� 56      $��5q�6   $��3��6$   .+�57      �� -   5        $��TBEmail��57      �� 58      -   5        $��
TBPassword��58      �� 59      -   5        $��CBGender��59      �� 5:      -	   5        $��	TBIdEmail��5:      $y�Hidden=�6+   5;      �� 5<      $��5q�6   $��Center=�6$   $��4��6?   .+�5=      �� -
   5        $
��Auto��5=      $��Добавить��6&   69   �� 5>      -   5        $
��Auto��5>      $ ��Редактировать��6&   6>   �� 5?      -   5        $
��Auto��5?      $��Удалить��6&   68   5@      5A      � 5D   
   $��Stretch=�6   $��Stretch=�6+   $��2��6I   .+�5E      &   System.Windows.Controls.DataGrid  -   5          Name$ DataGridOsn��5E   s      
IsReadOnly . $
��Auto��6X   $
��Auto��6e      AutoGenerateColumns .  ��6�   �� *dt+	   Columns	 5F      0  *System.Windows.Controls.DataGridTextColumn  5G      -   5        	Email��,  &System.Windows.Controls.DataGridColumn
   Header$
 Email��5G   9   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *Email+  5H      -   5        Password��$
 Пароль��5H   <    6L   �� *
Password+  5I      -   5        
First_name��$
 Имя��5I   >    6K   �� *
First_name+  5J      -   5        	Last_name��$
 Фамилия��5J   =    6N   �� *	Last_name+  5K      -   5        
Patronomyc��$
 Отчество��5K   >    6P   �� *
Patronomyc+  5L      -   5        	Phone��$
 Телефон��5L   9    6J   �� *Phone+  5M      -   5        
Gender��$
 Пол��5M   :    6G   �� *Gender+5O      5P      5Q      5R      