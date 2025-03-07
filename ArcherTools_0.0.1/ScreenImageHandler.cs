using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace ArcherTools_0._0._1
{
    internal class ScreenImageHandler
    {


        public static Task<Point> SearchImageOnScreen(string targetImagePath, double threshold)
        {
            Point screenPoint = new Point(0, 0);
            screenPoint = DetectImage(targetImagePath, threshold).Result;
            screenPoint = screenPoint == new Point(0, 0) ? DetectImageOther(targetImagePath, threshold).Result : screenPoint;
            return Task.FromResult(screenPoint);


        }

        public static Point SearchImageOnRegion(string sourceImagePath, Rectangle roi, double threshold)
        {
            using (var screenRegion = BitmapToMat(CaptureScreenRegion(roi)))
            {
                using (var grayScreenRegion = new Mat())
                {
                    CvInvoke.CvtColor(screenRegion, grayScreenRegion, ColorConversion.Bgr2Gray);

                    using (var templateImage = new Mat(sourceImagePath, ImreadModes.Grayscale))
                    {
                        using (var result = new Mat())
                        {
                            CvInvoke.MatchTemplate(grayScreenRegion, templateImage, result, TemplateMatchingType.CcoeffNormed);

                            Point minLoc = new Point(0, 0), maxLoc = new Point(0, 0);
                            double minVal = 0, maxVal = 0;
                            CvInvoke.MinMaxLoc(result, ref minVal, ref maxVal, ref minLoc, ref maxLoc);

                            if (maxVal >= threshold)
                            {
                                Debug.WriteLine("found");
                                return new Point(maxLoc.X + roi.X, maxLoc.Y + roi.Y);
                                
                            }
                            Debug.WriteLine("nope");
                            return Point.Empty;
                        }
                    }
                }
            }
        }

        private static Bitmap CaptureScreenRegion(Rectangle roi)
        {
            var screenshot = new Bitmap(roi.Width, roi.Height);
            using (var g = Graphics.FromImage(screenshot))
            {
                // Capture the screen within the specified ROI (Region of Interest)
                g.CopyFromScreen(roi.Location, Point.Empty, roi.Size);
            }

            // Convert the captured image to a Mat (EmguCV format)
            return screenshot;
        }



        private static Task<Point> DetectImage(string targetImagePath, double threshold)
        {
            using (var screenshotFirst = CaptureFirstScreen())
            using (var screenshotFirstMat = BitmapToMat(screenshotFirst))
            using (var greyScreenshot = new Mat())
            using (var targetImage = CvInvoke.Imread(targetImagePath, ImreadModes.Grayscale))
            using (var result = new Mat())
            {

                CvInvoke.CvtColor(screenshotFirstMat, greyScreenshot, ColorConversion.Bgr2Gray);

                CvInvoke.MatchTemplate(greyScreenshot, targetImage, result, TemplateMatchingType.CcoeffNormed);
                double minVal = 0.0, maxVal = 0.0;
                Point minLoc = new Point(), maxLoc = new Point();
                CvInvoke.MinMaxLoc(result, ref minVal, ref maxVal, ref minLoc, ref maxLoc);



                if (maxVal > threshold)
                {
#if DEBUG
                    Debug.WriteLine($"Target image found at location: {maxLoc.X}, {maxLoc.Y}");
#endif
                    return Task.FromResult(new Point(maxLoc.X, maxLoc.Y));
                }
                else
                {
                    return Task.FromResult(new Point(0, 0));

                }
            }
        }

        private static Task<Point> DetectImageOther(string targetImagePath, double threshold)
        {
            {
                if (CaptureSecondScreen() == null)
                {
                    return Task.FromResult(Point.Empty);
                }
                using (var screenshot = CaptureSecondScreen())
                using (var screenshotMat = BitmapToMat(screenshot))
                using (var greyScreenshot = new Mat())
                using (var targetImage = CvInvoke.Imread(targetImagePath, ImreadModes.Grayscale))
                using (var result = new Mat())
                {

                    CvInvoke.CvtColor(screenshotMat, greyScreenshot, ColorConversion.Bgr2Gray);

                    CvInvoke.MatchTemplate(greyScreenshot, targetImage, result, TemplateMatchingType.CcoeffNormed);
                    double minVal = 0.0, maxVal = 0.0;
                    Point minLoc = new Point(), maxLoc = new Point();
                    CvInvoke.MinMaxLoc(result, ref minVal, ref maxVal, ref minLoc, ref maxLoc);



                    if (maxVal > threshold)
                    {
#if DEBUG
                        Debug.WriteLine($"Target image found at location: {maxLoc.X + Screen.PrimaryScreen.Bounds.Width}, {maxLoc.Y}");
#endif
                        return Task.FromResult(new Point(maxLoc.X + Screen.PrimaryScreen.Bounds.Width, maxLoc.Y));
                    }
                    else
                    {
                        return Task.FromResult(new Point(0, 0));
                    }

                }
            }
        }



        static Bitmap CaptureFirstScreen()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            var screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(screenshot))
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
                var screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
                using (var g = Graphics.FromImage(screenshot))
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
                return new Mat();
            }
        }
    }
}