   M S B A M L   `   `   `  ����I  EWpfDataBinding, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:WpfDataBindingWpfDataBinding  5   	       WpfDataBinding.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:WpfDataBinding     5   	     � Title$  Person��5   	   $	��450��6   $	��800��6%   .��5	      � $��20q�6    � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��8* �6   .+�5   
   �� $��Jméno��6   $��0��6!   $��0��61   �� 5   
   -   5         ��Name$ tbJmeno��5      $��1��6#   $��0��63   ��5      �� *Jmeno+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5   
   $��Příjmení��6   $��0��6$   $��1��64   �� 5   
   -   5        $ 
tbPrijmeni��5      $��1��6&   $��1��66   ��5      �� *
Prijmeni+$ TwoWay=�$ 	LostFocus=��� 5   
   $��Datum narození��6   $��0��6*   $��2��6:   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ datNar��5      $��1��6%   $��2��65      Text 5      �� *
Narozeni+�� 5   
   -   5         ��Name$ btShow��5      $��Ukaž��6!   $��0��60   $��3��6@   6M   �� 5   
   -   5        $ btClear��5      $��Čisti��6"   $��1��62   $��3��6B   6O   �� 5    
   -   5        	 ��Name$	 
stavPerson��5       $��4��6(   $��2��65   ��5!      �� *Status+5#      5$      