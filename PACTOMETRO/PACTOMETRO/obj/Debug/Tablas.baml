   M S B A M L   `   `   `  ����E  APACTOMETRO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PACTOMETRO
PACTOMETRO  5   	       PACTOMETRO.Tablas   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:PACTOMETRO     5   	     � 
ResizeMode$  NoResize=�5   	    � Title$ Tablas��5	   	   $	��450��6   $	��800��6%   .��5
      � $��25q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ eleccionesListView��5      $��0��6/   $��0��6?   $��0 0 0 10q�6�    �� View 5      �� 5      .P�5      �� $��Nombre��6%    �� DisplayMemberBinding 65   �� *Nombre+�� 5      $��Escaños��6%    66   �� *
Escaños+�� 5      $��Mayoria��6%    66   �� *	Mayoria+�� 5      $��Fecha��6%    �� CellTemplate 5      �� 5      .R�5   "   �� ��6,   �� *Fecha+ �� StringFormat$ 
dd/MM/yyyy��5      5       5!      5#      �� 5$   
   -   5        $ partidosListView��5$      $��0��6-   $��1��6=   $��0 10 0 0q�6J    5%      �� 5&      .P�5'      �� $��Nombre��6%    65   �� *Nombre+�� 5(      $��Escaños��6%    66   �� *
Escaños+�� 5)      $��Color��6%    5*      �� 5+      .R�5-   "   �� $	��100��6,   $��20��68   ��6D   �� *Color+5.      50      51      53      �� 55   
   $��1��6   $��0��6%   	 �� Orientation$	 Vertical=�62   $��15q�6I   .+�56      �� -   5        $��5q�56      .��57      �� $��add.png��6   58      �� 59      -   5        $��5q�59      .��5:      �� $��edit.png��6   5;      �� 5<      -   5        $��5q�5<      .��5=      �� $��
delete.png��6   5>      5?      5@      5A      