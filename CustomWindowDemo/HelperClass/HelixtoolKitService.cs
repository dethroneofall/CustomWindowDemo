using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CustomWindowDemo.HelperClass
{
    public class HelixtoolKitService : IHelixToolKitService
    {
        private ObjReader reader;
        public Model3DGroup Load3DObject(string path)
        {
            reader = new ObjReader();
            return reader.Read(path);
        }
    }

}
