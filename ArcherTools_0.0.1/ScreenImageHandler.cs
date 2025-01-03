using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace ArcherTools_0._0._1
{
    internal class ScreenImageHandler
    {

        private static Mat reusableFirstGrayScreenshot = new Mat();
        private static Mat reusableSecondGrayScreenshot = new Mat();


        public static Point SearchImageOnScreen(string targetImagePath, double threshold)
        {
            Point screenPoint = new Point(0, 0);
            var taskFirstScreen = Task.Run(() =>
            {
                screenPoint = DetectImage(targetImagePath, threshold);
            });
            var taskSecondScreen = Task.Run(() =>
            {
                screenPoint = DetectImageOther(targetImagePath, threshold);
            });
            Task.WhenAny(taskFirstScreen, taskSecondScreen).Wait();
            return screenPoint;


        }

        public static Point SearchImageOnRegion(string sourceImagePath, Rectangle roi, double threshold)
        {
            using (Mat screenRegion = CaptureScreenRegion(roi))
            {
                using (Mat grayScreenRegion = new Mat())
                {
                    CvInvoke.CvtColor(screenRegion, grayScreenRegion, ColorConversion.Bgr2Gray);

                    using (Mat templateImage = new Mat(sourceImagePath, ImreadModes.Grayscale))
                    {
                        using (Mat result = new Mat())
                        {
                            CvInvoke.MatchTemplate(grayScreenRegion, templateImage, result, TemplateMatchingType.CcoeffNormed);

                            Point minLoc = new Point(0, 0), maxLoc = new Point(0, 0);
                            double minVal = 0, maxVal = 0;
                            CvInvoke.MinMaxLoc(result, ref minVal, ref maxVal, ref minLoc, ref maxLoc);

                            if (maxVal >= threshold)
                            {
                                return new Point(maxLoc.X + roi.X, maxLoc.Y + roi.Y);
                            }

                            return Point.Empty;
                        }
                    }
                }
            }
        }

        private static Mat CaptureScreenRegion(Rectangle roi)
        {
            Bitmap screenshot = new Bitmap(roi.Width, roi.Height);
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                // Capture the screen within the specified ROI (Region of Interest)
                g.CopyFromScreen(roi.Location, Point.Empty, roi.Size);
            }

            // Convert the captured image to a Mat (EmguCV format)
            return screenshot.ToMat();
        }



        private static Point DetectImage(string targetImagePath, double threshold)
        {
            using (Bitmap screenshotFirst = CaptureFirstScreen())
            using (Mat screenshotFirstMat = BitmapToMat(screenshotFirst))
            using (Mat targetImage = CvInvoke.Imread(targetImagePath, ImreadModes.Grayscale))
            using (Mat result = new Mat())
            {

                CvInvoke.CvtColor(screenshotFirstMat, reusableFirstGrayScreenshot, ColorConversion.Bgr2Gray);

                CvInvoke.MatchTemplate(reusableFirstGrayScreenshot, targetImage, result, TemplateMatchingType.CcoeffNormed);
                double minVal = 0.0, maxVal = 0.0;
                Point minLoc = new Point(), maxLoc = new Point();
                CvInvoke.MinMaxLoc(result, ref minVal, ref maxVal, ref minLoc, ref maxLoc);



                if (maxVal > threshold)
                {
#if DEBUG
                    Debug.WriteLine($"Target image found at location: {maxLoc.X}, {maxLoc.Y}");
#endif
                    return new Point(maxLoc.X, maxLoc.Y);
                }
                else
                {
                    return new Point(0, 0);

                }
            }
        }

        private static Point DetectImageOther(string targetImagePath, double threshold)
        {
            {
                if (CaptureSecondScreen() == null)
                {
                    return Point.Empty;
                }
                using (Bitmap screenshot = CaptureSecondScreen())
                using (Mat screenshotMat = BitmapToMat(screenshot))
                using (Mat targetImage = CvInvoke.Imread(targetImagePath, ImreadModes.Grayscale))
                using (Mat result = new Mat())
                {

                    CvInvoke.CvtColor(screenshotMat, reusableSecondGrayScreenshot, ColorConversion.Bgr2Gray);

                    CvInvoke.MatchTemplate(reusableSecondGrayScreenshot, targetImage, result, TemplateMatchingType.CcoeffNormed);
                    double minVal = 0.0, maxVal = 0.0;
                    Point minLoc = new Point(), maxLoc = new Point();
                    CvInvoke.MinMaxLoc(result, ref minVal, ref maxVal, ref minLoc, ref maxLoc);



                    if (maxVal > threshold)
                    {
#if DEBUG
                        Debug.WriteLine($"Target image found at location: {maxLoc.X + Screen.PrimaryScreen.Bounds.Width}, {maxLoc.Y}");
#endif
                        return new Point(maxLoc.X + Screen.PrimaryScreen.Bounds.Width, maxLoc.Y);
                    }
                    else
                    {
                        return new Point(0, 0);
                    }

                }
            }
        }



        static Bitmap CaptureFirstScreen()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(bounds.Left, bounds.Top, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            }

            return screenshot;
        }

        static Bitmap CaptureSecondScreen()
        {
            if (Screen.AllScreens.Length > 1)
            {
                Rectangle bounds = Screen.AllScreens[1].Bounds;
                Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(screenshot))
                {
                    g.CopyFromScreen(bounds.Left, bounds.Top, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
                }

                return screenshot;
            }
            else
            {
                return null;
            }
        }

        private static Mat? BitmapToMat(Bitmap bitmap)
        {
            try
            {
                return bitmap.ToMat();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}