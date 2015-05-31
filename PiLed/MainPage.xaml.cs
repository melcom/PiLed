using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PiLed
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly ToggleButton[][] tbs;
        private readonly StackPanel[] spColumns;
        private int columnIndex;
        private DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();

            #region ToggleButton

            tbs = new ToggleButton[8][];

            for(int i=0; i < 8; i++)
            {
                tbs[i] = new ToggleButton[8];
            }

            tbs[0][0] = tb00;
            tbs[0][1] = tb01;
            tbs[0][2] = tb02;
            tbs[0][3] = tb03;
            tbs[0][4] = tb04;
            tbs[0][5] = tb05;
            tbs[0][6] = tb06;
            tbs[0][7] = tb07;
            tbs[1][0] = tb10;
            tbs[1][1] = tb11;
            tbs[1][2] = tb12;
            tbs[1][3] = tb13;
            tbs[1][4] = tb14;
            tbs[1][5] = tb15;
            tbs[1][6] = tb16;
            tbs[1][7] = tb17;
            tbs[2][0] = tb20;
            tbs[2][1] = tb21;
            tbs[2][2] = tb22;
            tbs[2][3] = tb23;
            tbs[2][4] = tb24;
            tbs[2][5] = tb25;
            tbs[2][6] = tb26;
            tbs[2][7] = tb27;
            tbs[3][0] = tb30;
            tbs[3][1] = tb31;
            tbs[3][2] = tb32;
            tbs[3][3] = tb33;
            tbs[3][4] = tb34;
            tbs[3][5] = tb35;
            tbs[3][6] = tb36;
            tbs[3][7] = tb37;
            tbs[4][0] = tb40;
            tbs[4][1] = tb41;
            tbs[4][2] = tb42;
            tbs[4][3] = tb43;
            tbs[4][4] = tb44;
            tbs[4][5] = tb45;
            tbs[4][6] = tb46;
            tbs[4][7] = tb47;
            tbs[5][0] = tb50;
            tbs[5][1] = tb51;
            tbs[5][2] = tb52;
            tbs[5][3] = tb53;
            tbs[5][4] = tb54;
            tbs[5][5] = tb55;
            tbs[5][6] = tb56;
            tbs[5][7] = tb57;
            tbs[6][0] = tb60;
            tbs[6][1] = tb61;
            tbs[6][2] = tb62;
            tbs[6][3] = tb63;
            tbs[6][4] = tb64;
            tbs[6][5] = tb65;
            tbs[6][6] = tb66;
            tbs[6][7] = tb67;
            tbs[7][0] = tb70;
            tbs[7][1] = tb71;
            tbs[7][2] = tb72;
            tbs[7][3] = tb73;
            tbs[7][4] = tb74;
            tbs[7][5] = tb75;
            tbs[7][6] = tb76;
            tbs[7][7] = tb77;

            #endregion

            #region StackPanel

            spColumns = new StackPanel[8];

            spColumns[0] = spColumn0;
            spColumns[1] = spColumn1;
            spColumns[2] = spColumn2;
            spColumns[3] = spColumn3;
            spColumns[4] = spColumn4;
            spColumns[5] = spColumn5;
            spColumns[6] = spColumn6;
            spColumns[7] = spColumn7;

            #endregion

            columnIndex = 0;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, object e)
        {
            spColumns[columnIndex].Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
            spColumns[columnIndex == 0 ? 7 : columnIndex - 1].Background = new SolidColorBrush(Windows.UI.Color.FromArgb(0,0,0,0));

            byte current = GetTCurrentToggleButtonsValue();

            tbMessage.Text = string.Format("Sending {0} value", current);

            if (++columnIndex >= 8)
                columnIndex = 0;
        }

        private byte GetTCurrentToggleButtonsValue()
        {
            uint b = 0;

            for(int j=0; j < 8; j++)
            {
                if (tbs[columnIndex][j].IsChecked.Value)
                {
                    b = b | ((uint)1 << j);
                }
            }

            return (byte)b;
        }

        private void btStart_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            tbMessage.Text = "Start timer";
        }

        private void btStop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            tbMessage.Text = "Stop timer";
        }

        private void btClear_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tbs[i][j].IsChecked = false;
                }
            }
        }
    }
}
