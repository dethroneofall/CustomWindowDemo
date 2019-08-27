using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CustomWindowDemo.ViewModel
{
    public class TaskThreeViewModel : HelperClass.BaseFunctionality
    {
        private bool PaintBoundBox { get; set; }
        public TaskThreeViewModel(HelperClass.IDialogService dialogService, 
            HelperClass.IHelixToolKitService helixService, bool paintBoundingBox)
        {
            this.IDialogService = dialogService;
            this.IHelixToolKitService = helixService;
            this.PaintBoundBox = paintBoundingBox;
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
                        Model3D model = IHelixToolKitService.Load3DObject(IDialogService.FilePath);
                        Model.Children.Add(model);
                          
                        if (this.PaintBoundBox)
                        {
                            MeshBuilder meshBuilder = new MeshBuilder();
                            meshBuilder.AddBoundingBox(new Rect3D(Model.Bounds.Location,
                            new Size3D(model.Bounds.Size.X, model.Bounds.Size.Y,
                            model.Bounds.Size.Z)), 0.3);
                            GeometryModel3D geometry = new GeometryModel3D(meshBuilder.ToMesh(),
                                Materials.Brown);
                            Model.Children.Add(geometry);
                            PipeVisual3D leftLine = new PipeVisual3D();
                            PipeVisual3D rightLine = new PipeVisual3D();

                            PipeVisual3D upLine = new PipeVisual3D();
                            PipeVisual3D downLine = new PipeVisual3D();

                            PipeVisual3D topLine = new PipeVisual3D();
                            PipeVisual3D bottomLine = new PipeVisual3D();

                            Rect3D bounds = model.Bounds;
                            double centerX = bounds.X + bounds.SizeX / 2;
                            double centerY = bounds.Y + bounds.SizeY / 2;
                            double centerZ = bounds.Z + bounds.SizeZ / 2;


                            leftLine.Point1 = new Point3D(centerX, centerY, centerZ);
                            leftLine.Point2 = new Point3D((centerX * 2) - bounds.SizeX, centerY, centerZ);
                            leftLine.Diameter = 0.03;

                            rightLine.Point1 = new Point3D(centerX, centerY, centerZ);
                            rightLine.Point2 = new Point3D((centerX * 2) + bounds.SizeX, centerY, centerZ);
                            rightLine.Diameter = 0.03;



                            upLine.Point1 = new Point3D(centerX, centerY, centerZ);
                            upLine.Point2 = new Point3D(centerX, (centerY + centerY) + bounds.SizeY, centerZ);
                            upLine.Diameter = 0.03;

                            downLine.Point1 = new Point3D(centerX, centerY, centerZ);
                            downLine.Point2 = new Point3D(centerX, (centerY - centerY) - bounds.SizeY, centerZ);
                            downLine.Diameter = 0.03;

                            topLine.Point1 = new Point3D(centerX, centerY, centerZ);
                            topLine.Point2 = new Point3D(centerX, centerY, (centerZ + centerZ) + bounds.SizeZ);
                            topLine.Diameter = 0.03;

                            bottomLine.Point1 = new Point3D(centerX, centerY, centerZ);
                            bottomLine.Point2 = new Point3D(centerX, centerY, (centerZ - centerZ) - bounds.SizeZ);
                            bottomLine.Diameter = 0.03;

                            Model.Children.Add(leftLine.Content);
                            Model.Children.Add(rightLine.Content);

                            Model.Children.Add(upLine.Content);
                            Model.Children.Add(downLine.Content);

                            Model.Children.Add(topLine.Content);
                            Model.Children.Add(bottomLine.Content);
                        }
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
    }
}
