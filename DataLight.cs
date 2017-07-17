using SimpleLight.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Phone.Media.Capture;

namespace SimpleLight
{
    static class DataLight
    {
        static private AudioVideoCaptureDevice CamVideo;
        static private bool Enabled;

        async static public Task<bool> Init()
        {
            if (isExist)
                return true;

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    CamVideo = await AudioVideoCaptureDevice.OpenForVideoOnlyAsync(CameraSensorLocation.Back, new Windows.Foundation.Size(640, 480));
                    CamVideo.GetProperty(KnownCameraAudioVideoProperties.VideoTorchMode);
                    break;
                }
                catch
                {
                }
            }

            if (CamVideo == null)
                MessageBox.Show(AppResources.Text_Error_Light, AppResources.Text_Error_Title, MessageBoxButton.OK);

            return isExist;
        }

        static public bool isEnable
        {
            get
            {
                try
                {
                    return isExist ? ((UInt32)CamVideo.GetProperty(KnownCameraAudioVideoProperties.VideoTorchMode) == (UInt32)VideoTorchMode.On) : false;
                }
                catch
                {
                    return false;
                }
            }
        }

        static public bool isExist
        {
            get
            {
                return CamVideo != null;
            }
        }

        static public void Enable()
        {
            try
            {
                if (isExist)
                    CamVideo.SetProperty(KnownCameraAudioVideoProperties.VideoTorchMode, VideoTorchMode.On);
            }
            catch
            {
            }
        }

        static public void Disable()
        {
            try
            {
                if (isExist)
                    CamVideo.SetProperty(KnownCameraAudioVideoProperties.VideoTorchMode, VideoTorchMode.Off);
            }
            catch
            {
            }
        }

        static public bool Change()
        {
            if (isEnable)
                Disable();
            else
                Enable();

            return isEnable;
        }

        static public void Sleep()
        {
            Enabled = isEnable;

            if (Enabled)
                Disable();

            CamVideo.Dispose();
            CamVideo = null;
        }

        async static public void Wakeup()
        {
            await Init();

            if (Enabled)
                Enable();
        }
    }
}
