   M S B A M L   `   `   `  ����B  >Parking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Parking.WindowsParking.Windows  5   	   $    Parking.Windows.BuildingWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Parking.Windows     5   	     � Title$!  Форма Строений��5   	   $	��400��6    $	��900��60   $��Tahoma)�5	   	   $��14(�5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowState$ 	Maximized=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $	��120 �6   �� 5      .+�5   
   � $��Center=�6   $��0��6*   .+�5      �� $
��Left=�6   $��60��6/   $��60��6;   $��/Resources/60526.png��6F   $��
5, 0, 0, 0q�6d   �� 5      -   5        $��
Назад��5   '   $��Right=�67   $��
0, 0, 5, 0q�6S   �� 5      $!��Форма Строений��6   $��18(�6.   $��Medium"�6<   $��Center=�6P   $��Center=�6m   5      � 5   
   $��5q�6   $��1��6    � ColumnDefinitions 5      �� 5      $
��0.8* �6#   �� 5      �� 5       �� 5!      �� 5"      .+�5$      �� $��5q�6   $��0��6$   .+�5%      �� $��Строение��6   �� 5&      $��
Улицы��6   �� 5'      $#��Количество мест��6   5(      �� 5)      $��5q�6   $��1��6$   .+�5*      �� -   5        $��
TBBuilding��5*      �� 5+      -   5        $��	CBAddress��5+      �� 5,      -   5        $��TBNumberQuant��5,      �� 5-      -   5        $��TBIDBuilding��5-      5.      �� 5/      $��5q�6   $��Center=�6$   $��2��6?   .+�50      �� -   5        $
��Auto��50      $��Добавить��6&   69   �� 51      -   5        $
��Auto��51      $ ��Редактировать��6&   6>   �� 52      -   5        $
��Auto��52      $��Удалить��6&   68   53      �� 54      $��5q�6   $��3��6$   $��Center=�64   .+�55      �� -	   5        $
��Auto��55      $��
Обзор��6&   66   �� 56      -
   5        $
��Auto��56      $��Загрузить��6&   6:   57      �� 58      $��5q�6   $��4��6$   .+�59      �� -   5        $��	PicWorker��59   '   $
��Fill=�6   5:      5;      5<      � 5?   
   $��Stretch=�6   $��Stretch=�6+   $��2��6I   .+�5@      &   System.Windows.Controls.DataGrid  -   5          Name$ DataGridOsn��5@   s      
IsReadOnly . $
��Auto��6X   $
��Auto��6e      AutoGenerateColumns .  ��6�   �� *dt+   Columns 5A      0  *System.Windows.Controls.DataGridTextColumn  5B      ,  &System.Windows.Controls.DataGridColumn	   
Visibility$	 Hidden=�6*   -   5        id��
   Header$
 id��5B   J   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6V   �� *id+  5C      $	 Visible=�6*   -   5        building��$
 Строение��5C   Q    6c   �� *
building+  5D      -   5        address��$
 
Адрес��5D   ;    6J   �� *	address+  5E      -   5        number_quant��$#
 Количество мест��5E   @    6Y   �� *number_quant+4  .System.Windows.Controls.DataGridTemplateColumn  5F      $
 
Схема��6-      
IsReadOnly .    CellTemplate 5G      �� 5H      .R�5I   "   ��  ��Name$ chema��6(   $��20��67   $��20��6C   ��6N   �� *chema+5J      5L      5N      5O      5P      5Q      