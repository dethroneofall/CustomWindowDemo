using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CustomWindowDemo.HelperClass
{
    public interface IHelixToolKitService
    {
        Model3DGroup Load3DObject(string path);
    }
}
