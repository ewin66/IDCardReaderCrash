using SSCDC.IDCardReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IDCardReaderCrash
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        IDCReader iDCReader = new IDCReader();

        public MainWindow()
        {
            InitializeComponent();
            iDCReader.idcReaderReadSuccess += iDCReader_idcReaderReadSuccess;
        }

        void iDCReader_idcReaderReadSuccess(IDCardInfo iDCardInfo)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.tbAddress.Text = iDCardInfo.Address;
            }));
        }

        private void btReadCard_Click(object sender, RoutedEventArgs e)
        {
            IDCardInfo iDCardInfo = null;
            bool issucc = this.iDCReader.open();
            try
            {
                if (issucc)
                {
                    iDCardInfo = this.iDCReader.read();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
                //Console.WriteLine(ex.Message);
                //Console.Write(ex.StackTrace);
            }
            if (iDCardInfo != null)
            {
                this.tbAddressDire.Text = iDCardInfo.Address;
            }
            this.iDCReader.close();
        }
    }
}
