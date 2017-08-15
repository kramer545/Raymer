using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowMapEditor
{
    public class PaintCommand : Command
    {
        private Texture2D paintingTexture;
        private Brush brush;

        public PaintCommand(Brush brush, Texture2D paintingTexture)
        {
            this.brush = brush;
            this.paintingTexture = paintingTexture;
        }

        public override void Execute()
        {
            int x = (int)Mathf.Lerp(0, paintingTexture.width, brush.CurrentUVPosition.x);
            int y = (int)Mathf.Lerp(0, paintingTexture.height, brush.CurrentUVPosition.y);

            int sizeInPixels = (int)(Mathf.Min(paintingTexture.width, paintingTexture.height) * brush.Size * 0.5f);

            Vector2 direction = brush.SmoothedDirection;

            direction.y = -direction.y;
            direction = (direction + new Vector2(1, 1)) * 0.5f;

            for (int i = x - sizeInPixels; i <= x + sizeInPixels; i++)
            {
                if (i < 0 || i >= paintingTexture.width)
                {
                    continue;
                }
                for (int j = y - sizeInPixels; j <= y + sizeInPixels; j++)
                {
                    if (j < 0 || j >= paintingTexture.height)
                    {
                        continue;
                    }

                    Color currentColor = paintingTexture.GetPixel(i, j);

                    float distToPixel = Vector2.Distance(new Vector2(i, j), new Vector2(x, y));

                    if (distToPixel > sizeInPixels)
                    {
                        continue;
                    }

                    float alpha = 1 - Mathf.SmoothStep(0, brush.Softness, distToPixel / (sizeInPixels * 0.5f));

                    Color drawColor = new Color();
                    if (brush.drawType == Brush.DrawType.FlowMap)
                    {
                        drawColor.r = direction.x;
                        drawColor.g = direction.y;
                        drawColor.b = currentColor.b;
                    }
                    else if (brush.drawType == Brush.DrawType.Alpha)
                    {
                        drawColor.r = currentColor.r;
                        drawColor.g = currentColor.g;
                        drawColor.b = brush.AlphaIntensivity;
                    }

                    paintingTexture.SetPixel(i, j, drawColor * alpha + currentColor * (1 - alpha));
                }
            }
            paintingTexture.Apply();
        }
    }
}
