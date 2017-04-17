using System;
using System.Windows;
using System.Windows.Controls;
using Windows.Phone.Media.Capture;


namespace SimpleLight
{
    class LightC
    {
        private bool Enabled;
        private AudioVideoCaptureDevice CamVideo;
        private MainPage Parent;
        private Image Img_Lamp_Dis;
        private Image Img_Lamp_En;

        public LightC(MainPage parent)
        {
            if ((Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible)
            {
                Img_Lamp_Dis = AppData.ImageLoad("/Images/black_lamp_dis.png");
                Img_Lamp_En = AppData.ImageLoad("/Images/black_lamp_en.png");
            }
            else
            {
                Img_Lamp_Dis = AppData.ImageLoad("/Images/white_lamp_dis.png");
                Img_Lamp_En = AppData.ImageLoad("/Images/white_lamp_en.png");
            }

            Parent = parent;
            LightInit(AppData.Settings.start_on);
        }

        private async void LightInit(bool en)
        {
            bool flag = true;
            Parent.but_flash.IsEnabled = false;

            try
            {
                CamVideo.GetProperty(KnownCameraAudioVideoProperties.VideoTorchMode);
                flag = false;
            }
            catch
            {
            }

            if (flag)
            {
                CamVideo = await AudioVideoCaptureDevice.OpenForVideoOnlyAsync(CameraSensorLocation.Back, new Windows.Foundation.Size(640, 480));
            }

            Parent.but_flash.IsEnabled = true;
            if (en)
                Enable();
            else
                Disable();
        }

        private bool IsExists()
        {
            if (CamVideo == null)
                return false;
            else
                return true;
        }

        private bool IsEnable()
        {
            try
            {
                if ((UInt32)CamVideo.GetProperty(KnownCameraAudioVideoProperties.VideoTorchMode) == (UInt32)VideoTorchMode.On)
                    return true;
            }
            catch
            {
            }
            return false;
        }

        public void Change()
        {
            if (IsEnable())
                Disable();
            else
                Enable();
        }

        public void Enable()
        {
            try
            {
                CamVideo.SetProperty(KnownCameraAudioVideoProperties.VideoTorchMode, VideoTorchMode.On);
                Parent.but_flash.Content = Img_Lamp_En;
                Enabled = true;
            }
            catch
            {
            }
        }

        public void Disable()
        {
            try
            {
                CamVideo.SetProperty(KnownCameraAudioVideoProperties.VideoTorchMode, VideoTorchMode.Off);
                Parent.but_flash.Content = Img_Lamp_Dis;
                Enabled = false;
            }
            catch
            {
            }
        }

        public void Sleep()
        {
            if (IsEnable())
            {
                try
                {
                    CamVideo.SetProperty(KnownCameraAudioVideoProperties.VideoTorchMode, VideoTorchMode.Off);
                    Parent.but_flash.Content = Img_Lamp_Dis;
                }
                catch
                {
                }
            }
        }

        public void Wakeup()
        {
            LightInit(Enabled);
        }
    }
}
