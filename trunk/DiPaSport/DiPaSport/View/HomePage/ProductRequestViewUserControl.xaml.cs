using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Com.IT.DiPaSport.View.BasePage;
using Com.IT.DiPaSport.Model.Interfaces;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;

namespace Com.IT.DiPaSport.View.HomePage
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProductRequestViewUserControl : BaseUserControl
    {
        /// <summary>
        /// The home action
        /// </summary>
        public HomeActionListener HomeAction { private get; set; }

        /// <summary>
        /// Gets or sets the image file.
        /// </summary>
        /// <value>
        /// The image file.
        /// </value>
        public Stream ImageFile { private get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRequestViewUserControl"/> class.
        /// </summary>
        public ProductRequestViewUserControl()
        {
            InitializeComponent();
        }

        public override void OnLoad()
        {
            if (ImageFile != null)
            {
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(ImageFile);
                ImageRequest.Source = bmp;
            }
        }

        /// <summary>
        /// Handles the Tap event of the RequestBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs"/> instance containing the event data.</param>
        private void RequestBack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (HomeAction != null)
            {
                HomeAction.OnBack();
            }
        }

        private void SendProductRequestButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://mydomain.cc/saveimage.php");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            string postData = String.Format("image={0}", "test123456789");   

            // Getting the request stream.
            request.BeginGetRequestStream
                (result =>
                {
                    // Sending the request.
                    using (var requestStream = request.EndGetRequestStream(result))
                    {
                        using (StreamWriter writer = new StreamWriter(requestStream))
                        {
                            writer.Write(postData);
                            writer.Flush();
                        }
                    }

                    // Getting the response.
                    request.BeginGetResponse(responseResult =>
                    {
                        var webResponse = request.EndGetResponse(responseResult);
                        using (var responseStream = webResponse.GetResponseStream())
                        {
                            using (var streamReader = new StreamReader(responseStream))
                            {
                                string srresult = streamReader.ReadToEnd();
                            }
                        }
                    }, null);
                }, null);
            */
            /*
                saveimage.php should look like this:

                <?
                function base64_to_image( $imageData, $outputfile ) {
                    // encode & write data (binary)
                    $ifp = fopen( $outputfile, "wb" );
                    fwrite( $ifp, base64_decode( $imageData ) );
                    fclose( $ifp );
                    // return output filename
                    return( $outputfile );
                }       

                if (isset($_POST['image'])) {
                    base64_to_jpeg($_POST['image'], "my_path_to_store_images.jpg");
                }
                else
                    die("no image data found");
                ?>
             */
        }
    }
}
