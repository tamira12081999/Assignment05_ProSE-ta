﻿using System;
using System.Drawing;

namespace Assignment05_ProSE
{
    public class SoebelFilter
    {
        static Bitmap Result;

        static double Threshold = 15;
        public Bitmap GetBoundary(Bitmap originalImg)
        {
            Result = new Bitmap(originalImg.Width, originalImg.Height);
            

            if (Monitor.TryEnter(this))
            {
                FilterRedChannel(originalImg);
                
                if (Monitor.TryEnter(this))
                {
                    FilterGreenChaannel(originalImg);
                    
                    if (Monitor.TryEnter(this))
                    {
                        FilterBlueChaannel(originalImg);
                        
                    }
                }
            }
            Monitor.Exit(this);

            //FilterRedChannel(originalImg);

            //FilterGreenChaannel(originalImg);
            ////Blue Channel
            //FilterBlueChaannel(originalImg);


            return Result;
        }

        private static void FilterRedChannel(Bitmap img)

        {
            //Monitor.Enter(img);
            for (int x = 1; x < img.Width - 1; x++)
            {
                for (int y = 1; y < img.Height - 1; y++)
                {
                    int gradiantX = (Convert.ToInt32(img.GetPixel(x - 1, y).R - img.GetPixel(x + 1, y).R)) / 2;
                    int gradiantY = (Convert.ToInt32(img.GetPixel(x, y - 1).R - img.GetPixel(x, y + 1).R)) / 2;

                    double magnitude = Math.Sqrt((gradiantX * gradiantX) + (gradiantY * gradiantY));

                    Color originalImgColor = img.GetPixel(x, y);
                    CheckPixelsMagnitude(magnitude, x, y, originalImgColor);
                    
                    
                }
            }
            Console.WriteLine("Red done");
            //Monitor.Exit(img);
        }
        private static void FilterGreenChaannel(Bitmap img)
        {
            //Monitor.Enter(img);
            for (int x = 1; x < img.Width - 1; x++)
            {
                for (int y = 1; y < img.Height - 1; y++)
                {
                    int gradiantX = (Convert.ToInt32(img.GetPixel(x - 1, y).G - img.GetPixel(x + 1, y).G)) / 2;
                    int gradiantY = (Convert.ToInt32(img.GetPixel(x, y - 1).G - img.GetPixel(x, y + 1).G)) / 2;

                    double magnitude = Math.Sqrt((gradiantX * gradiantX) + (gradiantY * gradiantY));

                    Color originalImgColor = img.GetPixel(x, y);
                    CheckPixelsMagnitude(magnitude, x, y, originalImgColor);
                }
            }
            Console.WriteLine("Green done");
            //Monitor.Exit(img);
        }
        private static void FilterBlueChaannel(Bitmap img)
        {
            //Monitor.Enter(img);
            for (int x = 1; x < img.Width - 1; x++)
            {
                for (int y = 1; y < img.Height - 1; y++)
                {
                    int gradiantX = (Convert.ToInt32(img.GetPixel(x - 1, y).B - img.GetPixel(x + 1, y).B)) / 2;
                    int gradiantY = (Convert.ToInt32(img.GetPixel(x, y - 1).B - img.GetPixel(x, y + 1).B)) / 2;

                    double magnitude = Math.Sqrt((gradiantX * gradiantX) + (gradiantY * gradiantY));

                    Color originalImgColor = img.GetPixel(x, y);
                    CheckPixelsMagnitude(magnitude, x, y, originalImgColor);

                }
            }
            Console.WriteLine("Blue done");
            //Monitor.Exit(img);
        }
        private static void CheckPixelsMagnitude(double magnitude, int x, int y, Color originalImgColor)
        {
            
            if (Result.GetPixel(x, y) != Color.Black)
            {
                if (magnitude > Threshold)
                {
                    Result.SetPixel(x, y, originalImgColor);
                }
                else
                {
                    Result.SetPixel(x, y, Color.Black);
                }
            }
            

        }

    }
}