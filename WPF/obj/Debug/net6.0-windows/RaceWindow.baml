   M S B A M L   `   `   `  ????>  :WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WPFWPF  5   	       WPF.RaceWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:WPF     5   	     ? Title$  Race Informatie??5   	   $	??450??6!   $	??800??6.    ? DataContext 5	         WPF.RaceContextClass  5
   
   .??5      ? .+?5   
   ?? -   5         ??Name$ Crashed??5      $
??Bold"?6!   $??Crashed Drivers:??63   $
??Left=?6N   $??0,0,0,0q?6i   $	??Top=?6z   ? 5   
   $??0,23,0,0q?6    ? RowDefinitions 5      ?? 5      $
??Auto ?6    60   ?? 5      $??1* ?6    6.   .+?5      ?? $??	0,0,0,295q?6H   $??2??6[   ??6   ?? *IsBrokenParticipantsList+ ?? View 5      ?? 5      .P?5      ??  ?? Width$	 350??6)   $
??Naam??65    ?? DisplayMemberBinding 6C   ?? *
Item1.Name+6q   ?? 5      $	 350??6)   $
??Tijd??65    6C   ?? *Item2+6l   5      5      5      ?? 5   
   -   5        $ TimeElapsedLabel??5      $
??Bold"?6*   $??Time Elapsed: ??6<   $
??Left=?6U   $??	0,150,0,0q?6p   $	??Top=?6?   6?   ?? 5   
   -   5        $ TimeElapsed??5      $
??Bold"?6%   $
??Left=?6W   $??
80,150,0,0q?6r   $	??Top=?6?   ??67   ?? *TimeElapsed+6?   ?? 5!   
   -   5        $ EquipmentData??5!      $
??Bold"?6'   $??Equipment Data:??69   $
??Left=?6S   $??	0,185,0,0q?6n   $	??Top=?6?   ? 5#   
    5$      ?? 5%      $
??Auto ?6    60   ?? 5&      $??2* ?6    6.   .+?5(      ?? $??	0,210,0,0q?6J   $??1??6]   ??6   ?? *ParticipantPerformanceList+ 5)      ?? 5*      .P?5+      ?? $	 200??6)   $
??Naam??65    6C   ?? *
Item1.Name+6q   ?? 5,      $ 75??6)   $??Performance??64    6I   ?? *Item2.Performance+6~   ?? 5-      $ 75??6)   $??Speed??64    6C   ?? *Item2.Speed+6r   ?? 5.      $ 75??6)   $??Quality??64    6E   ?? *Item2.Quality+6v   ?? 5/      $ 75??6)   $??Color??64    6C   ?? *Item1.TeamColor+6v   50      52      53      54      55      