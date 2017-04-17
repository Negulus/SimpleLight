using System;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.IO.IsolatedStorage;

namespace SimpleLight
{
    public class MemSet
    {
        public bool start_on;
        public bool lock_on;
        public MemSet()
        {
            start_on = true;
            lock_on = true;
        }
    }

    static class AppData
    {
        static public LightC Light;

        static public MemSet Settings;

        static public void SetSave()
        {
            XmlSerializer Serializer = new XmlSerializer(typeof(MemSet));
//            StreamWriter StreamW = new StreamWriter("/SimpleLight_Set.xml");
//            Serializer.Serialize(StreamW, AppData.Settings);
//            StreamW.Close();

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var stream = store.OpenFile("SimpleLight_Set.xml", FileMode.Create))
                {
                    Serializer.Serialize(stream, AppData.Settings);
                }
            }
        }

        static public void SetLoad()
        {
            try
            {
                XmlSerializer Serializer = new XmlSerializer(typeof(MemSet));
//                FileStream StreamR = new FileStream("/SimpleLight_Set.xml", FileMode.Open);
//                AppData.Settings = (MemSet)Serializer.Deserialize(StreamR);

                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (var stream = store.OpenFile("SimpleLight_Set.xml", FileMode.Open))
                    {
                        AppData.Settings = (MemSet)Serializer.Deserialize(stream);
                    }
                }
            }
            catch
            {
                AppData.Settings = new MemSet();
            }
        }

        public static Image ImageLoad(string path)
        {
            Image tmpimg = new Image();
            tmpimg.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            return tmpimg;
        }
    }
}
