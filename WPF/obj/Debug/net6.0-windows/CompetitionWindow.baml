   M S B A M L   `   `   `  ????>  :WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WPFWPF  5   	       WPF.CompetitionWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:WPF     5   	     ? Title$  Competitie Informatie??5   	   $	??450??6'   $	??800??64    ? DataContext 5	      !   WPF.CompetitionContextClass  5
   
   .??5      ? .+?5   
   ?? -   5         ??Name$ DriversListLabel??5      $
??Bold"?6*   $#??Driver Competition Positions:??6<   $
??Left=?6d   $??0,0,0,0q?6   $	??Top=?6?   ? 5   
   $??0,23,0,0q?6    ? RowDefinitions 5      ?? 5      $
??Auto ?6    60   ?? 5      $??1* ?6    6.   .+?5      ?? $??	0,0,0,295q?6E   $??2??6X   ??6   ?? *ParticipantPointsList+ ?? View 5      ?? 5      .P?5      ??  ?? Width$	 350??6)   $
??Naam??65    ?? DisplayMemberBinding 6C   ?? *
Item1.Name+6q   ?? 5      $	 350??6)   $??Punten??65    6E   ?? *Item2+6n   5      5      5      ?? 5   
   -   5        $ NextTrackLabel??5      $
??Bold"?6(   $??Next Track Name:??6:   $
??Left=?6U   $??	0,150,0,0q?6p   $	??Top=?6?   ?? 5   
   -   5        $ 	NextTrack??5      $
??Bold"?6#   $
??Left=?6W   $??120,150,0,0q?6r   $	??Top=?6?   ??65   ?? *nextTrackName+6?   ?? 5    
   -   5        $ TrackListLabel??5       $
??Bold"?6(   $??Complete Track List:??6:   $
??Left=?6Y   $??	0,175,0,0q?6t   $	??Top=?6?   ? 5!   
   $??	0,200,0,0q?6    5"      ?? 5#      $
??Auto ?6    60   ?? 5$      $??2* ?6    6.   .+?5&      ?? $??0,0,0,0q?69   $??2??6J   ??6   ?? *	TrackList+ 5'      ?? 5(      .P?5)      ?? $	 350??6)   $??
Track Naam??65    6I   ?? *Name+6q   5*      5,      5-      5.      5/      