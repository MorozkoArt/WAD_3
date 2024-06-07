using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Laba3
{
    public interface CarModel
    {      
        string CarName { get; }
        string CarModel { get; }
        string Рorsepower { get; }
        string MaxSpeed { get; }
        string CarType { get; }
    }
}
