using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SimpleLight.Resources;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Windows.Phone.Media.Capture;

namespace SimpleLight
{
    public partial class MainPage : PhoneApplicationPage
    {
        DataPage Data = new DataPage();

        public MainPage()
        {
            InitializeComponent();
            grid_root.DataContext = Data;

            ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).Text = AppResources.AppBarBut_Set;


            Init();
        }

        async void Init()
        {
            Data.But_Enable = await DataLight.Init();

            if (DataLight.isExist)
            {
                if (Settings.EnableOnStart)
                    DataLight.Enable();
                Data.Enable = DataLight.isEnable;
            }
        }
        
        private void but_flash_Click(object sender, RoutedEventArgs e)
        {
            Data.Enable = DataLight.Change();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SetPage.xaml", UriKind.Relative));
        }
        
        class DataPage : INotifyPropertyChanged
        {
            ImageSource image_dis;
            ImageSource image_en;

            private bool _But_Enable;
            public bool But_Enable
            {
                get
                {
                    return _But_Enable;
                }
                set
                {
                    _But_Enable = value;
                    NotifyPropertyChanged("But_Enable");
                }
            }

            public ImageSource But_Image { get; set; }
            public bool Enable
            {
                set
                {
                    But_Image = value ? image_en : image_dis;
                    NotifyPropertyChanged("But_Image");
                }
            }

            public DataPage()
            {
                if ((Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible)
                {
                    image_dis = new BitmapImage(new Uri("/Images/black_lamp_dis.png", UriKind.RelativeOrAbsolute));
                    image_en = new BitmapImage(new Uri("/Images/black_lamp_en.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    image_dis = new BitmapImage(new Uri("/Images/white_lamp_dis.png", UriKind.RelativeOrAbsolute));
                    image_en = new BitmapImage(new Uri("/Images/white_lamp_en.png", UriKind.RelativeOrAbsolute));
                }

                Enable = false;
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void NotifyPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}