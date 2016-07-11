using System;
using System.Resources;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Globalization;

namespace DrawingClient
{
    /// <summary>
    ///             TextRes
    /// </summary>
    public sealed class TextRes
    {
        private TextRes()
        {

        }

        public static String Get(String name, CultureInfo ci)
        {
            String s;
            ResourceManager rm = ResourceManager.CreateFileBasedResourceManager(DrawingClient.Properties.Settings.Default.ResFileName.ToString() +  "." + DrawingClient.Properties.Settings.Default.CustCulture.ToString(), System.Environment.CurrentDirectory/*DrawingClient.Properties.Settings.Default.ResPath.ToString()*/, null);
            s = rm.GetString(name, ci);
            if (s == null)
                return "N/A: " + name;
            else
                return s;
        }
    }
}

