using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace CustomWindowDemo.ViewModel
{
    public class TaskFiveViewModel : HelperClass.BaseFunctionality
    {
        private double minValue;
        private double maxValue;
        private HelperClass.RelayCommand minCommand;
        private HelperClass.RelayCommand maxCommand;
        private HelperClass.RelayCommand startCommand;
        private HelperClass.RelayCommand stopCommand;
        private DispatcherTimer timer;
        private TranslateTransform3D translate3D;
        private bool Flag = false;

        public HelperClass.RelayCommand MinCommand
        {
            get
            {
                return minCommand ?? (minCommand = new HelperClass.RelayCommand(obj =>
                {

                    MinValue = (double)obj;
                }));
            }

        }
        public HelperClass.RelayCommand StartCommand
        {
            get
            {
                return startCommand ?? (startCommand = new HelperClass.RelayCommand(obj =>
                {
                    this.timer.Start();
                }));
            }

        }
        public HelperClass.RelayCommand StopCommand
        {
            get
            {
                return stopCommand ?? (stopCommand = new HelperClass.RelayCommand(obj =>
                {
                    this.timer.Stop();
                }));
            }

        }

        public HelperClass.RelayCommand MaxCommand
        {
            get
            {
                return maxCommand ?? (maxCommand = new HelperClass.RelayCommand(obj =>
                {
                    MaxValue = (double)obj;
                }));
            }
        }
        public TaskFiveViewModel(HelperClass.IDialogService dialogService,
            HelperClass.IHelixToolKitService helixService)
        {
            MinValue = -25;
            MaxValue = 5;
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(100);
            this.timer.Tick += timer_Tick;
            this.IDialogService = dialogService;
            this.IHelixToolKitService = helixService;
            this.translate3D = new TranslateTransform3D();
         
        }
        private void timer_Tick(object sender, EventArgs e)
        {

            if (!(Model.Bounds.Z <= MinValue) && Flag)
                this.translate3D.OffsetZ--;
            else
                Flag = false;

            if (!(Model.Bounds.Z >= MaxValue) && !Flag)
                this.translate3D.OffsetZ++;
            else
                Flag = true;

            Model.Transform = this.translate3D;
        }
        public double MinValue
        {
            get
            {
                return this.minValue;
            }
            set
            {
                this.minValue = value;
                OnPropertyChanged(nameof(MinValue));
            }
        }

        public HelperClass.RelayCommand LoadModel
        {
            get
            {
                return Load ?? (Load = new HelperClass.RelayCommand(obj =>
                {
                    if (IDialogService.OpenFileDialog())
                    {
                        Model.Children.Clear();
                        MeshBuilder meshBuilder = new MeshBuilder();
                        Model3D model = IHelixToolKitService.Load3DObject(IDialogService.FilePath);
                        model.Transform = new TranslateTransform3D()
                        {
                            OffsetZ = MinValue
                        };
                        Model.Children.Add(model);
                    }
                }));
            }
        }
        public HelperClass.RelayCommand ClearModel
        {
            get
            {
                return Clear ?? (Clear = new HelperClass.RelayCommand(obj =>
                {
                    Model.Children.Clear();
                }));
            }
        }

        public double MaxValue
        {
            get
            {
                return this.maxValue;
            }
            set
            {
                this.maxValue = value;
                OnPropertyChanged(nameof(MaxValue));
            }
        }

    }
}
