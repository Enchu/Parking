   M S B A M L   `   `   `  ����B  >Parking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Parking.WindowsParking.Windows  5   	   U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5   	   #    Parking.Windows.StatistWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Parking.Windows     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5   	     � Title$%  Форма статистики��5	   	   $	��450��6"   $	��800��62   $��Tahoma)�5
   	   $��14(�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowState$ 	Maximized=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��60 �6   �� 5      .+�5   
   � $��Center=�6   $��0��6*   .+�5      �� $
��Left=�6   $��60��6/   $��60��6;   $��/Resources/60526.png��6F   $��
5, 0, 0, 0q�6d   �� 5      -   5        $��
Назад��5   '   $��Right=�67   $��
0, 0, 5, 0q�6S   �� 5      $%��Форма Статистики��6   $��18(�60   $��Medium"�6>   $��Center=�6R   $��Center=�6o   5      � 5   
   $��5q�6   $��1��6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5      �� $
��От��6   $��70��6%   (  "System.Windows.Controls.DatePicker  5       -   5        $
��DPOt��5          DisplayDateStart$ 
01.01.2018X�6)      DisplayDateEnd$ 
01.01.2022X�6G   $
��Auto��6c   �� 5!      $
��До��6   $��70��6%     5"      -   5        $
��DPDo��5"      $ 
01.01.2018X�6)   $ 
01.01.2022X�6G   $
��Auto��6c   �� 5#      -   5        $	��150��5#   /   $��Сформировать��6;   �� 5$      -   5        $	��150��5$   3   $��
Отчет��6?   5%      5&      � 5)   
   $��2��6   .+�5*        LiveCharts.Wpf.PieChart  -   5        $��Chart��5*      &   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Right=�5+         InnerRadius$	 100Q�6.   	   Series	 5*   (   �� *SeriesCollection+
   ChartLegend
 5,      "  LiveCharts.Wpf.DefaultLegend  5-         
BulletSize$ 20Q�6(   6:      DataTooltip 5/      #  LiveCharts.Wpf.DefaultTooltip  50         
BulletSize$ 20Q�6)   6;   52      53      54      55      