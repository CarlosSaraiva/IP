using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            StartWebRequest();
        }

        void StartWebRequest()
        {
            HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create("http://myexternalip.com/raw");
            request.BeginGetResponse(new AsyncCallback(FinishWebRequest), request);
            
        }

        private async void FinishWebRequest(IAsyncResult result)
        {
            HttpWebResponse response = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                StreamReader stream = new StreamReader(response.GetResponseStream());
                textBox.Text = stream.ReadToEnd();
            });           
        }
    }
}
