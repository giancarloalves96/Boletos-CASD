   M S B A M L   `   `   `  ���� G  CBoletos_CASD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Boletos_CASDBoletos_CASD      Boletos_CASD.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:Boletos_CASD       � Title$0  *Sistema de Gerenciamento de Boletos - CASD��$	��350��$	��525�� � 
ResizeMode$ CanMinimize=� � Icon ��fbfile:///C:/Users/FMMC_/Documents/GitHub/Boletos-CASD/Boletos_CASD/component/Images/logoCASD_v2.png�� J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command$	 New��.��� .+�]� $��0q�.+�o� $	��Top=�$��22��$	��Top=�.@�m� $��A_rquivo��$��22��.@�m� -   $��I_nício��m� -   $��C_arregar dados��m� -   $��M_andar email���� m� -   $��_Sair��m� $��_Editar��.@�m�  m� Command$	 Cut��m� $
 Copy��m� $ Paste��m� $��_Ferramentas��.@�m� -   $��_Analisar dados��m� $��Conf_igurações��.@�m� -   $��P_ropriedades���� m� $��S_obre���� -    ��Name$ 	mainLabel��$	��Top=�$	��Top=�$��Boletos CASD��$��24(�
������ $��5,0,0,0q�� -	    �Name$ sectionGrid��$	��Top=�$��10q�.+�� -
   $ 	emailGrid��$y�Hidden=�.+��� -    ��Name$ txt_subject��	 �� AcceptsReturn	 . $��0,0,10,0q�$	��Top=�$��20��
 J� RenderTransformOrigin$
 0.5,0.5&�$	��382��$	��Top=�$��Right=�.�� � ContextMenu �� .@�m� $	 Cut��m� $
 Copy��m� $ Paste��.���� -   $ 
txt_person��	 . $��
0,30,262,0q�$	��Top=�$	��Top=�$��Right=�$	��130��$��20��$��@aluno��.�� �� .@�m� $	 Cut��m� $
 Copy��m� $ Paste��.���� -   $ txt_message��	 . $��10,60,10,35q�$	��Top=�$m��gOlá, @aluno!
Este é o seu boleto do mês de #MES.
Por favor, pagar até o dia 15.

Saudações,
CASD.��.�� �� .@�m� $	 Cut��m� $
 Copy��m� $ Paste��.��� $��319,28,10,0q�$	��Top=�.+��� -    ��Name$ CB_month_emailGrid��$	��Top=�$��0,0,65,0q�$��Right=�$��84��.@���  �� 
IsSelected . $
��Mês���� $��Janeiro���� $��	Fevereiro���� $��Março���� $��Abril���� $
��Maio���� $��Junho���� $��Junho���� $��Agosto���� $��Setembro���� $��Outubro���� $��Novembro���� $��Dezembro���� -   $ CB_year_emailGrid��$��0q�$��Right=�$��60��.@��� $	��Ano����  . $
��2017���� $
��2018���� $
��2019���� $
��2020���� $
��2021���� $
��2022���� -    ��Name$ button_sendEmail��$��Enviar��$��Right=�$��	0,0,10,10q�$��Bottom=�$��75���� $
��Left=�$��45,0,0,0q�$
��Wrap=�$��Assunto��$	��Top=��� $
��Left=�$��	10,31,0,0q�$
��Wrap=�$��Personalizador��$	��Top=��� $��Right=�$��
0,31,164,0q�$
��Wrap=�$��Mês referente��$	��Top=�$��14��� -   $ databaseGrid��$��0q�$y�Hidden=�.+�]� .+�]� $��10,0,0,0q�.+��� $
��Left=�$
��Wrap=�$
��Data��$	��Top=�$��0,15,0,0q�$	��Top=�$��22���� $
��Left=�$
��Wrap=�$	��Top=�$��0,10,0,0q�$	��Top=�$��22���� $�PDF com os ���� $�boletos���� $
��Left=�$
��Wrap=�$	��Top=�$��0,10,0,0q�$	��Top=�$��22���� $�Planilha com os���� $� e-mails��]� $��30,0,0,0q�.+�� $	��Top=�$��0,10,0,5q�$	��Top=�.+��� -   $ CB_month_databaseGrid��$	��Top=�$��0q�$
��Left=�$��84��$	��Top=�.@���  . $
��Mês���� $��Janeiro���� $��	Fevereiro���� $��Março���� $��Abril���� $
��Maio���� $��Junho���� $��Junho���� $��Agosto���� $��Setembro���� $��Outubro���� $��Novembro���� $��Dezembro���� -   $ CB_year_databaseGrid��$��89,0,0,0q�$
��Left=�$��60��$
 -1.005,0.682&�.@��� $	��Ano����  . $
��2017���� $
��2018���� $
��2019���� $
��2020���� $
��2021���� $
��2022��� $	��Top=�$	��Top=�$��0,10,0,0q�$��22��.+��� -   $ browseTextBox1��$��0,0,50,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��Top=� �� 	UndoLimit$	 250���� -   $ browseButton1��$	��...��$
��Left=�$��18��$	��Top=�$��	302,0,0,0q�$��18��� $	��Top=�$	��Top=�$��0,10,0,0q�$��22��.+��� -   $ browseTextBox2��$��0,0,51,0q�$
 Wrap=�$	��Top=�$	��Top=�$	 250���� -   $ browseButton2��$	��...��$
��Left=�$��18��$	��Top=�$��	302,0,0,0q�$��18���� -   $ createDatabaseButton��$��Carregar dados��$��Right=�$��0,0,80,0q�$��Bottom=�$��85��$��20���� -   $ cancelButton��$��Cancelar��$��Right=�$��0q�$��Bottom=�$��75��$��20��� -   $ researchGrid��$��0q�$y�Hidden=�.+�&   System.Windows.Controls.DataGrid  -     Name$ dataGrid��   
IsReadOnly .    AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Nome��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Nome+  $ Email�� �� *Email+� -   $ propertiesGrid��$��0q�$y�Hidden=�.+��� $
��Left=�$��	10,38,0,0q�$
��Wrap=�$��Email��$	��Top=��� -   $ 
emailToUse��$
��Left=�$��23��$��
208,37,0,0q�$
 Wrap=�$	��Top=�$	��281���� $
��Left=�$��	10,66,0,0q�$
��Wrap=�$��Senha��$	��Top=�G� -    G�Name$ passwordBox��$
��Left=�$��
208,66,0,0q�$	��Top=�$	��281��$��23���� -    ��Name$ logoCasd��$��-32,-14,-28,63q�$��logoCASD.jpeg��$
��Fill=�