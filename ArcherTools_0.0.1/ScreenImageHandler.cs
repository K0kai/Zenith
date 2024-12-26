using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Emgu.CV.CvEnum;
using System.Drawing.Imaging;
using ArcherTools_0._0._1.controllers;

namespace ArcherTools_0._0._1
{
    internal class ScreenImageHandler
    {
  
            private static Mat reusableGrayscaleScreenshot = new Mat();

        public static void DetectImage(string targetImagePath)
            {
            using (Bitmap screenshotFirst = CaptureFirstScreen())
            using (Mat screenshotFirstMat = BitmapToMat(screenshotFirst))
            using (Mat targetImage = CvInvoke.Imread(targetImagePath, ImreadModes.Grayscale))
            using (Mat result = new Mat())
            {
                
                CvInvoke.CvtColor(screenshotFirstMat, reusableGrayscaleScreenshot, ColorConversion.Bgr2Gray);

                CvInvoke.MatchTemplate(reusableGrayscaleScreenshot, targetImage, result, TemplateMatchingType.CcoeffNormed);
                double minVal = 0.0, maxVal = 0.0;
                Point minLoc = new Point(), maxLoc = new Point();
                CvInvoke.MinMaxLoc(result, ref minVal, ref maxVal, ref minLoc, ref maxLoc);

                double threshold = 0.8;

                if (maxVal > threshold)
                {
                    Debug.WriteLine($"Target image found at location: {maxLoc.X}, {maxLoc.Y}");
                    MouseHandler.SetCursorPos(maxLoc.X, maxLoc.Y);
                }
                else
                {
                    Debug.WriteLine("Target image not found, trying second monitor.");
                    using (Bitmap screenshotSecond = CaptureSecondScreen())                    
                    using (Mat screenshotSecondMat = BitmapToMat(screenshotSecond))                    
                    {                        
                        CvInvoke.CvtColor(screenshotSecondMat, reusableGrayscaleScreenshot, ColorConversion.Bgr2Gray);

                        CvInvoke.MatchTemplate(reusableGrayscaleScreenshot, targetImage, result, TemplateMatchingType.CcoeffNormed);
                        CvInvoke.MinMaxLoc(result, ref minVal, ref maxVal, ref minLoc, ref maxLoc);

                        if (maxVal > threshold)
                        {
                            Debug.WriteLine($"Target image found at location: {maxLoc.X}, {maxLoc.Y}");
                            MouseHandler.SetCursorPos(maxLoc.X, maxLoc.Y);
                        }
                        else { Debug.WriteLine("Target image not found in any monitor."); }
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

        private static Mat BitmapToMat(Bitmap bitmap)
        {
            return bitmap.ToMat();
        }
    }
    }
