using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LoadingManager
{
    public class CLoadingManager
    {
        private static LoadingWindow LoadingWnd;
        private static Thread ThreadFormLoading;
        private static string Title;
        private static string Message;

        public static void Show(string _Title = "Loading Window", string _Message = "Loading")
        {
            if (null == LoadingWnd)
            {
                Title = _Title;
                Message = _Message;

                ThreadFormLoading = new Thread(new ThreadStart(ShowLoadingForm));
                ThreadFormLoading.IsBackground = true;
                ThreadFormLoading.Start();
            }
        }

        private static void ShowLoadingForm()
        {
            LoadingWnd = new LoadingWindow();
            LoadingWnd.FormCloseEvent += new LoadingWindow.FormCloseHandler(Hide);
            LoadingWnd.ShowLoadingWindow(Title, Message);
            LoadingWnd.ShowDialog();
        }

        public static void Hide(int _DelayTime = 100)
        {
            Thread.Sleep(_DelayTime);
            if (null != LoadingWnd)
            {
                LoadingWnd.FormCloseEvent -= new LoadingWindow.FormCloseHandler(Hide);
                LoadingWnd.FormClose = true;
                LoadingWnd = null;
            }
        }
    }
}
