using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace IDCardReaderCrash
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window, INotifyPropertyChanged
    {
        private String _Address;
        public String Address
        {
            get { return _Address; }
            set { if (_Address != value) { _Address = value; RaisePropertyChanged("Address"); } }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(name)); }
        }

        public Window1()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
