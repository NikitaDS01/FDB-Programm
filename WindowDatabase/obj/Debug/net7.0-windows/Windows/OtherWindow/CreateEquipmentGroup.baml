   M S B A M L   `   `   `  ����I  EWindowDatabase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:WindowDatabase.Windows.OtherWindow"WindowDatabase.Windows.OtherWindow  5   	   S.clr-namespace:WindowDatabase.Core.UserControls WindowDatabase.Core.UserControls  5   	   =    7WindowDatabase.Windows.OtherWindow.CreateEquipmentGroup   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:WindowDatabase.Windows.OtherWindow     5   	   =uc.clr-namespace:WindowDatabase.Core.UserControls     5   	     � Title$  Окно методик��5	   	   $	��450��5
   	   $	��800��6   $	��450��5   	   $	��400��6      ColorWindow#��[  5   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      $
��0.3* �6   .+�5   
   <   6WindowDatabase.Core.UserControls.ParameterTableControl     Title$ 
Номер��6#   $��0��5   '   $��0��67   $��3��5   '      
IsReadOnly 5   '   �� *IsReadOnlyName+   
InsideText 5   '   �� *Index+ �� Mode$ TwoWay=�;   5WindowDatabase.Core.UserControls.LinkParameterControl  5   
      Title$% Ген.оборудование��6"   	   	TypeTable$	 	Generator=�5   "   $��0��5    '   $��1��67   
   Path
 5!   '   �� *LinkEquipmentGEN+$ TwoWay=�  5"   
   $% Тел.оборудование��6"   $	 	Telemetry=�5#   "   $��2��5$   '   $��1��67   
 5%   '   �� *LinkEquipmentTEL+$ TwoWay=�  5&   
   $% Изм.оборудование��6"   $	 	Measuring=�5'   "   $��1��5(   '   $��1��67   
 5)   '   �� *LinkEquipmentIN+$ TwoWay=��� 5*   
   $��0��6   $��3��6!   $��Сохранить��5+      $��2q�5-      ��5,      �� *SaveCommand+5-      �� 5.   
   $��2��6   $��3��6!   $��
Выйти��50      $��2q�51      ��5/      �� *CloseCommand+51      52      53      