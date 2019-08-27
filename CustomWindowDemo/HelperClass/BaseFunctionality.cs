using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CustomWindowDemo.HelperClass
{
    public class BaseFunctionality : INotifyPropertyChanged
    {
        private Model3DGroup model = new Model3DGroup();
        protected HelperClass.RelayCommand Load;
        protected HelperClass.RelayCommand Clear;

        public  Model3DGroup Model
        {
            get
            {
                return this.model;
            }
            set
            {
                this.model = value;
                OnPropertyChanged(nameof(Model));
            }
        }
        protected HelperClass.IDialogService IDialogService { get; set; }
        protected HelperClass.IHelixToolKitService IHelixToolKitService { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
