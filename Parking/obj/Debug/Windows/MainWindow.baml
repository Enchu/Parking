   M S B A M L   `   `   `  ����B  >Parking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ParkingParking  5   	       Parking.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Parking     5   	     � Title$'  !Форма Авторизации��5   	   $	��300��6$   $	��400��64   $��Tahoma)�6C   $��18(�6W   .��5	      (� $��0��6   $��Stretch=�6   $��Stretch=�69   .��5
   
   �  � RowDefinitions 5      �� 5      $��60 �6    �� 5      .+�5      � $��Center=�6   $��0��6.   .+�5      �� $
��Left=�6   $��60��63   $��60��6?   $��/Resources/60526.png��6J   $��
5, 0, 0, 0q�6h   �� 5      -   5        $��
Выход��5   +   $��Right=�6;   $��
0, 0, 5, 0q�6W   �� 5      $'��!Форма Авторизации��6   $��18(�65   $��Medium"�6C   $��Center=�6W   $��Center=�6t   5      � 5      $��1��6   .+�5      �� -   5        $��
pwdTextBox��5      $��106,97,178,175q�6,   $	��167��6D   $y�Hidden=�6P   $��30��6d    J� RenderTransformOrigin$ 0.5,0.5&�6p   �� 5      -   5        $��LoginBox��5   "   $�� ��6   $
��Left=�62   $��31��6M   $��
106,44,0,0q�6Y    �� TextWrapping$
 Wrap=�6m   $	��Top=�6�   $	��167��6�   G� 5      -   5         G�Name$ PasswordBox��5   *    G� Password$  ��6    G� PasswordChar$ *��6?   $	��Top=�6P   $��106,97,179,0q�6h   �� 5      -   5         ��Name$ revealModeCheckBox��5      $#��Показать/Скрыть��67    d� 	IsChecked$ FalseY�6Q   $��106,136,174,134q�6c   6|   �� 5      $��
Логин��6   $
��Left=�6(   $��	38,44,0,0q�6C   $	��Top=�6V   $��Arial)�6n   �� 5      $��Пароль��6   $
��Left=�6)   $��
24,102,0,0q�6D   $	��Top=�6X   $��Arial)�6p   �� 5      -   5        $"��Авторизоваться��5      $
��Left=�63   $��106,187,0,0q�6N   $	��Top=�6c   $	��167��6{   $��40��6�   $��Arial)�6�   �� 5      -   5        $��ImageCaptcha��5   ,   $y�Hidden=�6   $
��Left=�6@   $��87��6[   $��
291,38,0,0q�6g   $	��Top=�6{   $	��151��6�   �� 5       -   5        $	��Cap��5    .   $y�Hidden=�6   $
��Left=�69   $��34��6T   $��291,145,0,0q�6`   $
 Wrap=�6u   $�� ��6�   $	��Top=�6�   $	��155��6�   �� 5!      -	   5        	 ��Name$	 
statusText��5!      $��10,211,356,10q�60   $��Center=�6G   $��86��6d   5"      5#      5$      5%      